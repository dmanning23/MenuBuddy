using InputHelper;
using Microsoft.Xna.Framework;

namespace MenuBuddy
{
	/// <summary>
	/// Interface for the widget object.
	/// A widget is a screen item that is displayed on the page
	/// </summary>
	public interface IWidget : IScreenItem, IScalable, IHighlightable, ITransitionable, IBackgroundable
	{
		/// <summary>
		/// How many pixels worth of padding to add around this widget.
		/// </summary>
		Vector2 Padding { set; }
	}
}