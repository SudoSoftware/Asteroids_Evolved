using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using AsteroidsEvolved.GameInput;

namespace AsteroidsEvolved
{
    class ControlsScreen : MenuScreen
    {
		public ControlsScreen(ScreenManager manager, Screen exit_screen, MenuStyle style, Player player)
            : base(manager, exit_screen, "Controls", style)
        {
            this.AddItem( new KeySelector("Left:    ", UserInput.InputType.LEFT, (HumanInput)player.userInput) );

			this.AddItem(new KeySelector("Right:   ", UserInput.InputType.RIGHT, (HumanInput)player.userInput));

			this.AddItem(new KeySelector("Up:      ", UserInput.InputType.UP, (HumanInput)player.userInput));

			this.AddItem(new KeySelector("Down:    ", UserInput.InputType.DOWN, (HumanInput)player.userInput));

			this.AddItem(new KeySelector("Escape:  ", UserInput.InputType.ESCAPE, (HumanInput)player.userInput));

			this.AddItem(new KeySelector("Fire:    ", UserInput.InputType.FIRE, (HumanInput)player.userInput));

            this.AddItem(
                new MenuQuitButton("Return to Main Menu", this)
            );
        }
    }
}
