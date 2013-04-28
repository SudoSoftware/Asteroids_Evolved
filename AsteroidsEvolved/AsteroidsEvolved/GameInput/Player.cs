using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace AsteroidsEvolved.GameInput
{
	class Player
	{
		private GameParameters.Mode player_mode;
		public UserInput userInput;

		public GameParameters.Mode PlayerMode
		{
			get { return player_mode; }
			set
			{
				switch (value)
				{
					case GameParameters.Mode.HU:
						player_mode = value;
						userInput = new HumanInput();
					break;

					case GameParameters.Mode.AI:
					player_mode = value;
						userInput = new AI();
					break;
				}
			}
		}

		public int score = 0;
		public int lives = 3;
		public Vector2 score_position = new Vector2(50, 50);
		public Vector2 life_position = new Vector2(50, 125);
		public Vector2 life_increment = new Vector2(25, 0);

		public Player(GameParameters.Mode init_mode)
		{
			player_mode = init_mode;

			switch (init_mode)
			{
				case GameParameters.Mode.HU:
				userInput = new HumanInput();
				break;

				case GameParameters.Mode.AI:
				userInput = new AI();
				break;
			}
		}

		public void reset_vars()
		{
			score = 0;
			lives = 3;
		}

		public Player mirror()
		{
			score_position = new Vector2(
				1500,
				50);

			life_position = new Vector2(
				1500,
				125
				);

			life_increment = -life_increment;

			return this;
		}

		public void update()
		{
			if (player_mode != GameParameters.Mode.NA)
				userInput.Update();
		}
	}
}
