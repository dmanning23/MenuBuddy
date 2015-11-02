using Microsoft.Xna.Framework;
using System;
using Vector2Extensions;

namespace MenuBuddy
{
	/// <summary>
	/// This is a class to help transition items on/off the screen
	/// </summary>
	public class Transition
	{
		#region Properties

		/// <summary>
		/// Indicates how long the screen takes to
		/// transition on when it is activated.
		/// </summary>
		public TimeSpan OnTime { get; set; }

		/// <summary>
		/// Indicates how long the screen takes to
		/// transition off when it is deactivated.
		/// </summary>
		public TimeSpan OffTime { get; set; }

		/// <summary>
		/// Get the position delta to add to an item during screen transition.  
		/// Ranges from 0.0f (fully active, no transition) to 1.0f (transitioned fully off to nothing).
		/// </summary>
		public float TransitionPosition { get; protected set; }

		/// <summary>
		/// Gets the current alpha of the screen transition.
		/// Ranges from 1.0f (fully active, no transition) to 0.0f (transitioned fully off to nothing).
		/// </summary>
		public float Alpha
		{
			get { return 1.0f - TransitionPosition; }
		}

		/// <summary>
		/// The current state of this item.
		/// </summary>
		public TransitionState State { get; set; }

		#endregion //Properties

		#region Methods

		public Transition()
		{
			OnTime = TimeSpan.FromSeconds(0.75);
			OffTime = TimeSpan.FromSeconds(0.5);
			TransitionPosition = 1.0f;
		}

		/// <summary>
		/// Helper for updating the screen transition position.
		/// </summary>
		public bool Update(GameTime gameTime, bool transitionOn)
		{
			//are we transitioning on or off?
			var time = transitionOn ? OnTime : OffTime;
			var direction = transitionOn ? -1f : 1f;

			// How much should we move by?
			float transitionDelta = 1.0f;
			if (time != TimeSpan.Zero)
			{
				transitionDelta = (float)(gameTime.ElapsedGameTime.TotalMilliseconds /
										  time.TotalMilliseconds);
			}

			// Update the transition position.
			if (transitionDelta != 0.0f)
			{
				TransitionPosition += transitionDelta * direction;
			}

			// Did we reach the end of the transition?
			if ((transitionOn  && (TransitionPosition <= 0.0f)) ||
				(!transitionOn && (TransitionPosition >= 1.0f)))
			{
				TransitionPosition = MathHelper.Clamp(TransitionPosition, 0.0f, 1.0f);
				return false;
			}

			// Otherwise we are still busy transitioning.
			return true;
		}

		/// <summary>
		/// Helper modifies a color to fade its alpha value during screen transitions.
		/// </summary>
		public Color AlphaColor(Color color)
		{
#if XNA
			return new Color(color.R, color.G, color.B, Alpha);
#else
			return new Color(color, Alpha);
#endif
		}

		#endregion //Methods

		#region Transition Positions

		public Point Position(Rectangle rect, TransitionType transition)
		{
			var pos = Position(rect.Location.ToVector2(), transition);
			return pos.ToPoint();
		}

		public virtual Vector2 Position(Point pos, TransitionType transition)
		{
			return Position(pos.ToVector2(), transition);
		}

		public virtual Vector2 Position(Vector2 pos, TransitionType transition)
		{
			switch (transition)
			{
				case TransitionType.PopLeft: { return PopLeftTransition(pos); }
				case TransitionType.PopRight: { return PopRightTransition(pos); }
				case TransitionType.PopTop: { return PopTopTransition(pos); }
				case TransitionType.PopBottom: { return PopBottomTransition(pos); }
				case TransitionType.SlideLeft: { return SlideLeftTransition(pos); }
				case TransitionType.SlideRight: { return SlideRightTransition(pos); }
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
			if (TransitionPosition != 0.0f)
			{
				// Make the menu slide into place during transitions, using a
				// power curve to make things look more interesting (this makes
				// the movement slow down as it nears the end).
				var transitionOffset = (float)Math.Pow(TransitionPosition, 2.0);
				if (State == TransitionState.TransitionOn)
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
			if (TransitionPosition != 0.0f)
			{
				// Make the menu slide into place during transitions, using a
				// power curve to make things look more interesting (this makes
				// the movement slow down as it nears the end).
				var transitionOffset = (float)Math.Pow(TransitionPosition, 2.0);
				if (State == TransitionState.TransitionOn)
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
			if (TransitionPosition != 0.0f)
			{
				// Make the menu slide into place during transitions, using a
				// power curve to make things look more interesting (this makes
				// the movement slow down as it nears the end).
				var transitionOffset = (float)Math.Pow(TransitionPosition, 2.0);
				if (State == TransitionState.TransitionOn)
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
			if (TransitionPosition != 0.0f)
			{
				// Make the menu slide into place during transitions, using a
				// power curve to make things look more interesting (this makes
				// the movement slow down as it nears the end).
				var transitionOffset = (float)Math.Pow(TransitionPosition, 2.0);
				if (State == TransitionState.TransitionOn)
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
			if (TransitionPosition != 0.0f)
			{
				// Make the menu slide into place during transitions, using a
				// power curve to make things look more interesting (this makes
				// the movement slow down as it nears the end).
				var transitionOffset = (float)Math.Pow(TransitionPosition, 2.0);
				if (State == TransitionState.TransitionOn)
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
			if (TransitionPosition != 0.0f)
			{
				// Make the menu slide into place during transitions, using a
				// power curve to make things look more interesting (this makes
				// the movement slow down as it nears the end).
				var transitionOffset = (float)Math.Pow(TransitionPosition, 2.0);
				if (State == TransitionState.TransitionOn)
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

		#endregion //Transition Positions
	}
}