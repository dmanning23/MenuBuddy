namespace MenuBuddy
{
	/// <summary>
	/// This is some boilerplate code for doing a controller based game.
	/// </summary>
	public abstract class ControllerGame : DefaultGame
	{
		#region Methods

		protected ControllerGame() : base(GameType.Controller)
		{
		}

		protected override void InitInput()
		{
			//add the input helper for menus
			InputHelper = new ControllerInputHelper();

			var input = new ControllerInputHandler(this);
		}

		#endregion //Methods
	}
}
