using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AsteroidsEvolved;

namespace AsteroidsEvolved
{
    class ModeButton : ScrollButton
    {
        String player_name;
        GameParameters.Player player;

        public ModeButton(String player_name, GameParameters.Player player)
            : base(new List<String>() {"None", "Human Player", "HAL 9000"})
        {
            this.player_name = player_name;
            this.player = player;

            current_index = (int)player.player_mode;
            display_text = player_name + options[(int)player.player_mode];
        }

        public override void HandleInput(Microsoft.Xna.Framework.GameTime time, UserInput input)
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
            player.player_mode = (GameParameters.Mode) current_index;

            // MAGIC NUMBERS !!!!
            switch (current_index)
            {
                    // Single Player
                case 0:
                    player.player_mode = GameParameters.Mode.NA;
                    break;

                    // Mulit Player
                case 1:
                    player.player_mode = GameParameters.Mode.HU;
                    break;

                    // I'm sorry, Dave. I'm afraid I can't let you know
                    // what this case is.
                case 2:
                    player.player_mode = GameParameters.Mode.AI;
                    break;
            }
        }
    }
}
