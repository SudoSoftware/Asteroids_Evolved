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
						set_keybindings();
					break;

					case GameParameters.Mode.AI:
						player_mode = value;
						userInput = new AI();
					break;
				}
			}
		}



		public void set_keybindings()
		{
			if (player_num == 1)
			{
				((HumanInput)userInput).LeftKey = GameParameters.DefaultKeyBindings.Player1.LEFT;
				((HumanInput)userInput).RightKey = GameParameters.DefaultKeyBindings.Player1.RIGHT;
				((HumanInput)userInput).UpKey = GameParameters.DefaultKeyBindings.Player1.UP;
				((HumanInput)userInput).DownKey = GameParameters.DefaultKeyBindings.Player1.DOWN;
				((HumanInput)userInput).EscKey = GameParameters.DefaultKeyBindings.Player1.ESCAPE;
				((HumanInput)userInput).FireKey = GameParameters.DefaultKeyBindings.Player1.FIRE;
				((HumanInput)userInput).TeleportKey = GameParameters.DefaultKeyBindings.Player1.TELEPORT;
			}

			else if (player_num == 2)
			{
				((HumanInput)userInput).LeftKey = GameParameters.DefaultKeyBindings.Player2.LEFT;
				((HumanInput)userInput).RightKey = GameParameters.DefaultKeyBindings.Player2.RIGHT;
				((HumanInput)userInput).UpKey = GameParameters.DefaultKeyBindings.Player2.UP;
				((HumanInput)userInput).DownKey = GameParameters.DefaultKeyBindings.Player2.DOWN;
				((HumanInput)userInput).EscKey = GameParameters.DefaultKeyBindings.Player2.ESCAPE;
				((HumanInput)userInput).FireKey = GameParameters.DefaultKeyBindings.Player2.FIRE;
				((HumanInput)userInput).TeleportKey = GameParameters.DefaultKeyBindings.Player2.TELEPORT;
			}

			else
				System.Diagnostics.Debug.WriteLine("fubar");
		}



		private int player_num;
		public int score = 0;
		public int lives = 3;
		public Vector2 score_position = new Vector2(50, 50);
		public Vector2 life_position = new Vector2(50, 125);
		public Vector2 life_increment = new Vector2(25, 0);

		public Player(int player_num, GameParameters.Mode init_mode)
		{
			this.player_num = player_num;
			PlayerMode = init_mode;

			//switch (init_mode)
			//{
			//    case GameParameters.Mode.HU:
			//        userInput = new HumanInput();
			//    break;

			//    case GameParameters.Mode.AI:
			//        userInput = new AI();
			//    break;
			//}
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
