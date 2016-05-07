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
		bool IsQuiet { get; }

		Vector2 Size { set; }

		/// <summary>
		/// A description of the function of the button.
		/// </summary>
		string Description { get; }

		void Clicked(object obj, ClickEventArgs e);
	}
}