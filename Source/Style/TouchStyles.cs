using FontBuddyLib;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MenuBuddy
{
	/// <summary>
	/// Static storage of SpriteFont objects and colors for use throughout the game.
	/// </summary>
	public class TouchStyles : DefaultStyles
	{
		public TouchStyles(Game game) : base(game)
		{
		}

		public override void Initialize()
		{
			base.Initialize();

			MenuEntryStyle.HasBackground = true;
			MenuEntryStyle.HasOutline = true;
			MenuEntryStyle.SelectedTextColor = MenuEntryStyle.UnselectedTextColor;


			//load the selected text stuff
			var shadow = new ShadowTextBuddy();
			shadow.ShadowSize = 1.0f;
			shadow.ShadowOffset = new Vector2(7.0f, 7.0f);
			shadow.Font = Game.Content.Load<SpriteFont>(MenuEntryFontName);
			MenuEntryStyle.SelectedFont = shadow;
			MenuEntryStyle.SelectedTextColor = Color.White;
			MenuEntryStyle.SelectedShadowColor = Color.Black;

			//load unselected text stuff
			MenuEntryStyle.UnselectedFont = MenuEntryStyle.SelectedFont;
			MenuEntryStyle.UnselectedTextColor = MenuEntryStyle.SelectedTextColor;
			MenuEntryStyle.UnselectedShadowColor = MenuEntryStyle.SelectedShadowColor;

			//set the messagebox style
			shadow = new ShadowTextBuddy();
			shadow.ShadowSize = 1.0f;
			shadow.ShadowOffset = new Vector2(7.0f, 7.0f);
			shadow.Font = Game.Content.Load<SpriteFont>(MessageBoxFontName);
			MessageBoxStyle.SelectedFont = shadow;
			MessageBoxStyle.UnselectedFont = MessageBoxStyle.SelectedFont;
			MessageBoxStyle.UnselectedTextColor = MessageBoxStyle.SelectedTextColor;
			MessageBoxStyle.UnselectedShadowColor = MessageBoxStyle.SelectedShadowColor;
		}
	}
}
