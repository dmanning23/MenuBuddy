using GameTimer;
using Microsoft.Xna.Framework;
using System;

namespace MenuBuddy
{
	/// <summary>
	/// A screen is a single layer that has update and draw logic, and which
	/// can be combined with other layers to build up a complex menu system.
	/// For instance the main menu, the options menu, the "are you sure you
	/// want to quit" message box, and the main game itself are all implemented
	/// as screens.
	/// </summary>
	public abstract class Screen : IScreen
	{
		#region Properties

		/// <summary>
		/// Checks whether this screen is active and can respond to user input.
		/// </summary>
		protected bool OtherScreenHasFocus { get; private set; }

		/// <summary>
		/// Normally when one screen is brought up over the top of another,
		/// the first screen will transition off to make room for the new one.
		/// This property indicates whether screens underneath it need to bother transitioning off.
		/// </summary>
		public bool CoverOtherScreens { get; set; }

		/// <summary>
		/// There are two possible reasons why a screen might be transitioning
		/// off. It could be temporarily going away to make room for another
		/// screen that is on top of it, or it could be going away for good.
		/// This property indicates whether the screen is exiting for real:
		/// if set, the screen will automatically remove itself as soon as the
		/// transition finishes.
		/// </summary>
		public bool IsExiting
		{
			get { return _isExiting; }
			protected internal set
			{
				bool fireEvent = !_isExiting && value;
				_isExiting = value;
				if (fireEvent && (Exiting != null))
				{
					Exiting(this, EventArgs.Empty);
				}
			}
		}

		private bool _isExiting = false;

		public bool IsActive
		{
			get
			{
				return !OtherScreenHasFocus &&
					   (Transition.State == TransitionState.TransitionOn ||
						Transition.State == TransitionState.Active);
			}
		}

		/// <summary>
		/// Gets the manager that this screen belongs to.
		/// </summary>
		public ScreenManager ScreenManager { get; set; }

		/// <summary>
		/// Gets the index of the player who is currently controlling this screen,
		/// or null if it is accepting input from any player. This is used to lock
		/// the game to a specific player profile. The main menu responds to input
		/// from any connected gamepad, but whichever player makes a selection from
		/// this menu is given control over all subsequent screens, so other gamepads
		/// are inactive until the controlling player returns to the main menu.
		/// </summary>
		public PlayerIndex? ControllingPlayer { get; set; }

		/// <summary>
		/// Gets or sets the name of this screen
		/// </summary>
		public string ScreenName { get; set; }

		/// <summary>
		/// Event that gets called when the screen is exiting
		/// </summary>
		public event EventHandler Exiting;

		public GameClock Time { get; private set; }

		/// <summary>
		/// Object used to aid in transitioning on and off
		/// </summary>
		public Transition Transition { get; set; }

		/// <summary>
		/// The style of this screen.
		/// Inherits from the ScreenManager style
		/// </summary>
		public StyleSheet Style { get; private set; }

		#endregion

		#region Initialization

		/// <summary>
		/// Initializes a new instance of the <see cref="MenuBuddy.GameScreen"/> class.
		/// </summary>
		protected Screen(string screenName = "")
		{
			CoverOtherScreens = true;
			IsExiting = false;
			ScreenName = screenName;

			Transition = new Transition();

			Time = new GameClock();
			Time.Start();
		}

		public virtual void SetStyle(StyleSheet styles)
		{
			Style = styles;
		}

		/// <summary>
		/// Load graphics content for the screen.
		/// </summary>
		public virtual void LoadContent()
		{
		}

		/// <summary>
		/// Unload content for the screen.
		/// </summary>
		public virtual void UnloadContent()
		{
		}

		#endregion

		#region Update and Draw

		/// <summary>
		/// Allows the screen to run logic, such as updating the transition position.
		/// Unlike HandleInput, this method is called regardless of whether the screen
		/// is active, hidden, or in the middle of a transition.
		/// </summary>
		public virtual void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
		{
			OtherScreenHasFocus = otherScreenHasFocus;
			Time.Update(gameTime);

			if (IsExiting)
			{
				// If the screen is going away to die, it should transition off.
				Transition.State = TransitionState.TransitionOff;

				if (!Transition.Update(gameTime, false))
				{
					// When the transition finishes, remove the screen.
					ScreenManager.RemoveScreen(this);
				}
			}
			else if (coveredByOtherScreen)
			{
				// If the screen is covered by another, it should transition off.
				if (Transition.Update(gameTime, false))
				{
					// Still busy transitioning.
					Transition.State = TransitionState.TransitionOff;
				}
				else
				{
					// Transition finished!
					Transition.State = TransitionState.Hidden;
				}
			}
			else
			{
				// Otherwise the screen should transition on and become active.
				if (Transition.Update(gameTime, true))
				{
					// Still busy transitioning.
					Transition.State = TransitionState.TransitionOn;
				}
				else
				{
					// Transition finished!
					Transition.State = TransitionState.Active;
				}
			}
		}

		/// <summary>
		/// This is called when the screen should draw itself.
		/// </summary>
		public virtual void Draw(GameTime gameTime)
		{
		}

		/// <summary>
		/// Fade the background behind this screen
		/// </summary>
		protected void FadeBackground()
		{
			//gray out the screens under this one
			ScreenManager.DrawHelper.FadeBackground(Transition.Alpha * 2.0f / 3.0f);
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// Tells the screen to go away. Unlike ScreenManager.RemoveScreen, which
		/// instantly kills the screen, this method respects the transition timings
		/// and will give the screen a chance to gradually transition off.
		/// </summary>
		public void ExitScreen()
		{
			if (Transition.OffTime == TimeSpan.Zero)
			{
				// If the screen has a zero transition time, remove it immediately.
				ScreenManager.RemoveScreen(this);
			}
			else
			{
				// Otherwise flag that it should transition off and then exit.
				IsExiting = true;
			}
		}

		public override string ToString()
		{
			return ScreenName;
		}

		#endregion
	}
}