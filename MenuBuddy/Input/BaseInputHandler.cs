using HadoukInput;
using InputHelper;
using Microsoft.Xna.Framework;
using System;

namespace MenuBuddy
{
	/// <summary>
	/// Abstract base class for input handlers that process keyboard and controller input using HadoukInput.
	/// </summary>
	public abstract class BaseInputHandler : DrawableGameComponent, IInputHandler, IUpdateable, IDisposable
	{
		#region Properties

		/// <summary>
		/// Gets the input state containing current keyboard and controller information.
		/// </summary>
		public IInputState InputState { get; private set; }

		/// <summary>
		/// Event raised when a click has been handled by a screen or widget.
		/// </summary>
		public event EventHandler<ClickEventArgs> OnClickHandled;

		#endregion //Properties

		#region Methods

		/// <summary>
		/// Initializes a new instance of the <see cref="BaseInputHandler"/> class.
		/// </summary>
		/// <param name="game">The game instance.</param>
		protected BaseInputHandler(Game game)
			: base(game)
		{
			InputState = new InputState();
		}

		/// <summary>
		/// Updates the input state each frame.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);

			InputState.Update();
		}

		/// <summary>
		/// Handles input for the specified screen. If the screen implements <see cref="IGameScreen"/>,
		/// passes the input state to it for processing.
		/// </summary>
		/// <param name="screen">The screen to receive input.</param>
		public virtual void HandleInput(IScreen screen)
		{
			//check if this is the game screen
			var game = screen as IGameScreen;
			if (null != game)
			{
				game.HandleInput(InputState);
			}
		}

		/// <summary>
		/// Releases resources used by this input handler.
		/// </summary>
		/// <param name="disposing">True if disposing managed resources.</param>
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
			OnClickHandled = null;
		}

		/// <summary>
		/// Raises the <see cref="OnClickHandled"/> event.
		/// </summary>
		/// <param name="obj">The source of the event.</param>
		/// <param name="e">The click event arguments.</param>
		public void ClickHandled(object obj, ClickEventArgs e)
		{
			if (null != OnClickHandled)
			{
				OnClickHandled(obj, e);
			};
		}

		#endregion //Methods
	}
}