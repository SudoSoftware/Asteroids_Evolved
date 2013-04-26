using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using AsteroidsEvolved.Threading;

namespace AsteroidsEvolved
{
	//note that values that affect variables such as movement, turn rate, etc are in units per millisecond
	class GameParameters
	{
        public static readonly Point TARGET_RESOLUTION = new Point(1920, 1080);
        public static readonly Microsoft.Xna.Framework.Rectangle screenSize = new Microsoft.Xna.Framework.Rectangle(0, 0, TARGET_RESOLUTION.X, TARGET_RESOLUTION.Y);

		public class Ship
		{
			public static readonly String MODEL = "Models/p1_wedge";
			public static readonly float SIZE = 24;
			public static readonly float ACCELERATION = 0.15f;
			public static readonly float TURN_RATE = 0.002f;
			public static readonly float SLOW_RATE = 0.004f;
		}



		public class Rocket
		{
			public static readonly String MODEL = "Models/ShockwaveRocket"; //ShockwaveRocket looks better than LargeRocket
			public static readonly float SIZE = 8;
            public static readonly float SPEED = 2f;
            public static readonly TimeSpan lifeDuration = new TimeSpan(0, 0, 5);
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
            public static readonly String BACKGROUND = "Textures/starfield";
			public static readonly Rectangle BOUNDS = new Rectangle(-1920 / 2, -1080 / 2, 1920, 1080);
			public static readonly float SPEED_LIMIT = 32f;
		}



		public static KeyboardState keyboardState = Keyboard.GetState();
        public static SpriteBatch sbatch;
        public static ContentManager cmanager;
        public static ThreadPool threading;

        //menu parameters
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

        //audio
        public static readonly String DEFAULT_MENU_THEME = "audio/04. Another Mysterious Pipe Appeared";
	}
}
