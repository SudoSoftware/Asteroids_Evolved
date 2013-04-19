using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using AsteroidsEvolved.World;
using AsteroidsEvolved.Threading;
using AsteroidsEvolved.Threading.WorkItems;
using System.Collections.Generic;

namespace AsteroidsEvolved
{
	public class AsteroidsGame : Microsoft.Xna.Framework.Game
	{
		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;

		Scene scene;
		ThreadPool threading = ThreadPool.getInstance();


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

			scene = new Scene(new Camera(graphics));
			scene.setShip(new Ship(Content.Load<Model>(GameParameters.Ship.MODEL)));

			queueUpdatingShip();
			threading.startWork(); //comment out to switch back to regular XNA cycle

			base.Initialize();
		}


	
		protected override void LoadContent()
		{ } //content already loaded in Initialize function



		protected override void UnloadContent()
		{
			Content.Unload();
		}



		public void queueUpdatingShip()
		{
			List<WorldObject> ships = new List<WorldObject>();
			ships.Add(scene.getShip());
			threading.enqueueWorkItem(new WorldObjectUpdater(ships));
		}

		

		protected override void Update(GameTime gameTime)
		{
			if (Keyboard.GetState().IsKeyDown(Keys.Escape))
				this.Exit();

			//scene.getShip().update(gameTime.ElapsedGameTime); //uncomment out to switch back to regular XNA cycle

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
