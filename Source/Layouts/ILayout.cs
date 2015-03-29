using System.Collections.Generic;

namespace MenuBuddy
{
	/// <summary>
	/// This is a thing on the screen that can contain a collection of other things
	/// </summary>
	public interface ILayout : IScreenItem
	{
		/// <summary>
		/// Add a screen item to this screen.
		/// </summary>
		/// <param name="item"></param>
		void AddItem(IScreenItem item);

		/// <summary>
		/// Get all the widgets currently displayed in this layout
		/// </summary>
		List<IWidget> Widgets { get; }
	}
}