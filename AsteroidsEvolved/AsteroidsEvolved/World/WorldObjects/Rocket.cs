using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace AsteroidsEvolved.World.WorldObjects
{
	class Rocket : WorldObject
	{
		private Vector2 movementVector, directionVector;
		private Ship owner;
        private TimeSpan destructTimer = new TimeSpan();


		public Rocket(Scene scene, Model model, Ship owner, Vector3 location, Vector2 inertia, Vector2 heading) :
			base(scene, model, location, GameParameters.Rocket.SIZE)
		{
            directionVector = heading;
            directionVector.Normalize();

			this.owner = owner;

            rotation.Z = (float)Math.Atan2(-directionVector.Y, directionVector.X) - MathHelper.PiOver2;

            movementVector = inertia + directionVector * GameParameters.Rocket.SPEED;
		}



        public override void update(System.TimeSpan elapsedGameTime)
        {
            destructTimer += elapsedGameTime.Duration();

            //if (destructTimer > GameParameters.Rocket.lifeDuration)
            //    scene.killItem(this);

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
