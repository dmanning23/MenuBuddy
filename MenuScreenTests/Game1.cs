//#define MOUSE
//#define TOUCH

using InputHelper;
using MenuBuddy;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace MenuScreenTests
{
	/// <summary>
	/// This is the main type for your game
	/// </summary>
#if TOUCH
	public class Game1 : TouchGame
#elif MOUSE
	public class Game1 : MouseGame
#else
	public class Game1 : ControllerGame
#endif
	{
		#region Methods

		public Game1()
		{
#if MOUSE
			IsMouseVisible = true;
#endif
		}

		protected override void InitStyles()
		{
			base.InitStyles();
			StyleSheet.HasOutline = true;
		}

		/// <summary>
		/// Get the set of screens needed for the main menu
		/// </summary>
		/// <returns>The gameplay screen stack.</returns>
		public override IScreen[] GetMainMenuScreenStack()
		{
			return new IScreen[] { new BackgroundScreen(), new MainMenuScreen() };
		}

		protected override void Update(GameTime gameTime)
		{
			try
			{
				base.Update(gameTime);
			}
			catch (Exception ex)
			{
				ScreenManager.ErrorScreen(ex);
			}
		}

		protected override void Draw(GameTime gameTime)
		{
			try
			{
				base.Draw(gameTime);
			}
			catch (Exception ex)
			{
				ScreenManager.ErrorScreen(ex);
			}
		}

#endregion //Methods
	}
}

