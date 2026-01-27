using System.Collections.Generic;

namespace MenuBuddy
{
	/// <summary>
	/// Delegate that returns a collection of screen items, used to provide dynamic item lists.
	/// </summary>
	public delegate IEnumerable<IScreenItem> ScreenItemListDelegate();

}