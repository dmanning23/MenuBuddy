using GameTimer;
using HadoukInput;
using Microsoft.Xna.Framework;
using ResolutionBuddy;
using System;
using System.Collections.Generic;

namespace MenuBuddy
{
	/// <summary>
	/// Base class for screens that contain a menu of options. The user can
	/// move up and down to select an entry, or cancel to back out of the screen.
	/// </summary>
	public abstract class MenuScreen : GameScreen
	{
		#region Fields

		/// <summary>
		/// Ammount of time that passes before attract mode is activated
		/// </summary>
		private const float _AttractModeTime = 15.0f;

		/// <summary>
		/// index of the currently selected menu entry
		/// </summary>
		private int m_SelectedIndex;

		#endregion

		#region Properties

		/// <summary>
		/// Gets the list of menu entries, so derived classes can add or change the menu contents.
		/// </summary>
		protected IList<MenuEntry> MenuEntries { get; private set; }

		/// <summary>
		/// Get the currently selected menu entry index, -1 if no entry selected
		/// </summary>
		protected int SelectedIndex
		{
			get { return m_SelectedIndex; }
			set
			{
				//set teh selected menu item
				m_SelectedIndex = value;

				//set the highlighted item to match
				HighlightedIndex = m_SelectedIndex;

				//reset the menu clock so the entries don't pop
				MenuClock.Start();
			}
		}

		/// <summary>
		/// Get the currently selected menu entry, null if no menu entry selected
		/// </summary>
		public MenuEntry SelectedEntry
		{
			get 
			{
				if ((SelectedIndex > -1) &&
					(SelectedIndex < MenuEntries.Count))
				{
					return MenuEntries[SelectedIndex]; 
				}

				//no menu entry selected or something is not setup correctly
				return null;
			}
		}

		/// <summary>
		/// Get the currently selected menu entry index, -1 if no entry selected
		/// </summary>
		protected int HighlightedIndex { get; set; }

		/// <summary>
		/// Get the currently Highlighted menu entry, null if no menu entry selected
		/// </summary>
		public MenuEntry HighlightedEntry
		{
			get
			{
				if ((HighlightedIndex > -1) &&
					(HighlightedIndex < MenuEntries.Count))
				{
					return MenuEntries[HighlightedIndex];
				}

				//no menu entry selected or something is not setup correctly
				return null;
			}
		}

		/// <summary>
		/// Countdown timer that is used to tell when to start attract mode
		/// </summary>
		/// <value>The time since input.</value>
		public CountdownTimer TimeSinceInput { get; private set; }

		/// <summary>
		/// Gets or sets the menu clock.
		/// </summary>
		/// <value>The menu clock.</value>
		protected GameClock MenuClock { get; private set; }

		/// <summary>
		/// amount to offset the menu options from center screen
		/// </summary>
		public Vector2 MenuOptionOffset { get; set; }

		public float TitleScale { get; set; }

		/// <summary>
		/// Set this to true and the menu won't play any noises when things are selected etc.
		/// </summary>
		public bool QuietMenu { get; set; }

		/// <summary>
		/// flag to draw the selection rect around the menu entries as the text size, or use the menu entry rect to place them
		/// </summary>
		public bool TextSelectionRect { get; set; }

		#endregion

		#region Initialization

		/// <summary>
		/// Constructor.
		/// </summary>
		protected MenuScreen(string strMenuTitle = "")
			: base(strMenuTitle)
		{
			MenuOptionOffset = Vector2.Zero;
			TextSelectionRect = true;
			QuietMenu = false;
			TitleScale = 1.0f;
			MenuEntries = new List<MenuEntry>();
			MenuClock = new GameClock();
			MenuClock.Start();

			TransitionOnTime = TimeSpan.FromSeconds(0.75);
			TransitionOffTime = TimeSpan.FromSeconds(0.5);

			TimeSinceInput = new CountdownTimer();
			ResetInputTimer();
		}

		#endregion

		#region Handle Input

		/// <summary>
		/// Responds to user input, changing the selected entry and accepting
		/// or cancelling the menu.
		/// </summary>
		public override void HandleInput(InputState input, GameTime rGameTime)
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

			//Check if one of those menu entries was selected with the mouse
			if (ScreenManager.TouchMenus)
			{
				CheckForMenuHeld();
				CheckForMouseClick();
				CheckForTouch();
				CheckForHighlightedItem();
			}
			else
			{
				// Accept or cancel the menu? We pass in our ControllingPlayer, which may
				// either be null (to accept input from any player) or a specific index.
				// If we pass a null controlling player, the InputState helper returns to
				// us which player actually provided the input. We pass that through to
				// OnSelectEntry and OnCancel, so they can tell which player triggered them.
				PlayerIndex playerIndex;

				if (input.IsMenuSelect(ControllingPlayer, out playerIndex))
				{
					MenuSelect(playerIndex);
				}
				else if (input.IsMenuCancel(ControllingPlayer, out playerIndex))
				{
					OnCancel(playerIndex);
				}
			}
		}

