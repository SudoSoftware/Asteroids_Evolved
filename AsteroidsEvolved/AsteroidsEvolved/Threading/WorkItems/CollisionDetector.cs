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
			scene.requestAsteroidsMutex();
			Ship ship = scene.getShip();
			foreach (Asteroid asteroid in scene.getAsteroids())
				if (ship.intersects(asteroid))
					ship.handleIntersection(asteroid);
			scene.releaseAsteroidsMutex();

			//test for collision between rockets and asteroids
		}
	}
}
