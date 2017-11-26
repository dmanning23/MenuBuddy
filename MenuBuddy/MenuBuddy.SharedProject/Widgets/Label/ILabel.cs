using FontBuddyLib;
using InputHelper;
using Microsoft.Xna.Framework;

namespace MenuBuddy
{
	/// <summary>
	/// Interface of a peice of text that can be displayed on the screen
	/// </summary>
	public interface ILabel : IWidget, IClickable
	{
		/// <summary>
		/// The text of this label
		/// </summary>
		string Text { get; set; }

		FontSize FontSize { get; set; }

		Color? ShadowColor { get; set; }

		Color? TextColor { get; set; }

		void ScaleToFit(int rowWidth);

		void ShrinkToFit(int rowWidth);
	}
}