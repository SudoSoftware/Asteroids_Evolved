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
    class CreditsScreen : Screen
    {
        Vector2 position;
        String display_string;

        public CreditsScreen(ScreenManager manager, Screen exit_screen, Vector2 position)
            : base(manager, exit_screen)
        {
            this.position = position;


            display_string =
@"Asteroids: REDUX
        Producer: Dr. James Dean Mathias

        Lead Programming: Parker Michaelson and
            Jesse Victors

        Lead Art: The Internet

        Lead Story: What Story?
        ";
        }

        public override void Draw()
        {
            SpriteBatch sb = manager.RM.SpriteB;

            SpriteFont font = (SpriteFont) manager.RM.FontHash["IntroFont"];

            sb.Begin();
            sb.DrawString(font, display_string, position, Color.AntiqueWhite);
            sb.End();
        }
    }
}
