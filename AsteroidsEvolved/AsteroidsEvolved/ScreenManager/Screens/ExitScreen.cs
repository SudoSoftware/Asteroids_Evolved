using System;
using AsteroidsEvolved.GameInput;
using Microsoft.Xna.Framework;

namespace AsteroidsEvolved
{
    class ExitScreen : Screen
    {
        public ExitScreen (ScreenManager manager, Screen exit_screen) : base (manager, null)
        {
        }

		public override void HandleInput(GameTime time, UserInput input)
        {
            manager.GM.Exit();
        }
    }
}
