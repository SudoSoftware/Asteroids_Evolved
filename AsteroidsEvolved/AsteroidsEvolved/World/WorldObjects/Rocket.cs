using Microsoft.Xna.Framework.Graphics;

namespace AsteroidsEvolved.World.WorldObjects
{
	class Rocket : WorldObject
	{
		public Rocket(Model model) :
			base(model, GameParameters.Rocket.SIZE)
		{
			
		}
	}
}
