using System;
using System.Collections.Generic;
using AsteroidsEvolved;
using AsteroidsEvolved.GameInput;

namespace AsteroidsEvolved
{
    class ModeButton : ScrollButton
    {
        String player_name;
        Player player;

        public ModeButton(String player_name, Player player)
            : base(new List<String>() {"None", "Human Player", "HAL 9000"})
        {
            this.player_name = player_name;
            this.player = player;

            current_index = (int)player.PlayerMode;
			display_text = player_name + options[(int)player.PlayerMode];
        }

		public override void HandleInput(Microsoft.Xna.Framework.GameTime time, HumanInput input)
        {
            if (options.Count == 0)
                return;

            if (input.justPressed(UserInput.InputType.FIRE))
                RunAction();
            else if (input.justPressed(UserInput.InputType.LEFT))
            {
                ScrollLeft();
                display_text = player_name + options[current_index];
            }
            else if (input.justPressed(UserInput.InputType.RIGHT))
            {
                ScrollRight();
                display_text = player_name + options[current_index];
            }
        }

        public override void RunAction()
        {
			player.PlayerMode = (GameParameters.Mode)current_index;

            // MAGIC NUMBERS !!!!
            switch (current_index)
            {
                    // Single Player
                case 0:
					player.PlayerMode = GameParameters.Mode.NA;
                    break;

                    // Multi Player
                case 1:
					player.PlayerMode = GameParameters.Mode.HU;
                    break;

                    // I'm sorry, Dave. I'm afraid I can't let you know
                    // what this case does.
                case 2:
					player.PlayerMode = GameParameters.Mode.AI;
                    break;
            }
        }
    }
}
