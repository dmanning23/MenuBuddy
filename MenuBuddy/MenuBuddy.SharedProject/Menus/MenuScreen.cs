using HadoukInput;
using InputHelper;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MenuBuddy
{
	/// <summary>
	/// Base class for screens that contain a menu of options. The user can
	/// move up and down to select an entry, or cancel to back out of the screen.
	/// </summary>
	public class MenuScreen : WidgetScreen, IMenuScreen
	{
		#region Properties

		protected class TabbedItem
		{
			public int TabOrder { get; set; }
			public int TabSubOrder { get; set; }
			public IScreenItem Widget { get; set; }

			public override string ToString()
			{
				return Widget.ToString();
			}
		}

		/// <summary>
		/// The game type, as loaded from the screenmanager.game
		/// </summary>
		private GameType _gameType;

		/// <summary>
		/// Gets the list of menu entries, so derived classes can add or change the menu contents.
		/// </summary>
		private List<TabbedItem> MenuItems { get; set; } = new List<TabbedItem>();

		/// <summary>
		/// Get the currently selected menu entry index, -1 if no entry selected
		/// </summary>
		public int SelectedIndex { get; set; }

		/// <summary>
		/// Get the currently selected menu entry, null if no menu entry selected
		/// </summary>
		public IScreenItem SelectedItem
		{
			get
			{
				if ((GameType.Controller == _gameType) &&
					(SelectedIndex > -1) &&
					(SelectedIndex < MenuItems.Count))
				{
					return MenuItems[SelectedIndex].Widget;
				}

				//no menu entry selected or something is not setup correctly
				return null;
			}
		}

		#endregion

		#region Methods

		/// <summary>
		/// Constructor.
		/// </summary>
		public MenuScreen(string menuTitle = "", ContentManager content = null)
			: base(menuTitle, content)
		{
			CoverOtherScreens = true;
			CoveredByOtherScreens = true;
		}

		public override Task LoadContent()
		{
			var game = ScreenManager?.Game as DefaultGame;
			_gameType = null != game ? game.GameType : GameType.Controller;

			return base.LoadContent();
		}

		public override void UnloadContent()
		{
			base.UnloadContent();
			MenuItems = null;
		}

		public void AddMenuItem(IScreenItem menuItem, int tabOrder = 0)
		{
			//create the tab item
			var tabItem = new TabbedItem
			{
				TabOrder = tabOrder,
				TabSubOrder = MenuItems.Count,
				Widget = menuItem
			};
			MenuItems.Add(tabItem);

			//Sort the list
			MenuItems.Sort((a, b) =>
			{
				if (a.TabOrder != b.TabOrder)
				{
					return a.TabOrder.CompareTo(b.TabOrder);
				}
				else
				{
					return a.TabSubOrder.CompareTo(b.TabSubOrder);
				}
			});
		}

		#endregion //Methods

		#region Handle Input

		public void HandleInput(IInputState input)
		{
			var inputState = input as InputState;
			if (null == inputState)
			{
				return;
			}

			//Check all the input
			if (inputState.IsMenuUp(ControllingPlayer))
			{
				// Move to the previous menu entry
				MenuUp();
			}
			else if (inputState.IsMenuDown(ControllingPlayer))
			{
				// Move to the next menu entry
				MenuDown();
			}

			//checkl the left/right messages
			if (inputState.IsMenuLeft(ControllingPlayer))
			{
				//send a left message to the current menu entry
				MenuLeft();
			}
			else if (inputState.IsMenuRight(ControllingPlayer))
			{
				//send a right message to the current menu entry
				MenuRight();
			}

			// Accept or cancel the menu? We pass in our ControllingPlayer, which may
			// either be null (to accept input from any player) or a specific index.
			// If we pass a null controlling player, the InputState helper returns to
			// us which player actually provided the input. We pass that through to
			// OnSelectEntry and OnCancel, so they can tell which player triggered them.

			if (inputState.IsMenuSelect(ControllingPlayer, out int playerIndex))
			{
				var selectedItem = SelectedItem as IButton;
				if (null != selectedItem)
				{
					selectedItem.Clicked(this, new ClickEventArgs
					{
						PlayerIndex = playerIndex
					});
				}
			}
			else if (inputState.IsMenuCancel(ControllingPlayer, out playerIndex))
			{
				Cancelled(this, new ClickEventArgs
				{
					PlayerIndex = playerIndex
				});
			}

			var highlightable = SelectedItem as IHighlightable;
			if (null != highlightable && highlightable.Highlightable)
			{
				highlightable.IsHighlighted = IsActive;
			}
		}

		private void MenuUp()
		{
			if (MenuItems.Count > 1)
			{
				//don't roll over
				SetSelectedIndex(Math.Max(0, SelectedIndex - 1));
			}
		}

		private void MenuDown()
		{
			if (MenuItems.Count > 1)
			{
				//don't roll over
				SetSelectedIndex(Math.Min(SelectedIndex + 1, MenuItems.Count - 1));
			}
		}

		public void SetSelectedIndex(int index)
		{
			SelectedIndex = index;

			HighlightSelectedItem();

			ResetInputTimer();
		}

		public void SetSelectedItem(IScreenItem item)
		{
			SetSelectedIndex(MenuItems.FindIndex(x => x.Widget == item));
		}

		private void MenuLeft()
		{
			var menuEntry = SelectedItem as ILeftRightItem;
			if (null != menuEntry)
			{
				//run the sleected evetn
				menuEntry.OnLeftEntry();

				ResetInputTimer();
			}
		}

		private void MenuRight()
		{
			var menuEntry = SelectedItem as ILeftRightItem;
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
		public void RemoveMenuItem(IScreenItem entry)
		{
			//try to remove the entry from the list
			RemoveMenuItem(MenuItems.FirstOrDefault(x => x.Widget == entry));
		}

		/// <summary>
		/// Remove a menu entry from the menu
		/// </summary>
		/// <param name="index">the index of the item to remove</param>
		public virtual void RemoveMenuItem(int index)
		{
			//check if there are enough items
			if (index < MenuItems.Count())
			{
				RemoveMenuItem(MenuItems[index]);
			}
		}

		private void RemoveMenuItem(TabbedItem item)
		{
			//try to remove the entry from the list
			if (null != item && MenuItems.Remove(item))
			{
				//set the selected item if needed
				if (SelectedIndex >= MenuItems.Count)
				{
					SelectedIndex = MenuItems.Count - 1;
				}
			}
		}

		private void HighlightSelectedItem()
		{
			//set teh highlighted item
			for (int i = 0; i < MenuItems.Count; i++)
			{
				var highlightable = MenuItems[i].Widget as IHighlightable;
				if (null != highlightable)
				{
					var position = i == SelectedIndex ? SelectedItem.Position.ToVector2() : Vector2.Zero;
					highlightable.CheckHighlight(new HighlightEventArgs(position, ScreenManager.DefaultGame.InputHelper));
				}
			}
		}

		public virtual void Cancelled(object obj, ClickEventArgs e)
		{
			ExitScreen();
		}

		#endregion
	}
}