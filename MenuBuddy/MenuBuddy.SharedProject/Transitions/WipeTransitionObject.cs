using Microsoft.Xna.Framework;
using System;

namespace MenuBuddy
{
	/// <summary>
	/// This is a transition object that wipes from one side of the screen to another
	/// </summary>
	public class WipeTransitionObject : BaseTransitionObject
	{
		#region Properties

		/// <summary>
		/// The type of wipe to use for this transition
		/// </summary>
		public TransitionWipeType WipeType { get; set; }

		#endregion //Properties

		#region Methods

		public WipeTransitionObject(TransitionWipeType wipe, IScreenTransition screenTransition = null) :
			base(screenTransition)
		{
			WipeType = wipe;
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
			switch (WipeType)
			{
				case TransitionWipeType.PopLeft: { return PopLeftTransition(pos); }
				case TransitionWipeType.PopRight: { return PopRightTransition(pos); }
				case TransitionWipeType.PopTop: { return PopTopTransition(pos); }
				case TransitionWipeType.PopBottom: { return PopBottomTransition(pos); }
				case TransitionWipeType.SlideLeft: { return SlideLeftTransition(pos); }
				case TransitionWipeType.SlideRight: { return SlideRightTransition(pos); }
				case TransitionWipeType.SlideTop: { return SlideTopTransition(pos); }
				case TransitionWipeType.SlideBottom: { return SlideBottomTransition(pos); }
				default:
					{
						//None transition type
						return pos;
					}
			}
		}

		private Vector2 PopLeftTransition(float x, float y)
		{
			return PopLeftTransition(new Vector2(x, y));
		}

		private Vector2 PopRightTransition(float x, float y)
		{
			return PopRightTransition(new Vector2(x, y));
		}

		private Vector2 PopTopTransition(float x, float y)
		{
			return PopTopTransition(new Vector2(x, y));
		}

		private Vector2 PopBottomTransition(float x, float y)
		{
			return PopBottomTransition(new Vector2(x, y));
		}

		/// <summary>
		/// Slide in from the left, slide out the right
		/// </summary>
		/// <param name="pos"></param>
		/// <returns></returns>
		private Vector2 PopLeftTransition(Vector2 pos)
		{
			if (ScreenTransition.TransitionPosition != 0.0f)
			{
				// Make the menu slide into place during transitions, using a
				// power curve to make things look more interesting (this makes
				// the movement slow down as it nears the end).
				var transitionOffset = (float)Math.Pow(ScreenTransition.TransitionPosition, 2.0);
				if (ScreenTransition.State == TransitionState.TransitionOn)
				{
					pos.X -= transitionOffset * 256;
				}
				else
				{
					pos.X -= transitionOffset * 512;
				}
			}

			return pos;
		}

		/// <summary>
		/// Slide in from the right, slide out left
		/// </summary>
		/// <param name="pos"></param>
		/// <returns></returns>
		private Vector2 PopRightTransition(Vector2 pos)
		{
			if (ScreenTransition.TransitionPosition != 0.0f)
			{
				// Make the menu slide into place during transitions, using a
				// power curve to make things look more interesting (this makes
				// the movement slow down as it nears the end).
				var transitionOffset = (float)Math.Pow(ScreenTransition.TransitionPosition, 2.0);
				if (ScreenTransition.State == TransitionState.TransitionOn)
				{
					pos.X += transitionOffset * 256;
				}
				else
				{
					pos.X += transitionOffset * 512;
				}
			}

			return pos;
		}

		/// <summary>
		/// Slide in from the top, slide out the top
		/// </summary>
		/// <param name="pos"></param>
		/// <returns></returns>
		private Vector2 PopTopTransition(Vector2 pos)
		{
			if (ScreenTransition.TransitionPosition != 0.0f)
			{
				// Make the menu slide into place during transitions, using a
				// power curve to make things look more interesting (this makes
				// the movement slow down as it nears the end).
				var transitionOffset = (float)Math.Pow(ScreenTransition.TransitionPosition, 2.0);
				if (ScreenTransition.State == TransitionState.TransitionOn)
				{
					pos.Y -= transitionOffset * 256;
				}
				else
				{
					pos.Y -= transitionOffset * 512;
				}
			}

			return pos;
		}

