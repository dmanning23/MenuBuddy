using FontBuddyLib;
using GameTimer;
using Microsoft.Xna.Framework;

namespace MenuBuddy
{
	/// <summary>
	/// Interface of a peice of text that can be displayed on the screen
	/// </summary>
	public interface ILabel : IWidget
	{
		float Size { get; }

		/// <summary>
		/// The text of this label
		/// </summary>
		string Text { get; }
	}
}