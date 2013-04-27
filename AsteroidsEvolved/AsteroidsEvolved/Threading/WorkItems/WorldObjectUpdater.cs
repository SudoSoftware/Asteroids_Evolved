using System;
using AsteroidsEvolved.World.WorldObjects;
using AsteroidsEvolved.World;

namespace AsteroidsEvolved.Threading.WorkItems
{
	abstract class WorldObjectUpdater : WorkItem
	{
		private DateTime lastExecuted = DateTime.Now;
		protected Scene scene;


		public WorldObjectUpdater(Scene scene)
		{
			this.scene = scene;
		}



		public override void execute()
		{
			DateTime now = DateTime.Now;
			updateObjects(now - lastExecuted);
			lastExecuted = now;
		}


		public abstract void updateObjects(TimeSpan elapsedTime);
	}
}
