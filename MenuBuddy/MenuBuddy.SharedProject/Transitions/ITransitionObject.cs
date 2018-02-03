using Microsoft.Xna.Framework;

namespace MenuBuddy
{
	/// <summary>
	/// This is an interface of an obect that can transition on & off the screen.
	/// </summary>
	public interface ITransitionObject
	{
		Point Position(IScreenTransition screen, Rectangle rect);

		Vector2 Position(IScreenTransition screen, Point pos);

		Vector2 Position(IScreenTransition screen, Vector2 pos);
	}
}