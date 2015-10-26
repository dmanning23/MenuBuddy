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
		#region Singleton

		public new static void Init(Game game)
		{
			var inst = new TouchStyles(game);
			inst.Initialize();
			_instance = inst;
		}

		#endregion //Singleton

		protected TouchStyles(Game game)
			: base(game)
		{
		}

		protected override void InitMenuEntryStyle()
		{
			base.InitMenuEntryStyle();

			MenuEntryStyle.HasBackground = true;
			MenuEntryStyle.HasOutline = true;
			MenuEntryStyle.SelectedTextColor = MenuEntryStyle.UnselectedTextColor;

			//load the selected text stuff
			var shadow = new ShadowTextBuddy();
			shadow.ShadowSize = 1.0f;
			shadow.ShadowOffset = new Vector2(7.0f, 7.0f);
			shadow.Font = _game.Content.Load<SpriteFont>(MenuEntryFontName);
			MenuEntryStyle.SelectedFont = shadow;
			MenuEntryStyle.SelectedTextColor = Color.White;
			MenuEntryStyle.SelectedShadowColor = Color.Black;

			//load unselected text stuff
			MenuEntryStyle.UnselectedFont = MenuEntryStyle.SelectedFont;
			MenuEntryStyle.UnselectedTextColor = MenuEntryStyle.SelectedTextColor;
			MenuEntryStyle.UnselectedShadowColor = MenuEntryStyle.SelectedShadowColor;
		}

		protected override void InitMessageBoxTyle()
		{
			base.InitMessageBoxTyle();

			//set the messagebox style
			MessageBoxStyle.HasBackground = true;
			MessageBoxStyle.HasOutline = true;
			MessageBoxStyle.SelectedTextColor = MenuEntryStyle.UnselectedTextColor;

			var shadow = new ShadowTextBuddy();
			shadow.ShadowSize = 1.0f;
			shadow.ShadowOffset = new Vector2(4.0f, 4.0f);
			shadow.Font = _game.Content.Load<SpriteFont>(MessageBoxFontName);
			MessageBoxStyle.SelectedFont = shadow;
			MessageBoxStyle.SelectedTextColor = Color.White;
			MessageBoxStyle.SelectedShadowColor = Color.Black;

			MessageBoxStyle.UnselectedFont = MessageBoxStyle.SelectedFont;
			MessageBoxStyle.UnselectedTextColor = MessageBoxStyle.SelectedTextColor;
			MessageBoxStyle.UnselectedShadowColor = MessageBoxStyle.SelectedShadowColor;
		}
	}
}
