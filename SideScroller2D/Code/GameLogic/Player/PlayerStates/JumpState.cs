using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

using Microsoft.Xna.Framework;

using SideScroller2D.Code.GameLogic.Level;
using SideScroller2D.Code.Input;
using SideScroller2D.Code.Audio;
using SideScroller2D.Code.Collision;

namespace SideScroller2D.Code.GameLogic.Player.PlayerStates
{
    class JumpState : InAirState
    {
        protected float jumpPower = 280f;
        protected float gravitySlowDownMultiplier = 0.39f;

        public JumpState(Player player)
            : base(player)
        {

        }

        public override void OnEnter()
        {
            player.ChangeAnimation(Player.Animations.Jump);

            player.Speed.Y = -jumpPower;

            // Cannot wall jump on first frame
            canWallJump = false;

            AudioManager.PlaySound(GameSounds.PlayerJump);
        }

        public override void Update(GameTime gameTime)
        {
            // Enable wall jump on first frame
            if (!canWallJump)
                canWallJump = true;

            ApplyGravity();

            player.UpdateHorizontalMovementControls();

            if (player.Speed.Y > 0)
                player.ChangeState(player.FallState);
        }

        public override float GetGravity()
        {
            if (InputManager.IsDown(player.Inputs.Jump))
                return defaultGravity * gravitySlowDownMultiplier;

            return base.GetGravity() * 1.4f;
        }

        public override void OnCollision(CollisionResult collisionResult, List<Tile> tiles)
        {
            if (collisionResult.Vertical == CollisionResult.VerticalResults.OnTop)
            {
                List<Tile> headBonkTiles = new List<Tile>();
                bool doHeadBonk = false;
                float newXPos = player.Position.X;

                foreach (Tile tile in tiles)
                {
                    if (tile == null || !tile.Solid)
                        continue;

                    if (player.Hitbox.Top == tile.Hitbox.Bottom)
                    {
                        doHeadBonk = true;
                        headBonkTiles.Add(tile);

                        // If the player doesn't move horizontaly it can do a corner slide
                        //if (player.Speed.X != 0)
                        //    continue;

                        // Check if the player can slide allong the corner of the tile
                        bool leftCornerSlide = player.Speed.X <= 0 && player.Hitbox.Left < tile.Hitbox.Left && tile.Hitbox.Left - player.Hitbox.Left > 4;
                        bool rightCornerSlide = player.Speed.X >= 0 && player.Hitbox.Right > tile.Hitbox.Right && player.Hitbox.Right - tile.Hitbox.Right > 4;

                        if (headBonkTiles.Count == 1 && (leftCornerSlide || rightCornerSlide))
                        {
                            doHeadBonk = false;

                            if (player.Hitbox.Left < tile.Hitbox.Left)
                                newXPos = tile.Hitbox.Left - player.Hitbox.Width;

                            else if (player.Hitbox.Right > tile.Hitbox.Right)
                                newXPos = tile.Hitbox.Right;
                        }
                    }
                }

                if (doHeadBonk)
                    HeadBonk(headBonkTiles);

                else if (newXPos != player.Position.X)
                {
                    player.Position = new Vector2(newXPos, player.Position.Y - 1);

                    foreach (Tile tile in headBonkTiles)
                    {
                        if (tile.TileType == TileTypes.Breakable)
                        {
                            var timer = new Timer(1000f / 60f * 6f);
                            timer.Elapsed += (sender, e) => HeadBonkBreak(sender, e, tile);
                            timer.AutoReset = false;
                            timer.Enabled = true;
                        }
                    }
                }
            }

            base.OnCollision(collisionResult, tiles);
        }

        protected virtual void HeadBonk(List<Tile> headBonkTiles)
        {
            player.Speed.Y = 0;

            foreach (Tile tile in headBonkTiles)
            {
                if (tile.TileType == TileTypes.Breakable)
                    HeadBonkBreak(tile);
            }
        }

        private void HeadBonkBreak(Object source, ElapsedEventArgs e, Tile tileToBreak)
        {
            HeadBonkBreak(tileToBreak);
        }

        private void HeadBonkBreak(Tile tileToBreak)
        {
            tileToBreak.Break();
        }
    }
}
