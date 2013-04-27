
using Microsoft.Xna.Framework;

namespace AsteroidsEvolved.GameInput
{
	class Player
	{
		public GameParameters.Mode player_mode;
		public UserInput userInput = new UserInput(); //todo: this is temporary

		public int score = 0;
		public int lives = 3;
		public Vector2 score_position = new Vector2(50, 50);
		public Vector2 life_position = new Vector2(50, 125);
		public Vector2 life_increment = new Vector2(25, 0);

		public Player(GameParameters.Mode init_mode)
		{
			player_mode = init_mode;
		}

		public void reset_vars()
		{
			score = 0;
			lives = 3;
		}

		public Player mirror()
		{
			score_position = new Vector2(
				1600,
				50);

			life_position = new Vector2(
				1600,
				125
				);

			life_increment = -life_increment;

			return this;
		}
	}
}
