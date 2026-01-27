using MenuBuddy;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using ResolutionBuddy;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MenuBuddySample
{
	public class TextEditMessageBoxTest : WidgetScreen
	{
		public TextEditMessageBoxTest(ContentManager content = null) : base("TextEditMessageBoxTest", content)
		{
			CoverOtherScreens = true;
			CoveredByOtherScreens = false;
		}

		public override async Task LoadContent()
		{
			await base.LoadContent();

			AddCancelButton();

			//create the dropdown widget
			var drop = new TextEditWithDialog("", Content)
			{
				Vertical = VerticalAlignment.Center,
				Horizontal = HorizontalAlignment.Center,
				Size = new Vector2(350, 128),
				Position = new Point(256, Resolution.ScreenArea.Center.Y - 72),
				HasOutline = true,
				MessageBoxTitle = "Text Edit Message Box Test",
				MessageBoxDescription = "Enter some text:",
				IsPassword = false,
			};
			AddItem(drop);

			var drop2 = new TextEditWithDialog("", Content)
			{
				Vertical = VerticalAlignment.Center,
				Horizontal = HorizontalAlignment.Center,
				Size = new Vector2(350, 128),
				Position = new Point(256, Resolution.ScreenArea.Center.Y + 72),
				HasOutline = true,
				MessageBoxTitle = "Password Test",
				MessageBoxDescription = "Enter text to render as a password:",
				IsPassword = true,
			};
			AddItem(drop2);
		}
	}
}
