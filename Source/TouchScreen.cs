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
	public abstract class TouchScreen : MenuScreen
	{
		#region Initialization

		/// <summary>
		/// Constructor.
		/// </summary>
		protected TouchScreen(string title)
			: base(title)
		{
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
			for (int i = 0; i < MenuEntries.Count; i++)
			{
				MenuEntries[i].Draw(this, Font, MenuClock);
			}

			DrawMenuTitle(ScreenName, 1.0f, gameTime);

			ScreenManager.SpriteBatchEnd();
		}

		#endregion
	}
}