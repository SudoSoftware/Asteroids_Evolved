using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace AsteroidsEvolved.GameInput
{
	class AI : UserInput
	{
		Ship ship;
		Vector2 dest;

		public AI()
		{
			dest = new Vector2();
			//this.ship = ship;
			currentState.Add(InputType.UP);
		}

		protected override void UpdateState()
		{
			CheckForHumanInput();

			MyRandom rand = new MyRandom();
			Vector2 pos = new Vector2();

			if ((pos - dest).Length() < 500)
				dest = new Vector2(rand.nextRange(-960, 960), rand.nextRange(-512, 512));

			if (dest.X > pos.X)
				currentState.Add(InputType.RIGHT);
			else
				currentState.Add(InputType.LEFT);

			if (dest.Y > pos.Y)
				currentState.Add(InputType.UP);
			else
				currentState.Add(InputType.DOWN);

			//if (rand.nextGaussian(0, 9000) < 10)
			//    currentState.Add(InputType.FIRE);
		}


		private void CheckForHumanInput()
		{
			bool humanInputHappened = Keyboard.GetState().GetPressedKeys().Length > 0;
			if (humanInputHappened)
				currentState.Add(InputType.ESCAPE);
		}
	}
}
