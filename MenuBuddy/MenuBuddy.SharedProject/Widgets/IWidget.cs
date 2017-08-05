using InputHelper;
using Microsoft.Xna.Framework;

namespace MenuBuddy
{
	/// <summary>
	/// Interface for the widget object.
	/// A widget is a screen item that is displayed on the page
	/// </summary>
	public interface IWidget : IScreenItem, IScalable, IHighlightable, ITransitionable
	{
		/// <summary>
		/// Even though this is a IHighlightable object, sometimes you just don't want them to be highlighted.
		/// </summary>
		bool Highlightable { get; set; }

		bool HasBackground { get; }

		bool HasOutline { get; }

		/// <summary>
		/// How many pixels worth of padding to add around this widget.
		/// </summary>
		Vector2 Padding { set; }
	}
}