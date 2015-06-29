using MenuBuddy;
using MouseBuddy;
using ResolutionBuddy;
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

		/// <summary>
		/// Initialize the default styles to use for this game.
		/// </summary>
		protected override void InitStyles()
		{
			//uncomment this line if need to test widget placement
			//gameStyle.HasOutline = true;

			TouchStyles.Init(this);
			var style = DefaultStyles.Instance();
		}

		protected override void InitInput()
		{
			//create the touch manager component
			var touches = new TouchManager(this, Resolution.ScreenToGameCoord);

#if MOUSE
			var mouse = new MouseManager(this);
#endif

			//add the input helper for menus
			var input = new TouchInputHelper(this);
			//var input = new ControllerInputHelper(this);
		}

		#endregion //Methods
	}
}
