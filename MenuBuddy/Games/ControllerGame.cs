namespace MenuBuddy
{
	/// <summary>
	/// Base class for games that use gamepad/controller input.
	/// Automatically sets up controller input handling.
	/// </summary>
	public abstract class ControllerGame : DefaultGame
	{
		#region Methods

		/// <summary>
		/// Initializes a new instance of the <see cref="ControllerGame"/> class.
		/// </summary>
		protected ControllerGame() : base(GameType.Controller)
		{
		}

		/// <summary>
		/// Initializes controller input handling for this game.
		/// </summary>
		protected override void InitInput()
		{
			//add the input helper for menus
			InputHelper = new ControllerInputHelper(this);

			var input = new ControllerInputHandler(this);
		}

		#endregion //Methods
	}
}
