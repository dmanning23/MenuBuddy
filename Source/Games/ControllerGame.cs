using MenuBuddy;

namespace MenuBuddy
{
	/// <summary>
	/// This is some boilerplate code for doing a controller based game.
	/// </summary>
	public abstract class ControllerGame : DefaultGame
	{
		#region Methods

		protected ControllerGame()
		{
		}

		/// <summary>
		/// Initialize the default styles to use for this game.
		/// </summary>
		protected override void InitStyles()
		{
			DefaultStyles.Init(this);
			var style = DefaultStyles.Instance();
		}

		protected override void InitInput()
		{
			//add the input helper for menus
			//var input = new TouchInputHelper(this);
			var input = new ControllerInputHelper(this);
		}

		#endregion //Methods
	}
}
