using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using AsteroidsEvolved.GameInput;

namespace AsteroidsEvolved
{
    class MenuItem
    {
        protected bool active;

	    protected String display_text;

        public MenuItem (String init_text)
	    {
            active = true;
	        display_text = init_text;
	    }

        public void SetActive(bool setbool)
        {
            active = setbool;
        }

		public virtual void HandleInput(GameTime time, HumanInput input)
	    {
    	}

    	public virtual void Draw (ScreenManager manager, MenuStyle style, Vector2 position, bool selected)
    	{
    	    SpriteBatch sb = manager.RM.SpriteB;
    	    String font = style.menu_font;
            Color color = style.menu_color;
            if (selected)
                color = style.selected_color;

            sb.Begin();
            sb.DrawString(
                (SpriteFont)manager.RM.FontHash[font],
                display_text,
                position,
                color
            );
            sb.End();
    	}
    }
}
