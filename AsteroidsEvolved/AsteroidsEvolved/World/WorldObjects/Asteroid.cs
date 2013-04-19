using Microsoft.Xna.Framework.Graphics;

namespace AsteroidsEvolved.World.WorldObjects
{
	class Asteroid : WorldObject
	{
		public Asteroid(Model model) :
			base(model, GameParameters.Asteroid.SIZE)
		{
			
		}
	}
}
