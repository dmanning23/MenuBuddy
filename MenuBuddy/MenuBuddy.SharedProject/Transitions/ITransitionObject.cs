using Microsoft.Xna.Framework;

namespace MenuBuddy
{
	/// <summary>
	/// This is an interface of an obect that can transition on & off the screen.
	/// </summary>
	public interface ITransitionObject
	{
		Point Position(ScreenTransition screen, Rectangle rect);

		Vector2 Position(ScreenTransition screen, Point pos);

		Vector2 Position(ScreenTransition screen, Vector2 pos);
	}
}