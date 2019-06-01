using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using SideScroller2D.Code.GameLogic.Player.PlayerStates;
using SideScroller2D.Code.Graphics;
using SideScroller2D.Code.Utilities;
using SideScroller2D.Code.Input;
using SideScroller2D.Code.Collision;

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
        public const float RunSpeed = 148f;
        public const float RunAcceleration = 0.09f;

        public PlayerBaseState CurrentState { get; protected set; }

        public readonly PlayerIndex PlayerIndex;
        public readonly PlayerInputs Inputs;

        // States
        public IdleState IdleState;
        public RunState RunState;
        public JumpState JumpState;
        public FallState FallState;
        public WallSlideState WallSlideState;

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

            ChangeState(IdleState);
        }

        public void ChangeState(PlayerBaseState newState)
        {
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

            currentAnimation.Update((float)gameTime.ElapsedGameTime.TotalMilliseconds);

            if (Speed.X * Acceleration.X < 0)
                sprite.SpriteEffect = SpriteEffects.FlipHorizontally;
            else if (Speed.X * Acceleration.X > 0)
                sprite.SpriteEffect = SpriteEffects.None;
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

        public override void OnCollision(CollisionResult collisionResult, List<Rectangle> colliders)
        {
            CurrentState.OnCollision(collisionResult, colliders);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }
}
