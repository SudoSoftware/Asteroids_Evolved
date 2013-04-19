using System.Collections.Generic;
using AsteroidsEvolved.World.WorldObjects;

namespace AsteroidsEvolved.Threading
{
	class ShipUpdater : WorkItem
	{
		private List<Ship> ships;


		public ShipUpdater(List<Ship> ships)
		{
			this.ships = ships;
		}



		public override void execute()
		{
			foreach (Ship ship in ships)
				ship.update();
		}
	}
}
