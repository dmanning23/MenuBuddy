using Microsoft.Xna.Framework;
using System;

namespace MenuBuddy
{
	/// <summary>
	/// This is a class that takes a start postion and transitions to the final position
	/// </summary>
	public class PointTransitionObject : ITransitionObject
	{
		#region Properties

		/// <summary>
		/// This is the location that the transition object will start from.
		/// </summary>
		public Vector2 StartPosition { get; set; }

		#endregion //Properties

		#region Methods

		public PointTransitionObject(Vector2 startPosition)
		{
			StartPosition = startPosition;
		}

		public Point Position(IScreenTransition screen, Rectangle rect)
		{
			var pos = Position(screen, rect.Location.ToVector2());
			return pos.ToPoint();
		}

		public Vector2 Position(IScreenTransition screen, Point pos)
		{
			return Position(screen, pos.ToVector2());
		}

		public Vector2 Position(IScreenTransition screen, Vector2 pos)
		{
			if (screen.TransitionPosition != 0.0f)
			{
				//get the transition offset
				var transitionOffset = (float)Math.Pow(screen.TransitionPosition, 2.0);
				return Vector2.Lerp(pos, StartPosition, transitionOffset);
			}

			//just return the end position if no transition stuff.
			return pos;
		}

		#endregion //Methods
	}
}