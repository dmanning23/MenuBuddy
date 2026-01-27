using InputHelper;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ResolutionBuddy;
using System;
using System.Collections.Generic;

namespace MenuBuddy
{
	/// <summary>
	/// A hamburger menu button that displays a three-line icon and opens a <see cref="HamburgerMenuScreen"/> when clicked.
	/// </summary>
	public class Hamburger : RelativeLayoutButton, IContextMenu
	{
		#region Properties

		/// <summary>
		/// The image widget displaying the hamburger icon.
		/// </summary>
		protected IImage HamburgerImage { get; set; }

		/// <summary>
		/// Whether the hamburger menu opens from the left (<c>true</c>) or right (<c>false</c>) side of the screen.
		/// </summary>
		protected bool LeftRight { get; set; }

		/// <summary>
		/// The list of menu items displayed when the hamburger menu is opened.
		/// </summary>
		private List<ContextMenuItem> HamburgerItems { get; set; }

		#endregion //Properties

		#region Methods

		/// <summary>
		/// Initializes a new <see cref="Hamburger"/> button with the specified icon and screen alignment.
		/// </summary>
		/// <param name="hamburgerIcon">The texture to use for the hamburger icon.</param>
		/// <param name="leftRight">Whether the menu opens from the left (<c>true</c>) or right (<c>false</c>).</param>
		/// <param name="screenManager">The screen manager used to display the hamburger menu screen.</param>
		public Hamburger(Texture2D hamburgerIcon, bool leftRight, ScreenManager screenManager)
		{
			HamburgerItems = new List<ContextMenuItem>();
			LeftRight = leftRight;

			//create the image for the icon
			HamburgerImage = new Image(hamburgerIcon)
			{
				Size = new Vector2(64f, 64f),
				Vertical = VerticalAlignment.Top,
				Horizontal = HorizontalAlignment.Center,
				FillRect = true,
				TransitionObject = new WipeTransitionObject(TransitionWipeType.PopTop)
			};

			//set the hamburger button properties
			Size = new Vector2(64f, 64f);
			Vertical = VerticalAlignment.Top;
			Horizontal = LeftRight ? HorizontalAlignment.Left : HorizontalAlignment.Right;
			TransitionObject = new WipeTransitionObject(TransitionWipeType.PopTop);
			Position = new Point(LeftRight ?  Resolution.TitleSafeArea.Left : Resolution.TitleSafeArea.Right, Resolution.TitleSafeArea.Top);
			AddItem(HamburgerImage);
			OnClick += async (obj, e) =>
			{
				await screenManager.AddScreen(new HamburgerMenuScreen(HamburgerImage.Texture, HamburgerItems, LeftRight));
			};
		}

		/// <inheritdoc/>
		public void AddItem(Texture2D icon, string iconText, ClickDelegate clickEvent)
		{
			HamburgerItems.Add(new ContextMenuItem(icon, iconText, clickEvent));
		}

		#endregion //Methods
	}
}
