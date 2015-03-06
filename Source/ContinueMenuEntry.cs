using MenuBuddy;

namespace MenuBuddy
{
	/// <summary>
	/// THis is a menu entry that pops up from the bottom of a window and says "Continue"
	/// </summary>
	public class ContinueMenuEntry : MenuEntry
	{
		public ContinueMenuEntry() : base("Continue")
		{
			TransitionType = MenuTransition.Bottom;
		}
	}
}