		protected virtual void MenuUp()
		{
			if (MenuEntries.Count > 1)
			{
				//don't roll over
				SelectedIndex = Math.Max(0, SelectedIndex - 1);

				if (!QuietMenu)
				{
					//play menu noise
					ScreenManager.MenuChange.Play();
				}

				ResetInputTimer();
			}
		}

		protected virtual void MenuDown()
		{
			if (MenuEntries.Count > 1)
			{
				//don't roll over
				SelectedIndex = Math.Min(SelectedIndex + 1, MenuEntries.Count - 1);

				if (!QuietMenu)
				{
					//play menu noise
					ScreenManager.MenuChange.Play();
				}

				ResetInputTimer();
			}
		}

		protected virtual void MenuLeft()
		{
			if (null != SelectedEntry)
			{
				//play menu noise
				if (SelectedEntry.PlayLeftRightSound && !QuietMenu)
				{
					ScreenManager.MenuChange.Play();
				}

				//run the sleected evetn
				SelectedEntry.OnLeftEntry();

				ResetInputTimer();
			}
		}

		protected virtual void MenuRight()
		{
			if (null != SelectedEntry)
			{
				//play menu noise
				if (SelectedEntry.PlayLeftRightSound && !QuietMenu)
				{
					ScreenManager.MenuChange.Play();
				}

				//run the sleected evetn
				SelectedEntry.OnRightEntry();

				ResetInputTimer();
			}
		}

		/// <summary>
		/// Check if the player is holding the menu entry down
		/// If the player isn't holding it down, the current selected entry is set to -1
		/// </summary>
		/// <returns></returns>
		protected void CheckForMenuHeld()
		{
			//flag used to reset selected entry
			bool heldDown = false;

			//check if the player is holding the LMouseButton down in the menu entry
			if (ScreenManager.InputState.LMouseDown)
			{
				for (int i = 0; i < MenuEntries.Count; i++)
				{
					if (MenuEntries[i].ButtonRect.Contains(ScreenManager.MousePos))
					{
						SelectedIndex = i;
						heldDown = true;
						break;
					}
				}
			}

			//check if the player is holding down the touch screen in the menu entry
			if (null != ScreenManager.Touch)
			{
				foreach (var touch in ScreenManager.Touch.Touches)
				{
					for (int i = 0; i < MenuEntries.Count; i++)
					{
						if (MenuEntries[i].ButtonRect.Contains(touch))
						{
							SelectedIndex = i;
							heldDown = true;
							break;
						}
					}
				}
			}

			//well the player isn't holding anything... reset the selected entry
			if (!heldDown)
			{
				SelectedIndex = -1;
			}
		}

		protected void CheckForMouseClick()
		{
			//did the user click somewhere?
			if (ScreenManager.InputState.LMouseClick)
			{
				//ok find which menu entry was clicked
				var mousePos = ScreenManager.MousePos;
				for (int i = 0; i < MenuEntries.Count; i++)
				{
					if (MenuEntries[i].ButtonRect.Contains(mousePos))
					{
						SelectedIndex = i;
						FireMenuSelectEvent(PlayerIndex.One, MenuEntries[i]);
						break;
					}
				}
			}
		}

		protected void CheckForTouch()
		{
			if (null != ScreenManager.Touch)
			{
				foreach (var tapPos in ScreenManager.Touch.Taps)
				{
					for (int i = 0; i < MenuEntries.Count; i++)
					{
						if (MenuEntries[i].ButtonRect.Contains(tapPos))
						{
							SelectedIndex = i;
							FireMenuSelectEvent(PlayerIndex.One, MenuEntries[i]);
							break;
						}
					}
				}
			}
		}

		/// <summary>
		/// Check if the cursor is in a menu item
		/// </summary>
		/// <returns></returns>
		protected void CheckForHighlightedItem()
		{
			//check if the mouse cursor is in a button
			for (int i = 0; i < MenuEntries.Count; i++)
			{
				//Check if the mouse cursor is inside this menu entry
				if (MenuEntries[i].ButtonRect.Contains(ScreenManager.MousePos))
				{
					HighlightedIndex = i;
					break;
				}
			}
		}

		/// <summary>
		/// User hit the "menu select" button.
		/// </summary>
		/// <param name="playerIndex"></param>
		protected virtual void MenuSelect(PlayerIndex playerIndex)
		{
			if (null != SelectedEntry)
			{
				FireMenuSelectEvent(playerIndex, SelectedEntry);
			}
		}

