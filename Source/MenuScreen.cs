using GameTimer;
using ResolutionBuddy;
using HadoukInput;
using Microsoft.Xna.Framework;
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
		private int m_SelectedEntry;

		#endregion

		#region Properties

		/// <summary>
		/// Gets the list of menu entries, so derived classes can add
		/// or change the menu contents.
		/// </summary>
		protected IList<MenuEntry> MenuEntries { get; private set; }

		protected int SelectedEntry
		{
			get { return m_SelectedEntry; }
			set
			{
				//set teh selected menu item
				m_SelectedEntry = value;

				//reset the menu clock so the entries don't pop
				MenuClock.Start();
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
		/// y value to offset the menu options
		/// </summary>
		public float MenuOptionOffset { get; set; }

		public float TitleScale { get; set; }

		/// <summary>
		/// Set this to true and the menu won't play any noises when things are selected etc.
		/// </summary>
		public bool QuietMenu { get; set; }

		/// <summary>
		/// Flag to draw menu entries at center screen, or to use the menu entry rect to place them
		/// </summary>
		public bool CascadeMenuEntries { get; set; }

		/// <summary>
		/// flag to draw the selection rect around the menu entries as the text size, or use the menu entry rect to place them
		/// </summary>
		public bool TextSelectionRect { get; set; }

		#endregion

		#region Initialization

		/// <summary>
		/// Constructor.
		/// </summary>
		public MenuScreen(string strMenuTitle = "")
			: base(strMenuTitle)
		{
			CascadeMenuEntries = true;
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
				if (!ScreenManager.InputState.LMouseDown)
				{
					m_SelectedEntry = -1;
				}
				CheckForMouseClick();
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
				SelectedEntry = Math.Max(0, SelectedEntry - 1);

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
				SelectedEntry = Math.Min(SelectedEntry + 1, MenuEntries.Count - 1);

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
			if (MenuEntries.Count >= 1)
			{
				//play menu noise
				if (MenuEntries[SelectedEntry].PlayLeftRightSound && !QuietMenu)
				{
					ScreenManager.MenuChange.Play();
				}

				//run the sleected evetn
				MenuEntries[SelectedEntry].OnLeftEntry();

				ResetInputTimer();
			}
		}

		protected virtual void MenuRight()
		{
			if (MenuEntries.Count >= 1)
			{
				//play menu noise
				if (MenuEntries[SelectedEntry].PlayLeftRightSound && !QuietMenu)
				{
					ScreenManager.MenuChange.Play();
				}

				//run the sleected evetn
				MenuEntries[SelectedEntry].OnRightEntry();

				ResetInputTimer();
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
						m_SelectedEntry = i;
						FireMenuSelectEvent(PlayerIndex.One, MenuEntries[i]);
						break;
					}
				}
			}
		}

		/// <summary>
		/// User hit the "menu select" button.
		/// </summary>
		/// <param name="playerIndex"></param>
		protected virtual void MenuSelect(PlayerIndex playerIndex)
		{
			if (MenuEntries.Count >= 1)
			{
				FireMenuSelectEvent(playerIndex, MenuEntries[SelectedEntry]);
			}
		}

		/// <summary>
		/// a menu entry was selected and we want to fire it's select event
		/// </summary>
		/// <param name="playerIndex"></param>
		/// <param name="entry"></param>
		private void FireMenuSelectEvent(PlayerIndex playerIndex, MenuEntry entry)
		{
			if (!QuietMenu)
			{
				//play menu noise
				ScreenManager.MenuSelect.Play();
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
				bool isSelected = IsActive && (i == SelectedEntry);
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
			Vector2 entryPos = new Vector2(MenuEntryPositionX(), MenuOptionOffset + (Resolution.TitleSafeArea.Center.Y * 0.9f));

			ScreenManager.SpriteBatchBegin();

			// Draw each menu entry in turn.
			for (int i = 0; i < MenuEntries.Count; i++)
			{
				//If we aren't doing cascading menus, use the menu position
				if (!CascadeMenuEntries)
				{
					entryPos = MenuEntries[i].Position;
				}

				//Draw the menu option
				bool isSelected = IsActive && (i == SelectedEntry);
				MenuEntries[i].Draw(this, entryPos, isSelected, MenuClock);

				//update the menu entry Y position
				entryPos.Y += (MenuEntries[i].GetHeight(this));
			}

			DrawMenuTitle(ScreenName, TitleScale);

			ScreenManager.SpriteBatchEnd();
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
		public float MenuEntryPositionX()
		{
			return XPositionWithOffset((float)Resolution.TitleSafeArea.Center.X);
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