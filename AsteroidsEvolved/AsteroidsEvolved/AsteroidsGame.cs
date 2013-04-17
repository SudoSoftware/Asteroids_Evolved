using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace AsteroidsEvolved
{
	public class AsteroidsGame : Microsoft.Xna.Framework.Game
	{
		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;

		private Camera camera;
		private Ship ship;


		public AsteroidsGame()
		{
			graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
		}

		

		protected override void Initialize()
		{
			base.Initialize();
		}


	
		protected override void LoadContent()
		{
			spriteBatch = new SpriteBatch(GraphicsDevice);
			ship = new Ship(Content.Load<Model>(GameParameters.SHIP_MODEL));
			camera = new Camera(graphics);
		}



		protected override void UnloadContent()
		{
		}

		

		protected override void Update(GameTime gameTime)
		{
			handleInput();
			

			ship.update(gameTime);


			base.Update(gameTime);
		}



		private void handleInput()
		{
			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
				this.Exit();

			if (Keyboard.GetState().IsKeyDown(Keys.Up))
				ship.moveForward();
		}



		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.CornflowerBlue);

			ship.draw(camera);

			base.Draw(gameTime);
		}



		private void DrawModel(Model DrawMe, Matrix world)
		{
			// Draw the model. A model can have multiple meshes, so loop.
			foreach (ModelMesh Mesh in DrawMe.Meshes)
			{
				// This is where the mesh orientation is set, as well as our camera and projection.
				foreach (BasicEffect effect in Mesh.Effects)
				{
					effect.EnableDefaultLighting();
					effect.World = world;
					effect.View = camera.getView();
					effect.Projection = camera.getProjection();
				}
				
				Mesh.Draw();
			}
		}
	}
}
