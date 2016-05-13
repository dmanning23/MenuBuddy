using TouchScreenBuddy;

namespace MenuBuddy
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
			//create the touch manager component
			var touches = new TouchComponent(this, ResolutionBuddy.Resolution.ScreenToGameCoord);

			//add the input helper for menus
			var input = new TouchInputHelper(this);
		}

		#endregion //Methods
	}
}
