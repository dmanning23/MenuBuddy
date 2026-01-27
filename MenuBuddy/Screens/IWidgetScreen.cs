using InputHelper;

namespace MenuBuddy
{
	/// <summary>
	/// Interface for screens that can display and manage widgets.
	/// Combines screen functionality with container, click, highlight, drag, and drop capabilities.
	/// </summary>
	public interface IWidgetScreen : IScreen, IScreenItemContainer, IClickable, IHighlightable, IDraggable, IDroppable
	{
		/// <summary>
		/// Gets or sets whether this screen is modal. Modal screens consume all input,
		/// preventing screens underneath from receiving clicks or other input events.
		/// </summary>
		bool Modal { get; set; }
	}
}