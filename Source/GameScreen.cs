using System;
using FontBuddyLib;
using HadoukInput;
using Microsoft.Xna.Framework;
using ResolutionBuddy;

namespace MenuBuddy
{
	/// <summary>
	/// A screen is a single layer that has update and draw logic, and which
	/// can be combined with other layers to build up a complex menu system.
	/// For instance the main menu, the options menu, the "are you sure you
	/// want to quit" message box, and the main game itself are all implemented
	/// as screens.
	/// </summary>
	public abstract class GameScreen
	{
		#region Member Variables

		private readonly FontBuddy menuFont = new FontBuddy();

		/// <summary>
		/// Checks whether this screen is active and can respond to user input.
		/// </summary>
		private bool m_bOtherScreenHasFocus;

		#endregion //Member Variables

		#region Properties

		/// <summary>
		/// Normally when one screen is brought up over the top of another,
		/// the first screen will transition off to make room for the new
		/// one. This property indicates whether the screen is only a small
		/// popup, in which case screens underneath it do not need to bother
		/// transitioning off.
		/// </summary>
		public bool IsPopup { get; protected set; }

		/// <summary>
		/// Indicates how long the screen takes to
		/// transition on when it is activated.
		/// </summary>
		public TimeSpan TransitionOnTime { get; protected set; }

		/// <summary>
		/// Indicates how long the screen takes to
		/// transition off when it is deactivated.
		/// </summary>
		public TimeSpan TransitionOffTime { get; protected set; }

		/// <summary>
		/// Get the position delta to add to an item during screen transition.  
		/// Ranges from 0.0f (fully active, no transition) to 1.0f (transitioned fully off to nothing).
		/// </summary>
		public float TransitionPosition { get; protected set; }

		/// <summary>
		/// Gets the current alpha of the screen transition.
		/// Ranges from 1.0f (fully active, no transition) to 0.0f (transitioned fully off to nothing).
		/// </summary>
		public float TransitionAlpha
		{
			get { return 1.0f - TransitionPosition; }
		}

		/// <summary>
		/// Gets the current screen transition state.
		/// </summary>
		public EScreenState ScreenState { get; protected set; }

		/// <summary>
		/// There are two possible reasons why a screen might be transitioning
		/// off. It could be temporarily going away to make room for another
		/// screen that is on top of it, or it could be going away for good.
		/// This property indicates whether the screen is exiting for real:
		/// if set, the screen will automatically remove itself as soon as the
		/// transition finishes.
		/// </summary>
		public bool IsExiting { get; protected internal set; }

		public bool IsActive
		{
			get
			{
				return !m_bOtherScreenHasFocus &&
				       (ScreenState == EScreenState.TransitionOn ||
				        ScreenState == EScreenState.Active);
			}
		}

		/// <summary>
		/// Gets the manager that this screen belongs to.
		/// </summary>
		public ScreenManager ScreenManager { get; internal set; }

		/// <summary>
		/// Gets the index of the player who is currently controlling this screen,
		/// or null if it is accepting input from any player. This is used to lock
		/// the game to a specific player profile. The main menu responds to input
		/// from any connected gamepad, but whichever player makes a selection from
		/// this menu is given control over all subsequent screens, so other gamepads
		/// are inactive until the controlling player returns to the main menu.
		/// </summary>
		public PlayerIndex? ControllingPlayer { get; internal set; }

		/// <summary>
		/// get the title safe area
		/// </summary>
		/// <value>The screen rect.</value>
		public Rectangle ScreenRect
		{
			get { return Resolution.TitleSafeArea; }
		}

		#endregion

		#region Initialization

