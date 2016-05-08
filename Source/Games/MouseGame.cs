using MouseBuddy;

namespace MenuBuddy
{
	/// <summary>
	/// This is some boilerplate code for doing a mousepointer based game.
	/// </summary>
	public abstract class MouseGame : DefaultGame
	{
		#region Methods

		protected MouseGame()
		{
		}

		protected override void InitInput()
		{
			var mouse = new MouseComponent(this);

			//add the input helper for menus
			var input = new TouchInputHelper(this);
		}

		#endregion //Methods
	}
}
