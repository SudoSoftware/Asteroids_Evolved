using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace AsteroidsEvolved
{
	class Ship : WorldObject
	{
		public Vector2 movementVector = new Vector2(0, -1);
		public float velocity = 0;


		public Ship(Model model) :
			base(model, GameParameters.Ship.SIZE)
		{
			rotation.X = MathHelper.ToRadians(90.0f);
		}



		public override void update(TimeSpan elapsedGameTime)
		{
			if (Keyboard.GetState().IsKeyDown(Keys.Up))
				accelerate(elapsedGameTime);
			if (Keyboard.GetState().IsKeyDown(Keys.Left))
				turnLeft(elapsedGameTime);
			if (Keyboard.GetState().IsKeyDown(Keys.Right))
				turnRight(elapsedGameTime);

			System.Diagnostics.Debug.WriteLine(elapsedGameTime.TotalMilliseconds + "	" + movementVector + "	" + velocity);

			velocity = Math.Max(velocity - (float)elapsedGameTime.TotalMilliseconds * GameParameters.Ship.SLOW_RATE, 0);
			translate(movementVector.X * velocity, -movementVector.Y * velocity);

			base.update(elapsedGameTime);
		}



		public void turnLeft(TimeSpan elapsedGameTime)
		{
			System.Diagnostics.Debug.WriteLine("turning left");
			rotation.Z += (float)elapsedGameTime.TotalMilliseconds * GameParameters.Ship.TURN_RATE;

			double theta = Math.Atan2(movementVector.X, movementVector.Y);
			theta += elapsedGameTime.TotalMilliseconds * GameParameters.Ship.TURN_RATE;

			movementVector.X = (float)Math.Sin(theta);
			movementVector.Y = (float)Math.Cos(theta);
		}



		public void turnRight(TimeSpan elapsedGameTime)
		{
			System.Diagnostics.Debug.WriteLine("turning right");
			rotation.Z -= (float)elapsedGameTime.TotalMilliseconds * GameParameters.Ship.TURN_RATE;

			double theta = Math.Atan2(movementVector.X, movementVector.Y);
			theta -= elapsedGameTime.TotalMilliseconds * GameParameters.Ship.TURN_RATE;

			movementVector.X = (float)Math.Sin(theta);
			movementVector.Y = (float)Math.Cos(theta);
		}



		public void accelerate(TimeSpan elapsedGameTime)
		{
			System.Diagnostics.Debug.WriteLine("accelerating");
			velocity = Math.Min(velocity + (float)elapsedGameTime.TotalMilliseconds * GameParameters.Ship.ACCELERATION, GameParameters.World.SPEED_LIMIT);
		}
	}
}
