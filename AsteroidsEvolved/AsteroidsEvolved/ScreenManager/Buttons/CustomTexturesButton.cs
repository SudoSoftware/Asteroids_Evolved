﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AsteriodsEvolved;
using AsteroidsEvolved;

namespace AsteriodsEvolved
{
    class CustomTexturesButton : ToggleButton
    {
        ResourceManager manager;

        public CustomTexturesButton(ResourceManager manager)
            : base("Custom Textures", false)
        {
            this.manager = manager;
        }

        public override void  RunAction()
        {
 	        base.RunAction();

            if (state)
            {
                manager.LoadResources("custom/");
            }
            else
            {
                manager.LoadResources("default/");
            }
			
			GameParameters.USING_CUSTOM_TEXTURES = state;
        }
    }
}
