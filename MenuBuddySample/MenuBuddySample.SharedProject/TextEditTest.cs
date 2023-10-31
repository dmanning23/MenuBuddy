using MenuBuddy;
using Microsoft.Xna.Framework;
using ResolutionBuddy;
using System.Threading.Tasks;

namespace MenuBuddySample
{
	public class TextEditTest : WidgetScreen
	{
		public TextEditTest() : base("TextEditTest")
		{
			CoverOtherScreens = true;
			CoveredByOtherScreens = true;
		}

		public override async Task LoadContent()
		{
			await base.LoadContent();

			AddCancelButton();

			//create the dropdown widget
			var drop = new TextEdit("", Content);
			drop.Vertical = VerticalAlignment.Center;
			drop.Horizontal = HorizontalAlignment.Center;
			drop.Size = new Vector2(350, 128);
			drop.Position = Resolution.ScreenArea.Center;
			drop.HasOutline = true;

			AddItem(drop);
		}
	}
}
