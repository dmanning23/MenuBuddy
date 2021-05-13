using Microsoft.Xna.Framework;

namespace MenuBuddy
{
	public abstract class BaseTransitionObject : ITransitionObject
	{
		#region Properties

		public IScreenTransition ScreenTransition { get; set; }

		/// <summary>
		/// Whether the transition object is transitioning to the left or to the right
		/// </summary>
		public bool LeftOrRight { get; protected set; }

		#endregion //Properties

		#region Methods

		public BaseTransitionObject(IScreenTransition screenTransition)
		{
			ScreenTransition = screenTransition;
			LeftOrRight = false;
		}

		public IScreenTransition GetScreenTransition(IScreen screen)
		{
			return ScreenTransition ?? screen.Transition;
		}

		public bool Done(IScreen screen)
		{
			return GetScreenTransition(screen).State == TransitionState.Active;
		}

		public float OnTime(IScreen screen)
		{
			return GetScreenTransition(screen).OnTime;
		}

		public abstract Point Position(IScreen screen, Rectangle rect);

		public abstract Vector2 Position(IScreen screen, Point pos);

		public abstract Vector2 Position(IScreen screen, Vector2 pos);

		public void Dispose()
		{
			ScreenTransition = null;
		}

		#endregion //Methods
	}
}
