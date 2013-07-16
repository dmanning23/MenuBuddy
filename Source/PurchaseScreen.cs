using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.GamerServices;

namespace MenuBuddy
{
	/// <summary>
	/// This is a special screen that forces the player to buy or exit the game
	/// </summary>
	class PurchaseScreen : MessageBoxScreen
	{
		#region Members

		#endregion //Members

		#region Initialization

		/// <summary>
		/// Constructor fills in the menu contents.
		/// </summary>
		public PurchaseScreen() : base("Thank you for playing the Opposites trial!\nPlease buy the full version to continue playing.", true)
		{
		}

		#endregion //Initialization

		#region Methods

		public override void LoadContent()
		{
			//First check the receipts
		}

		/// <summary>
		/// Updates the menu.
		/// </summary>
		public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
		{
			base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);
		}

		#endregion //Methods

		#region Handle Input

		#endregion //Handle Input
	}
}