using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using SideScroller2D.Code.GameLogic.Player.PlayerStates;
using SideScroller2D.Code.GameLogic.Level;
using SideScroller2D.Code.Utilities;
using SideScroller2D.Code.Collision;
using SideScroller2D.Code.Graphics;
using SideScroller2D.Code.Input;

namespace SideScroller2D.Code.GameLogic.Player
{
    class Player : MovableEntity
    {
        public enum Animations
        {
            Idle,
            Walk,
            Jump,
            Fall,
            Duck,
            WallSlide
        }

        // Stats
        public const float RunSpeed = 128f;
        public const float RunAcceleration = 0.09f;

        public bool HoldsDirectionButtonTowardsFacingDirection { get { return (InputManager.IsDown(Inputs.Right) && FacingDirection == 1) || (InputManager.IsDown(Inputs.Left) && FacingDirection == -1); } }

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

        public int SpeedDirection
        {
            get
            {
                if (Speed.X * Acceleration.X > 0)
                    return 1;
                else if (Speed.X * Acceleration.X < 0)
                    return -1;
                return 0;
            }
        }

        public PlayerBaseState CurrentState { get; protected set; }
        public PlayerBaseState PreviousState { get; protected set; }

        public readonly PlayerIndex PlayerIndex;
        public readonly PlayerInputs Inputs;

        // States
        public IdleState IdleState;
        public RunState RunState;
        public JumpState JumpState;
        public FallState FallState;
        public WallSlideState WallSlideState;
        public WallJumpState WallJumpState;

        SpriteSheet characterSheet;

        Dictionary<Animations, SpriteSheetAnimation> animations;
        SpriteSheetAnimation currentAnimation;

        public Player(PlayerIndex playerIndex)
        {
            this.PlayerIndex = playerIndex;
            Inputs = InputManager.PlayerInputs[(int)PlayerIndex];

            sprite = new Sprite(AssetsManager.GetTexture("character_nina"));
            characterSheet = new SpriteSheet(sprite.Texture, 16, 16);

            sprite.Origin = new Vector2(3, 2);
            hitbox = new Rectangle(0, 0, 10, 14);

            animations = new Dictionary<Animations, SpriteSheetAnimation>();
            animations.Add(Animations.Idle, new SpriteSheetAnimation(sprite, characterSheet, new int[] { 0 }));
            animations.Add(Animations.Walk, new SpriteSheetAnimation(sprite, characterSheet, new int[] { 0, 1 }));
            animations.Add(Animations.Jump, new SpriteSheetAnimation(sprite, characterSheet, new int[] { 2 }));
            animations.Add(Animations.Fall, new SpriteSheetAnimation(sprite, characterSheet, new int[] { 3 }));
            animations.Add(Animations.WallSlide, new SpriteSheetAnimation(sprite, characterSheet, new int[] { 7 }));

            InitializeStates();
        }

        private void InitializeStates()
        {
            IdleState = new IdleState(this);
            RunState = new RunState(this);
            JumpState = new JumpState(this);
            FallState = new FallState(this);
            WallSlideState = new WallSlideState(this);
            WallJumpState = new WallJumpState(this);

            CurrentState = IdleState;
            ChangeState(IdleState);
        }

        public void ChangeState(PlayerBaseState newState)
        {
            PreviousState = CurrentState;

            CurrentState = newState;
            CurrentState.OnEnter();
        }

        public void ChangeAnimation(Animations animation)
        {
            currentAnimation = animations[animation];
            currentAnimation.ResetAnimation();
        }

        public override void Update(GameTime gameTime)
        {
            CurrentState.Update(gameTime);

            currentAnimation.Update(Main.DeltaTimeMiliseconds);

            if (SpeedDirection != 0)
                FacingDirection = SpeedDirection;
        }

        public void UpdateHorizontalMovementControls()
        {
            UpdateHorizontalMovementControls(RunSpeed, RunAcceleration);
        }

        public void UpdateHorizontalMovementControls(float speed, float accelerationSpeed)
        {
            if (InputManager.IsDown(Inputs.Right))
            {
                if (Acceleration.X < 1)
                    Acceleration.X = Math.Min(1, Acceleration.X + accelerationSpeed);

                Speed.X = speed;
            }
            else if (InputManager.IsDown(Inputs.Left))
            {
                if (Acceleration.X > -1)
                    Acceleration.X = Math.Max(-1, Acceleration.X - accelerationSpeed);

                Speed.X = speed;
            }
            else if (Acceleration.X != 0)
            {
                int dir = Acceleration.X > 0 ? 1 : -1;
                Acceleration.X -= accelerationSpeed * dir;

                if ((Acceleration.X < 0 && dir == 1) || (Acceleration.X > 0 && dir == -1))
                {
                    Speed.X = 0;
                    Acceleration.X = 0;
                }
            }
        }

        public void UpdateInBounds()
        {
            if (position.X > Main.TargetWidth - hitbox.Width)
                position = new Vector2(Main.TargetWidth - hitbox.Width, Position.Y);

            else if (position.X < 0)
                position = new Vector2(0, Position.Y);
        }

        public void OnCollision(CollisionResult collisionResult, List<Tile> tiles)
        {
            CurrentState.OnCollision(collisionResult, tiles);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
#if DEBUG
            var font = AssetsManager.GetFont("default_pixel_font");

            spriteBatch.DrawString(font, "Speed: " + Speed.ToString(), Vector2.Zero, Color.White, 0, Vector2.Zero, 0.25f, SpriteEffects.None, 1f);
            spriteBatch.DrawString(font, "Accel: " + Acceleration.ToString(), new Vector2(0, 10), Color.White, 0, Vector2.Zero, 0.25f, SpriteEffects.None, 1f);
            spriteBatch.DrawString(font, "Total: " + (Speed * Acceleration).ToString(), new Vector2(0, 20), Color.White, 0, Vector2.Zero, 0.25f, SpriteEffects.None, 1f);

            spriteBatch.DrawString(font, "State: " + CurrentState.ToString().Replace("SideScroller2D.Code.GameLogic.Player.PlayerStates.", "").Replace("State", ""), new Vector2(0, 30), Color.White, 0, Vector2.Zero, 0.25f, SpriteEffects.None, 1f);

            spriteBatch.DrawString(font, "Position: " + Position.ToString(), new Vector2(0, 40), Color.White, 0, Vector2.Zero, 0.25f, SpriteEffects.None, 1f);
            //spriteBatch.DrawString(font, "Hitbox: " + Hitbox.ToString(), new Vector2(0, 50), Color.White, 0, Vector2.Zero, 0.25f, SpriteEffects.None, 1f);
#endif
            base.Draw(spriteBatch);
        }
    }
}
