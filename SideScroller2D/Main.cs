﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using SideScroller2D.Graphics;
using SideScroller2D.StateManagement;

namespace SideScroller2D
{
    public class Main : Game
    {
        public const int TargetWidth = 400;
        public const int TargetHeight = 300;

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Color clearColor = Color.CornflowerBlue;

        StateManager stateManager;

        public Main()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            Window.Title = "Side Scroller";

            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 600;
            graphics.ApplyChanges();
        }

        protected override void Initialize()
        {
            float scaleX = graphics.PreferredBackBufferWidth / TargetWidth;
            float scaleY = graphics.PreferredBackBufferHeight / TargetHeight;

            var spriteBatchSettings = SpriteBatchSettings.Default;
            spriteBatchSettings.SpriteBatchScale = Matrix.CreateScale(new Vector3(scaleX, scaleY, 1));

            stateManager = new StateManager(spriteBatchSettings);
            new GameState(stateManager);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            stateManager.LoadContent(Content);
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            stateManager.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(clearColor);

            stateManager.Draw(spriteBatch);

            base.Draw(gameTime);
        }
    }
}
