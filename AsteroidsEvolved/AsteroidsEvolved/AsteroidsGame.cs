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
using AsteroidsEvolved.GameInput;

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

            manager = new ScreenManager(this, graphics, Content, spriteBatch);
            manager.RM.LoadResources("");
            MediaPlayer.IsRepeating = true;

			setDefaultKeyBindings();
			initializeScreens(Content.Load<Texture2D>(GameParameters.World.BACKGROUND));

			base.Initialize();
		}



		private void initializeScreens(Texture2D background)
		{
			Vector2 SCREEN_PARAMETERS =
				new Vector2(
					GameParameters.TARGET_RESOLUTION.X,
					GameParameters.TARGET_RESOLUTION.Y
				);

			MenuStyle style =
				new MenuStyle(
					GameParameters.Menu.DEFAULT_TITLE_FACTOR * SCREEN_PARAMETERS,
					GameParameters.Menu.DEFAULT_MENU_FACTOR * SCREEN_PARAMETERS,
					GameParameters.Menu.DEFAULT_MENU_ITEM_DISPLACEMENT * SCREEN_PARAMETERS,
					"TitleFont",
					"MenuFont",
					GameParameters.Menu.DEFAULT_TITLE_COLOR,
					GameParameters.Menu.DEFAULT_MENU_COLOR,
					GameParameters.Menu.DEFAULT_SELECTED_ITEM_COLOR
				);

			MainMenuScreen main_menu =
				new MainMenuScreen(manager, new ExitScreen(manager, null), style);
			IntroScreen intro_screen = new IntroScreen(
				manager,
				main_menu,
				new Vector2(
					GameParameters.TARGET_RESOLUTION.X * (float)1 / 8,
					GameParameters.TARGET_RESOLUTION.Y * (float)3 / 8
					)
					);


			manager.AddScreen(new BackgroundScreen(manager, background));

			manager.AddScreen(intro_screen);
			manager.FocusScreen(intro_screen);
		}



		private void setDefaultKeyBindings()
		{
			HumanInput player1keys = (HumanInput)GameParameters.Player1.userInput;

			player1keys.LeftKey = GameParameters.DefaultKeyBindings.Player1.LeftKey;
			player1keys.RightKey = GameParameters.DefaultKeyBindings.Player1.RightKey;
			player1keys.UpKey = GameParameters.DefaultKeyBindings.Player1.UpKey;
			player1keys.DownKey = GameParameters.DefaultKeyBindings.Player1.DownKey;
			player1keys.EscKey = GameParameters.DefaultKeyBindings.Player1.EscKey;
			player1keys.FireKey = GameParameters.DefaultKeyBindings.Player1.FireKey;
			player1keys.TeleportKey = GameParameters.DefaultKeyBindings.Player1.TeleportKey;
		}


	
		protected override void LoadContent()
		{ } //content already loaded in Initialize function



		protected override void UnloadContent()
		{
			Content.Unload();
		}
		

        
		protected override void Update(GameTime gameTime)
		{
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
