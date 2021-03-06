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
			LeftOrRight = wipe == TransitionWipeType.PopLeft || wipe == TransitionWipeType.SlideLeft;
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
			switch (WipeType)
			{
				case TransitionWipeType.PopLeft: { return PopLeftTransition(screenTransition, pos); }
				case TransitionWipeType.PopRight: { return PopRightTransition(screenTransition, pos); }
				case TransitionWipeType.PopTop: { return PopTopTransition(screenTransition, pos); }
				case TransitionWipeType.PopBottom: { return PopBottomTransition(screenTransition, pos); }
				case TransitionWipeType.SlideLeft: { return SlideLeftTransition(screenTransition, pos); }
				case TransitionWipeType.SlideRight: { return SlideRightTransition(screenTransition, pos); }
				case TransitionWipeType.SlideTop: { return SlideTopTransition(screenTransition, pos); }
				case TransitionWipeType.SlideBottom: { return SlideBottomTransition(screenTransition, pos); }
				default:
					{
						//None transition type
						return pos;
					}
			}
		}

		private Vector2 PopLeftTransition(IScreenTransition screenTransition, float x, float y)
		{
			return PopLeftTransition(screenTransition, new Vector2(x, y));
		}

		private Vector2 PopRightTransition(IScreenTransition screenTransition, float x, float y)
		{
			return PopRightTransition(screenTransition, new Vector2(x, y));
		}

		private Vector2 PopTopTransition(IScreenTransition screenTransition, float x, float y)
		{
			return PopTopTransition(screenTransition, new Vector2(x, y));
		}

		private Vector2 PopBottomTransition(IScreenTransition screenTransition, float x, float y)
		{

			return PopBottomTransition(screenTransition, new Vector2(x, y));
		}

		/// <summary>
		/// Slide in from the left, slide out the right
		/// </summary>
		/// <param name="pos"></param>
		/// <returns></returns>
		private Vector2 PopLeftTransition(IScreenTransition screenTransition, Vector2 pos)
		{
			if (screenTransition.TransitionPosition != 0.0f)
			{
				// Make the menu slide into place during transitions, using a
				// power curve to make things look more interesting (this makes
				// the movement slow down as it nears the end).
				var transitionOffset = (float)Math.Pow(screenTransition.TransitionPosition, 2.0);
				if (screenTransition.State == TransitionState.TransitionOn)
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
		private Vector2 PopRightTransition(IScreenTransition screenTransition, Vector2 pos)
		{
			if (screenTransition.TransitionPosition != 0.0f)
			{
				// Make the menu slide into place during transitions, using a
				// power curve to make things look more interesting (this makes
				// the movement slow down as it nears the end).
				var transitionOffset = (float)Math.Pow(screenTransition.TransitionPosition, 2.0);
				if (screenTransition.State == TransitionState.TransitionOn)
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
		private Vector2 PopTopTransition(IScreenTransition screenTransition, Vector2 pos)
		{
			if (screenTransition.TransitionPosition != 0.0f)
			{
				// Make the menu slide into place during transitions, using a
				// power curve to make things look more interesting (this makes
				// the movement slow down as it nears the end).
				var transitionOffset = (float)Math.Pow(screenTransition.TransitionPosition, 2.0);
				if (screenTransition.State == TransitionState.TransitionOn)
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
		private Vector2 PopBottomTransition(IScreenTransition screenTransition, Vector2 pos)
		{
			if (screenTransition.TransitionPosition != 0.0f)
			{
				// Make the menu slide into place during transitions, using a
				// power curve to make things look more interesting (this makes
				// the movement slow down as it nears the end).
				var transitionOffset = (float)Math.Pow(screenTransition.TransitionPosition, 2.0);
				if (screenTransition.State == TransitionState.TransitionOn)
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
		private Vector2 SlideLeftTransition(IScreenTransition screenTransition, Vector2 pos)
		{
			if (screenTransition.TransitionPosition != 0.0f)
			{
				// Make the menu slide into place during transitions, using a
				// power curve to make things look more interesting (this makes
				// the movement slow down as it nears the end).
				var transitionOffset = (float)Math.Pow(screenTransition.TransitionPosition, 2.0);
				if (screenTransition.State == TransitionState.TransitionOn)
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
		private Vector2 SlideRightTransition(IScreenTransition screenTransition, Vector2 pos)
		{
			if (screenTransition.TransitionPosition != 0.0f)
			{
				// Make the menu slide into place during transitions, using a
				// power curve to make things look more interesting (this makes
				// the movement slow down as it nears the end).
				var transitionOffset = (float)Math.Pow(screenTransition.TransitionPosition, 2.0);
				if (screenTransition.State == TransitionState.TransitionOn)
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
		private Vector2 SlideTopTransition(IScreenTransition screenTransition, Vector2 pos)
		{
			if (screenTransition.TransitionPosition != 0.0f)
			{
				// Make the menu slide into place during transitions, using a
				// power curve to make things look more interesting (this makes
				// the movement slow down as it nears the end).
				var transitionOffset = (float)Math.Pow(screenTransition.TransitionPosition, 2.0);
				if (screenTransition.State == TransitionState.TransitionOn)
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
		private Vector2 SlideBottomTransition(IScreenTransition screenTransition, Vector2 pos)
		{
			if (screenTransition.TransitionPosition != 0.0f)
			{
				// Make the menu slide into place during transitions, using a
				// power curve to make things look more interesting (this makes
				// the movement slow down as it nears the end).
				var transitionOffset = (float)Math.Pow(screenTransition.TransitionPosition, 2.0);
				if (screenTransition.State == TransitionState.TransitionOn)
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