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
	}
}