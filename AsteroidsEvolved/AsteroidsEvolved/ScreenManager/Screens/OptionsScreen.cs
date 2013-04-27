using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsteroidsEvolved
{
    class OptionsScreen : MenuScreen
    {
        public OptionsScreen(ScreenManager manager, Screen exit_screen, MenuStyle style)
            : base(manager, exit_screen, "Options", style)
        {
            this.AddItem(
                new AddScreenButton("Controls", manager, typeof(ControlsScreen),
                    new Object[] { manager, this, style }
                )
            );

            this.AddItem(
                new ModeButton("Player 1:", GameParameters.Player1)
                );

            this.AddItem(
                new ModeButton("Player 2:", GameParameters.Player2)
                );


            this.AddItem(new MenuQuitButton("Quit", this));
        }
}
}
