using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using AsteroidsEvolved.World;

namespace AsteroidsEvolved
{
	public class AsteroidsGame : Microsoft.Xna.Framework.Game
	{
		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;

		Scene scene;

		public AsteroidsGame()
		{
			graphics = new GraphicsDeviceManager(this);
			graphics.PreferredBackBufferHeight = GameParameters.SCREEN_SIZE.Height;
			graphics.PreferredBackBufferWidth = GameParameters.SCREEN_SIZE.Width;
			graphics.IsFullScreen = true;
			Content.RootDirectory = "Content";
		}

		

		protected override void Initialize()
		{
			base.Initialize();
		}


	
		protected override void LoadContent()
		{
			spriteBatch = new SpriteBatch(GraphicsDevice);

			scene = new Scene(new Camera(graphics));
			scene.setShip(new Ship(Content.Load<Model>(GameParameters.SHIP_MODEL)));
			
		}



		protected override void UnloadContent()
		{
		}

		

		protected override void Update(GameTime gameTime)
		{
			if (Keyboard.GetState().IsKeyDown(Keys.Escape))
				this.Exit();

			scene.update(gameTime);

			base.Update(gameTime);
		}



		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.CornflowerBlue);

			scene.draw();

			base.Draw(gameTime);
		}
	}
}
