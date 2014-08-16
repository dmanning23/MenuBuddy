using FontBuddyLib;
using GameTimer;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace MenuBuddy
{
	/// <summary>
	/// Helper class represents a single entry in a MenuScreen. By default this
	/// just draws the entry text string, but it can be customized to display menu
	/// entries in different ways. This also provides an event that will be raised
	/// when the menu entry is selected.
	/// </summary>
	public class PrettyMenuEntry : MenuEntry
	{
		#region Properties

		/// <summary>
		/// Gets or sets the font used to draw this menu entry.
		/// </summary>
		public SpriteFont Font { get; set; }

		/// <summary>
		/// A description of the function of the button.
		/// </summary>
		public string Description { get; set; }

		/// <summary>
		/// An optional texture drawn with the text.
		/// </summary>
		/// <remarks>If present, the text will be centered on the texture.</remarks>
		public Texture2D Texture { get; set; }

		#endregion

		#region Methods

		/// <summary>
		/// constructor
		/// </summary>
		/// <param name="text"></param>
		public PrettyMenuEntry(string text)
			: base(text)
		{
		}

		#endregion //Methods
	}
}