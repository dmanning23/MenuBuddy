using InputHelper;
using Microsoft.Xna.Framework;

namespace MenuBuddy
{
	/// <summary>
	/// Interface for the button object
	/// This is a widget that can be selected
	/// </summary>
	public interface IButton : IWidget, IScreenItemContainer, IClickable
	{
		bool IsQuiet { get; set; }

		Vector2 Size { set; }

		/// <summary>
		/// A description of the function of the button.
		/// </summary>
		string Description { get; }

		/// <summary>
		/// name of the sound effect to play when this thing is highlighted
		/// </summary>
		string HighlightedSound { set; }

		/// <summary>
		/// name of the sound effect to play when this thing is selected
		/// </summary>
		string ClickedSound { set; }

		void Clicked(object obj, ClickEventArgs e);
	}
}