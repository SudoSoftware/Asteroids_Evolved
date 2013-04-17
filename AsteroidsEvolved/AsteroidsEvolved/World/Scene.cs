using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace AsteroidsEvolved.World
{
	class Scene
	{
		private Camera camera;
		private Ship ship;


		public Scene(Camera camera)
		{
			this.camera = camera;
		}



		public void update(GameTime gameTime)
		{
			if (Keyboard.GetState().IsKeyDown(Keys.Up))
				ship.translate(0, 3.0f);
			if (Keyboard.GetState().IsKeyDown(Keys.Down))
				ship.translate(0, -3.0f);
			if (Keyboard.GetState().IsKeyDown(Keys.Left))
				ship.translate(-3.0f, 0);
			if (Keyboard.GetState().IsKeyDown(Keys.Right))
				ship.translate(3.0f, 0);

			ship.update(gameTime);
		}



		public void draw()
		{
			ship.draw(camera);
		}



		public void setShip(Ship newShip)
		{
			ship = newShip;
		}
	}
}
