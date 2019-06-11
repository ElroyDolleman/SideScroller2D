using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using SideScroller2D.Code.Graphics;
using SideScroller2D.Code.StateManagement;
using SideScroller2D.Code.StateManagement.States;
using SideScroller2D.Code.Utilities.Time;
using SideScroller2D.Code.Utilities;
using SideScroller2D.Code.Input;
using SideScroller2D.Code.Audio;

namespace SideScroller2D.Code
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

            RNGManager.Initialize();
            InputManager.Initialize();

            stateManager = new StateManager(spriteBatchSettings);
            new GameState(stateManager);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            JsonLoader.SetRootDirectory(Content.RootDirectory);

            AssetsManager.LoadTexture2D(Content, "character_nina");
            AssetsManager.LoadTexture2D(Content, "player_dust_particles");
            AssetsManager.LoadTexture2D(Content, "brick_piece");

            AssetsManager.LoadTileset(Content, "tileset01.json", "tileset01");

            AssetsManager.LoadFont(Content, "default_pixel_font");

            AudioManager.LoadAllSounds(Content);

            stateManager.OnContentLoaded();
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

#if DEBUG
        bool frameByFrameAdvancement = false;
        private static KeyboardState currentKeyboardState;
        private static KeyboardState previousKeyboardState;
#endif

        protected override void Update(GameTime gameTime)
        {
            //if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            //    Exit();

#if DEBUG
            previousKeyboardState = currentKeyboardState;
            currentKeyboardState = Keyboard.GetState();

            if (currentKeyboardState.IsKeyDown(Keys.F) && previousKeyboardState.IsKeyUp(Keys.F))
            {
                frameByFrameAdvancement = !frameByFrameAdvancement;
            }

            if (frameByFrameAdvancement && !(currentKeyboardState.IsKeyDown(Keys.N) && previousKeyboardState.IsKeyUp(Keys.N)) )
            {
                base.Update(gameTime);
                return;
            }
#endif

            ElapsedTime.Update(gameTime);

            InputManager.UpdateState();
            TimerManager.Update();

            stateManager.Update();

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
