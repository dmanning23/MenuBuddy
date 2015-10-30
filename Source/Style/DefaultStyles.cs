using FontBuddyLib;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

namespace MenuBuddy
{
	/// <summary>
	/// Static storage of SpriteFont objects and colors for use throughout the game.
	/// This class is accessed via a singleton interface.
	/// </summary>
	public class DefaultStyles
	{
		#region Singleton

		protected static DefaultStyles _instance;

		public static DefaultStyles Instance()
		{
			Debug.Assert(null != _instance);
			return _instance;
		}

		public static void Init(Game game)
		{
			var inst = new DefaultStyles(game);
			inst.Initialize();
			_instance = inst;
		}


		public static void InitUnitTests()
		{
			_instance = new DefaultStyles();
			_instance.MainStyle = new StyleSheet
			{
				Name = "MainStyle",
				Transition = TransitionType.SlideLeft
			};
		}

		#endregion //Singleton

		#region Fields

		protected Game _game;

		#endregion //Fields

		#region Properties

		public static string MenuTitleFontName { protected get; set; }
		public static string MenuEntryFontName { protected get; set; }
		public static string MessageBoxFontName { protected get; set; }
		public static string MenuSelectSoundName { protected get; set; }
		public static string MenuChangeSoundName { protected get; set; }
		public static string MessageBoxBackground { protected get; set; }
		public static string ButtonBackground { protected get; set; }

		public StyleSheet MainStyle { get; set; }
		public StyleSheet MenuTitleStyle { get; private set; }
		public StyleSheet MenuEntryStyle { get; set; }
		public StyleSheet MessageBoxStyle { get; private set; }

		#endregion //Properties

		#region Methods

		protected DefaultStyles(Game game)
		{
			_game = game;
		}

		/// <summary>
		/// default constructor used for unit tests
		/// </summary>
		protected DefaultStyles()
		{
		}

		protected void Initialize()
		{
			InitMainStyle();

			InitMenuEntryStyle();

			InitMessageBoxTyle();

			InitMenuTitleStyle();
        }

		protected virtual void InitMainStyle()
		{
			//set the main style
			MainStyle = new StyleSheet
			{
				Name = "MainStyle",
				Transition = TransitionType.SlideLeft
			};

			//load the selected text stuff
			var pulsate = new PulsateBuddy();
			pulsate.ShadowOffset = Vector2.Zero;
			pulsate.ShadowSize = 1.0f;
			pulsate.Font = _game.Content.Load<SpriteFont>(MenuEntryFontName);
			MainStyle.SelectedFont = pulsate;
			MainStyle.SelectedTextColor = Color.Red;
			MainStyle.SelectedShadowColor = Color.Black;

			//load unselected text stuff
			var shadow = new ShadowTextBuddy();
			shadow.ShadowSize = 1.0f;
			shadow.ShadowOffset = Vector2.Zero;
			shadow.Font = _game.Content.Load<SpriteFont>(MenuEntryFontName);
			MainStyle.UnselectedFont = shadow;
			MainStyle.UnselectedTextColor = Color.White;
			MainStyle.UnselectedShadowColor = Color.Black;

			//set the outline and button background colors
			MainStyle.SelectedOutlineColor = new Color(0.8f, 0.8f, 0.8f, 0.7f);
			MainStyle.SelectedBackgroundColor = new Color(0.0f, 0.0f, 0.2f, 0.7f);
			MainStyle.UnselectedOutlineColor = new Color(0.8f, 0.8f, 0.8f, 0.5f);
			MainStyle.UnselectedBackgroundColor = new Color(0.0f, 0.0f, 0.2f, 0.5f);

			//load the sound effects
			MainStyle.SelectedSoundEffect = _game.Content.Load<SoundEffect>(MenuSelectSoundName);
			MainStyle.SelectionChangeSoundEffect = _game.Content.Load<SoundEffect>(MenuChangeSoundName);
		}

		protected virtual void InitMenuEntryStyle()
		{
			//set the menu entry style
			MenuEntryStyle = new StyleSheet(MainStyle)
			{
				Name = "MenuEntryStyle",
				BackgroundImage = _game.Content.Load<Texture2D>(ButtonBackground)
			};
		}

		protected virtual void InitMessageBoxTyle()
		{
			//set the messagebox style
			MessageBoxStyle = new StyleSheet(MenuEntryStyle)
			{
				Name = "MessageBoxStyle"
			};

			var pulsate = new PulsateBuddy();
			pulsate.ShadowSize = 1.0f;
			pulsate.ShadowOffset = Vector2.Zero;
			pulsate.PulsateSize *= 0.5f;
			pulsate.Font = _game.Content.Load<SpriteFont>(MessageBoxFontName);
			MessageBoxStyle.SelectedFont = pulsate;

			var shadow = new ShadowTextBuddy();
			shadow.ShadowSize = 1.0f;
			shadow.ShadowOffset = Vector2.Zero;
			shadow.Font = _game.Content.Load<SpriteFont>(MessageBoxFontName);
			MessageBoxStyle.UnselectedFont = shadow;

			MessageBoxStyle.BackgroundImage = _game.Content.Load<Texture2D>(MessageBoxBackground);
		}

		protected virtual void InitMenuTitleStyle()
		{
			//set the menu title style
			MenuTitleStyle = new StyleSheet(MainStyle);
			MenuTitleStyle.SelectedFont = new FontBuddy();
			MenuTitleStyle.SelectedFont.Font = _game.Content.Load<SpriteFont>(MenuTitleFontName);
			MenuTitleStyle.SelectedTextColor = Color.White;
			MenuTitleStyle.UnselectedFont = new FontBuddy();
			MenuTitleStyle.UnselectedFont.Font = _game.Content.Load<SpriteFont>(MenuTitleFontName);
			MenuTitleStyle.UnselectedTextColor = Color.White;
			MenuTitleStyle.Transition = TransitionType.PopTop;
		}

		#endregion //Methods
	}
}
