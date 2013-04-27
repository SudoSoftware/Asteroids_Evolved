using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using AsteroidsEvolved.World.WorldObjects;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using System.Threading;

namespace AsteroidsEvolved.World
{
	class Scene
	{
		private Texture2D background;

		private Camera camera;
		private Ship ship;
		private List<Asteroid> asteroids = new List<Asteroid>();
        private List<Rocket> rockets = new List<Rocket>();
		private Mutex asteroidsMutex = new Mutex(), rocketsMutex = new Mutex();


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
			requestAsteroidsMutex();
			asteroids.Add(asteroid);
			releaseAsteroidsMutex();
		}
        


        public void addRocket(Rocket rocket)
        {
            // We can't have a whole bunch of rockets on the screen.
            if (rockets.Count >= 5)
                //rockets.Remove(rockets[0]);
                // Do nothing instead.
                return;

			requestRocketsMutex();
            rockets.Add(rocket);
			releaseRocketsMutex();
        }



        public void killItem(WorldObject obj)
        {
			requestAsteroidsMutex();
            if (obj.GetType() == typeof(Asteroid))
                asteroids.Remove((Asteroid)obj);
			releaseAsteroidsMutex();

			requestRocketsMutex();
            if (obj.GetType() == typeof(Rocket))
                rockets.Remove((Rocket)obj);
			releaseRocketsMutex();
        }



		public List<Asteroid> getAsteroids()
		{
			return asteroids;
		}



		private List<Rocket> getRockets()
		{
			return rockets;
		}



		public void requestAsteroidsMutex()
		{
			asteroidsMutex.WaitOne();
		}



		public void releaseAsteroidsMutex()
		{
			asteroidsMutex.ReleaseMutex();
		}



		public void requestRocketsMutex()
		{
			rocketsMutex.WaitOne();
		}



		public void releaseRocketsMutex()
		{
			rocketsMutex.ReleaseMutex();
		}
	}
}
