using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace MenuBuddy
{
    public interface IEditBox
    {
		FontSize FontSize { get; set; }

		Color? ShadowColor { get; set; }

		Color? TextColor { get; set; }
	}
}
