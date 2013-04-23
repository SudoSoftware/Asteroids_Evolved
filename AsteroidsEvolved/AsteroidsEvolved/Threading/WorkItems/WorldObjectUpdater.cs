using System;
using System.Collections.Generic;
using AsteroidsEvolved.World.WorldObjects;

namespace AsteroidsEvolved.Threading.WorkItems
{
	class WorldObjectUpdater : WorkItem
	{
		private List<WorldObject> objs;
		private DateTime lastExecuted = DateTime.Now;


		public WorldObjectUpdater(List<WorldObject> objs)
		{
			this.objs = objs;
		}



		public override void execute()
		{
			DateTime now = DateTime.Now;

			foreach (WorldObject obj in objs.ToArray())
				obj.update(now - lastExecuted);

			lastExecuted = now;
		}
	}
}
