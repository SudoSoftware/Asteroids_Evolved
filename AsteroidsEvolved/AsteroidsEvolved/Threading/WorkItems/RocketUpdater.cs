using System;
using System.Collections.Generic;
using AsteroidsEvolved.World;
using AsteroidsEvolved.World.WorldObjects;

namespace AsteroidsEvolved.Threading.WorkItems
{
	class RocketUpdater : WorldObjectUpdater
	{
		public RocketUpdater(Scene scene) :
			base(scene)
		{ }



		public override void updateObjects(TimeSpan elapsedTime)
		{
			scene.requestRocketsMutex();
			List<Rocket> rockets = scene.getRockets();
			List<Rocket> expiredRockets = new List<Rocket>();
			foreach (Rocket rocket in rockets)
			{
				rocket.update(elapsedTime);
				if (!rocket.isAlive())
					expiredRockets.Add(rocket);
			}

			
			foreach (Rocket deadRocket in expiredRockets)
				rockets.Remove(deadRocket);
			scene.releaseRocketsMutex();
		}
	}
}
