using GameTimer;
using Microsoft.Xna.Framework;
using System;

namespace MenuBuddy
{
	/// <summary>
	/// This is a class to help transition items on/off the screen
	/// </summary>
	public class ScreenTransition : IScreenTransition, IDisposable
	{
		#region Properties

		/// <summary>
		/// Indicates how long the screen takes to
		/// transition on when it is activated.
		/// </summary>
		public float OnTime { get; set; }

		/// <summary>
		/// Indicates how long the screen takes to
		/// transition off when it is deactivated.
		/// </summary>
		public float OffTime { get; set; }

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

		CountdownTimer _transitionTimer;

		public event EventHandler OnStateChange;

		#endregion //Properties

		#region Methods

		public ScreenTransition()
		{
			OnTime = 0.75f;
			OffTime = 0.5f;
			_transitionTimer = new CountdownTimer();
			TransitionPosition = 1.0f;
			State = TransitionState.Hidden;
		}

		/// <summary>
		/// Helper for updating the screen transition position.
		/// </summary>
		/// <param name="gameTime">the current game time</param>
		/// <param name="transitionOn">whether this transtion is moving onto or off of the screen</param>
		/// <returns>True if this transition is still ongoing, false if the transition has finished moving on/off</returns>
		public bool Update(GameClock gameTime, bool transitionOn)
		{
			//Will that transition flag change the state of the thing?
			if (StateChange(transitionOn))
			{
				//are we transitioning on or off?
				var time = transitionOn ? OnTime : OffTime;

				//Start the countdown timer
				_transitionTimer.Start(time);

				//fire off the event handler
				if (OnStateChange != null)
				{
					OnStateChange(this, new EventArgs());
				}
			}

			_transitionTimer.Update(gameTime);

			// Update the transition position.
			if (transitionOn)
			{
				TransitionPosition = _transitionTimer.Lerp;
			}
			else
			{
				TransitionPosition = 1f - _transitionTimer.Lerp;
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
			var alpha = color.A / 255f;
			return new Color(color, alpha * Alpha);
		}

		/// <summary>
		/// Check whether not the state of this transition object should change
		/// </summary>
		/// <param name="transitionOn">whether the object should transition on or off</param>
		/// <returns>True if the transitionOn flag should change the state</returns>
		public bool StateChange(bool transitionOn)
		{
			if (transitionOn)
			{
				//if the current state is hidden or transitioning off and if we want to transition on
				return ((State == TransitionState.Hidden) ||
					(State == TransitionState.TransitionOff));
			}
			else
			{
				//if the current state is active or transitioning on and if we want to transition off
				return ((State == TransitionState.Active) ||
					(State == TransitionState.TransitionOn));
			}
		}

		public void Dispose()
		{
			OnStateChange = null;
		}

		#endregion //Methods
	}
}