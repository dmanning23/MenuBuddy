using FontBuddyLib;
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
	public abstract class TouchScreen : GameScreen
	{
		#region Properties

		/// <summary>
		/// Gets the list of menu entries, so derived classes can add or change the menu contents.
		/// </summary>
		protected IList<TouchEntry> Entries { get; private set; }

		/// <summary>
		/// Set this to true and the menu won't play any noises when things are selected etc.
		/// </summary>
		public bool QuietMenu { get; set; }

		public FontBuddy Font { get; set; }

		/// <summary>
		/// Gets or sets the menu clock.
		/// </summary>
		/// <value>The menu clock.</value>
		protected GameClock MenuClock { get; private set; }

		#endregion

		#region Initialization

		/// <summary>
		/// Constructor.
		/// </summary>
		protected TouchScreen(string title)
			: base(title)
		{
			this.
			QuietMenu = false;

			TransitionOnTime = TimeSpan.FromSeconds(0.75);
			TransitionOffTime = TimeSpan.FromSeconds(0.5);
			Font = new FontBuddy();
			Entries = new List<TouchEntry>();
			MenuClock = new GameClock();
			MenuClock.Start();
		}

		public override void LoadContent()
		{
			Font.Font = ScreenManager.MessageBoxFont;
			base.LoadContent();
		}

		#endregion

		#region Handle Input

		/// <summary>
		/// Responds to user input, changing the selected entry and accepting
		/// or cancelling the menu.
		/// </summary>
		public override void HandleInput(InputState input, GameTime rGameTime)
		{
			CheckForMouseClick();
			CheckForTouch();
		}

		protected void CheckForMouseClick()
		{
			//did the user click somewhere?
			if (ScreenManager.InputState.LMouseClick)
			{
				//ok find which menu entry was clicked
				var mousePos = ScreenManager.MousePos;
				for (int i = 0; i < Entries.Count; i++)
				{
					if (Entries[i].ButtonRect.Contains(mousePos))
					{
						FireMenuSelectEvent(PlayerIndex.One, Entries[i]);
						break;
					}
				}
			}
		}

		protected void CheckForTouch()
		{
			if (null != ScreenManager.Touch)
			{
				foreach (Vector2 tapPos in ScreenManager.Touch.Taps)
				{
					for (int i = 0; i < Entries.Count; i++)
					{
						if (Entries[i].ButtonRect.Contains(tapPos))
						{
							FireMenuSelectEvent(PlayerIndex.One, Entries[i]);
							break;
						}
					}
				}
			}
		}


		/// <summary>
		/// a menu entry was selected and we want to fire it's select event
		/// </summary>
		/// <param name="playerIndex"></param>
		/// <param name="entry"></param>
		private void FireMenuSelectEvent(PlayerIndex playerIndex, TouchEntry entry)
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
		/// Draws the menu.
		/// </summary>
		public override void Draw(GameTime gameTime)
		{
			//update the menu clock
			MenuClock.Update(gameTime);

			ScreenManager.SpriteBatchBegin();

			// Draw each menu entry in turn.
			for (int i = 0; i < Entries.Count; i++)
			{
				Entries[i].Draw(this, Font, MenuClock);
			}

			DrawMenuTitle(ScreenName, 1.0f, gameTime);

			ScreenManager.SpriteBatchEnd();
		}

		#endregion
	}
}