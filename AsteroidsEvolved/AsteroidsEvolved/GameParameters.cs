using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using AsteroidsEvolved.Threading;
using AsteroidsEvolved.GameInput;

namespace AsteroidsEvolved
{
	//note that values that affect variables such as movement, turn rate, etc are in units per millisecond
	class GameParameters
	{
        public static readonly Point TARGET_RESOLUTION = new Point(1920, 1080);
        public static readonly Microsoft.Xna.Framework.Rectangle screenSize = new Microsoft.Xna.Framework.Rectangle(0, 0, TARGET_RESOLUTION.X, TARGET_RESOLUTION.Y);
		public static SpriteBatch sbatch;
		public static ContentManager cmanager;
		public static Player Player1 = new Player(1, Mode.HU);
		public static Player Player2 = new Player(2, Mode.NA).mirror();


        // Player modes.
		public enum Mode
        {
            //Not Applicable
            NA=0,
            // Human Player
            HU=1,
            // Computer Player
            AI=2
        }



		public class DefaultKeyBindings
		{
			public struct Player1
			{
				public static readonly Keys LEFT = Keys.Left;
				public static readonly Keys RIGHT = Keys.Right;
				public static readonly Keys UP = Keys.Up;
				public static readonly Keys DOWN = Keys.Down;
				public static readonly Keys ESCAPE = Keys.Escape;
				public static readonly Keys FIRE = Keys.Space;
				public static readonly Keys TELEPORT = Keys.T;
			}



			public struct Player2
			{
				public static readonly Keys LEFT = Keys.A;
				public static readonly Keys RIGHT = Keys.D;
				public static readonly Keys UP = Keys.W;
				public static readonly Keys DOWN = Keys.S;
				public static readonly Keys ESCAPE = Keys.Escape;
				public static readonly Keys FIRE = Keys.P;
				public static readonly Keys TELEPORT = Keys.NumPad0;
			}
		}



		public struct Ship
		{
			public static readonly String MODEL = "Models/p1_wedge";
			public static readonly float SIZE = 24;
			public static readonly float ACCELERATION = 0.15f;
			public static readonly float TURN_RATE = 0.002f;
		}



		public struct Rocket
		{
			public static readonly String MODEL = "Models/ShockwaveRocket"; //ShockwaveRocket looks better than LargeRocket
			public static readonly float SIZE = 8;
            public static readonly float SPEED = 2f;
            public static readonly TimeSpan lifeDuration = new TimeSpan(0, 0, 5);
		}



		public struct Asteroid
		{
			public static readonly String MODEL = "Models/asteroid10";
			public static readonly float SIZE = 32;
		}



		public struct UFO
		{
			public static readonly String MODEL = "Models/UFO2";
			public static readonly float SIZE = 32;
		}



		public struct World
        {
            public static readonly String BACKGROUND = "Textures/starfield";
			public static readonly Rectangle BOUNDS = new Rectangle(-1920 / 2, -1080 / 2, 1920, 1080);
			public static readonly float SPEED_LIMIT = 32f;
		}



		public struct Menu
		{
			public static readonly Vector2 DEFAULT_TITLE_FACTOR = new Vector2((float)2.1 / 8, (float)1.0 / 20);
			public static readonly Vector2 DEFAULT_MENU_FACTOR = new Vector2((float)1.1 / 8, (float)1.1 / 6);
			public static readonly Vector2 DEFAULT_MENU_ITEM_DISPLACEMENT = new Vector2(0, ((float)1.0 / 20));
			public static readonly String DEFAULT_TITLE_FONT = "fonts/TitleFont";
			public static readonly String DEFAULT_MENU_FONT = "fonts/MenuFont";
			public static readonly String DEFAULT_LCARS_FONT = "fonts/LcarsFont";
			public static readonly String DEFAULT_INTRO_FONT = "fonts/IntroFont";
			public static readonly String DEFAULT_MENU_BACKGROUND = "textures/starfield";
			public static readonly Microsoft.Xna.Framework.Color DEFAULT_TITLE_COLOR =
				Microsoft.Xna.Framework.Color.AntiqueWhite;
			public static readonly Microsoft.Xna.Framework.Color DEFAULT_MENU_COLOR =
				Microsoft.Xna.Framework.Color.White;
			public static readonly Microsoft.Xna.Framework.Color DEFAULT_SELECTED_ITEM_COLOR =
				Microsoft.Xna.Framework.Color.Yellow;
		}



		public struct Audio
		{
			public static readonly String DEFAULT_MENU_THEME = "audio/04. Another Mysterious Pipe Appeared";
		}
	}
}
