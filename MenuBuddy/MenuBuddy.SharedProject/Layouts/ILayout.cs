using InputHelper;
using System.Collections.Generic;

namespace MenuBuddy
{
	/// <summary>
	/// This is a thing on the screen that can contain a collection of other things
	/// </summary>
	public interface ILayout : IScreenItemContainer, IScreenItem, IScalable, IClickable, IHighlightable, IDraggable, IDroppable
	{
		List<IScreenItem> Items
		{
			get;
		}

		bool RemoveItems<T>() where T : IScreenItem;
	}
}