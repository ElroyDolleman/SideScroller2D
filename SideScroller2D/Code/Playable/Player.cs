using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

using SideScroller2D.Code.Input;
using SideScroller2D.Code.Graphics;
using SideScroller2D.Code.Collision;
using SideScroller2D.Code.Graphics.Particles;
using SideScroller2D.Code.Utilities;
using SideScroller2D.Code.Utilities.Time;
using SideScroller2D.Code.Playable.PlayerStates;

namespace SideScroller2D.Code.Playable
{
    public enum PlayerAnimations
    {
        Idle,
        Run,
        Jump,
        Fall,
        Dive,
        WallSlide,
    }

    class Player : Actor
    {
        /// <summary>
        /// The FacingDirection is determined by the player's speed. If speed == 0, then the FacingDirection is determined by the sprite.
        /// The FacingDirection is 1 when facing right and -1 when facing left
        /// </summary>
        public int FacingDirection
        {
            get
            {
                return sprite.SpriteEffect == SpriteEffects.FlipHorizontally ? -1 : 1;
            }
            set
            {
                if (value == 1)
                    sprite.SpriteEffect = SpriteEffects.None;
                else if (value == -1)
                    sprite.SpriteEffect = SpriteEffects.FlipHorizontally;
#if DEBUG
                else
                    Console.WriteLine("Warning: FacingDirection can only be 1 or -1");
#endif
            }
        }

        public PlayerBaseState CurrentState { get; protected set; }

        /// <summary>
        /// The amount of time spent in the air
        /// </summary>
        public float AirTime { get; protected set; } = 0;

        public DustParticles DustParticles { get; protected set; }

        public readonly PlayerIndex PlayerIndex;
        public readonly PlayerInputs Inputs;

        public IdleState IdleState;
        public RunState RunState;
        public FallState FallState;
        public JumpState JumpState;
        public WallSlideState WallSlideState;
        public WallJumpState WallJumpState;

        private SpriteSheet characterSheet;
        private Dictionary<PlayerAnimations, SpriteSheetAnimation> animations;
        private SpriteSheetAnimation currentAnimation;

        public Player(PlayerIndex playerIndex, Vector2 spawnPosition)
        {
            this.PlayerIndex = playerIndex;
            Inputs = InputManager.PlayerInputs[(int)PlayerIndex];

            sprite = new Sprite(AssetsManager.GetTexture("character_nina"));
            characterSheet = new SpriteSheet(sprite.Texture, 16, 16);

            sprite.Origin = new Vector2(3, 2);
            Hitbox = new FloatRectangle(0, 0, 10, 14);

            ChangePosition(spawnPosition);

            animations = new Dictionary<PlayerAnimations, SpriteSheetAnimation>();
            animations.Add(PlayerAnimations.Idle, new SpriteSheetAnimation(sprite, characterSheet, new int[] { 0 }));
            animations.Add(PlayerAnimations.Run, new SpriteSheetAnimation(sprite, characterSheet, new int[] { 0, 1 }));
            animations.Add(PlayerAnimations.Jump, new SpriteSheetAnimation(sprite, characterSheet, new int[] { 2 }));
            animations.Add(PlayerAnimations.Fall, new SpriteSheetAnimation(sprite, characterSheet, new int[] { 3 }));
            animations.Add(PlayerAnimations.Dive, new SpriteSheetAnimation(sprite, characterSheet, new int[] { 10 }));
            animations.Add(PlayerAnimations.WallSlide, new SpriteSheetAnimation(sprite, characterSheet, new int[] { 7 }));

            DustParticles = new DustParticles(Position);

            InitializeStates();
        }

        private void InitializeStates()
        {
            IdleState = new IdleState(this);
            RunState = new RunState(this);
            FallState = new FallState(this);
            JumpState = new JumpState(this);
            WallSlideState = new WallSlideState(this);
            WallJumpState = new WallJumpState(this);

            ChangeState(IdleState);
        }

        public void Update()
        {
            CurrentState.Update();

            if (CurrentState.InAir)
                AirTime += ElapsedTime.Seconds;
            else
                AirTime = 0;

            if (Speed.X != 0)
                FacingDirection = Math.Sign(Speed.X);

            DustParticles.Update();

            currentAnimation.Update(ElapsedTime.Milliseconds);
        }

        public void UpdateXMovementControls()
        {
            UpdateXMovementControls(PlayerStats.RunAcceleration, PlayerStats.RunSpeed);
        }

        public void UpdateXMovementControls(float accelSpeed, float maxSpeed)
        {
            if (InputManager.IsDown(Inputs.Right))
            {
                if (Speed.X < maxSpeed)
                    Speed.X = Math.Min(Speed.X + accelSpeed, maxSpeed);

                else if (Speed.X > maxSpeed)
                    Speed.X = Math.Max(Speed.X - accelSpeed, maxSpeed);
            }
            else if (InputManager.IsDown(Inputs.Left))
            {
                if (Speed.X > -maxSpeed)
                    Speed.X = Math.Max(Speed.X - accelSpeed, -maxSpeed);

                else if (Speed.X < -maxSpeed)
                    Speed.X = Math.Min(Speed.X + accelSpeed, -maxSpeed);
            }
            else if (Speed.X != 0)
            {
                if (MathHelper.Distance(0, Speed.X) < accelSpeed)
                    Speed.X = 0;

                else
                    Speed.X -= accelSpeed * Math.Sign(Speed.X);
            }
        }

        public void ChangeState(PlayerBaseState newState)
        {
#if DEBUG
            //Console.WriteLine("Player::ChangeState  CurrentState = {0}, NewState = {1}", CurrentState, newState);
#endif
            CurrentState = newState;
            CurrentState.OnEnter();
        }

        public void ChangeAnimation(PlayerAnimations animation)
        {
            currentAnimation = animations[animation];
            currentAnimation.ResetAnimation();
        }

        public override void OnCollision(AABBCollider collider)
        {
            CurrentState.OnCollision(collider);
        }

        public override void OnCollisionResolution(CollisionResult collisionResult, List<AABBCollider> surroundingColliders)
        {
            CurrentState.OnCollisionResolution(collisionResult, surroundingColliders);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
#if DEBUG
            var font = AssetsManager.GetFont("default_pixel_font");

            spriteBatch.DrawString(font, "Speed: " + Speed.ToString(), Vector2.Zero, Color.White, 0, Vector2.Zero, 0.25f, SpriteEffects.None, 1f);

            spriteBatch.DrawString(font, "State: " + CurrentState.ToString(), new Vector2(0, 10), Color.White, 0, Vector2.Zero, 0.25f, SpriteEffects.None, 1f);

            spriteBatch.DrawString(font, "Position: " + Position.ToString(), new Vector2(0, 20), Color.White, 0, Vector2.Zero, 0.25f, SpriteEffects.None, 1f);
            //spriteBatch.DrawString(font, "Hitbox: " + Hitbox.ToString(), new Vector2(0, 50), Color.White, 0, Vector2.Zero, 0.25f, SpriteEffects.None, 1f);
#endif

            base.Draw(spriteBatch);
        }
    }
}