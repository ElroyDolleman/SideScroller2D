using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using SideScroller2D.Code.Graphics;
using SideScroller2D.Code.StateManagement;
using SideScroller2D.Code.StateManagement.States;
using SideScroller2D.Code.Utilities;
using SideScroller2D.Code.Input;
using SideScroller2D.Code.Audio;

namespace SideScroller2D.Code
{
    public class Main : Game
    {
        public const int TargetWidth = 400;
        public const int TargetHeight = 300;

        public static float DeltaTime { get; private set; }

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

            AssetsManager.LoadTileset(Content, "tileset01.json", "tileset01");

            AudioManager.LoadAllSounds(Content);

            stateManager.OnContentLoaded();
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

#if DEBUG
        bool frameByFrameAdvancement = false;
#endif

        protected override void Update(GameTime gameTime)
        {
            DeltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            InputManager.UpdateState();

#if DEBUG
            if (InputManager.CurrentKeyboardState.IsKeyDown(Keys.P) && InputManager.PreviousKeyboardState.IsKeyUp(Keys.P))
            {
                frameByFrameAdvancement = !frameByFrameAdvancement;
            }

            // Frame by frame advancement
            if (frameByFrameAdvancement && (InputManager.CurrentKeyboardState.IsKeyUp(Keys.F) || InputManager.PreviousKeyboardState.IsKeyDown(Keys.F)))
            {
                base.Update(gameTime);
                return;
            }
#endif

            //if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            //    Exit();

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
