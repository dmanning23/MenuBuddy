using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using HadoukInput;
using GameTimer;

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
		/// index of the currently selected menu entry
		/// </summary>
		private int m_SelectedEntry = 0;

		#endregion

		#region Properties

		/// <summary>
		/// Gets the list of menu entries, so derived classes can add
		/// or change the menu contents.
		/// </summary>
		protected IList<MenuEntry> MenuEntries { get; private set; }

		/// <summary>
		/// Gets or sets the menu title position.
		/// </summary>
		public string MenuTitle { get; set; }

		protected int SelectedEntry
		{
			get
			{
				return m_SelectedEntry;
			}
			set
			{
				//set teh selected menu item
				m_SelectedEntry = value;

				//reset the menu clock so the entries don't pop
				MenuClock.Start();
			}

		}

		/// <summary>
		/// Gets or sets the menu clock.
		/// </summary>
		/// <value>The menu clock.</value>
		private GameClock MenuClock { get; set; }

		#endregion

		#region Initialization

		/// <summary>
		/// Constructor.
		/// </summary>
		public MenuScreen(string strMenuTitle)
		{
			MenuEntries = new List<MenuEntry>();
			MenuClock = new GameClock();
			MenuClock.Start();
			MenuTitle = strMenuTitle;
			TimeSinceInput = 0.0;
			PrevTimeSinceInput = 0.0;

			TransitionOnTime = TimeSpan.FromSeconds(0.75);
			TransitionOffTime = TimeSpan.FromSeconds(0.5);
		}

		#endregion

		#region Handle Input

		/// <summary>
		/// Responds to user input, changing the selected entry and accepting
		/// or cancelling the menu.
		/// </summary>
		public override void HandleInput(InputState input, GameTime rGameTime)
		{
			// Move to the previous menu entry?
			if (input.IsMenuUp(ControllingPlayer))
			{
				MenuUp();
			}

			// Move to the next menu entry?
			if (input.IsMenuDown(ControllingPlayer))
			{
				MenuDown();
			}

			// Accept or cancel the menu? We pass in our ControllingPlayer, which may
			// either be null (to accept input from any player) or a specific index.
			// If we pass a null controlling player, the InputState helper returns to
			// us which player actually provided the input. We pass that through to
			// OnSelectEntry and OnCancel, so they can tell which player triggered them.
			PlayerIndex playerIndex;

			if (input.IsMenuSelect(ControllingPlayer, out playerIndex))
			{
				OnSelectEntry(SelectedEntry, playerIndex);

				//play menu noise
				ScreenManager.MenuSelect.Play();
				TimeSinceInput = 0.0;
				PrevTimeSinceInput = 0.0f;
			}
			else if (input.IsMenuCancel(ControllingPlayer, out playerIndex))
			{
				OnCancel(playerIndex);
			}
		}

		protected virtual void MenuUp()
		{
			if (MenuEntries.Count > 1)
			{
				SelectedEntry--;

				if (SelectedEntry < 0)
				{
					SelectedEntry = MenuEntries.Count - 1;
				}

				//play menu noise
				ScreenManager.MenuChange.Play();

				TimeSinceInput = 0.0;
				PrevTimeSinceInput = 0.0f;
			}
		}

		protected virtual void MenuDown()
		{
			if (MenuEntries.Count > 1)
			{
				SelectedEntry++;

				if (SelectedEntry >= MenuEntries.Count)
				{
					SelectedEntry = 0;
				}

				//play menu noise
				ScreenManager.MenuChange.Play();

				TimeSinceInput = 0.0;
				PrevTimeSinceInput = 0.0f;
			}
		}

		/// <summary>
		/// Handler for when the user has chosen a menu entry.
		/// </summary>
		protected virtual void OnSelectEntry(int entryIndex, PlayerIndex playerIndex)
		{
			MenuEntries[SelectedEntry].OnSelectEntry(playerIndex);
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
				PrevTimeSinceInput = TimeSinceInput;
				TimeSinceInput += gameTime.ElapsedGameTime.TotalMilliseconds;
			}
		}

		/// <summary>
		/// Draws the menu.
		/// </summary>
		public override void Draw(GameTime gameTime)
		{
			MenuClock.Update(gameTime);

			// Make the menu slide into place during transitions, using a
			// power curve to make things look more interesting (this makes
			// the movement slow down as it nears the end).
			float transitionOffset = (float)Math.Pow(TransitionPosition, 2);
			float fMenuPositionX = ScreenRect.Center.X;
			if (ScreenState == EScreenState.TransitionOn)
			{
				fMenuPositionX -= transitionOffset * 256;
			}
			else
			{
				fMenuPositionX += transitionOffset * 512;
			}
			float fMenuPositionY = ScreenRect.Center.Y;

			SpriteBatch spriteBatch = ScreenManager.SpriteBatch;
			spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.NonPremultiplied);

			// Draw each menu entry in turn.
			for (int i = 0; i < MenuEntries.Count; i++)
			{
				//Draw the menu option
				bool isSelected = IsActive && (i == SelectedEntry);
				MenuEntries[i].Draw(this, new Vector2(fMenuPositionX, fMenuPositionY), isSelected, MenuClock);

				//update the menu entry Y position
				fMenuPositionY += (MenuEntries[i].GetHeight(this));
			}

			DrawMenuTitle(MenuTitle, 1.0f);

			spriteBatch.End();
		}

		#endregion
	}
}