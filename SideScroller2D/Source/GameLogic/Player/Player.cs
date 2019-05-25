using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using SideScroller2D.GameLogic.Player.PlayerStates;
using SideScroller2D.Graphics;
using SideScroller2D.Managers;
using SideScroller2D.Input;

namespace SideScroller2D.GameLogic.Player
{
    class Player : Entity
    {
        public enum Animations
        {
            Idle,
            Walk,
            Jump,
            Fall,
            Duck
        }

        public readonly PlayerIndex PlayerIndex;
        public readonly PlayerInputs Inputs;

        PlayerBaseState currentState;
        SpriteSheet characterSheet;

        Dictionary<Animations, SpriteSheetAnimation> animations;
        SpriteSheetAnimation currentAnimation;

        public Player(PlayerIndex playerIndex)
        {
            this.PlayerIndex = playerIndex;
            Inputs = InputManager.PlayerInputs[(int)PlayerIndex];

            sprite = new Sprite(AssetsManager.GetTexture("character_nina"));
            characterSheet = new SpriteSheet(sprite, 16, 16);

            animations = new Dictionary<Animations, SpriteSheetAnimation>();
            animations.Add(Animations.Idle, new SpriteSheetAnimation(characterSheet, new int[] { 0 }));
            animations.Add(Animations.Walk, new SpriteSheetAnimation(characterSheet, new int[] { 0, 1 }));
            animations.Add(Animations.Jump, new SpriteSheetAnimation(characterSheet, new int[] { 1 }));
            animations.Add(Animations.Fall, new SpriteSheetAnimation(characterSheet, new int[] { 2 }));

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
        }

        public override void Update(GameTime gameTime)
        {
            currentState.Update(gameTime);

            currentAnimation.Update((float)gameTime.ElapsedGameTime.TotalMilliseconds);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }
}
