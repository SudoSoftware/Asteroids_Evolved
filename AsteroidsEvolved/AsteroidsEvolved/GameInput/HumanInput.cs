﻿using System;
using Microsoft.Xna.Framework.Input;

namespace AsteroidsEvolved.GameInput
{
	class HumanInput : UserInput
	{
		public Keys EscKey, LeftKey, RightKey, UpKey, DownKey, FireKey, TeleportKey;

		public HumanInput()
		{
			lastState.Clear();
			currentState.Clear();
		}


		protected override void UpdateState()
		{
			KeyboardState keyboardState = Keyboard.GetState();

			if (keyboardState.IsKeyDown(EscKey))
				currentState.Add(InputType.ESCAPE);

			if (keyboardState.IsKeyDown(UpKey))
				currentState.Add(InputType.UP);

			if (keyboardState.IsKeyDown(DownKey))
				currentState.Add(InputType.DOWN);

			if (keyboardState.IsKeyDown(LeftKey))
				currentState.Add(InputType.LEFT);

			if (keyboardState.IsKeyDown(RightKey))
				currentState.Add(InputType.RIGHT);

			if (keyboardState.IsKeyDown(FireKey) || keyboardState.IsKeyDown(Keys.Enter))
				currentState.Add(InputType.FIRE);

			if (keyboardState.IsKeyDown(TeleportKey))
				currentState.Add(InputType.TELEPORT);
		}



		public void SetInputKey(InputType input, Keys key)
		{
			switch (input)
			{
				case InputType.LEFT:
					LeftKey = key;
					break;

				case InputType.RIGHT:
					RightKey = key;
					break;

				case InputType.UP:
					UpKey = key;
					break;

				case InputType.DOWN:
					DownKey = key;
					break;

				case InputType.ESCAPE:
					EscKey = key;
					break;

				case InputType.FIRE:
					FireKey = key;
					break;
			}
		}
	}
}
