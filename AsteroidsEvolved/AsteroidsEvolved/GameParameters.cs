using System;
using Microsoft.Xna.Framework;

namespace AsteroidsEvolved
{
	class GameParameters
	{
		public static String SHIP_MODEL = "Models/p1_wedge";

		public static Rectangle SCREEN_SIZE = new Rectangle(0, 0, 1024, 768);
		public static int SCREEN_WORLD_RATIO = 32; //increasing the coefficient makes models smaller
		public static Rectangle WORLD_BOUNDS = new Rectangle(
			-SCREEN_SIZE.Width / (SCREEN_WORLD_RATIO * 2),
			-SCREEN_SIZE.Height / (SCREEN_WORLD_RATIO * 2),
			SCREEN_SIZE.Width / SCREEN_WORLD_RATIO,
			SCREEN_SIZE.Height / SCREEN_WORLD_RATIO
			);
	}
}
