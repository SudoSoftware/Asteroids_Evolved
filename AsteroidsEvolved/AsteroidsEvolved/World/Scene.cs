using System;
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



		public void update()
		{
			ship.update(new TimeSpan());
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
