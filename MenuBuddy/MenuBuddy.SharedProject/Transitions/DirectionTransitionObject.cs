using Microsoft.Xna.Framework;
using System;

namespace MenuBuddy
{
	/// <summary>
	/// This is a class that takes a start point and a direction and transitions to a final point
	/// </summary>
	public class DirectionTransitionObject : BaseTransitionObject
	{
		#region Properties

		private Vector2 _direction = Vector2.Zero;

		/// <summary>
		/// unit vector direction this button is shooting from to the target position
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

		public DirectionTransitionObject(Vector2 dir, IScreenTransition screenTransition = null) :
			base(screenTransition)
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
			//get the target point
			var target = GetTargetPosition(pos);

			if (ScreenTransition.TransitionPosition != 0.0f)
			{
				//get the transition offset
				var transitionOffset = (float)Math.Pow(ScreenTransition.TransitionPosition, 2.0);
				return Vector2.Lerp(target, pos, transitionOffset);
			}

			return target;
		}

		#endregion //Methods
	}
}