using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;

namespace AsteroidsEvolved.GameInput
{
	class UserInput
	{
		public enum InputType
		{
			UP, DOWN, LEFT, RIGHT,
			FIRE, ESCAPE, TELEPORT
		}


		protected List<InputType> lastState = new List<InputType>();
		protected List<InputType> currentState = new List<InputType>();
		protected DateTime lastInputTime;


		public static Keys EscKey, LeftKey, RightKey, UpKey, DownKey, FireKey, TeleportKey;


		public UserInput() :
			this(true)
		{
			resetLastInputTime();
		}



		public UserInput(bool immediateUpdate)
		{
            lastInputTime = new DateTime();

			if (immediateUpdate)
				Update();
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



		public void Update()
		{
			lastState.Clear();
			lastState.AddRange(currentState);
			currentState.Clear();

			UpdateState();

			if (currentState.Count > 0)
				resetLastInputTime();
		}



		public void resetLastInputTime()
		{
			lastInputTime = DateTime.Now;
		}



		protected virtual void UpdateState()
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



		public bool justPressed(InputType type)
		{
			return onNow(type) && !onLastTime(type);
		}



		public bool onLastTime(InputType type)
		{
			return lastState.Contains(type);
		}



		public bool onNow(InputType type)
		{
			return currentState.Contains(type);
		}



		public TimeSpan GetTimeSinceLastInput()
		{
			return DateTime.Now.Subtract(lastInputTime);
		}
	}
}
