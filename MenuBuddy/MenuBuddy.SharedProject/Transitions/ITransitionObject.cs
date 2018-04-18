using Microsoft.Xna.Framework;

namespace MenuBuddy
{
	/// <summary>
	/// This is an interface of an obect that can transition on & off the screen.
	/// </summary>
	public interface ITransitionObject
	{
		/// <summary>
		/// Whether the transition object is transitioning to the left or to the right
		/// </summary>
		bool LeftOrRight { get; }

		bool Done(IScreen screen);

		float OnTime(IScreen screen);

		IScreenTransition ScreenTransition { set; }

		IScreenTransition GetScreenTransition(IScreen screen);

		Point Position(IScreen screen, Rectangle rect);

		Vector2 Position(IScreen screen, Point pos);

		Vector2 Position(IScreen screen, Vector2 pos);
	}
}