using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using AsteroidsEvolved.World.WorldObjects;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace AsteroidsEvolved.World
{
	class Scene
	{
        private Texture2D background;

		private Camera camera;
		private Ship ship;
		private List<Asteroid> asteroids = new List<Asteroid>();
        private List<Rocket> rockets = new List<Rocket>();


		public Scene(Camera camera, Texture2D background)
		{
			this.camera = camera;
            this.background = background;
		}



		public void draw()
		{
            GameParameters.sbatch.Begin();
            GameParameters.sbatch.Draw(background, new Rectangle(0, 0, 1920, 1080), Color.White);
            GameParameters.sbatch.End();

			ship.draw(camera);

			foreach (Asteroid asteroid in asteroids.ToArray())
				asteroid.draw(camera);

            foreach (Rocket rocket in rockets.ToArray())
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
            // We can't have a whole bunch of rockets on the screen.
            if (rockets.Count >= 5)
                rockets.Remove(rockets[0]);

            rockets.Add(rocket);
        }



        public void killItem(WorldObject obj)
        {
            if (obj.GetType() == typeof(Asteroid))
                asteroids.Remove((Asteroid)obj);

            if (obj.GetType() == typeof(Rocket))
                rockets.Remove((Rocket)obj);
        }
	}
}
