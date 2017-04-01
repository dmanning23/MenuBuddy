using InputHelper;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ResolutionBuddy;
using System;
using System.Collections.Generic;

namespace MenuBuddy
{
	public class Hamburger : RelativeLayoutButton, IHamburger
	{
		#region Properties

		protected IImage HamburgerImage { get; set; }

		protected bool LeftRight { get; set; }

		private List<HamburgerItem> HamburgerItems { get; set; }

		#endregion //Properties

		#region Methods

		public Hamburger(Texture2D hamburgerIcon, bool leftRight, ScreenManager screenManager)
		{
			HamburgerItems = new List<HamburgerItem>();
			LeftRight = leftRight;

			//create the image for the icon
			HamburgerImage = new Image(hamburgerIcon)
			{
				Size = new Vector2(64f, 64f),
				Vertical = VerticalAlignment.Top,
				Horizontal = HorizontalAlignment.Center,
				FillRect = true,
				Transition = new WipeTransitionObject(TransitionWipeType.PopTop)
			};

			//set the hamburger button properties
			Size = new Vector2(64f, 64f);
			Vertical = VerticalAlignment.Top;
			Horizontal = LeftRight ? HorizontalAlignment.Left : HorizontalAlignment.Right;
			Transition = new WipeTransitionObject(TransitionWipeType.PopTop);
			Position = new Point(LeftRight ?  Resolution.TitleSafeArea.Left : Resolution.TitleSafeArea.Right, Resolution.TitleSafeArea.Top);
			AddItem(HamburgerImage);
			OnClick += (obj, e) =>
			{
				screenManager.AddScreen(new HamburgerMenuScreen(HamburgerImage.Texture, HamburgerItems, LeftRight));
			};
		}

		public void AddItem(Texture2D icon, string iconText, ClickDelegate clickEvent)
		{
			HamburgerItems.Add(new HamburgerItem(icon, iconText, clickEvent));
		}

		#endregion //Methods
	}
}
