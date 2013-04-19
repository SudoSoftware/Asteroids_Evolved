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
				accelerate();
			if (Keyboard.GetState().IsKeyDown(Keys.Left))
				turnLeft();
			if (Keyboard.GetState().IsKeyDown(Keys.Right))
				turnRight();

			velocity = Math.Max(velocity - GameParameters.Ship.SLOW_RATE, 0);
			translate(movementVector.X * velocity, -movementVector.Y * velocity);

			base.update(elapsedGameTime);
		}



		public void turnLeft()
		{
			double theta = Math.Atan2(movementVector.X, movementVector.Y);
			theta += GameParameters.Ship.TURN_RATE;

			rotation.Z += GameParameters.Ship.TURN_RATE;

			movementVector.X = (float)Math.Sin(theta);
			movementVector.Y = (float)Math.Cos(theta);
		}



		public void turnRight()
		{
			double theta = Math.Atan2(movementVector.X, movementVector.Y);
			theta -= GameParameters.Ship.TURN_RATE;

			rotation.Z -= GameParameters.Ship.TURN_RATE;

			movementVector.X = (float)Math.Sin(theta);
			movementVector.Y = (float)Math.Cos(theta);
		}



		public void accelerate()
		{
			velocity = Math.Min(velocity + GameParameters.Ship.ACCELERATION, GameParameters.World.SPEED_LIMIT);
		}
	}
}
