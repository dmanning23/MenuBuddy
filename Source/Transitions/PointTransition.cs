using Microsoft.Xna.Framework;
using System;
using System.Diagnostics;

namespace MenuBuddy
{
	/// <summary>
	/// This is a class that takes a start point and a direction and transitions to a final point
	/// </summary>
	public class PointTransition : Transition
	{
		#region Properties

		private Vector2 _direction = Vector2.Zero;

		/// <summary>
		/// unit vector direction this button is shooting out
		/// </summary>
		public Vector2 Direction
		{
			get
			{
				return _direction;
			}
			set
			{
				_direction = value;
				_direction.Normalize();
			}
		}

		#endregion //Properties

		#region Methods

		public PointTransition(Vector2 dir)
		{
			Direction = dir;
		}

		/// <summary>
		/// After you've set the start, dir, image, calculate the button rect for this dude
		/// </summary>
		private Vector2 GetTargetPosition(Vector2 start)
		{
			//set teh target position
			return start + Direction;
		}

		#endregion //Methods

		#region Transition Positions

		public override Vector2 Position(Vector2 pos, TransitionType transition)
		{
			//get the target point
			var target = GetTargetPosition(pos);

			if (TransitionPosition != 0.0f)
			{
				//get the transition offset
				var transitionOffset = (float)Math.Pow(TransitionPosition, 2.0);
				return Vector2.Lerp(target, pos, transitionOffset);
			}

			return target;
		}

		#endregion //Transition Positions
	}
}