using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsteroidsEvolved
{
    class ModeButton : ScrollButton
    {
        public ModeButton()
            : base(new List<String>() {"Single Player", "Multi Player", "HAL (AI) Mode"})
        {
            current_index = (int)GameParameters.selected_mode;
            display_text = "Game Mode: " + options[(int)GameParameters.selected_mode];
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
                display_text = "Game Mode: " + options[current_index];
            }
            else if (input.justPressed(UserInput.InputType.RIGHT))
            {
                ScrollRight();
                display_text = "Game Mode: " + options[current_index];
            }
        }

        public override void RunAction()
        {
            GameParameters.selected_mode = (GameParameters.Mode) current_index;

            // MAGIC NUMBERS !!!!
            switch (current_index)
            {
                    // Single Player
                case 0:
                    GameParameters.selected_mode = GameParameters.Mode.SP;
                    break;

                    // Mulit Player
                case 1:
                    GameParameters.selected_mode = GameParameters.Mode.MP;
                    break;

                    // I'm sorry, Dave. I'm afraid I can't let you know
                    // what this case is.
                case 2:
                    GameParameters.selected_mode = GameParameters.Mode.AI;
                    break;
            }
        }
    }
}
