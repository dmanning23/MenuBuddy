using InputHelper;

namespace MenuBuddy
{
	/// <summary>
	/// This the interface for a screen that can display a bunch of widgets.
	/// </summary>
	public interface IWidgetScreen : IScreen, IScreenItemContainer, IClickable, IHighlightable, IDraggable, IDroppable
	{
		/// <summary>
		/// Modal screens will eat all input so no screens underneath can get accidently clicked
		/// </summary>
		bool Modal { get; set; }
	}
}