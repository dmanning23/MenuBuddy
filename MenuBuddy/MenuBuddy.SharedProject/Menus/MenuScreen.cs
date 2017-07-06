using HadoukInput;
using InputHelper;
using Microsoft.Xna.Framework;
using ResolutionBuddy;
using System;
using System.Linq;

namespace MenuBuddy
{
	/// <summary>
	/// Base class for screens that contain a menu of options. The user can
	/// move up and down to select an entry, or cancel to back out of the screen.
	/// </summary>
	public abstract class MenuScreen : WidgetScreen, IMenuScreen
	{
		#region Fields

		private Point _menuTitlePosition;

		private Point _menuEntryPosition;

		/// <summary>
		/// The game type, as loaded from the screenmanager.game
		/// </summary>
		private GameType _gameType;

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
		public int SelectedIndex { get; private set; }

		/// <summary>
		/// Get the currently selected menu entry, null if no menu entry selected
		/// </summary>
		public IMenuEntry SelectedEntry
		{
			get
			{
				if ((GameType.Controller == _gameType) &&
					(SelectedIndex > -1) &&
					(SelectedIndex < MenuEntries.Items.Count))
				{
					return MenuEntries.Items[SelectedIndex] as IMenuEntry;
				}

				//no menu entry selected or something is not setup correctly
				return null;
			}
		}

		protected Point MenuTitlePosition
		{
			get { return _menuTitlePosition; }
			set
			{
				_menuTitlePosition = value;
				if (null != MenuTitle)
				{
					MenuTitle.Position = _menuTitlePosition;
				}
			}
		}

		protected Point MenuEntryPosition
		{
			get { return _menuEntryPosition; }
			set
			{
				_menuEntryPosition = value;
				MenuEntries.Position = _menuEntryPosition;
			}
		}

		public override string ScreenName
		{
			get
			{
				return base.ScreenName;
			}
			set
			{
				base.ScreenName = value;
				if (null != MenuTitle)
				{
					MenuTitle.Text = base.ScreenName;
				}
			}
		}

		#endregion

		#region Methods

		/// <summary>
		/// Constructor.
		/// </summary>
		protected MenuScreen(string menuTitle = "")
			: base(menuTitle)
		{
			CoverOtherScreens = true;
			CoveredByOtherScreens = true;

			_menuEntryPosition = new Point(Resolution.TitleSafeArea.Center.X, (int)(Resolution.TitleSafeArea.Center.Y * 0.8f));
			_menuTitlePosition = new Point(Resolution.TitleSafeArea.Center.X, (int)(Resolution.TitleSafeArea.Center.Y * 0.4f));
		}

		public override void LoadContent()
		{
			base.LoadContent();

			var game = ScreenManager?.Game as DefaultGame;
			_gameType = null != game ? game.GameType : GameType.Controller;

			//Create the stack layout for teh menu entries
			MenuEntries = new StackLayout()
			{
				Alignment = StackAlignment.Top,
				Horizontal = HorizontalAlignment.Center,
				Layer = 1.0f,
				Position = MenuEntryPosition
			};
			AddItem(MenuEntries);

			//Add the menu title
			MenuTitle = new MenuTitle(ScreenName)
			{
				Layer = 2.0f,
				Position = MenuTitlePosition
			};
			AddItem(MenuTitle);
		}

		protected void AddMenuEntry(IMenuEntry menuEntry)
		{
			menuEntry.LoadContent(this);
			MenuEntries.AddItem(menuEntry);
		}

		/// <summary>
		/// This method adds a continue button to the menu and attachs it to OnCancel
		/// </summary>
		protected IMenuEntry AddContinueButton()
		{
			var continueButton = new ContinueMenuEntry();
			continueButton.OnClick += ((obj, e) => { ExitScreen(); });
			AddMenuEntry(continueButton);
			return continueButton;
		}

		#endregion //Methods

		#region Handle Input

