using System;
using Microsoft.Xna.Framework;

namespace AsteroidsEvolved
{
	class GameParameters
	{
		public class Ship
		{
			public static readonly String MODEL = "Models/p1_wedge";
			public static readonly float SIZE = 32;
			public static readonly float ACCELERATION = 0.2f;
			public static readonly float TURN_RATE = 0.03f;
			public static readonly float SLOW_RATE = 0.05f;
		}



		public class Rocket
		{
			public static readonly String MODEL = "Models/DERP";
			public static readonly float SIZE = 32;
		}



		public class World
		{
			public static readonly Rectangle BOUNDS = new Rectangle(-1024 / 2, -768 / 2, 1024, 768);
			public static readonly float SPEED_LIMIT = 25f;
		}
	}
}
