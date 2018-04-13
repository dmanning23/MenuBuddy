using Microsoft.Xna.Framework;

namespace MenuBuddy
{
	/// <summary>
	/// This is an interface of an obect that can transition on & off the screen.
	/// </summary>
	public interface ITransitionObject
	{
		IScreenTransition ScreenTransition { set; }

		IScreenTransition GetScreenTransition(IScreen screen);

		Point Position(IScreen screen, Rectangle rect);

		Vector2 Position(IScreen screen, Point pos);

		Vector2 Position(IScreen screen, Vector2 pos);
	}
}