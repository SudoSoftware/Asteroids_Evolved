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
			foreach (Rocket rocket in rockets)
				rocket.update(elapsedTime);
			scene.releaseRocketsMutex();
		}
	}
}
