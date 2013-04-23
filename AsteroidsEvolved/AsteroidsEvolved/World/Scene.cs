using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using AsteroidsEvolved.World.WorldObjects;
using System.Collections.Generic;

namespace AsteroidsEvolved.World
{
	class Scene
	{
		private Camera camera;
		private Ship ship;
		private List<Asteroid> asteroids = new List<Asteroid>();
        private List<Rocket> rockets = new List<Rocket>();


		public Scene(Camera camera)
		{
			this.camera = camera;
		}



		public void draw()
		{
			ship.draw(camera);

			foreach (Asteroid asteroid in asteroids)
				asteroid.draw(camera);

            foreach (Rocket rocket in rockets)
                rocket.draw(camera);
		}



		public Ship getShip()
		{
			return ship;
		}



		public void setShip(Ship newShip)
		{
			ship = newShip;
		}



		public void addAsteroid(Asteroid asteroid)
		{
			asteroids.Add(asteroid);
		}
        


        public void addRocket(Rocket rocket)
        {
            rockets.Add(rocket);
        }
	}
}
