using System;
using Microsoft.Xna.Framework;
using AsteroidsEvolved.GameInput;

namespace AsteroidsEvolved
{
    class HighScoresScreen : MenuScreen
    {
        public HighScoresScreen(ScreenManager manager, Screen exit_screen, MenuStyle style)
            : base(manager, exit_screen, "High Scores", style)
        {
            PersistanceManager score_manager = new PersistanceManager("highScores.xml");

            for (int i = 0; i < 5; ++i)
                this.AddItem(
                    new MenuItem((i + 1) + ":   " + Convert.ToInt32(score_manager.LoadAll()))
                );

            this.AddItem(
                new MenuQuitButton("Return to Main Menu", this)
            );

            selected_index = menu_items.Count-1;
        }

		public override void HandleInput(Microsoft.Xna.Framework.GameTime time, HumanInput input)
        {
            if (input.justPressed(UserInput.InputType.ESCAPE))
                ExitScreen();

            if (input.justPressed(UserInput.InputType.FIRE))
                menu_items[selected_index].HandleInput(time, input);
        }
    }
}
