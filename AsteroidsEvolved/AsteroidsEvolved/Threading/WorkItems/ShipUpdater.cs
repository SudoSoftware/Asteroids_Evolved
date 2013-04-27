using System;
using System.Collections.Generic;
using AsteroidsEvolved.World;

namespace AsteroidsEvolved.Threading.WorkItems
{
	class ShipUpdater : WorldObjectUpdater
	{
		public ShipUpdater(Scene scene) :
			base(scene)
		{ }



		public override void updateObjects(TimeSpan elapsedTime)
		{
			List<Ship> ships = scene.getShips();
			foreach (Ship ship in ships)
				ship.update(elapsedTime);
		}
	}
}
