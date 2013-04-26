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
    class IntroScreen : Screen
    {
        private DateTime start_time;
        private Vector2 position;

        private String display_string;

        public IntroScreen(ScreenManager manager, Screen exit_screen, Vector2 position)
            : base(manager, exit_screen)
        {
            start_time = DateTime.Now;
            this.position = position;

            display_string =
@"Parker Michaelson and Jesse Victors present:
                Asteroids: REDUX

  Based on a Concept by Dr. J.D. Mathias";

            manager.FocusScreen(this);
        }

        public override void GotFocus()
        {
            if (MainMenuScreen.menu_theme == null)
                MainMenuScreen.menu_theme =
                    manager.RM.Content.Load<Song>(GameParameters.DEFAULT_MENU_THEME);

            MediaPlayer.Play(MainMenuScreen.menu_theme);
            manager.current_song = MainMenuScreen.menu_theme;
        }

        public override void ExitScreen()
        {
            manager.KillScreen(this);
            manager.AddScreen(exit_screen);
            manager.FocusScreen(exit_screen);
        }

        public override void Update(GameTime time)
        {
            base.Update(time);

            TimeSpan intro_length = DateTime.Now - start_time;

            if (intro_length.Seconds >= 6)
                ExitScreen();
        }

        public override void Draw()
        {
            SpriteBatch sb = manager.RM.SpriteB;

            sb.Begin();
            sb.DrawString(
                (SpriteFont) manager.RM.FontHash["IntroFont"],
                display_string,
                position,
                Color.AntiqueWhite
            );
            sb.End();
        }
    }
}
