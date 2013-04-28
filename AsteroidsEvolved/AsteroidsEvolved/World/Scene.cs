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

		private List<Ship> ships = new List<Ship>();
		private List<Asteroid> asteroids = new List<Asteroid>();
        private List<Rocket> rockets = new List<Rocket>();
		private Mutex shipMutex = new Mutex(), asteroidsMutex = new Mutex(), rocketsMutex = new Mutex();


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

			foreach (Ship ship in ships)
				ship.draw(camera);

			//requestAsteroidsMutex();
			foreach (Asteroid asteroid in asteroids)
				asteroid.draw(camera);
			//releaseRocketsMutex();

			//requestRocketsMutex();
            foreach (Rocket rocket in rockets)
                rocket.draw(camera);
			//releaseRocketsMutex();
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



		public void addShip(Ship ship)
		{
			shipMutex.WaitOne();
			ships.Add(ship);
			shipMutex.ReleaseMutex();
			System.Diagnostics.Debug.WriteLine(ships.Count);
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



		public List<Rocket> getRockets()
		{
			return rockets;
		}



		public List<Ship> getShips()
		{
			return ships;
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
