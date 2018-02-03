using Microsoft.Xna.Framework;
using System;

namespace MenuBuddy
{
	/// <summary>
	/// This is a transition object that wipes from one side of the screen to another
	/// </summary>
	public class WipeTransitionObject : ITransitionObject
	{
		#region Properties

		/// <summary>
		/// The type of wipe to use for this transition
		/// </summary>
		public TransitionWipeType WipeType { get; set; }

		#endregion //Properties

		#region Methods

		public WipeTransitionObject(TransitionWipeType wipe)
		{
			WipeType = wipe;
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
			switch (WipeType)
			{
				case TransitionWipeType.PopLeft: { return PopLeftTransition(screen, pos); }
				case TransitionWipeType.PopRight: { return PopRightTransition(screen, pos); }
				case TransitionWipeType.PopTop: { return PopTopTransition(screen, pos); }
				case TransitionWipeType.PopBottom: { return PopBottomTransition(screen, pos); }
				case TransitionWipeType.SlideLeft: { return SlideLeftTransition(screen, pos); }
				case TransitionWipeType.SlideRight: { return SlideRightTransition(screen, pos); }
				case TransitionWipeType.SlideTop: { return SlideTopTransition(screen, pos); }
				case TransitionWipeType.SlideBottom: { return SlideBottomTransition(screen, pos); }
				default:
					{
						//None transition type
						return pos;
					}
			}
		}

		private Vector2 PopLeftTransition(IScreenTransition screen, float x, float y)
		{
			return PopLeftTransition(screen, new Vector2(x, y));
		}

		private Vector2 PopRightTransition(IScreenTransition screen, float x, float y)
		{
			return PopRightTransition(screen, new Vector2(x, y));
		}

		private Vector2 PopTopTransition(IScreenTransition screen, float x, float y)
		{
			return PopTopTransition(screen, new Vector2(x, y));
		}

		private Vector2 PopBottomTransition(IScreenTransition screen, float x, float y)
		{
			return PopBottomTransition(screen, new Vector2(x, y));
		}

		/// <summary>
		/// Slide in from the left, slide out the right
		/// </summary>
		/// <param name="pos"></param>
		/// <returns></returns>
		private Vector2 PopLeftTransition(IScreenTransition screen, Vector2 pos)
		{
			if (screen.TransitionPosition != 0.0f)
			{
				// Make the menu slide into place during transitions, using a
				// power curve to make things look more interesting (this makes
				// the movement slow down as it nears the end).
				var transitionOffset = (float)Math.Pow(screen.TransitionPosition, 2.0);
				if (screen.State == TransitionState.TransitionOn)
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
		private Vector2 PopRightTransition(IScreenTransition screen, Vector2 pos)
		{
			if (screen.TransitionPosition != 0.0f)
			{
				// Make the menu slide into place during transitions, using a
				// power curve to make things look more interesting (this makes
				// the movement slow down as it nears the end).
				var transitionOffset = (float)Math.Pow(screen.TransitionPosition, 2.0);
				if (screen.State == TransitionState.TransitionOn)
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
		private Vector2 PopTopTransition(IScreenTransition screen, Vector2 pos)
		{
			if (screen.TransitionPosition != 0.0f)
			{
				// Make the menu slide into place during transitions, using a
				// power curve to make things look more interesting (this makes
				// the movement slow down as it nears the end).
				var transitionOffset = (float)Math.Pow(screen.TransitionPosition, 2.0);
				if (screen.State == TransitionState.TransitionOn)
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
		private Vector2 PopBottomTransition(IScreenTransition screen, Vector2 pos)
		{
			if (screen.TransitionPosition != 0.0f)
			{
				// Make the menu slide into place during transitions, using a
				// power curve to make things look more interesting (this makes
				// the movement slow down as it nears the end).
				var transitionOffset = (float)Math.Pow(screen.TransitionPosition, 2.0);
				if (screen.State == TransitionState.TransitionOn)
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
		private Vector2 SlideLeftTransition(IScreenTransition screen, Vector2 pos)
		{
			if (screen.TransitionPosition != 0.0f)
			{
				// Make the menu slide into place during transitions, using a
				// power curve to make things look more interesting (this makes
				// the movement slow down as it nears the end).
				var transitionOffset = (float)Math.Pow(screen.TransitionPosition, 2.0);
				if (screen.State == TransitionState.TransitionOn)
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
		private Vector2 SlideRightTransition(IScreenTransition screen, Vector2 pos)
		{
			if (screen.TransitionPosition != 0.0f)
			{
				// Make the menu slide into place during transitions, using a
				// power curve to make things look more interesting (this makes
				// the movement slow down as it nears the end).
				var transitionOffset = (float)Math.Pow(screen.TransitionPosition, 2.0);
				if (screen.State == TransitionState.TransitionOn)
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
		private Vector2 SlideTopTransition(IScreenTransition screen, Vector2 pos)
		{
			if (screen.TransitionPosition != 0.0f)
			{
				// Make the menu slide into place during transitions, using a
				// power curve to make things look more interesting (this makes
				// the movement slow down as it nears the end).
				var transitionOffset = (float)Math.Pow(screen.TransitionPosition, 2.0);
				if (screen.State == TransitionState.TransitionOn)
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
		private Vector2 SlideBottomTransition(IScreenTransition screen, Vector2 pos)
		{
			if (screen.TransitionPosition != 0.0f)
			{
				// Make the menu slide into place during transitions, using a
				// power curve to make things look more interesting (this makes
				// the movement slow down as it nears the end).
				var transitionOffset = (float)Math.Pow(screen.TransitionPosition, 2.0);
				if (screen.State == TransitionState.TransitionOn)
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