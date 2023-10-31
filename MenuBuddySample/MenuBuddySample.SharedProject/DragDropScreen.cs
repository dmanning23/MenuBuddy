using MenuBuddy;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MenuBuddySample
{
	public class DragDropScreen : WidgetScreen
	{
		public DragDropScreen() : base("DragDrop test")
		{
			CoverOtherScreens = true;
			CoveredByOtherScreens = true;
		}

		public override async Task LoadContent()
		{
			await base.LoadContent();

			var image = new Image(Content.Load<Texture2D>("Potion3a"))
			{
				Horizontal = HorizontalAlignment.Center,
				Vertical = VerticalAlignment.Center
			};

			var button = new DragDropButton()
			{
				Position = new Point(200, 100),
				Size = image.Rect.Size.ToVector2(),
				Horizontal = HorizontalAlignment.Center,
				Vertical  = VerticalAlignment.Center
			};
			button.AddItem(image);

			AddItem(button);

			image = new Image(Content.Load<Texture2D>("hourglass"))
			{
				Horizontal = HorizontalAlignment.Center,
				Vertical = VerticalAlignment.Center
			};

			button = new DragDropButton()
			{
				Position = new Point(400, 100),
				Size = image.Rect.Size.ToVector2(),
				Horizontal = HorizontalAlignment.Center,
				Vertical = VerticalAlignment.Center
			};
			button.AddItem(image);

			AddItem(button);

			AddCancelButton();
		}
	}
}
