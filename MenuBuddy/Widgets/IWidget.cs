using InputHelper;

namespace MenuBuddy
{
	/// <summary>
	/// Base interface for displayable widgets that support scaling, highlighting, transitions, and background rendering.
	/// </summary>
	public interface IWidget : IScreenItem, IScalable, IHighlightable, ITransitionable, IBackgroundable
	{
		/// <summary>
		/// Whether this widget is hidden and should not be drawn.
		/// </summary>
		bool IsHidden { get; set; }

		/// <summary>
		/// Whether this widget responds to tap input.
		/// </summary>
		bool IsTappable { get; }

		/// <summary>
		/// Whether the widget was tapped on the current frame (rising edge of a tap).
		/// </summary>
		bool WasTapped { get; }

		/// <summary>
		/// Whether a tap is currently being held on this widget.
		/// </summary>
		bool IsTapHeld { get; }
	}
}