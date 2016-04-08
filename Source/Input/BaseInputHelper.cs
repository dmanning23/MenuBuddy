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