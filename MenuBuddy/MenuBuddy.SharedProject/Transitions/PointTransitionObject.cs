using Microsoft.Xna.Framework;
using System;

namespace MenuBuddy
{
	/// <summary>
	/// This is a class that takes a start postion and transitions to the final position
	/// </summary>
	public class PointTransitionObject : BaseTransitionObject
	{
		#region Properties

		/// <summary>
		/// This is the location that the transition object will start from.
		/// </summary>
		public Vector2 StartPosition { get; set; }

		#endregion //Properties

		#region Methods

		public PointTransitionObject(Vector2 startPosition, IScreenTransition screenTransition = null) :
			base(screenTransition)
		{
			StartPosition = startPosition;
		}

		public override Point Position(Rectangle rect)
		{
			var pos = Position(rect.Location.ToVector2());
			return pos.ToPoint();
		}

		public override Vector2 Position(Point pos)
		{
			return Position(pos.ToVector2());
		}

		public override Vector2 Position(Vector2 pos)
		{
			if (ScreenTransition.TransitionPosition != 0.0f)
			{
				//get the transition offset
				var transitionOffset = (float)Math.Pow(ScreenTransition.TransitionPosition, 2.0);
				return Vector2.Lerp(pos, StartPosition, transitionOffset);
			}

			//just return the end position if no transition stuff.
			return pos;
		}

		#endregion //Methods
	}
}