using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using HadoukInput;

namespace MenuBuddy
{
	/// <summary>
	/// Base class for screens that contain a menu of options. The user can
	/// move up and down to select an entry, or cancel to back out of the screen.
	/// </summary>
	public abstract class MenuScreen : GameScreen
	{
		#region Fields

		private List<MenuEntry> m_listMenuEntries = new List<MenuEntry>();

		protected int m_iSelectedEntry = 0;

		private string m_strMenuTitle;

		#endregion

		#region Properties

		/// <summary>
		/// Gets the list of menu entries, so derived classes can add
		/// or change the menu contents.
		/// </summary>
		protected IList<MenuEntry> MenuEntries
		{
			get { return m_listMenuEntries; }
		}

		/// <summary>
		/// Gets or sets the menu title position.
		/// </summary>
		public string MenuTitle
		{
			get { return m_strMenuTitle; }
			set { m_strMenuTitle = value; }
		}

		#endregion

		#region Initialization

		/// <summary>
		/// Constructor.
		/// </summary>
		public MenuScreen(string strMenuTitle)
		{
			this.m_strMenuTitle = strMenuTitle;
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
				OnSelectEntry(m_iSelectedEntry, playerIndex);

				//TODO: play menu noise
				//SPFLib.CAudioManager.PlayCue("menu select");
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
			if (m_listMenuEntries.Count > 1)
			{
				m_iSelectedEntry--;

				if (m_iSelectedEntry < 0)
				{
					m_iSelectedEntry = m_listMenuEntries.Count - 1;
				}

				//TODO: play menu noise
				//SPFLib.CAudioManager.PlayCue("menu move");
				TimeSinceInput = 0.0;
				PrevTimeSinceInput = 0.0f;
			}
		}

		protected virtual void MenuDown()
		{
			if (m_listMenuEntries.Count > 1)
			{
				m_iSelectedEntry++;

				if (m_iSelectedEntry >= m_listMenuEntries.Count)
				{
					m_iSelectedEntry = 0;
				}

				//TODO: play menu noise
				//SPFLib.CAudioManager.PlayCue("menu move");
				TimeSinceInput = 0.0;
				PrevTimeSinceInput = 0.0f;
			}
		}

		/// <summary>
		/// Handler for when the user has chosen a menu entry.
		/// </summary>
		protected virtual void OnSelectEntry(int entryIndex, PlayerIndex playerIndex)
		{
			m_listMenuEntries[m_iSelectedEntry].OnSelectEntry(playerIndex);
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
			for (int i = 0; i < m_listMenuEntries.Count; i++)
			{
				bool isSelected = IsActive && (i == m_iSelectedEntry);
				m_listMenuEntries[i].Update(this, isSelected, gameTime);
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
			SpriteBatch spriteBatch = ScreenManager.SpriteBatch;
			SpriteFont font = ScreenManager.Font;

			// Make the menu slide into place during transitions, using a
			// power curve to make things look more interesting (this makes
			// the movement slow down as it nears the end).
			float transitionOffset = (float)Math.Pow(TransitionPosition, 2);

			spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.NonPremultiplied);

			float fMenuPositionY = ScreenRect.Center.Y;

			// Draw each menu entry in turn.
			for (int i = 0; i < m_listMenuEntries.Count; i++)
			{
				MenuEntry menuEntry = m_listMenuEntries[i];

				//TODO: switch this all to FontBuddy

				//Get the menu entry X position
				float fMenuPositionX = ScreenRect.Center.X;
				if (ScreenState == EScreenState.TransitionOn)
				{
					fMenuPositionX -= transitionOffset * 256;
				}
				else
				{
					fMenuPositionX += transitionOffset * 512;
				}

				//Draw the menu option
				Vector2 position = new Vector2(fMenuPositionX, fMenuPositionY);
				bool isSelected = IsActive && (i == m_iSelectedEntry);
				menuEntry.Draw(this, position, isSelected, gameTime);

				//update the menu entry Y position
				fMenuPositionY += (menuEntry.GetHeight(this));
			}

			DrawMenuTitle(m_strMenuTitle, 1.0f);

			spriteBatch.End();
		}

		#endregion
	}
}