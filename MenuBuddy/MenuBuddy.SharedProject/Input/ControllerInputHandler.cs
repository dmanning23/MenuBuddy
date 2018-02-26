using Microsoft.Xna.Framework;

namespace MenuBuddy
{
	/// <summary>
	/// This is an input helper that uses the hadoukinput to do keyboard/controller input
	/// </summary>
	public class ControllerInputHandler : BaseInputHandler
	{
		#region Methods

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