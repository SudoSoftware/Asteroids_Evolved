using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using AsteroidsEvolved.World.WorldObjects;
using AsteroidsEvolved.World;
using AsteroidsEvolved.Threading.WorkItems;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using AsteroidsEvolved.Threading;
using AsteroidsEvolved.GameInput;

namespace AsteroidsEvolved
{
	class Ship : WorldObject
	{
		private Player player;
		private Vector2 movementVector = new Vector2(0, 0);
		private Vector2 directionVector = new Vector2(0, -1);


		public Ship(Scene scene, Model model, Vector3 position, Player player) :
			base(scene, model, position, GameParameters.Ship.SIZE)
		{
			rotation.X = MathHelper.ToRadians(90.0f);
			this.player = player;
		}



		public override void update(TimeSpan elapsedGameTime)
		{
			handleNavigation(elapsedGameTime);
			handleFiring(elapsedGameTime);

			translate(movementVector.X, -movementVector.Y);

			base.update(elapsedGameTime);
		}



		public void handleNavigation(TimeSpan elapsedGameTime)
		{
			if (player.userInput.onNow(UserInput.InputType.UP))
				accelerate(elapsedGameTime);

			if (player.userInput.onNow(UserInput.InputType.LEFT))
			{
				turnLeft(elapsedGameTime);
				rotation.Y = MathHelper.ToRadians(10f);
			}
			else if (player.userInput.onNow(UserInput.InputType.RIGHT))
			{
				turnRight(elapsedGameTime);
				rotation.Y = MathHelper.ToRadians(-10f);
			}
			else
				rotation.Y = 0f;

			//if (GameParameters.keyboardState.IsKeyDown(UserInput.DownKey))
			//	movementVector = new Vector2();
		}



		public void handleFiring(TimeSpan elapsedGameTime)
		{
			if (player.userInput.justPressed(UserInput.InputType.FIRE))
				fire(elapsedGameTime);
		}



		public void turnLeft(TimeSpan elapsedGameTime)
		{
			rotation.Z += (float)elapsedGameTime.TotalMilliseconds * GameParameters.Ship.TURN_RATE;

			double theta = Math.Atan2(directionVector.X, directionVector.Y);
			theta += elapsedGameTime.TotalMilliseconds * GameParameters.Ship.TURN_RATE;

			directionVector.X = (float)Math.Sin(theta);
			directionVector.Y = (float)Math.Cos(theta);
		}



		public void turnRight(TimeSpan elapsedGameTime)
		{
			rotation.Z -= (float)elapsedGameTime.TotalMilliseconds * GameParameters.Ship.TURN_RATE;

            double theta = Math.Atan2(directionVector.X, directionVector.Y);
            theta -= elapsedGameTime.TotalMilliseconds * GameParameters.Ship.TURN_RATE;

            directionVector.X = (float)Math.Sin(theta);
            directionVector.Y = (float)Math.Cos(theta);
		}



		public void accelerate(TimeSpan elapsedGameTime)
		{
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

			Rocket rocket = new Rocket(scene, GameParameters.cmanager.Load<Model>(GameParameters.Rocket.MODEL), this, manifests[0].position, movementVector, directionVector);
			scene.addRocket(rocket);
        }



		public override void handleIntersection(WorldObject obj)
		{
			System.Diagnostics.Debug.WriteLine("SHIP INTERSECTING!");
			scene.killItem(this);
		}
	}
}
