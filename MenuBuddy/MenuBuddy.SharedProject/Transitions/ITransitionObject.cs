using Microsoft.Xna.Framework;

namespace MenuBuddy
{
	/// <summary>
	/// This is an interface of an obect that can transition on & off the screen.
	/// </summary>
	public interface ITransitionObject
	{
		IScreenTransition ScreenTransition { get; set; }

		void LoadContent(IScreen screen);

		Point Position(Rectangle rect);

		Vector2 Position(Point pos);

		Vector2 Position(Vector2 pos);
	}
}