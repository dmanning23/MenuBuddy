using MenuBuddy;

namespace MenuBuddySample
{
	/// <summary>
	/// This is some boilerplate code for doing a controller based game.
	/// </summary>
	public abstract class TouchGame : DefaultGame
	{
		#region Methods

		protected TouchGame()
		{
		}

		protected override void InitInput()
		{
			//add the input helper for menus
			var input = new TouchInputHelper(this);
			//var input = new ControllerInputHelper(this);
		}

		#endregion //Methods
	}
}
