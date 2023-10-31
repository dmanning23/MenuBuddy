using MenuBuddy;
using Microsoft.Xna.Framework;
using ResolutionBuddy;
using System.Threading.Tasks;

namespace MenuBuddySample
{
	public class NumEditTest : WidgetScreen
	{
		public NumEditTest() : base("NumEditTest")
		{
			CoverOtherScreens = true;
			CoveredByOtherScreens = true;
		}

		public override async Task LoadContent()
		{
			await base.LoadContent();

			AddCancelButton();

			//create the dropdown widget
			var drop = new NumEdit(0f, Content);
			drop.Vertical = VerticalAlignment.Center;
			drop.Horizontal = HorizontalAlignment.Right;
			drop.Size = new Vector2(350, 128);
			drop.Position = Resolution.ScreenArea.Center;

			AddItem(drop);
		}
	}
}
