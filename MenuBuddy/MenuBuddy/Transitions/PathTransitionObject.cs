using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace MenuBuddy
{
	/// <summary>
	/// This is a class that takes a start postion and transitions to the final position
	/// </summary>
	public class PathTransitionObject : BaseTransitionObject
	{
		#region Properties

		/// <summary>
		/// This is the list of locations that the transition object will transition through.
		/// </summary>
		public List<Vector2> Positions { get; set; }

		#endregion //Properties

		#region Methods

		public PathTransitionObject(List<Vector2> positions, IScreenTransition screenTransition = null) :
			base(screenTransition)
		{
			Positions = positions;
			LeftOrRight = false;
		}

		public override Point Position(IScreen screen, Rectangle rect)
		{
			var pos = Position(screen, rect.Location.ToVector2());
			return pos.ToPoint();
		}

		public override Vector2 Position(IScreen screen, Point pos)
		{
			return Position(screen, pos.ToVector2());
		}

		public override Vector2 Position(IScreen screen, Vector2 pos)
		{
			var screenTransition = GetScreenTransition(screen);

			if (screenTransition.TransitionPosition != 0.0f)
			{
				//get the reverse transition position
				var invTrans = 1f - screenTransition.TransitionPosition;

				//get the indexes the points
				var firstIndex = (int)(invTrans * Positions.Count);
				var firstPoint = firstIndex < Positions.Count ? Positions[firstIndex] : pos;
				var secondIndex = firstIndex + 1;
				var secondPoint = secondIndex < Positions.Count ? Positions[secondIndex] : pos;

				//get the offsets of the points
				var firstOffset = (float)firstIndex / (float)Positions.Count;
				var secondOffset = (float)secondIndex / (float)Positions.Count;

				//get the current offset between the two points
				var pointDelta = secondOffset - firstOffset;
				var offsetDelta = invTrans - firstOffset;
				var offset = 0 < pointDelta ? offsetDelta / pointDelta : 1f;

				//set the left/right 
				LeftOrRight = firstPoint.X > secondPoint.X;

				return Vector2.Lerp(firstPoint, secondPoint, offset);
			}

			//just return the end position if no transition stuff.
			return pos;
		}

		#endregion //Methods
	}
}