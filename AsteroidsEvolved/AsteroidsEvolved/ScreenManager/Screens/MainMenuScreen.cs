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

namespace AsteroidsEvolved
{
    class MainMenuScreen : MenuScreen
    {
        public static Song menu_theme;

        public MainMenuScreen(ScreenManager manager, Screen exit_screen, MenuStyle style)
            : base(manager, exit_screen, "Asteroids: REDUX", style)
        {
            this.AddItem(
                new AddScreenButton("New Game", manager, typeof(GameScreen),
                    new Object[] { manager, this }
                )
            );

            this.AddItem(
                new AddScreenButton("High Scores", manager, typeof(HighScoresScreen),
                    new Object[] { manager, this, style }
                )
            );

            //this.AddItem(
            //    new AddScreenButton("Options", manager, typeof(OptionsScreen),
            //        new Object[] { manager, this, style }
            //    )
            //);

            this.AddItem(
                new AddScreenButton("Credits", manager, typeof(CreditsScreen),
                    new Object[] { manager, this,
                        new Vector2(
                            GameParameters.TARGET_RESOLUTION.X * (float)1/8,
                            GameParameters.TARGET_RESOLUTION.Y * (float)1/8
                        )
                    }
                )
            );

            this.AddItem(new MenuQuitButton("Quit", this));

            if (menu_theme == null)
                menu_theme = manager.RM.Content.Load<Song>(
                    "default/" + GameParameters.DEFAULT_MENU_THEME
                );
        }

        public override void GotFocus()
        {
            if (manager.current_song != menu_theme)
                MediaPlayer.Play(menu_theme);

            manager.current_song = menu_theme;
        }

        public override void  HandleInput(GameTime time, UserInput input)
        {
 	        base.HandleInput(time, input);

            // Handle non-input
            if (input.GetTimeSinceLastInput().Seconds >= 10)
            {
                //Screen s = new CentipedeGame(manager, this, true);
                //this.hidden_p = true;
                //manager.AddScreen(s);
                //manager.FocusScreen(s);
            }
        }
    }
}
