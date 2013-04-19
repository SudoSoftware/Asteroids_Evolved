using System.Collections.Generic;
using AsteroidsEvolved.World.WorldObjects;

namespace AsteroidsEvolved.Threading.WorkItems
{
	class ParticleEffectsUpdater : WorkItem
	{
		public ParticleEffectsUpdater()
		{

		}


		//note, if the Particle (or whatever its eventual name is) is a WorldObject, and there's no need to do anything else except call .update, using the regular WorldObjectUpdater class instead would be a better idea. Then this class could be deleted to avoid code redundancy/duplication.
		public override void execute()
		{
			
		}
	}
}
