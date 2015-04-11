using GameTimer;
using Microsoft.Xna.Framework;
using ResolutionBuddy;
using System;
using System.Linq;
using Vector2Extensions;

namespace MenuBuddy
{
	/// <summary>
	/// Base class for screens that contain a menu of options. The user can
	/// move up and down to select an entry, or cancel to back out of the screen.
	/// </summary>
	public abstract class MenuScreen : WidgetScreen, IMenuScreen
	{
		#region Fields

		private Point _menuTitleOffset;
		private Point _menuEntryOffset;

		#endregion

		#region Properties

		/// <summary>
		/// The title of this menu
		/// </summary>
		public Label MenuTitle { get; private set; }

		/// <summary>
		/// Gets the list of menu entries, so derived classes can add or change the menu contents.
		/// </summary>
		protected StackLayout MenuEntries { get; private set; }

		/// <summary>
		/// Get the currently selected menu entry index, -1 if no entry selected
		/// </summary>
		protected int SelectedIndex { get; set; }

		/// <summary>
		/// Get the currently selected menu entry, null if no menu entry selected
		/// </summary>
		public IButton SelectedEntry
		{
			get
			{
				if ((SelectedIndex > -1) &&
					(SelectedIndex < MenuEntries.Items.Count))
				{
					return MenuEntries.Items[SelectedIndex] as IButton;
				}

				//no menu entry selected or something is not setup correctly
				return null;
			}
		}

		protected Point MenuEntryOffset
		{
			get { return _menuEntryOffset; }
			set
			{
				_menuEntryOffset = value;
				var prevPos = MenuEntries.Position;
				MenuEntries.Position = new Point(prevPos.X + _menuEntryOffset.X,
					prevPos.Y + _menuEntryOffset.Y);
			}
		}

		protected Point MenuTitleOffset
		{
			get { return _menuTitleOffset; }
			set
			{
				_menuTitleOffset = value;
				if (null != MenuTitle)
				{
					var prevPos = MenuTitle.Position;
					MenuTitle.Position = new Point(prevPos.X + _menuTitleOffset.X,
						prevPos.Y + _menuTitleOffset.Y);
				}
			}
		}

		#endregion

		#region Initialization

		/// <summary>
		/// Constructor.
		/// </summary>
		protected MenuScreen(string menuTitle = "")
			: base(menuTitle)
		{
		}

		public override void LoadContent()
		{
			base.LoadContent();

			//Create the stack layout for teh menu entries
			MenuEntries = new StackLayout();
			MenuEntries.Position = new Point(MenuEntryOffset.X + Resolution.TitleSafeArea.Center.X,
						MenuEntryOffset.Y + (int)(Resolution.TitleSafeArea.Center.Y * 0.8f));
			AddItem(MenuEntries);

			//Add the menu title
			var menuTitleSize = ScreenManager.Styles.MenuTitleStyle.SelectedFont.Font.MeasureString(ScreenName);
			MenuTitle = new Label(ScreenManager.Styles.MenuTitleStyle, ScreenName);
			MenuTitle.Position = new Point(
				MenuTitleOffset.X + Resolution.TitleSafeArea.Center.X,
				MenuTitleOffset.Y + Resolution.TitleSafeArea.Center.Y - (int)(menuTitleSize.Y * 1.75f));
			MenuTitle.Horizontal = HorizontalAlignment.Center;

			AddItem(MenuTitle);
		}

		protected void AddMenuEntry(MenuEntry menuEntry)
		{
			menuEntry.Horizontal = HorizontalAlignment.Center;
			MenuEntries.AddItem(menuEntry);
		}

		/// <summary>
		/// This method adds a continue button to the menu and attachs it to OnCancel
		/// </summary>
		protected MenuEntry AddContinueButton()
		{
			var continueButton = new ContinueMenuEntry(Style);
			continueButton.Selected += OnCancel;
			AddMenuEntry(continueButton);
			return continueButton;
		}

		#endregion

		#region Handle Input

		public void MenuUp()
		{
			if (MenuEntries.Items.Count > 1)
			{
				//don't roll over
				SelectedIndex = Math.Max(0, SelectedIndex - 1);

				if (!Style.IsQuiet && (null != Style.SelectionChangeSoundEffect))
				{
					//play menu noise
					Style.SelectionChangeSoundEffect.Play();
				}

				ResetInputTimer();
			}
		}

		public void MenuDown()
		{
			if (MenuEntries.Items.Count > 1)
			{
				//don't roll over
				SelectedIndex = Math.Min(SelectedIndex + 1, MenuEntries.Items.Count - 1);

				if (!Style.IsQuiet && (null != Style.SelectionChangeSoundEffect))
				{
					//play menu noise
					Style.SelectionChangeSoundEffect.Play();
				}

				ResetInputTimer();
			}
		}

		public void MenuLeft()
		{
			var menuEntry = SelectedEntry as MenuEntry;
			if (null != menuEntry)
			{
				//run the sleected evetn
				menuEntry.OnLeftEntry();

				ResetInputTimer();
			}
		}

		public void MenuRight()
		{
			var menuEntry = SelectedEntry as MenuEntry;
			if (null != menuEntry)
			{
				//run the sleected evetn
				menuEntry.OnRightEntry();

				ResetInputTimer();
			}
		}

		/// <summary>
		/// User hit the "menu select" button.
		/// </summary>
		/// <param name="playerIndex"></param>
		public override void OnSelect(PlayerIndex? playerIndex)
		{
			if (null != SelectedEntry)
			{
				//run the selected event
				SelectedEntry.OnSelect(playerIndex);
			}

			base.OnSelect(playerIndex);

		}

		/// <summary>
		/// Remove a menu entry from the menu
		/// </summary>
		/// <param name="entry"></param>
		protected void RemoveMenuEntry(MenuEntry entry)
		{
			//try to remove the entry from the list
			if (MenuEntries.RemoveItem(entry))
			{
				//set the selected item if needed
				if (SelectedIndex >= MenuEntries.Items.Count)
				{
					SelectedIndex = MenuEntries.Items.Count - 1;
				}
			}
		}

		/// <summary>
		/// Remove a menu entry from the menu
		/// </summary>
		/// <param name="entryText">the text of the item to remove</param>
		protected virtual void RemoveMenuEntry(string entryText)
		{
			//get the menu items as buttons
			var buttons = MenuEntries.Items.OfType<MenuEntry>();

			foreach (var menuItem in buttons)
			{
				//check if the menu item has the same text
				if (menuItem.Text == entryText)
				{
					RemoveMenuEntry(menuItem);
					return;
				}
			}
		}

		#endregion

		#region Update and Draw

		/// <summary>
		/// Updates the menu.
		/// </summary>
		public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
		{
			//Hide menu screens when they are covered
			if (otherScreenHasFocus)
			{
				coveredByOtherScreen = true;
			}

			base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);

			//update the timers
			if (!otherScreenHasFocus && !coveredByOtherScreen)
			{
				//set teh highlighted item
				var entries = MenuEntries.Items.OfType<MenuEntry>().ToList();
				for (int i = 0; i < entries.Count; i++)
				{
					entries[i].IsHighlighted = (i == SelectedIndex);
				}

				TimeSinceInput.Update(gameTime);
			}
			else if (null != SelectedEntry)
			{
				//make sure that highlighted entry isn't being a butt
				SelectedEntry.IsHighlighted = false;
			}
		}

		#endregion
	}
}