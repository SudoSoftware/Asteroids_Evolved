using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using AsteroidsEvolved;
using AsteroidsEvolved.GameInput;

namespace AsteroidsEvolved
{
    class KeySelector : MenuButton
    {
        private bool capture_mode;
        private String label_name;
        private UserInput.InputType edit_key;
		private HumanInput edit;

        public KeySelector(String init_text, UserInput.InputType edit_key, HumanInput edit)
            : base(init_text + edit_key.ToString())
        {
            capture_mode = false;
            this.edit_key = edit_key;
			this.edit = edit;
            label_name = init_text;
        }

		public override void HandleInput(GameTime time, UserInput input)
        {
            Keys[] temp = Keyboard.GetState().GetPressedKeys();

            if (temp.Length > 0 && edit != null)
            {
                Keys key = temp[0];
                if (capture_mode && key != Keys.Enter)
                {
                    edit.SetInputKey(edit_key, key);
                    display_text = label_name + key.ToString();
                    capture_mode = false;
                }
                else if (input.justPressed(UserInput.InputType.FIRE))
                    capture_mode = true;
            }
        }
        
    }
}
