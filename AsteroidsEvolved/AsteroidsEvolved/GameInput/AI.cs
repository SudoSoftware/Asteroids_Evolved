using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;
using AsteroidsEvolved.World;
using System.Collections.Generic;

namespace AsteroidsEvolved.GameInput
{
	class AI : UserInput
	{
		Ship ship;
		Vector2 dest;

		private MyRandom prng = new MyRandom();
		private DateTime lastFired = DateTime.Now, bornTime = DateTime.Now;
		private double millisTillFire = 500;
		private Vector2 goalLocation;

		public AI()
		{
			dest = new Vector2();
			//this.ship = ship;
			currentState.Add(InputType.UP);
		}



		protected override void UpdateState()
		{
			CheckForHumanInput();
			UpdateDecisions();
			//Navigate();
			Fire();

			//if (dest.Y > pos.Y)
			//    currentState.Add(InputType.UP);

			//if (rand.nextGaussian(0, 9000) < 10)
			//    currentState.Add(InputType.FIRE);
		}


		private void CheckForHumanInput()
		{
			bool humanInputHappened = Keyboard.GetState().GetPressedKeys().Length > 0;
			if (humanInputHappened)
				currentState.Add(InputType.ESCAPE);
		}


		private void UpdateDecisions()
		{
			const int minX = -512;
			const int maxX = 512;
			const int minY = -960;
			const int maxY = 960;

			if (GetDistanceToGo() < 5)
				goalLocation = new Vector2(prng.nextRange(minX, maxX), prng.nextRange(minY, maxY));

			
			Vector2 pos = new Vector2();

			if ((pos - dest).Length() < 500)
				dest = new Vector2(prng.nextRange(-960, 960), prng.nextRange(-512, 512));



			if (dest.X > pos.X)
				currentState.Add(InputType.RIGHT);
			else
				currentState.Add(InputType.LEFT);
		}



		private void Navigate()
		{
			Vector2 travelVector = GetTravelVector();

			if (Math.Abs(travelVector.X) >= 3)
			{
				if (travelVector.X > 0)
					currentState.Add(InputType.RIGHT);
				else
					currentState.Add(InputType.LEFT);
			}

			if (Math.Abs(travelVector.Y) >= 3)
			{
				if (travelVector.Y < 0)
					currentState.Add(InputType.UP);
				else
					currentState.Add(InputType.DOWN);
			}
		}


		private void Fire()
		{
			//fire logic here
			
			if (DateTime.Now.Subtract(lastFired).TotalMilliseconds > millisTillFire)
			{
				currentState.Add(InputType.FIRE);
				lastFired = DateTime.Now;
				millisTillFire = prng.nextGaussian(100, 100);
			}
		}



		private Vector2 GetTravelVector()
		{
			if (Scene.instance == null)
				return new Vector2();

			List<Ship> ships = Scene.instance.getShips();
			Ship controlledShip = null;
			foreach (Ship ship in ships)
				if (ship.getPlayer().PlayerMode == GameParameters.Mode.AI && ship.getPlayer().userInput == this)
					controlledShip = ship;

			Vector3 shipLoc = controlledShip.getPosOfFirstManifest();
			Vector2 vector = new Vector2(shipLoc.X, shipLoc.Y);
			vector = goalLocation - vector;
			return vector;
		}



		private float GetDistanceToGo()
		{
			return GetTravelVector().Length();
		}



		private void MoveTowardsLocation(Vector2 desiredLocation)
		{
			goalLocation = desiredLocation;
		}



		public DateTime GetBornTime()
		{
			return bornTime;
		}
	}
}
