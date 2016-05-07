using Microsoft.Xna.Framework;
using System;

namespace MenuBuddy
{
	/// <summary>
	/// This is a class to help transition items on/off the screen
	/// </summary>
	public class ScreenTransition
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

		public ScreenTransition()
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
			if ((transitionOn && (TransitionPosition <= 0.0f)) ||
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
	}
}