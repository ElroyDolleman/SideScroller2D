using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using SideScroller2D.GameLogic.Player.PlayerStates;
using SideScroller2D.Graphics;
using SideScroller2D.Utilities;
using SideScroller2D.Input;
using SideScroller2D.Collision;

namespace SideScroller2D.GameLogic.Player
{
    class Player : Entity, IMovableHitbox
    {
        public enum Animations
        {
            Idle,
            Walk,
            Jump,
            Fall,
            Duck
        }

        public Rectangle Hitbox { get { return new Rectangle(Position.ToPoint() + hitbox.Location, hitbox.Size); } }

        // Stats
        public const float RunSpeed = 148f;

        public readonly PlayerIndex PlayerIndex;
        public readonly PlayerInputs Inputs;

        private Rectangle hitbox;

        PlayerBaseState currentState;
        SpriteSheet characterSheet;

        Dictionary<Animations, SpriteSheetAnimation> animations;
        SpriteSheetAnimation currentAnimation;

        public Player(PlayerIndex playerIndex)
        {
            this.PlayerIndex = playerIndex;
            Inputs = InputManager.PlayerInputs[(int)PlayerIndex];

            sprite = new Sprite(AssetsManager.GetTexture("character_nina"));
            characterSheet = new SpriteSheet(sprite.Texture, 16, 16);

            hitbox = new Rectangle(0, 0, 16, 16);

            animations = new Dictionary<Animations, SpriteSheetAnimation>();
            animations.Add(Animations.Idle, new SpriteSheetAnimation(sprite, characterSheet, new int[] { 0 }));
            animations.Add(Animations.Walk, new SpriteSheetAnimation(sprite, characterSheet, new int[] { 0, 1 }));
            animations.Add(Animations.Jump, new SpriteSheetAnimation(sprite, characterSheet, new int[] { 2 }));
            animations.Add(Animations.Fall, new SpriteSheetAnimation(sprite, characterSheet, new int[] { 3 }));

            ChangeState(new IdleState(this));
        }

        public void ChangeState(PlayerBaseState newState)
        {
            currentState = newState;
            currentState.OnEnter();
        }

        public void ChangeAnimation(Animations animation)
        {
            currentAnimation = animations[animation];
            currentAnimation.ResetAnimation();
        }

        public override void Update(GameTime gameTime)
        {
            if (InputManager.IsDown(Inputs.Right))
            {
                Acceleration += new Vector2(0.09f, 0);

                if (Speed.X <= 0)
                {
                    Speed.X = Player.RunSpeed;
                    Acceleration = new Vector2(0, Acceleration.Y);
                }
            }
            else if (InputManager.IsDown(Inputs.Left))
            {
                Acceleration += new Vector2(0.09f, 0);

                if (Speed.X >= 0)
                {
                    Speed.X = -Player.RunSpeed;
                    Acceleration = new Vector2(0, Acceleration.Y);
                }
            }
            else
                Acceleration -= new Vector2(0.09f, 0);

            currentState.Update(gameTime);

            base.Update(gameTime);

            currentAnimation.Update((float)gameTime.ElapsedGameTime.TotalMilliseconds);

            if (Position.X > Main.TargetWidth - hitbox.Width)
                Position = new Vector2(Main.TargetWidth - hitbox.Width, Position.Y);
            else if (Position.X < 0)
                Position = new Vector2(0, Position.Y);

            if (Speed.X < 0)
                sprite.SpriteEffect = SpriteEffects.FlipHorizontally;
            else if (Speed.X > 0)
                sprite.SpriteEffect = SpriteEffects.None;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }
}
