using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AsteroidsEvolved.World.WorldObjects
{
	class Rocket : WorldObject
	{
		public Rocket(Model model, Vector3 location) :
			base(model, location, GameParameters.Rocket.SIZE)
		{
			
		}
	}
}
