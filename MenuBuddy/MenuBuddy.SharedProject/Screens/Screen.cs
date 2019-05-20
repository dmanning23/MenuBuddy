using GameTimer;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
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
	public abstract class Screen : IScreen, IDisposable
	{
		#region Properties

		/// <summary>
		/// Checks whether this game is active and can respond to user input.
		/// </summary>
		private bool OtherWindowHasFocus { get; set; }

		/// <summary>
		/// Whether or not this screen is currently covered by another screen
		/// </summary>
		private bool CurrentlyCovered { get; set; }

		/// <summary>
		/// Whether or not screens underneath this one should tranisition off
		/// </summary>
		public virtual bool CoverOtherScreens { get; set; }

		/// <summary>
		/// Whether or not this screen should transition off when covered by other screens
		/// </summary>
		public virtual bool CoveredByOtherScreens { get; set; }

		/// <summary>
		/// There are two possible reasons why a screen might be transitioning off. 
		/// It could be temporarily going away to make room for another
		/// screen that is on top of it, or it could be going away for good.
		/// This property indicates whether the screen is exiting for real:
		/// if set, the screen will automatically remove itself as soon as the
		/// transition finishes.
		/// </summary>
		public virtual bool IsExiting
		{
			get { return _isExiting; }
			protected set
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
				return !OtherWindowHasFocus && //the window is not covered
					(!CoveredByOtherScreens || !CurrentlyCovered) && //the screen is not covered or don't care
					(TransitionState == TransitionState.TransitionOn || TransitionState == TransitionState.Active); //the transition is correct state
			}
		}

		public bool HasFocus
		{
			get
			{
				return !OtherWindowHasFocus && //the window is not covered
					!CurrentlyCovered && //the screen is not covered
					(TransitionState == TransitionState.TransitionOn || TransitionState == TransitionState.Active); //the transition is correct state
			}
		}

		/// <summary>
		/// Gets the manager that this screen belongs to.
		/// </summary>
		public ScreenManager ScreenManager { get; set; }

		private bool OwnsContent { get; set; }

		public ContentManager Content { get; private set; }

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
		public virtual string ScreenName { get; set; }

		/// <summary>
		/// Event that gets called when the screen is exiting
		/// </summary>
		public event EventHandler Exiting;

		public GameClock Time { get; private set; }

		/// <summary>
		/// Object used to aid in transitioning on and off
		/// </summary>
		public IScreenTransition Transition { get; set; }

		public TransitionState TransitionState
		{
			get
			{
				return Transition.State;
			}
		}

		/// <summary>
		/// The style of this screen.
		/// Inherits from the ScreenManager style
		/// </summary>
		public StyleSheet Style { get; private set; }

		/// <summary>
		/// Set the layer of a screen to change it's location in the ScreenStack
		/// </summary>
		public int Layer { get; set; }

		/// <summary>
		/// Used by the ScreenStack to sort screens. Don't touch!
		/// </summary>
		public int SubLayer { get; set; }

		#endregion

		#region Initialization

		/// <summary>
		/// Initializes a new instance of the <see cref="MenuBuddy.GameScreen"/> class.
		/// </summary>
		protected Screen(string screenName = "", ContentManager content = null)
		{
			OwnsContent = content == null;
			Content = content;
			CoverOtherScreens = false;
			CoveredByOtherScreens = false;
			CurrentlyCovered = false;
			OtherWindowHasFocus = false;

			IsExiting = false;
			ScreenName = screenName;

			Transition = new ScreenTransition();

			Time = new GameClock();
			Time.Start();

			Layer = int.MaxValue;
		}

		/// <summary>
		/// Load graphics content for the screen.
		/// </summary>
		public virtual void LoadContent()
		{
			if (null != ScreenManager && null == Content)
			{
				var defaultGame = ScreenManager.Game as DefaultGame;
				Content = new ContentManager(defaultGame.Services, defaultGame.ContentRootDirectory);
			}
		}

		/// <summary>
		/// Unload content for the screen.
		/// </summary>
		public virtual void UnloadContent()
		{
			if (OwnsContent)
			{
				Content?.Unload();
			}
			Content = null;
		}

		#endregion

		#region Update and Draw

		/// <summary>
		/// Allows the screen to run logic, such as updating the transition position.
		/// Unlike HandleInput, this method is called regardless of whether the screen
		/// is active, hidden, or in the middle of a transition.
		/// </summary>
		/// <param name="gameTime"></param>
		/// <param name="otherWindowHasFocus"></param>
		/// <param name="covered"></param>
		public virtual void Update(GameTime gameTime, bool otherWindowHasFocus, bool covered)
		{
			//Grab the parameters that were passed in
			OtherWindowHasFocus = otherWindowHasFocus;
			CurrentlyCovered = covered;

			Time.Update(gameTime);

			var transitionResult = UpdateTransition(Transition, Time);

			if (IsExiting)
			{
				if (!transitionResult)
				{
					// When the transition finishes, remove the screen.
					ScreenManager.RemoveScreen(this);
				}
			}
		}

		public bool UpdateTransition(IScreenTransition screenTransition, GameClock gameTime)
		{
			var transitionResult = false;

			if (IsExiting)
			{
				// If the screen is going away to die, it should transition off.
				transitionResult = screenTransition.Update(gameTime, false);
				screenTransition.State = TransitionState.TransitionOff;
			}
			else if (CurrentlyCovered && CoveredByOtherScreens)
			{
				// If the screen is covered by another, it should transition off.
				transitionResult = screenTransition.Update(gameTime, false);
				if (transitionResult)
				{
					// Still busy transitioning.
					screenTransition.State = TransitionState.TransitionOff;
				}
				else
				{
					// Transition finished!
					screenTransition.State = TransitionState.Hidden;
				}
			}
			else
			{
				// Otherwise the screen should transition on and become active.
				transitionResult = screenTransition.Update(gameTime, true);
				if (transitionResult)
				{
					// Still busy transitioning.
					screenTransition.State = TransitionState.TransitionOn;
				}
				else
				{
					// Transition finished!
					screenTransition.State = TransitionState.Active;
				}
			}

			return transitionResult;
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
			FadeBackground(.66f);
		}

		/// <summary>
		/// Fade the background behind this screen
		/// </summary>
		protected void FadeBackground(float alpha)
		{
			//gray out the screens under this one
			ScreenManager.DrawHelper.FadeBackground(Transition.Alpha * alpha);
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// Tells the screen to go away. Unlike ScreenManager.RemoveScreen, which
		/// instantly kills the screen, this method respects the transition timings
		/// and will give the screen a chance to gradually transition off.
		/// </summary>
		public virtual void ExitScreen()
		{
			//flag that it should transition off and then exit.
			IsExiting = true;
		}

		public override string ToString()
		{
			return ScreenName;
		}

		public virtual void Dispose()
		{
			Exiting = null;

			//just double check that this is getting called
			if (null != Content)
			{
				throw new Exception($"Error: UnloadContent was not called in \"{ScreenName}\"");
			}
		}

		#endregion
	}
}