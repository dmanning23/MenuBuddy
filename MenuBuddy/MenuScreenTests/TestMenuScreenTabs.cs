using MenuBuddy;
using Microsoft.Xna.Framework;
using ResolutionBuddy;
using System.Threading.Tasks;

namespace MenuScreenTests
{
	public class TestMenuScreenTabs : MenuScreen
	{
		public override async Task LoadContent()
		{
			await base.LoadContent();
			AddButton(HorizontalAlignment.Left, VerticalAlignment.Top, "One!", 1);
			AddButton(HorizontalAlignment.Right, VerticalAlignment.Top, "Three!", 3);
			AddButton(HorizontalAlignment.Right, VerticalAlignment.Bottom, "Four!", 3);
			AddButton(HorizontalAlignment.Left, VerticalAlignment.Bottom, "Two!", 2);
		}

		private void AddButton(HorizontalAlignment horiz, VerticalAlignment vert, string text, int tabOrder)
		{
			//Add some buttons
			var button1 = new RelativeLayoutButton()
			{
				Name = text,
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
			AddMenuItem(button1, tabOrder);
			AddItem(button1);
		}
	}
}
