using MouseBuddy;

namespace MenuBuddy
{
	/// <summary>
	/// Base class for games that use mouse input.
	/// Automatically sets up mouse input handling.
	/// </summary>
	public abstract class MouseGame : DefaultGame
	{
		#region Methods

		/// <summary>
		/// Initializes a new instance of the <see cref="MouseGame"/> class.
		/// </summary>
		protected MouseGame() : base(GameType.Mouse)
		{
		}

		/// <summary>
		/// Initializes mouse input handling for this game.
		/// </summary>
		protected override void InitInput()
		{
			InputHelper = new MouseComponent(this, ResolutionBuddy.Resolution.ScreenToGameCoord);

			//add the input helper for menus
			var input = new MouseInputHandler(this);
		}

		#endregion //Methods
	}
}