		public void CheckInput(InputState input)
		{
			//Check all the input
			if (input.IsMenuUp(ControllingPlayer))
			{
				// Move to the previous menu entry
				MenuUp();
			}
			else if (input.IsMenuDown(ControllingPlayer))
			{
				// Move to the next menu entry
				MenuDown();
			}

			//checkl the left/right messages
			if (input.IsMenuLeft(ControllingPlayer))
			{
				//send a left message to the current menu entry
				MenuLeft();
			}
			else if (input.IsMenuRight(ControllingPlayer))
			{
				//send a right message to the current menu entry
				MenuRight();
			}

			// Accept or cancel the menu? We pass in our ControllingPlayer, which may
			// either be null (to accept input from any player) or a specific index.
			// If we pass a null controlling player, the InputState helper returns to
			// us which player actually provided the input. We pass that through to
			// OnSelectEntry and OnCancel, so they can tell which player triggered them.
			PlayerIndex playerIndex;

			if (input.IsMenuSelect(ControllingPlayer, out playerIndex))
			{
				if (null != SelectedEntry)
				{
					SelectedEntry.Clicked(this, new ClickEventArgs
					{
						PlayerIndex = playerIndex
					});
				}
			}
			else if (input.IsMenuCancel(ControllingPlayer, out playerIndex))
			{
				Cancelled(this, new ClickEventArgs
				{
					PlayerIndex = playerIndex
				});
			}

			if (null != SelectedEntry)
			{
				SelectedEntry.IsHighlighted = IsActive;
			}
		}

		private void MenuUp()
		{
			if (MenuEntries.Items.Count > 1)
			{
				//don't roll over
				SelectedIndex = Math.Max(0, SelectedIndex - 1);

				HighlightSeslectedItem();

				ResetInputTimer();
			}
		}

		private void MenuDown()
		{
			if (MenuEntries.Items.Count > 1)
			{
				//don't roll over
				SelectedIndex = Math.Min(SelectedIndex + 1, MenuEntries.Items.Count - 1);

				HighlightSeslectedItem();

				ResetInputTimer();
			}
		}

		private void MenuLeft()
		{
			var menuEntry = SelectedEntry as IMenuEntry;
			if (null != menuEntry)
			{
				//run the sleected evetn
				menuEntry.OnLeftEntry();

				ResetInputTimer();
			}
		}

		private void MenuRight()
		{
			var menuEntry = SelectedEntry as IMenuEntry;
			if (null != menuEntry)
			{
				//run the sleected evetn
				menuEntry.OnRightEntry();

				ResetInputTimer();
			}
		}

		/// <summary>
		/// Remove a menu entry from the menu
		/// </summary>
		/// <param name="entry"></param>
		protected void RemoveMenuEntry(IMenuEntry entry)
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
			var buttons = MenuEntries.Items.OfType<IMenuEntry>();

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

		/// <summary>
		/// Remove a menu entry from the menu
		/// </summary>
		/// <param name="index">the index of the item to remove</param>
		protected virtual void RemoveMenuEntry(int index)
		{
			//get the menu items as buttons
			var buttons = MenuEntries.Items.OfType<IMenuEntry>().ToList();

			//check if there are enough items
			if (index < buttons.Count())
			{
				RemoveMenuEntry(buttons[index]);
			}
		}

		#endregion

		#region Update and Draw

		///// <summary>
		///// Updates the menu.
		///// </summary>
		//public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
		//{
		//	base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);
		//}

		private void HighlightSeslectedItem()
		{
			//set teh highlighted item
			var entries = MenuEntries.Items.OfType<IMenuEntry>().ToList();
			for (int i = 0; i < entries.Count; i++)
			{
				entries[i].CheckHighlight(new HighlightEventArgs()
				{
					Position = SelectedEntry.Position.ToVector2()
				});
			}
		}

		public virtual void Cancelled(object obj, ClickEventArgs e)
		{
			ExitScreen();
		}

		#endregion
	}
}