using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using SideScroller2D.Code.GameLogic.Level;
using SideScroller2D.Code.Collision;
using SideScroller2D.Code.Particles;
using SideScroller2D.Code.Utilities;
using SideScroller2D.Code.Input;

namespace SideScroller2D.Code.GameLogic.Player.PlayerStates
{
    class WallSlideState : InAirState
    {
        float dustSpawnTimer;
        readonly float dustSpawnInterval = 9f / 60f;

        public WallSlideState(Player player)
            : base(player)
        {
            maxFallspeed = 42f;
        }

        public override void OnEnter()
        {
            player.Speed.X = 0;
            player.Acceleration.X = 0;

            player.ChangeAnimation(Player.Animations.WallSlide);

            dustSpawnTimer = 0f;
        }

        public override void Update(GameTime gameTime)
        {
            ApplyGravity();

            dustSpawnTimer += Main.DeltaTime;
        }

        public override void OnCollision(CollisionResult collisionResult, List<Tile> tiles)
        {
            base.OnCollision(collisionResult, tiles);

            if (player.CurrentState != this)
                return;

            bool hold = (player.FacingDirection == 1 && InputManager.IsDown(player.Inputs.Right)) || (player.FacingDirection == -1 && InputManager.IsDown(player.Inputs.Left));

            if (hold)
            {
                foreach (Tile tile in tiles)
                {
                    if (tile == null || !tile.Solid)
                        continue;

                    int side = player.FacingDirection == 1 ? player.Hitbox.Right : player.Hitbox.Left;
                    int colliderSide = player.FacingDirection == 1 ? tile.Hitbox.Left : tile.Hitbox.Right;

                    // As long as there is a wall next to the player prevent changing state
                    if (side == colliderSide && player.Hitbox.Top < tile.Hitbox.Bottom && player.Hitbox.Bottom > tile.Hitbox.Top)
                    {
                        if (dustSpawnTimer < dustSpawnInterval)
                            return;

                        dustSpawnTimer -= dustSpawnInterval;

                        DustManager.AddWallSlideDustEffect(new Vector2(player.FacingDirection == 1 ? player.Hitbox.Right + 1 : player.Hitbox.Left - 1, player.Hitbox.Bottom - 2), player.FacingDirection);
                        return;
                    }
                }
            }

            // Go to fall state when there is no wall next to the player or when the left/right button was released
            player.ChangeState(player.FallState);
        }
    }
}
