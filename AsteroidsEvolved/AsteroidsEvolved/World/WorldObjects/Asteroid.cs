using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AsteroidsEvolved.World.WorldObjects
{
	class Asteroid : WorldObject
	{
		private Vector2 velocity;
		private Vector3 rotationDir;

		public Asteroid(Scene scene, Model model, Vector3 location, Vector2 velocity) :
			base(scene, model, location, GameParameters.Asteroid.SIZE)
		{
			MyRandom rand = new MyRandom();
			this.velocity = velocity;

			const float range = (float)Math.PI * 2;
			rotation = new Vector3(rand.nextRange(-range, range), rand.nextRange(-range, range), rand.nextRange(-range, range));
			rotationDir = new Vector3((float)rand.nextRange(-1, 1), (float)rand.nextRange(-1, 1), (float)rand.nextRange(-1, 1));
		}



		public override void update(TimeSpan elapsedGameTime)
		{
			float val = (float)(0.004 * elapsedGameTime.TotalMilliseconds);

			rotation.X += val * rotationDir.X;
			rotation.Y += val * rotationDir.Y;
			//rotation.Z += val * rotationDir.Z;

			// MOVEMENT!
			translate(velocity.X, -velocity.Y);

			base.update(elapsedGameTime);
		}



		public override void handleIntersection(WorldObject obj)
		{
			if (obj.GetType() == typeof(Rocket))
				modelScale *= Matrix.CreateScale(0.9f);

		}
	}
}