		/// <summary>
		/// Initializes a new instance of the <see cref="MenuBuddy.GameScreen"/> class.
		/// </summary>
		public GameScreen()
		{
			IsPopup = false;
			TransitionOnTime = TimeSpan.Zero;
			TransitionOffTime = TimeSpan.Zero;
			IsExiting = false;
			ScreenState = EScreenState.TransitionOn;
			TransitionPosition = 1.0f;
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
			m_bOtherScreenHasFocus = otherScreenHasFocus;

			if (IsExiting)
			{
				// If the screen is going away to die, it should transition off.
				ScreenState = EScreenState.TransitionOff;

				if (!UpdateTransition(gameTime, TransitionOffTime, 1))
				{
					// When the transition finishes, remove the screen.
					ScreenManager.RemoveScreen(this);
				}
			}
			else if (coveredByOtherScreen)
			{
				// If the screen is covered by another, it should transition off.
				if (UpdateTransition(gameTime, TransitionOffTime, 1))
				{
					// Still busy transitioning.
					ScreenState = EScreenState.TransitionOff;
				}
				else
				{
					// Transition finished!
					ScreenState = EScreenState.Hidden;
				}
			}
			else
			{
				// Otherwise the screen should transition on and become active.
				if (UpdateTransition(gameTime, TransitionOnTime, -1))
				{
					// Still busy transitioning.
					ScreenState = EScreenState.TransitionOn;
				}
				else
				{
					// Transition finished!
					ScreenState = EScreenState.Active;
				}
			}
		}

		/// <summary>
		/// Helper for updating the screen transition position.
		/// </summary>
		private bool UpdateTransition(GameTime gameTime, TimeSpan time, int direction)
		{
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
			if (((direction < 0.0f) && (TransitionPosition <= 0.0f)) ||
			    ((direction > 0.0f) && (TransitionPosition >= 1.0f)))
			{
				TransitionPosition = MathHelper.Clamp(TransitionPosition, 0.0f, 1.0f);
				return false;
			}

			// Otherwise we are still busy transitioning.
			return true;
		}

		/// <summary>
		/// Allows the screen to handle user input. Unlike Update, this method
		/// is only called when the screen is active, and not when some other
		/// screen has taken the focus.
		/// </summary>
		public virtual void HandleInput(InputState input, GameTime rGameTime)
		{
		}

		/// <summary>
		/// This is called when the screen should draw itself.
		/// </summary>
		public virtual void Draw(GameTime gameTime)
		{
		}

		/// <summary>
		/// Draws the menu title.
		/// </summary>
		/// <param name="strTitle">The title of this menu</param>
		/// <param name="fScale">The scale to draw the menu title at</param>
		public void DrawMenuTitle(string strTitle, float fScale)
		{
			//dont do anything here if the menu title is empty
			if (string.IsNullOrEmpty(strTitle))
			{
				return;
			}

			// Draw the menu title.

			//Get the menu size and location
			var titlePosition = new Vector2(ScreenRect.Center.X, ScreenRect.Center.Y);
			var transitionOffset = (float)Math.Pow(TransitionPosition, 2);
			titlePosition.Y = ScreenRect.Center.Y - (ScreenManager.TitleFont.MeasureString(strTitle) * fScale).Y;
			titlePosition.Y -= transitionOffset * 100;

			//get the menu title color
			Color titleColor = FadeAlphaDuringTransition(Color.White);

			//create the font buddy object
			menuFont.Font = ScreenManager.TitleFont;

			//draw the menu title!
			menuFont.Write(strTitle, titlePosition, Justify.Center, fScale, titleColor, ScreenManager.SpriteBatch, 0.0f);
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
			if (TransitionOffTime == TimeSpan.Zero)
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

		/// <summary>
		/// Helper modifies a color to fade its alpha value during screen transitions.
		/// </summary>
		public Color FadeAlphaDuringTransition(Color color)
		{
			return new Color(color.R, color.G, color.B, TransitionAlpha);
		}

		/// <summary>
		/// This gets called when the input timer needs to be reset.
		/// Used by menu screens to pop up attract mode
		/// </summary>
		public virtual void ResetInputTimer()
		{
			//dont do anything in this screen, only in menu screen
		}

		#endregion
	}
}