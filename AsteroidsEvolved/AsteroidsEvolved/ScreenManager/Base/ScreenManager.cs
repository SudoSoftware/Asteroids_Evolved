using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Content;
using AsteroidsEvolved.GameInput;

namespace AsteroidsEvolved
{
    /*
     * This class is a top level class managing screen display and resource management.
     * */
    class ScreenManager
    {
	    // Screen with input focus.
       	Screen focus;

    	// Queue of Screens.
    	List<Screen> screenqueue;

        // The parent Game.
        Game gm;

        // The resource manager.
        public ResourceManager rm;

        // The current song.
        public Song current_song;

        // Input Class
		public UserInput input;

        // The screen which currently has focus.
        public Screen Focus
        {
            get { return focus; }
        }

        // The game accessor.
        public Game GM
        {
            get { return gm; }
        }

        // The resource manager's accessor.
        public ResourceManager RM
        {
            get { return rm; }
        }

        // Constructor
        public ScreenManager(Game game, GraphicsDeviceManager graphics, ContentManager content, SpriteBatch spriteb)
        {
            gm = game;
            rm = new ResourceManager(graphics, content, spriteb);

            screenqueue = new List<Screen>();

			input = (HumanInput)GameParameters.Player1.userInput;
        }

    	public void AddScreen (Screen new_screen)
    	{
           if (!screenqueue.Contains(new_screen))
             screenqueue.Add(new_screen);
	    }

    	public void KillScreen (Screen dead_screen)
    	{
    	    screenqueue.Remove(dead_screen);
    	}

    	public void FocusScreen (Screen focus_screen)
    	{
            focus = focus_screen;
            focus.GotFocus();
    	}

    	public void Update (GameTime time)
    	{
    	    foreach (Screen x in screenqueue.ToArray())
    	        x.Update (time);

            input.Update();

            if (focus != null)
    	        focus.HandleInput(time, input);
    	}

    	public void Draw ()
    	{
    	    foreach (Screen x in screenqueue.ToArray())
	            if (! x.hidden_p) x.Draw();
	    }
    }
}
