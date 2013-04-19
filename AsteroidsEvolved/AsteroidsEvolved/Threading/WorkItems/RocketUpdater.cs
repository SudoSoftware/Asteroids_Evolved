using System.Collections.Generic;
using AsteroidsEvolved.World.WorldObjects;

namespace AsteroidsEvolved.Threading.WorkItems
{
	class RocketUpdater : WorkItem
	{
		private List<Rocket> rockets;


		public RocketUpdater(List<Rocket> rockets)
		{
			this.rockets = rockets;
		}



		public override void execute()
		{
			foreach (Rocket rocket in rockets)
				rocket.update();
		}
	}
}
