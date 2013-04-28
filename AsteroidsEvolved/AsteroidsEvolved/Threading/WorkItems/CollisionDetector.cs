using System.Collections.Generic;
using AsteroidsEvolved.World.WorldObjects;
using AsteroidsEvolved.World;

namespace AsteroidsEvolved.Threading.WorkItems
{
	class CollisionDetector : WorkItem
	{
		private Scene scene;

		public CollisionDetector(Scene scene)
		{
			this.scene = scene;
		}



		public override void execute()
		{
			//test for collision between ship and asteroids
			testForShipAsteroidCollision();
			
			//test for collision between rockets and asteroids
			testForRocketAsteroidCollision();
		}



		private void testForShipAsteroidCollision()
		{
			//scene.requestAsteroidsMutex();
			foreach (Ship ship in scene.getShips())
				foreach (Asteroid asteroid in scene.getAsteroids())
					if (ship.intersects(asteroid))
						ship.handleIntersection(asteroid);
			//scene.releaseAsteroidsMutex();
		}


		private void testForRocketAsteroidCollision()
		{
			scene.requestRocketsMutex();
			foreach (Rocket rocket in scene.getRockets())
			{
				foreach (Asteroid asteroid in scene.getAsteroids())
				{
					if (rocket.intersects(asteroid))
					{
						rocket.handleIntersection(asteroid);
						asteroid.handleIntersection(rocket);
					}
				}
			}
			scene.releaseRocketsMutex();
		}
	}
}
