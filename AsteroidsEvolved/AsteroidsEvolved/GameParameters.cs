using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace AsteroidsEvolved
{
	//note that values that affect variables such as movement, turn rate, etc are in units per millisecond
	class GameParameters
	{
		public class Ship
		{
			public static readonly String MODEL = "Models/p1_wedge";
			public static readonly float SIZE = 24;
			public static readonly float ACCELERATION = 0.015f;
			public static readonly float TURN_RATE = 0.002f;
			public static readonly float SLOW_RATE = 0.004f;
		}



		public class Rocket
		{
			public static readonly String MODEL = "Models/ShockwaveRocket"; //ShockwaveRocket looks better than LargeRocket
			public static readonly float SIZE = 32;
		}



		public class Asteroid
		{
			public static readonly String MODEL = "Models/asteroid10";
			public static readonly float SIZE = 32;
		}



		public class UFO
		{
			public static readonly String MODEL = "Models/UFO2";
			public static readonly float SIZE = 32;
		}



		public class World
		{
			public static readonly Rectangle BOUNDS = new Rectangle(-1024 / 2, -768 / 2, 1024, 768);
			public static readonly float SPEED_LIMIT = 25f;
		}



		public static KeyboardState keyboardState = Keyboard.GetState();
	}
}
