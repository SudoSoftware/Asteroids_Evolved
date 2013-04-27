using System;
using Microsoft.Xna.Framework;
using AsteroidsEvolved.GameInput;

namespace AsteroidsEvolved
{
    class MenuButton : MenuItem
    {
        public MenuButton(String item_text)
            : base(item_text)
        {
            this.display_text = item_text;
        }

        public void SetText(String new_text)
        {
            display_text = new_text;
        }

        public virtual void RunAction()
        {
        }

        public override void HandleInput(GameTime time, UserInput input)
        {
            if (input.justPressed(UserInput.InputType.FIRE))
                RunAction();
        }
    }
}
