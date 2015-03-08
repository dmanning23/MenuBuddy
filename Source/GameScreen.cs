using FontBuddyLib;
using HadoukInput;
using Microsoft.Xna.Framework;
using ResolutionBuddy;
using System;
using GameTimer;

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
		#region Properties

		/// <summary>
		/// Checks whether this screen is active and can respond to user input.
		/// </summary>
		protected bool OtherScreenHasFocus { get; private set; }

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

		/// <summary>
		/// y value to offset the menu title
		/// </summary>
		public float MenuTitleOffset { get; set; }

		/// <summary>
		/// Gets or sets the name of this screen
		/// </summary>
		public string ScreenName { get; set; }

		/// <summary>
		/// Event that gets called when the screen is exiting
		/// </summary>
		public event EventHandler Exiting;

		protected FontBuddy MenuTitleFont { get; set; }

		public GameClock Time { get; private set; }

		#endregion

		#region Initialization

		/// <summary>
		/// Initializes a new instance of the <see cref="MenuBuddy.GameScreen"/> class.
		/// </summary>
		protected GameScreen(string strMenuTitle = "")
		{
			IsPopup = false;
			TransitionOnTime = TimeSpan.Zero;
			TransitionOffTime = TimeSpan.Zero;
			IsExiting = false;
			ScreenState = EScreenState.TransitionOn;
			TransitionPosition = 1.0f;
			ScreenName = strMenuTitle;

			Time = new GameClock();
			Time.Start();
		}

		/// <summary>
		/// Load graphics content for the screen.
		/// </summary>
		public virtual void LoadContent()
		{
			//create the font buddy object
			MenuTitleFont = new FontBuddy();
			MenuTitleFont.Font = ScreenManager.TitleFont;
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
		/// <param name="gameTime"></param>
		public virtual void DrawMenuTitle(string strTitle, float fScale, GameTime gameTime)
		{
			//dont do anything here if the menu title is empty
			if (string.IsNullOrEmpty(strTitle))
			{
				return;
			}

			// Draw the menu title.

			//Get the menu size and location
			var titlePosition = new Vector2(
				Resolution.TitleSafeArea.Center.X,
				Resolution.TitleSafeArea.Center.Y);

			var transitionOffset = (float)Math.Pow(TransitionPosition, 2);
			titlePosition.Y = MenuTitleOffset + 
				(Resolution.TitleSafeArea.Center.Y - (ScreenManager.TitleFont.MeasureString(strTitle) * fScale).Y);
			titlePosition.Y -= transitionOffset * 100;

			//get the menu title color
			Color titleColor = FadeAlphaDuringTransition(Color.White);

			//draw the menu title!
			MenuTitleFont.Write(strTitle, titlePosition, Justify.Center, fScale, titleColor, ScreenManager.SpriteBatch, Time);
		}

		/// <summary>
		/// Fade the background behind this screen
		/// </summary>
		protected void FadeBackground()
		{
			//gray out the screens under this one
			ScreenManager.FadeBackBufferToBlack(TransitionAlpha * 2.0f / 3.0f);
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
#if XNA
			return new Color(color.R, color.G, color.B, TransitionAlpha);
#else
			return new Color(color, TransitionAlpha);
#endif
		}

		/// <summary>
		/// This gets called when the input timer needs to be reset.
		/// Used by menu screens to pop up attract mode
		/// </summary>
		public virtual void ResetInputTimer()
		{
			//dont do anything in this screen, only in menu screen
		}

		public override string ToString()
		{
			return ScreenName;
		}

		#endregion

		#region Transition Positions

		public Vector2 EntryPosition(Vector2 pos, MenuEntry entry)
		{
			switch (entry.TransitionType)
			{
				case MenuTransition.Left: { return LeftTransition(pos); }
				case MenuTransition.Right: { return RightTransition(pos); }
				case MenuTransition.SlideLeft: { return SlideLeftTransition(pos); }
				case MenuTransition.SlideRight: { return SlideRightTransition(pos); }
				case MenuTransition.Top: { return TopTransition(pos); }
				default: { return BottomTransition(pos); }
			}
		}

		public Vector2 LeftTransition(float x, float y)
		{
			return LeftTransition(new Vector2(x, y));
		}

		public Vector2 RightTransition(float x, float y)
		{
			return RightTransition(new Vector2(x, y));
		}

		public Vector2 TopTransition(float x, float y)
		{
			return TopTransition(new Vector2(x, y));
		}

		public Vector2 BottomTransition(float x, float y)
		{
			return BottomTransition(new Vector2(x, y));
		}

		/// <summary>
		/// Slide in from the left, slide out the right
		/// </summary>
		/// <param name="pos"></param>
		/// <returns></returns>
		public Vector2 LeftTransition(Vector2 pos)
		{
			if (TransitionPosition != 0.0f)
			{
				// Make the menu slide into place during transitions, using a
				// power curve to make things look more interesting (this makes
				// the movement slow down as it nears the end).
				var transitionOffset = (float)Math.Pow(TransitionPosition, 2.0);
				if (ScreenState == EScreenState.TransitionOn)
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
		public Vector2 SlideLeftTransition(Vector2 pos)
		{
			if (TransitionPosition != 0.0f)
			{
				// Make the menu slide into place during transitions, using a
				// power curve to make things look more interesting (this makes
				// the movement slow down as it nears the end).
				var transitionOffset = (float)Math.Pow(TransitionPosition, 2.0);
				if (ScreenState == EScreenState.TransitionOn)
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
		public Vector2 RightTransition(Vector2 pos)
		{
			if (TransitionPosition != 0.0f)
			{
				// Make the menu slide into place during transitions, using a
				// power curve to make things look more interesting (this makes
				// the movement slow down as it nears the end).
				var transitionOffset = (float)Math.Pow(TransitionPosition, 2.0);
				if (ScreenState == EScreenState.TransitionOn)
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
		public Vector2 SlideRightTransition(Vector2 pos)
		{
			if (TransitionPosition != 0.0f)
			{
				// Make the menu slide into place during transitions, using a
				// power curve to make things look more interesting (this makes
				// the movement slow down as it nears the end).
				var transitionOffset = (float)Math.Pow(TransitionPosition, 2.0);
				if (ScreenState == EScreenState.TransitionOn)
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
		public Vector2 TopTransition(Vector2 pos)
		{
			if (TransitionPosition != 0.0f)
			{
				// Make the menu slide into place during transitions, using a
				// power curve to make things look more interesting (this makes
				// the movement slow down as it nears the end).
				var transitionOffset = (float)Math.Pow(TransitionPosition, 2.0);
				if (ScreenState == EScreenState.TransitionOn)
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
		public Vector2 BottomTransition(Vector2 pos)
		{
			if (TransitionPosition != 0.0f)
			{
				// Make the menu slide into place during transitions, using a
				// power curve to make things look more interesting (this makes
				// the movement slow down as it nears the end).
				var transitionOffset = (float)Math.Pow(TransitionPosition, 2.0);
				if (ScreenState == EScreenState.TransitionOn)
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

		#endregion //Transition Positions
	}
}