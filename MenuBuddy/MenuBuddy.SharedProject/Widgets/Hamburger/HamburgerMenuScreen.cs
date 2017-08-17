using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ResolutionBuddy;
using System;
using System.Collections.Generic;

namespace MenuBuddy
{
	/// <summary>
	/// This is the actionbar screen that pops up when the user clicks the hamburger icon
	/// </summary>
	internal class HamburgerMenuScreen : WidgetScreen
	{
		#region Properties

		private List<ContextMenuItem> HamburgerItems { get; set; }

		private Texture2D HamburgerIcon { get; set; }

		protected bool LeftRight { get; set; }

		#endregion //Properties

		#region Methods

		public HamburgerMenuScreen(Texture2D hamburgerIcon, List<ContextMenuItem> hamburgerItems, bool leftRight) : base("HamburgerMenuScreen")
		{
			HamburgerIcon = hamburgerIcon;
			LeftRight = leftRight;
			HamburgerItems = hamburgerItems;

			CoverOtherScreens = false;
			CoveredByOtherScreens = false;
		}

		public override void LoadContent()
		{
			base.LoadContent();

			var stack = new StackLayout()
			{
				Alignment = StackAlignment.Top,
				Position = new Point(LeftRight ? Resolution.TitleSafeArea.Left : Resolution.TitleSafeArea.Right, 0),
				Horizontal = LeftRight ? HorizontalAlignment.Left : HorizontalAlignment.Right,
				Vertical = VerticalAlignment.Top,
			};

			//first add the menu item to dismiss the screen
			CreateButton(new ContextMenuItem(HamburgerIcon, "", ((obj, e) => { ExitScreen(); })), stack);

			//add each menu item below this
			foreach (var hamburgerItem in HamburgerItems)
			{
				CreateButton(hamburgerItem, stack);
			}

			//create the scroll layout
			var scroll = new ScrollLayout()
			{
				Position = new Point(LeftRight ? Resolution.ScreenArea.Left : Resolution.ScreenArea.Right, Resolution.TitleSafeArea.Top),
				Horizontal = LeftRight ? HorizontalAlignment.Left : HorizontalAlignment.Right,
				Vertical = VerticalAlignment.Top,
				TransitionObject = new WipeTransitionObject(TransitionWipeType.PopTop),
				Size = new Vector2(Resolution.TitleSafeArea.Left + stack.Rect.Width, 
					Math.Min(stack.Rect.Height, (Resolution.ScreenArea.Bottom - Resolution.TitleSafeArea.Top)))
			};
			scroll.AddItem(stack);
			AddItem(scroll);
		}

		private void CreateButton(ContextMenuItem hamburgerItem, StackLayout stack)
		{
			var button = new StackLayoutButton()
			{
				Vertical = VerticalAlignment.Center,
				Horizontal = LeftRight ? HorizontalAlignment.Left : HorizontalAlignment.Right,
				TransitionObject = new WipeTransitionObject(TransitionWipeType.PopTop),
				Alignment = LeftRight ? StackAlignment.Left : StackAlignment.Right,
			};
			button.AddItem(new Image(hamburgerItem.Icon)
			{
				Size = new Vector2(32f, 32f),
				Vertical = VerticalAlignment.Center,
				Horizontal = LeftRight ? HorizontalAlignment.Left : HorizontalAlignment.Right,
				FillRect = true,
				TransitionObject = new WipeTransitionObject(TransitionWipeType.PopTop)
			});
			button.AddItem(new Shim()
			 {
				 Size = new Vector2(16f, 16f)
			 });
			button.AddItem(new Label(hamburgerItem.IconText, FontSize.Small)
			{
				Vertical = VerticalAlignment.Top,
				Horizontal = LeftRight ? HorizontalAlignment.Left : HorizontalAlignment.Right,
				TransitionObject = new WipeTransitionObject(TransitionWipeType.PopTop)
			});
			button.OnClick += (obj, e) => ExitScreen();
			button.OnClick += (obj, e) => hamburgerItem.ClickEvent(obj, e);

			stack.AddItem(button);
			stack.AddItem(new Shim()
			{
				Size = new Vector2(8f, 8f)
			});
		}

		#endregion //Methods
	}
}
