using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using AsteroidsEvolved.World;
using AsteroidsEvolved.World.WorldObjects;
using AsteroidsEvolved.Threading;
using AsteroidsEvolved.Threading.WorkItems;

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
            GameParameters.cmanager = Content;
            GameParameters.sbatch = spriteBatch;
            Texture2D background = Content.Load<Texture2D>(GameParameters.World.BACKGROUND);

			scene = new Scene(new Camera(graphics), background);
			addShip();
			addAsteroids();
            //addRocket();

            GameParameters.threading = threading;
			threading.startWork(); //comment out to switch back to regular XNA cycle

			base.Initialize();
		}


	
		protected override void LoadContent()
		{ } //content already loaded in Initialize function



		protected override void UnloadContent()
		{
			Content.Unload();
		}



		public void addShip()
		{
			List<WorldObject> objs = new List<WorldObject>();
			Ship ship = new Ship(scene, Content.Load<Model>(GameParameters.Ship.MODEL));
			
			scene.setShip(ship);
			objs.Add(ship);
			threading.enqueueWorkItem(new WorldObjectUpdater(objs));
		}



		public void addAsteroids()
		{
			List<WorldObject> objs = new List<WorldObject>();
			Asteroid asteroid = new Asteroid(scene, Content.Load<Model>(GameParameters.Asteroid.MODEL), new Vector3(200, 200, 0));

			scene.addAsteroid(asteroid);
			objs.Add(asteroid);
			threading.enqueueWorkItem(new WorldObjectUpdater(objs));
		}


        
        public void addRocket()
        {
            List<WorldObject> objs = new List<WorldObject>();
            Rocket rocket = new Rocket(scene, Content.Load<Model>(GameParameters.Rocket.MODEL), new Vector3(200, 200, 0), new Vector2(0, 0), new Vector2(1, -1));

            scene.addRocket(rocket);
            objs.Add(rocket);
            threading.enqueueWorkItem(new WorldObjectUpdater(objs));
        }

		
        
		protected override void Update(GameTime gameTime)
		{
			GameParameters.keyboardState = Keyboard.GetState();

			if (GameParameters.keyboardState.IsKeyDown(Keys.Escape))
			{
				threading.terminate();
				this.Exit();
			}

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
