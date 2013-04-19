using System.Collections.Generic;
using AsteroidsEvolved.World.WorldObjects;

namespace AsteroidsEvolved.Threading.WorkItems
{
	class AsteroidUpdater : WorkItem
	{
		private List<Asteroid> asteroids;


		public AsteroidUpdater(List<Asteroid> asteroids)
		{
			this.asteroids = asteroids;
		}



		public override void execute()
		{
			foreach (Asteroid asteroid in asteroids)
				asteroid.update();
		}
	}
}
