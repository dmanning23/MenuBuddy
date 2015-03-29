using FontBuddyLib;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;

namespace MenuBuddy
{
	/// <summary>
	/// Static storage of SpriteFont objects and colors for use throughout the game.
	/// </summary>
	public class DefaultStyles : GameComponent, IDefaultStyles
	{
		#region Properties

		public string MenuTitleFontName { protected get; set; }
		public string MenuEntryFontName { protected get; set; }
		public string MessageBoxFontName { protected get; set; }
		public string MenuSelectSoundName { protected get; set; }
		public string MenuChangeSoundName { protected get; set; }
		public string MessageBoxBackground { protected get; set; }
		public string ButtonBackground { protected get; set; }

		public StyleSheet MainStyle { get; private set; }
		public StyleSheet MenuTitleStyle { get; private set; }
		public StyleSheet MenuEntryStyle { get; private set; }
		public StyleSheet MessageBoxStyle { get; private set; }

		#endregion //Properties

		#region Methods

		public DefaultStyles(Game game)
			: base(game)
		{
			//Register ourselves to implement the DI container service.
			game.Components.Add(this);
			game.Services.AddService(typeof(IDefaultStyles), this);
		}

		public override void Initialize()
		{
			base.Initialize();

			//set the main style
			MainStyle = new StyleSheet();
			MainStyle.Transition = TransitionType.SlideLeft;

			//load the selected text stuff
			var pulsate = new PulsateBuddy();
			pulsate.ShadowOffset = Vector2.Zero;
			pulsate.ShadowSize = 1.0f;
			pulsate.Font = Game.Content.Load<SpriteFont>(MenuEntryFontName);
			MainStyle.SelectedFont = pulsate;
			MainStyle.SelectedTextColor = Color.Red;
			MainStyle.SelectedShadowColor = Color.Black;

			//load unselected text stuff
			var shadow = new ShadowTextBuddy();
			shadow.ShadowSize = 1.0f;
			shadow.ShadowOffset = Vector2.Zero;
			shadow.Font = Game.Content.Load<SpriteFont>(MenuEntryFontName);
			MainStyle.UnselectedFont = shadow;
			MainStyle.UnselectedTextColor = Color.White;
			MainStyle.UnselectedShadowColor = Color.Black;

			//set the outline and button background colors
			MainStyle.SelectedOutlineColor = new Color(0.8f, 0.8f, 0.8f, 0.7f);
			MainStyle.SelectedBackgroundColor = new Color(0.0f, 0.0f, 0.2f, 0.7f);
			MainStyle.UnselectedOutlineColor = new Color(0.8f, 0.8f, 0.8f, 0.5f);
			MainStyle.UnselectedBackgroundColor = new Color(0.0f, 0.0f, 0.2f, 0.5f);

			//load the sound effects
			MainStyle.SelectedSoundEffect = Game.Content.Load<SoundEffect>(MenuSelectSoundName);
			MainStyle.SelectionChangeSoundEffect = Game.Content.Load<SoundEffect>(MenuChangeSoundName);

			//set the menu entry style
			MenuEntryStyle = new StyleSheet(MainStyle);
			MenuEntryStyle.Texture = Game.Content.Load<Texture2D>(ButtonBackground);

			//set the messagebox style
			MessageBoxStyle = new StyleSheet(MenuEntryStyle);
			pulsate = new PulsateBuddy();
			pulsate.ShadowSize = 1.0f;
			pulsate.ShadowOffset = Vector2.Zero;
			pulsate.PulsateSize *= 0.25f;
			pulsate.Font = Game.Content.Load<SpriteFont>(MessageBoxFontName);
			MessageBoxStyle.SelectedFont = pulsate;

			shadow = new ShadowTextBuddy();
			shadow.ShadowOffset = Vector2.Zero;
			shadow.Font = Game.Content.Load<SpriteFont>(MessageBoxFontName);
			MessageBoxStyle.UnselectedFont = shadow;

			MessageBoxStyle.Texture = Game.Content.Load<Texture2D>(MessageBoxBackground);

			//set the menu title style
			MenuTitleStyle = new StyleSheet(MainStyle);
			MenuTitleStyle.SelectedFont = new ShadowTextBuddy();
			MenuTitleStyle.SelectedFont.Font = Game.Content.Load<SpriteFont>(MenuTitleFontName);
			MenuTitleStyle.SelectedTextColor = Color.White;
			MenuTitleStyle.Transition = TransitionType.PopTop;
		}

		#endregion //Methods
	}
}
