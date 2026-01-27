using Microsoft.Xna.Framework.Content;

namespace MenuBuddy
{
	/// <summary>
	/// THis is a menu entry that pops up from the bottom of a window and says "Continue"
	/// </summary>
	public class ContinueMenuEntry : MenuEntry
	{
		public ContinueMenuEntry(ContentManager content) : base("Continue", content)
		{
			TransitionObject = new WipeTransitionObject(TransitionWipeType.PopBottom);
		}
	}
}