using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsteroidsEvolved.Threading.WorkItems
{
	class WorldObjectUpdater : WorkItem
	{
		private List<WorldObject> objs;
		private DateTime lastExecuted;


		public WorldObjectUpdater(List<WorldObject> objs)
		{
			this.objs = objs;
		}



		public override void execute()
		{
			DateTime now = DateTime.Now;

			foreach (WorldObject obj in objs)
				obj.update(now - lastExecuted);

			lastExecuted = now;
		}
	}
}
