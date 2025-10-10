using MenuBuddy;
using Microsoft.Xna.Framework;
using ResolutionBuddy;
using System.Threading.Tasks;

namespace MenuScreenTests
{
	public class TestMenuScreen : MenuScreen
	{
		public override async Task LoadContent()
		{
			await base.LoadContent();
			AddButton(HorizontalAlignment.Left, VerticalAlignment.Top, "One!");
			AddButton(HorizontalAlignment.Left, VerticalAlignment.Bottom, "Two!");
			AddButton(HorizontalAlignment.Right, VerticalAlignment.Top, "Three!");
			AddButton(HorizontalAlignment.Right, VerticalAlignment.Bottom, "Four!");
		}

		private void AddButton(HorizontalAlignment horiz, VerticalAlignment vert, string text)
		{
			//Add some buttons
			var button1 = new RelativeLayoutButton()
			{
				TransitionObject = new WipeTransitionObject(TransitionWipeType.PopLeft),
				Horizontal = horiz,
				Vertical = vert,
				Position = new Point(horiz == HorizontalAlignment.Left ? Resolution.TitleSafeArea.Left : Resolution.TitleSafeArea.Right,
					vert == VerticalAlignment.Top ? Resolution.TitleSafeArea.Top : Resolution.TitleSafeArea.Bottom),
			};
			var label = new Label(text, Content)
			{
				Horizontal = HorizontalAlignment.Center,
				Vertical = VerticalAlignment.Center,
			};
			button1.Size = label.Rect.Size.ToVector2();
			button1.AddItem(label);
			AddMenuItem(button1);
			AddItem(button1);
		}
	}
}
