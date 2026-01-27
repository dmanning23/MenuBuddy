using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace MenuBuddy
{
	/// <summary>
	/// Interface for edit box styling properties shared by text and numeric input widgets.
	/// </summary>
    public interface IEditBox
    {
		/// <summary>
		/// The font size category for text in this edit box.
		/// </summary>
		FontSize FontSize { get; }

		/// <summary>
		/// An optional override color for the text shadow.
		/// </summary>
		Color? ShadowColor { get; set; }

		/// <summary>
		/// An optional override color for the text.
		/// </summary>
		Color? TextColor { get; set; }
	}
}
