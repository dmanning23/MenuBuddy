using HadoukInput;
using Microsoft.Xna.Framework;

namespace MenuBuddy
{
	/// <summary>
	/// This is an input helper that uses the hadoukinput to do keyboard/controller input
	/// </summary>
	public abstract class BaseInputHelper : DrawableGameComponent, IInputHelper
	{
		#region Properties

		/// <summary>
		/// This object controls all the controller and keyboard stuff.
		/// </summary>
		public InputState InputState { get; private set; }

		#endregion //Properties

		#region Methods

		protected BaseInputHelper(Game game)
			: base(game)
		{
			InputState = new InputState();

			//Register ourselves to implement the DI container service.
			game.Components.Add(this);
			game.Services.AddService(typeof(IInputHelper), this);
		}

		public virtual void HandleInput(IScreen screen)
		{
			//check if this is the game screen
			var game = screen as IGameScreen;
			if (null != game)
			{
				game.HandleInput(InputState);
			}
		}

		#endregion //Methods
	}
}