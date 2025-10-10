using MenuBuddy;
using Microsoft.Xna.Framework;
using ResolutionBuddy;
using System.Threading.Tasks;

namespace MenuBuddySample
{
	public class DropdownTest : MenuScreen
	{
		public DropdownTest() : base("Dropdown Test")
		{
			CoverOtherScreens = true;
			CoveredByOtherScreens = true;
		}

		public override async Task LoadContent()
		{
			await base.LoadContent();

			AddCancelButton();

			//create the dropdown widget
			var drop = new Dropdown<string>(this);
			drop.Vertical = VerticalAlignment.Center;
			drop.Horizontal = HorizontalAlignment.Center;
			drop.Size = new Vector2(350, 128);
			drop.Position = Resolution.ScreenArea.Center;

			string[] words = { "cat", "pants", "buttnuts", "cat1", "pants1", "whoa", "test1", "test2" };
			foreach (var word in words)
			{
				var dropitem = new DropdownItem<string>(word, drop)
				{
					Vertical = VerticalAlignment.Center,
					Horizontal = HorizontalAlignment.Center,
					Size = new Vector2(350, 64)
				};

				var label = new Label(word, Content, FontSize.Small)
				{
					Vertical = VerticalAlignment.Center,
					Horizontal = HorizontalAlignment.Center
				};

				dropitem.AddItem(label);
				drop.AddDropdownItem(dropitem);
			}

			drop.SelectedItem = "buttnuts";

			AddItem(drop);
			AddMenuItem(drop);
		}
	}
}
