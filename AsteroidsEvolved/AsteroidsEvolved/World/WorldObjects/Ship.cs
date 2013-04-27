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

namespace AsteroidsEvolved
{
	class Ship : WorldObject
	{
		public Vector2 movementVector = new Vector2(0, 0);
        public Vector2 directionVector = new Vector2(0, -1);

        public Vector2 launcherPos = new Vector2();
		public static SoundEffect pew;

        public TimeSpan lastShot = new TimeSpan();
        public TimeSpan fireDelayTime = new TimeSpan(0, 0, 0, 0, 250);

        public List<WorldObject> rocketList = new List<WorldObject>();
        public WorldObjectUpdater rocketPool;


		public Ship(Scene scene, Model model) :
			base(scene, model, new Vector3(), GameParameters.Ship.SIZE)
		{
			rotation.X = MathHelper.ToRadians(90.0f);

            //rocketPool = new WorldObjectUpdater(ref rocketList);
			ThreadPool.getInstance().enqueueWorkItem(rocketPool);
		}



		public override void update(TimeSpan elapsedGameTime)
		{
			if (GameParameters.keyboardState.IsKeyDown(UserInput.UpKey))
				accelerate(elapsedGameTime);

            if (GameParameters.keyboardState.IsKeyDown(UserInput.LeftKey))
            {
                turnLeft(elapsedGameTime);
                rotation.Y = MathHelper.ToRadians(10f);
            }
            else if (GameParameters.keyboardState.IsKeyDown(UserInput.RightKey))
            {
                turnRight(elapsedGameTime);
                rotation.Y = MathHelper.ToRadians(-10f);
            }
            else
                rotation.Y = 0f;

            if (GameParameters.keyboardState.IsKeyDown(UserInput.DownKey))
                movementVector = new Vector2();

            if (GameParameters.keyboardState.IsKeyDown(UserInput.FireKey))
                fire(elapsedGameTime);

            lastShot += elapsedGameTime.Duration();

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
            if (lastShot > fireDelayTime)
            {
                System.Diagnostics.Debug.WriteLine("firing");

                Rocket rocket = new Rocket(scene, GameParameters.cmanager.Load<Model>(GameParameters.Rocket.MODEL), manifests[0].position, movementVector, directionVector);
                scene.addRocket(rocket);
                rocketList.Add(rocket);
				pew.Play();

                lastShot = new TimeSpan();
            }
        }



		public override void handleIntersection(WorldObject obj)
		{
			System.Diagnostics.Debug.WriteLine("SHIP INTERSECTING!");
			scene.killItem(this);
		}
	}
}
