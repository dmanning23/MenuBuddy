using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using FontBuddyLib;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MenuBuddy
{
	/// <summary>
	/// Static storage of SpriteFont objects and colors for use throughout the game.
	/// </summary>
	static public class MenuDefaults
	{
		#region Fonts

		public static string MenuTitleFontName { get; set; }
		public static string MenuEntrySelectedFontName { get; set; }
		public static string MenuEntryUnselectedFontName { get; set; }
		public static string MessageBoxFontName { get; set; }

		public static IFontBuddy MenuTitleFont { get; set; }
		public static IFontBuddy MenuEntrySelectedFont { get; set; }
		public static IFontBuddy MenuEntryUnselectedFont { get; set; }
		public static IFontBuddy MessageBoxFont { get; set; }

		#endregion

		#region Initialization

		/// <summary>
		/// Load the fonts from the content pipeline.
		/// </summary>
		public static void LoadContent(ContentManager contentManager)
		{
			// check the parameters
			Debug.Assert(null != contentManager);

			MenuTitleFont = new ShadowTextBuddy()
			{
				Font = contentManager.Load<SpriteFont>(MenuTitleFontName)
			};

			MenuEntrySelectedFont = new PulsateBuddy()
			{
				Font = contentManager.Load<SpriteFont>(MenuEntrySelectedFontName)
			};

			MenuEntryUnselectedFont = new ShadowTextBuddy()
			{
				Font = contentManager.Load<SpriteFont>(MenuEntryUnselectedFontName)
			};

			var pulsate = new PulsateBuddy();
			pulsate.PulsateSize *= 0.25f;
			MessageBoxFont = pulsate;
			MessageBoxFont.Font = contentManager.Load<SpriteFont>(MessageBoxFontName);
		}

		/// <summary>
		/// Release all references to the fonts.
		/// </summary>
		public static void UnloadContent()
		{
			MenuTitleFont = null;
			MenuEntrySelectedFont = null;
			MenuEntryUnselectedFont = null;
		}

		#endregion
	}
}
