using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using AsteroidsEvolved.GameInput;

namespace AsteroidsEvolved
{

    class Screen
    {
	    public bool hidden_p;

        protected ScreenManager manager;
        protected Screen exit_screen;


        public Screen (ScreenManager manager, Screen exit_screen)
        {
	        this.manager = manager;
            this.exit_screen = exit_screen;
        }

        public virtual void GotFocus()
        {
        }

	    public virtual void ExitScreen ()
    	{
       	    manager.KillScreen(this);
            exit_screen.hidden_p = false;
    	    manager.AddScreen(exit_screen);
    	    manager.FocusScreen(exit_screen);
    	}

        public virtual void Update (GameTime time)
        {

        }

		public virtual void HandleInput(GameTime time, HumanInput input)
        {
            if (input.justPressed(UserInput.InputType.ESCAPE))
                ExitScreen();
        }

        public virtual void Draw ()
        {
            GraphicsDevice gd = manager.RM.Graphics.GraphicsDevice;
            SpriteBatch sb = manager.RM.SpriteB;

	        gd.Clear(Color.Red);
        }
    }

}
