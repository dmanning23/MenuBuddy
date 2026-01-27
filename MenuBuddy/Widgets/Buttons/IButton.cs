using InputHelper;
using Microsoft.Xna.Framework;

namespace MenuBuddy
{
	/// <summary>
	/// Interface for a clickable button widget that supports sound effects, click events, and left/right navigation.
	/// </summary>
	public interface IButton : IWidget, IScreenItemContainer, IClickable, ILeftRightItem
	{
		/// <summary>
		/// Whether this button suppresses sound effects when highlighted or clicked.
		/// </summary>
		bool IsQuiet { get; set; }

		/// <summary>
		/// The size of this button.
		/// </summary>
		Vector2 Size { set; }

		/// <summary>
		/// A text description of this button's function.
		/// </summary>
		string Description { get; }

		/// <summary>
		/// The name of the sound effect resource to play when this button is highlighted.
		/// </summary>
		string HighlightedSound { set; }

		/// <summary>
		/// The name of the sound effect resource to play when this button is clicked.
		/// </summary>
		string ClickedSound { set; }

		/// <summary>
		/// Handles a click event on this button.
		/// </summary>
		/// <param name="obj">The source of the click event.</param>
		/// <param name="e">The click event arguments containing position information.</param>
		void Clicked(object obj, ClickEventArgs e);
	}
}