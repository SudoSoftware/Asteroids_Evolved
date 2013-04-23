using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AsteroidsEvolved.World.WorldObjects
{
	class Asteroid : WorldObject
	{
		public Asteroid(Scene scene, Model model, Vector3 location) :
			base(scene, model, location, GameParameters.Asteroid.SIZE)
		{
			
		}



		public override void update(TimeSpan elapsedGameTime)
		{
			float val = (float)(0.002 * elapsedGameTime.TotalMilliseconds);

			rotation.X += val;
			rotation.Y += val;
			rotation.Z += val;

			base.update(elapsedGameTime);
		}
	}
}
