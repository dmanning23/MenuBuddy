using FontBuddyLib;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

namespace MenuBuddy
{
	public class StyleSheet
	{
		#region Singleton

		protected static StyleSheet _instance;

		public static StyleSheet Instance()
		{
			Debug.Assert(null != _instance);
			return _instance;
		}

		public static void Init(Game game)
		{
			_instance = new StyleSheet(game);
			_instance.Initialize();
		}

		public static void InitUnitTests()
		{
			_instance = new StyleSheet();
		}

		#endregion //Singleton

		#region Default Options

		/// <summary>
		/// The resource to use as the large font (menu title)
		/// </summary>
		public static string LargeFontResource { get; set; }

		/// <summary>
		/// the resource to use as the medium font (widgets)
		/// </summary>
		public static string MediumFontResource { get; set; }

		/// <summary>
		/// the resource to use as the small font (message boxes)
		/// </summary>
		public static string SmallFontResource { get; set; }

		public static Color NeutralTextColor { get; set; }

		public static Color HighlightedTextColor { get; set; }

		public static Color SelectedTextColor { get; set; }

		public static Color NeutralOutlineColor { get; set; }

		public static Color NeutralBackgroundColor { get; set; }

		public static Color HighlightedOutlineColor { get; set; }

		public static Color HighlightedBackgroundColor { get; set; }

		public static Color TextShadowColor { get; set; }

		public static string HighlightedSoundResource { get; set; }

		public static string ClickedSoundResource { get; set; }

		public static string ButtonBackgroundImageResource { get; set; }

		public static string MessageBoxBackgroundImageResource { get; set; }

		public static string CancelButtonImageResource { get; set; }

		public static string TreeExpandImageResource { get; set; }

		public static string TreeCollapseImageResource { get; set; }

		public static string LoadingScreenHourglassImageResource { get; set; }

		public static bool HasOutline { get; set; }

		public static TransitionWipeType Transition { get; set; }

		#endregion //Default Options

		#region Fields

		protected Game _game;

		private IFontBuddy _neutralLargeFont;

		private IFontBuddy _neutralMediumFont;

		private IFontBuddy _neutralSmallFont;

		private IFontBuddy _highlightedLargeFont;

		private IFontBuddy _highlightedMediumFont;

		private IFontBuddy _highlightedSmallFont;

		#endregion //Fields

		#region Properties

		public IFontBuddy LargeNeutralFont
		{
			get { return _neutralLargeFont; }
			set
			{
				//set the font item
				_neutralLargeFont = value;

				//also set teh shadow color
				var shadow = _neutralLargeFont as ShadowTextBuddy;
				if (null != shadow)
				{
					shadow.ShadowColor = TextShadowColor;
				}
			}
		}

		public IFontBuddy MediumNeutralFont
		{
			get { return _neutralMediumFont; }
			set
			{
				//set the font item
				_neutralMediumFont = value;

				//also set teh shadow color
				var shadow = _neutralMediumFont as ShadowTextBuddy;
				if (null != shadow)
				{
					shadow.ShadowColor = TextShadowColor;
				}
			}
		}

		public IFontBuddy SmallNeutralFont
		{
			get { return _neutralSmallFont; }
			set
			{
				//set the font item
				_neutralSmallFont = value;

				//also set teh shadow color
				var shadow = _neutralSmallFont as ShadowTextBuddy;
				if (null != shadow)
				{
					shadow.ShadowColor = TextShadowColor;
				}
			}
		}

		public IFontBuddy LargeHighlightedFont
		{
			get { return _highlightedLargeFont; }
			set
			{
				//set the font item
				_highlightedLargeFont = value;

				//also set teh shadow color
				var shadow = _highlightedLargeFont as ShadowTextBuddy;
				if (null != shadow)
				{
					shadow.ShadowColor = TextShadowColor;
				}
			}
		}

		public IFontBuddy MediumHighlightedFont
		{
			get { return _highlightedMediumFont; }
			set
			{
				//set the font item
				_highlightedMediumFont = value;

				//also set teh shadow color
				var shadow = _highlightedMediumFont as ShadowTextBuddy;
				if (null != shadow)
				{
					shadow.ShadowColor = TextShadowColor;
				}
			}
		}

		public IFontBuddy SmallHighlightedFont
		{
			get { return _highlightedSmallFont; }
			set
			{
				//set the font item
				_highlightedSmallFont = value;

				//also set teh shadow color
				var shadow = _highlightedSmallFont as ShadowTextBuddy;
				if (null != shadow)
				{
					shadow.ShadowColor = TextShadowColor;
				}
			}
		}

		#endregion //Properties

		#region Methods

		static StyleSheet()
		{
			LargeFontResource = @"Fonts\ArialBlack72";
			MediumFontResource = @"Fonts\ArialBlack48";
			SmallFontResource = @"Fonts\ArialBlack24";

			NeutralTextColor = Color.White;
			HighlightedTextColor = Color.White;
			SelectedTextColor = Color.Yellow;
			NeutralOutlineColor = new Color(0.8f, 0.8f, 0.8f, 0.5f);
			NeutralBackgroundColor = new Color(0.0f, 0.0f, 0.2f, 0.5f);
			HighlightedOutlineColor = new Color(0.8f, 0.8f, 0.8f, 0.7f);
			HighlightedBackgroundColor = new Color(0.0f, 0.0f, 0.2f, 0.7f);
			TextShadowColor = Color.Black;
			HighlightedSoundResource = @"MenuMove";
			ClickedSoundResource = @"MenuSelect";
			ButtonBackgroundImageResource = @"AlphaGradient";
			MessageBoxBackgroundImageResource = @"gradient";
			CancelButtonImageResource = @"Cancel";
			TreeExpandImageResource = @"Expand";
			TreeCollapseImageResource = @"Collapse";
			LoadingScreenHourglassImageResource = @"hourglass";
			HasOutline = true;
			Transition = TransitionWipeType.SlideLeft;
		}

		/// <summary>
		/// Default constructor
		/// </summary>
		public StyleSheet(Game game)
		{
			_game = game;
		}

		public StyleSheet()
		{
		}

		protected void Initialize()
		{
			//set up neutral fonts
			LargeNeutralFont = new FontBuddy()
			{
				Font = _game.Content.Load<SpriteFont>(LargeFontResource)
			};

			MediumNeutralFont = new ShadowTextBuddy()
			{
				ShadowSize = 1.0f,
				ShadowOffset = new Vector2(7.0f, 7.0f),
				Font = _game.Content.Load<SpriteFont>(MediumFontResource)
			};

			SmallNeutralFont = new ShadowTextBuddy()
			{
				ShadowSize = 1.0f,
				ShadowOffset = new Vector2(4.0f, 4.0f),
				Font = _game.Content.Load<SpriteFont>(SmallFontResource),
			};

			//set up highlighted stuff
			LargeHighlightedFont = new PulsateBuddy()
			{
				Font = _game.Content.Load<SpriteFont>(LargeFontResource)
			};

			MediumHighlightedFont = new PulsateBuddy()
			{
				ShadowSize = 1.0f,
				ShadowOffset = new Vector2(7.0f, 7.0f),
				Font = _game.Content.Load<SpriteFont>(MediumFontResource)
			};

			SmallHighlightedFont = new PulsateBuddy()
			{
				ShadowSize = 1.0f,
				ShadowOffset = new Vector2(4.0f, 4.0f),
				Font = _game.Content.Load<SpriteFont>(SmallFontResource),
			};
		}

		#endregion //Methods
	}
}