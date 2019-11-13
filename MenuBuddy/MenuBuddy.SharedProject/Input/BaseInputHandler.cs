using HadoukInput;
using InputHelper;
using Microsoft.Xna.Framework;
using System;

namespace MenuBuddy
{
	/// <summary>
	/// This is an input helper that uses the hadoukinput to do keyboard/controller input
	/// </summary>
	public abstract class BaseInputHandler : DrawableGameComponent, IInputHandler, IUpdateable, IDisposable
	{
		#region Properties

		/// <summary>
		/// This object controls all the controller and keyboard stuff.
		/// </summary>
		public IInputState InputState { get; private set; }

		public event EventHandler<ClickEventArgs> OnClickHandled;

		#endregion //Properties

		#region Methods

		protected BaseInputHandler(Game game)
			: base(game)
		{
			InputState = new InputState();
		}

		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);

			InputState.Update();
		}

		public virtual void HandleInput(IScreen screen)
		{
			//check if this is the game screen
			var game = screen as IGameScreen;
			if (null != game)
			{
				game.HandleInput(InputState);
			}

			//check if is a menu screen
			var menu = screen as IMenuScreen;
			if (null != menu)
			{
				menu.CheckInput(InputState);
			}
		}

		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
			OnClickHandled = null;
		}

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