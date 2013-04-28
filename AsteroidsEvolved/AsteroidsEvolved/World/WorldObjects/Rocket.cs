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
		private bool alive = true;


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
            destructTimer += elapsedGameTime;

            translate(
                movementVector.X,
                -movementVector.Y
                );

			if (alive)
				alive = destructTimer < GameParameters.Rocket.lifeDuration;

            // Rockets spin as well as move.
            float val = (float)(GameParameters.Rocket.ROTATION_SPEED * elapsedGameTime.TotalMilliseconds);
            rotation.Y += val;

            base.update(elapsedGameTime);
        }



		public bool isAlive()
		{
			return alive;
		}



		public override void handleIntersection(WorldObject obj)
		{
			if (obj.GetType() == typeof(Asteroid))
			{
				owner.getPlayer().score += 10;
				alive = false;
			}
		}
	}
}