		/// <summary>
		/// a menu entry was selected and we want to fire it's select event
		/// </summary>
		/// <param name="playerIndex"></param>
		/// <param name="entry"></param>
		private void FireMenuSelectEvent(PlayerIndex playerIndex, MenuEntry entry)
		{
			if (!QuietMenu && !entry.QuietEntry)
			{
				if (null != entry.SelectionSound)
				{
					//this menu entry has a special sound
					entry.SelectionSound.Play();
				}
				else
				{
					//play the defualt menu noise
					ScreenManager.MenuSelect.Play();
				}
			}

			//run the sleected evetn
			entry.OnSelectEntry(playerIndex);

			ResetInputTimer();
		}

		/// <summary>
		/// Handler for when the user has cancelled the menu.
		/// </summary>
		protected virtual void OnCancel(PlayerIndex playerIndex)
		{
			ExitScreen();
		}

		/// <summary>
		/// Helper overload makes it easy to use OnCancel as a MenuEntry event handler.
		/// </summary>
		protected void OnCancel(object sender, PlayerIndexEventArgs e)
		{
			OnCancel(e.PlayerIndex);
		}

		/// <summary>
		/// Remove a menu entry from the menu
		/// </summary>
		/// <param name="entry"></param>
		protected void RemoveMenuEntry(MenuEntry entry)
		{
			//remove the entry from the list
			MenuEntries.Remove(entry);

			//set the selected item if needed
			if (SelectedIndex >= MenuEntries.Count)
			{
				SelectedIndex = MenuEntries.Count - 1;
			}
		}

		#endregion

		#region Update and Draw

		/// <summary>
		/// Updates the menu.
		/// </summary>
		public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
		{
			base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);

			// Update each nested MenuEntry object.
			for (int i = 0; i < MenuEntries.Count; i++)
			{
				bool isSelected = IsActive && (i == SelectedIndex);
				MenuEntries[i].Update(this, isSelected, gameTime);
			}

			//update the timers
			if (!otherScreenHasFocus && !coveredByOtherScreen)
			{
				TimeSinceInput.Update(gameTime);
			}
		}

		/// <summary>
		/// Draws the menu.
		/// </summary>
		public override void Draw(GameTime gameTime)
		{
			//update the menu clock
			MenuClock.Update(gameTime);

			//Get the default position for menu entries
			Vector2 entryPos = EntryStartPosition();

			ScreenManager.SpriteBatchBegin();

			// Draw each menu entry in turn.
			for (int i = 0; i < MenuEntries.Count; i++)
			{
				//Draw the menu option
				bool isSelected = IsActive && (i == SelectedIndex);
				MenuEntries[i].Draw(this, entryPos, isSelected, MenuClock);

				//update the menu entry Y position
				entryPos.Y += (MenuEntries[i].GetHeight(this));
			}

			DrawMenuTitle(ScreenName, TitleScale, gameTime);

			ScreenManager.SpriteBatchEnd();
		}

		protected virtual Vector2 EntryStartPosition()
		{
			return (MenuOptionOffset + new Vector2(MenuEntryPositionX(), MenuEntryPositionY()));
		}

		/// <summary>
		/// This gets called when the input timer needs to be reset.
		/// Used by menu screens to pop up attract mode
		/// </summary>
		public override void ResetInputTimer()
		{
			TimeSinceInput.Start(_AttractModeTime);
		}

		/// <summary>
		/// Get the x position to draw menu entries at, using the transition position
		/// </summary>
		/// <returns>the correct position the place menu entries on X axis</returns>
		public virtual float MenuEntryPositionX()
		{
			return XPositionWithOffset((float)Resolution.TitleSafeArea.Center.X);
		}

		/// <summary>
		/// Get the x position to draw menu entries at, using the transition position
		/// </summary>
		/// <returns>the correct position the place menu entries on Y axis</returns>
		public virtual float MenuEntryPositionY()
		{
			return (Resolution.TitleSafeArea.Center.Y * 0.9f);
		}

		public float XPositionWithOffset(float pos)
		{
			float fMenuPositionX = pos;

			if (TransitionPosition != 0.0f)
			{
				// Make the menu slide into place during transitions, using a
				// power curve to make things look more interesting (this makes
				// the movement slow down as it nears the end).
				var transitionOffset = (float)Math.Pow(TransitionPosition, 2.0);
				if (ScreenState == EScreenState.TransitionOn)
				{
					fMenuPositionX -= transitionOffset * 256;
				}
				else
				{
					fMenuPositionX += transitionOffset * 512;
				}
			}

			return fMenuPositionX;
		}

		#endregion
	}
}