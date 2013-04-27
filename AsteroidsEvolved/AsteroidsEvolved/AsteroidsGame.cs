using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using AsteroidsEvolved.World;
using AsteroidsEvolved.World.WorldObjects;
using AsteroidsEvolved.Threading;
using AsteroidsEvolved.Threading.WorkItems;
using AsteroidsEvolved;

namespace AsteroidsEvolved
{
	public class AsteroidsGame : Microsoft.Xna.Framework.Game
	{
        ScreenManager manager;

		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;


		public AsteroidsGame()
		{
			graphics = new GraphicsDeviceManager(this);
			graphics.PreferredBackBufferHeight = GameParameters.World.BOUNDS.Height;
			graphics.PreferredBackBufferWidth = GameParameters.World.BOUNDS.Width;
			graphics.IsFullScreen = true;
			graphics.PreferMultiSampling = true;
			Content.RootDirectory = "Content";

            
		}

		

		protected override void Initialize()
		{
            spriteBatch = new SpriteBatch(GraphicsDevice);
            GameParameters.cmanager = Content;
            GameParameters.sbatch = spriteBatch;
            Texture2D background = Content.Load<Texture2D>(GameParameters.World.BACKGROUND);

            manager = new ScreenManager(this, graphics, Content, spriteBatch);
            manager.RM.LoadResources("");
            MediaPlayer.IsRepeating = true;

            // Set up default key bindings.
            UserInput.LeftKey = Keys.Left;
            UserInput.RightKey = Keys.Right;
            UserInput.UpKey = Keys.Up;
            UserInput.DownKey = Keys.Down;
            UserInput.EscKey = Keys.Escape;

            //UserInput.alt_LeftKey = Keys.A;
            //UserInput.alt_RightKey = Keys.D;
            //UserInput.alt_UpKey = Keys.W;
            //UserInput.alt_DownKey = Keys.D;

            Vector2 SCREEN_PARAMETERS =
                new Vector2(
                    GameParameters.TARGET_RESOLUTION.X,
                    GameParameters.TARGET_RESOLUTION.Y
                );

            MenuStyle style =
                new MenuStyle(
                    GameParameters.DEFAULT_TITLE_FACTOR * SCREEN_PARAMETERS,
                    GameParameters.DEFAULT_MENU_FACTOR * SCREEN_PARAMETERS,
                    GameParameters.DEFAULT_MENU_ITEM_DISPLACEMENT * SCREEN_PARAMETERS,
                    "TitleFont",
                    "MenuFont",
                    GameParameters.DEFAULT_TITLE_COLOR,
                    GameParameters.DEFAULT_MENU_COLOR,
                    GameParameters.DEFAULT_SELECTED_ITEM_COLOR
                );

            MainMenuScreen main_menu =
                new MainMenuScreen(manager, new ExitScreen(manager, null), style);
            IntroScreen intro_screen = new IntroScreen(
                manager,
                main_menu,
                new Vector2(
                    GameParameters.TARGET_RESOLUTION.X * (float)1/8,
                    GameParameters.TARGET_RESOLUTION.Y * (float)3/8
                    )
                    );


            manager.AddScreen(new BackgroundScreen(manager, background));

            manager.AddScreen(intro_screen);
            manager.FocusScreen(intro_screen);

			base.Initialize();
		}


	
		protected override void LoadContent()
		{ } //content already loaded in Initialize function



		protected override void UnloadContent()
		{
			Content.Unload();
		}
		

        
		protected override void Update(GameTime gameTime)
		{
			GameParameters.keyboardState = Keyboard.GetState();

            manager.Update(gameTime);

			base.Update(gameTime);
		}



		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.CornflowerBlue);

            manager.Draw();

			base.Draw(gameTime);
		}
	}
}
