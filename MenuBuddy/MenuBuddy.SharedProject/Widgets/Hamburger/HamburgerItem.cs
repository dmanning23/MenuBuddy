using InputHelper;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace MenuBuddy
{
    internal class HamburgerItem
    {
		public Texture2D Icon { get; set; }
		public string IconText { get; set; }
		public ClickDelegate ClickEvent { get; set; }

		public HamburgerItem(Texture2D icon, string iconText, ClickDelegate clickEvent)
		{
			Icon = icon;
			IconText = iconText;
			ClickEvent = clickEvent;
		}
	}
}
