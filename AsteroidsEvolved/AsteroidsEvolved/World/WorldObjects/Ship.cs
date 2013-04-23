using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using AsteroidsEvolved.World.WorldObjects;

namespace AsteroidsEvolved
{
	class Ship : WorldObject
	{
		public Vector2 movementVector = new Vector2(0, 0);
        public Vector2 directionVector = new Vector2(0, -1);

		public Ship(Model model) :
			base(model, new Vector3(), GameParameters.Ship.SIZE)
		{
			rotation.X = MathHelper.ToRadians(90.0f);
		}



		public override void update(TimeSpan elapsedGameTime)
		{
			if (GameParameters.keyboardState.IsKeyDown(Keys.Up))
				accelerate(elapsedGameTime);
            if (GameParameters.keyboardState.IsKeyDown(Keys.Left))
				turnLeft(elapsedGameTime);
            if (GameParameters.keyboardState.IsKeyDown(Keys.Right))
				turnRight(elapsedGameTime);
            if (GameParameters.keyboardState.IsKeyDown(Keys.Space))
                fire(elapsedGameTime);

            //speed = Math.Max(speed - (float)elapsedGameTime.TotalMilliseconds * GameParameters.Ship.SLOW_RATE, 0);
            //movementVector = movementVector * Math.Max(speed - (float)elapsedGameTime.TotalMilliseconds * GameParameters.Ship.SLOW_RATE, 0);


            System.Diagnostics.Debug.WriteLine(elapsedGameTime.TotalMilliseconds + "	" + movementVector + "	" + movementVector.Length());

			translate(movementVector.X, -movementVector.Y);

			base.update(elapsedGameTime);
		}



		public void turnLeft(TimeSpan elapsedGameTime)
		{
			System.Diagnostics.Debug.WriteLine("turning left");
			rotation.Z += (float)elapsedGameTime.TotalMilliseconds * GameParameters.Ship.TURN_RATE;

			double theta = Math.Atan2(directionVector.X, directionVector.Y);
			theta += elapsedGameTime.TotalMilliseconds * GameParameters.Ship.TURN_RATE;

			directionVector.X = (float)Math.Sin(theta);
			directionVector.Y = (float)Math.Cos(theta);
		}



		public void turnRight(TimeSpan elapsedGameTime)
		{
			System.Diagnostics.Debug.WriteLine("turning right");
			rotation.Z -= (float)elapsedGameTime.TotalMilliseconds * GameParameters.Ship.TURN_RATE;

            double theta = Math.Atan2(directionVector.X, directionVector.Y);
            theta -= elapsedGameTime.TotalMilliseconds * GameParameters.Ship.TURN_RATE;

            directionVector.X = (float)Math.Sin(theta);
            directionVector.Y = (float)Math.Cos(theta);
		}



		public void accelerate(TimeSpan elapsedGameTime)
		{
			System.Diagnostics.Debug.WriteLine("accelerating");

            movementVector += directionVector * GameParameters.Ship.ACCELERATION;

            if (movementVector.Length() > GameParameters.World.SPEED_LIMIT)
            {
                movementVector.Normalize();
                movementVector = movementVector * GameParameters.World.SPEED_LIMIT;
            }
		}



        public void fire(TimeSpan elapsedGameTime)
        {
            System.Diagnostics.Debug.WriteLine("firing");

            
        }
	}
}
