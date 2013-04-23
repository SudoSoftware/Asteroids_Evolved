using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace AsteroidsEvolved.World.WorldObjects
{
	class Rocket : WorldObject
	{
        public Vector2 movementVector = new Vector2(0, 0);
        public Vector2 directionVector = new Vector2(0, 0);

        private TimeSpan destructTimer = new TimeSpan();

		public Rocket(Scene scene, Model model, Vector3 location, Vector2 inertia, Vector2 heading) :
			base(scene, model, location, GameParameters.Rocket.SIZE)
		{
            directionVector = heading;
            directionVector.Normalize();

            rotation.Z = (float)Math.Atan2(-directionVector.Y, directionVector.X) - MathHelper.PiOver2;

            movementVector = inertia + directionVector*GameParameters.Rocket.SPEED;
		}

        public override void update(System.TimeSpan elapsedGameTime)
        {
            destructTimer += elapsedGameTime.Duration();

            if (destructTimer > GameParameters.Rocket.lifeDuration)
                scene.killItem(this);

            translate(
                movementVector.X,
                -movementVector.Y
                );

            // Rockets spin as well as move.
            float val = (float)(0.002 * elapsedGameTime.TotalMilliseconds);
            rotation.Y += val;

            base.update(elapsedGameTime);
        }
	}
}