		/// <summary>
		/// Slide in from the bottom, slide out bottom
		/// </summary>
		/// <param name="pos"></param>
		/// <returns></returns>
		private Vector2 PopBottomTransition(Vector2 pos)
		{
			if (ScreenTransition.TransitionPosition != 0.0f)
			{
				// Make the menu slide into place during transitions, using a
				// power curve to make things look more interesting (this makes
				// the movement slow down as it nears the end).
				var transitionOffset = (float)Math.Pow(ScreenTransition.TransitionPosition, 2.0);
				if (ScreenTransition.State == TransitionState.TransitionOn)
				{
					pos.Y += transitionOffset * 256;
				}
				else
				{
					pos.Y += transitionOffset * 512;
				}
			}

			return pos;
		}

		/// <summary>
		/// Slide in from the left, slide out the left
		/// </summary>
		/// <param name="pos"></param>
		/// <returns></returns>
		private Vector2 SlideLeftTransition(Vector2 pos)
		{
			if (ScreenTransition.TransitionPosition != 0.0f)
			{
				// Make the menu slide into place during transitions, using a
				// power curve to make things look more interesting (this makes
				// the movement slow down as it nears the end).
				var transitionOffset = (float)Math.Pow(ScreenTransition.TransitionPosition, 2.0);
				if (ScreenTransition.State == TransitionState.TransitionOn)
				{
					pos.X -= transitionOffset * 256;
				}
				else
				{
					pos.X += transitionOffset * 512;
				}
			}

			return pos;
		}

		/// <summary>
		/// Slide in from the left, slide out the left
		/// </summary>
		/// <param name="pos"></param>
		/// <returns></returns>
		private Vector2 SlideRightTransition(Vector2 pos)
		{
			if (ScreenTransition.TransitionPosition != 0.0f)
			{
				// Make the menu slide into place during transitions, using a
				// power curve to make things look more interesting (this makes
				// the movement slow down as it nears the end).
				var transitionOffset = (float)Math.Pow(ScreenTransition.TransitionPosition, 2.0);
				if (ScreenTransition.State == TransitionState.TransitionOn)
				{
					pos.X += transitionOffset * 256;
				}
				else
				{
					pos.X -= transitionOffset * 512;
				}
			}

			return pos;
		}

		/// <summary>
		/// Slide in from the left, slide out the left
		/// </summary>
		/// <param name="pos"></param>
		/// <returns></returns>
		private Vector2 SlideTopTransition(Vector2 pos)
		{
			if (ScreenTransition.TransitionPosition != 0.0f)
			{
				// Make the menu slide into place during transitions, using a
				// power curve to make things look more interesting (this makes
				// the movement slow down as it nears the end).
				var transitionOffset = (float)Math.Pow(ScreenTransition.TransitionPosition, 2.0);
				if (ScreenTransition.State == TransitionState.TransitionOn)
				{
					pos.Y -= transitionOffset * 256;
				}
				else
				{
					pos.Y += transitionOffset * 512;
				}
			}

			return pos;
		}

		/// <summary>
		/// Slide in from the left, slide out the left
		/// </summary>
		/// <param name="pos"></param>
		/// <returns></returns>
		private Vector2 SlideBottomTransition(Vector2 pos)
		{
			if (ScreenTransition.TransitionPosition != 0.0f)
			{
				// Make the menu slide into place during transitions, using a
				// power curve to make things look more interesting (this makes
				// the movement slow down as it nears the end).
				var transitionOffset = (float)Math.Pow(ScreenTransition.TransitionPosition, 2.0);
				if (ScreenTransition.State == TransitionState.TransitionOn)
				{
					pos.Y += transitionOffset * 256;
				}
				else
				{
					pos.Y -= transitionOffset * 512;
				}
			}

			return pos;
		}

		#endregion //Methods
	}
}