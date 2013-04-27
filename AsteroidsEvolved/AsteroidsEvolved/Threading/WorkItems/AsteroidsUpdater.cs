using System;
using System.Collections.Generic;
using AsteroidsEvolved.World;
using AsteroidsEvolved.World.WorldObjects;

namespace AsteroidsEvolved.Threading.WorkItems
{
	class AsteroidsUpdater : WorldObjectUpdater
	{
		public AsteroidsUpdater(Scene scene) :
			base(scene)
		{ }



		public override void updateObjects(TimeSpan elapsedTime)
		{
			List<Asteroid> asteroids = scene.getAsteroids();
			foreach (Asteroid asteroid in asteroids)
				asteroid.update(elapsedTime);
		}
	}
}
