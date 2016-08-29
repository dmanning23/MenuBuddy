using InputHelper;

namespace MenuBuddy
{
	/// <summary>
	/// This the interface for a screen that can display a bunch of widgets.
	/// </summary>
	public interface IWidgetScreen : IScreenItemContainer, IClickable, IHighlightable, IDraggable, IDroppable
	{
	}
}