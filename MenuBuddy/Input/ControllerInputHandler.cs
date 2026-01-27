using Microsoft.Xna.Framework;

namespace MenuBuddy
{
	/// <summary>
	/// Input handler for gamepad/controller input. Registers itself as a game service.
	/// </summary>
	public class ControllerInputHandler : BaseInputHandler
	{
		#region Methods

		/// <summary>
		/// Initializes a new instance of the <see cref="ControllerInputHandler"/> class
		/// and registers it as the <see cref="IInputHandler"/> service.
		/// </summary>
		/// <param name="game">The game instance.</param>
		public ControllerInputHandler(Game game)
			: base(game)
		{
			//Register ourselves to implement the DI container service.
			game.Components.Add(this);
			game.Services.AddService(typeof(IInputHandler), this);
		}

		#endregion //Methods
	}
}