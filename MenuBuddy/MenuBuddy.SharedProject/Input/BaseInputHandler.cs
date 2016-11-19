using HadoukInput;
using Microsoft.Xna.Framework;

namespace MenuBuddy
{
	/// <summary>
	/// This is an input helper that uses the hadoukinput to do keyboard/controller input
	/// </summary>
	public abstract class BaseInputHandler : DrawableGameComponent, IInputHandler
	{
		#region Properties

		/// <summary>
		/// This object controls all the controller and keyboard stuff.
		/// </summary>
		public InputState InputState { get; private set; }

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

		public abstract void HandleInput(IScreen screen);

		#endregion //Methods
	}
}