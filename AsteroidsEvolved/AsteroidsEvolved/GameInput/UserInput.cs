using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;

namespace AsteroidsEvolved.GameInput
{
	abstract class UserInput
	{
		public enum InputType
		{
			UP, DOWN, LEFT, RIGHT,
			FIRE, ESCAPE, TELEPORT
		}


		private List<InputType> lastState = new List<InputType>();
		protected List<InputType> currentState = new List<InputType>();
		private DateTime lastInputTime;


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



		public void Update()
		{
			lastState.Clear();
			lastState.AddRange(currentState);
			currentState.Clear();

			UpdateState();

			if (currentState.Count > 0)
				resetLastInputTime();
		}



		protected abstract void UpdateState();



		public void resetLastInputTime()
		{
			lastInputTime = DateTime.Now;
		}



		public bool justPressed(InputType type)
		{
			if (currentState.Count > 0)
				System.Diagnostics.Debug.WriteLine("CURRENT STATE WIN " + onNow(type) + "	" + !onLastTime(type));
			if (onNow(type) && !onLastTime(type))
				System.Diagnostics.Debug.WriteLine("***************************************");
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
