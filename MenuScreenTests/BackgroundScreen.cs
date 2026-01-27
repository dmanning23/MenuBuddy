using FontBuddyLib;
using MenuBuddy;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ResolutionBuddy;
using System.Threading.Tasks;

namespace MenuScreenTests
{
	/// <summary>
	/// The background screen sits behind all the other menu screens.
	/// It draws a background image that remains fixed in place regardless
	/// of whatever transitions the screens on top of it may be doing.
	/// </summary>
	internal class BackgroundScreen : Screen
	{
		#region Properties

		private readonly FontBuddy _dannobotText;

		private readonly RainbowTextBuddy _titleText;

		#endregion //Properties

		#region Methods

		/// <summary>
		/// Constructor.
		/// </summary>
		public BackgroundScreen()
		{
			Transition.OnTime = 0.5f;
			Transition.OffTime = 0.5f;

			_titleText = new RainbowTextBuddy();
			_dannobotText = new FontBuddy();
		}

		/// <summary>
		/// Loads graphics content for this screen. The background texture is quite
		/// big, so we use our own local ContentManager to load it. This allows us
		/// to unload before going from the menus into the game itself, wheras if we
		/// used the shared ContentManager provided by the Game class, the content
		/// would remain loaded forever.
		/// </summary>
		public override async Task LoadContent()
		{
			await base.LoadContent();

			_titleText.LoadContent(Content, @"Fonts\ArialBlack48");
			_titleText.ShadowOffset = new Vector2(-5.0f, 3.0f);
			_titleText.ShadowSize = 1.025f;
			_titleText.RainbowSpeed = 4.0f;

			_dannobotText.LoadContent(Content, @"Fonts\ArialBlack24");
		}

		/// <summary>
		/// Unloads graphics content for this screen.
		/// </summary>
		public override void UnloadContent()
		{
		}

		/// <summary>
		/// Draws the background screen.
		/// </summary>
		public override void Draw(GameTime gameTime)
		{
			SpriteBatch spriteBatch = ScreenManager.SpriteBatch;

			ScreenManager.SpriteBatchBegin();

			Rectangle screenFullRect = ResolutionBuddy.Resolution.ScreenArea;

			//Draw the game title!
			_titleText.ShadowColor = new Color(0.15f, 0.15f, 0.15f, Transition.Alpha);
			_titleText.Write("MenuBuddySample!!!",
							new Vector2(Resolution.TitleSafeArea.Center.X, Resolution.TitleSafeArea.Center.Y * 0.05f),
			                Justify.Center,
			                1.5f,
			                new Color(0.85f, 0.85f, 0.85f, Transition.Alpha),
			                spriteBatch,
			                Time);

			//draw "dannobot games"
			_dannobotText.Write("@DannobotGames",
							   new Vector2((Resolution.TitleSafeArea.Right * 0.97f),
										   ((Resolution.TitleSafeArea.Bottom) - (_dannobotText.MeasureString("@DannobotGames").Y * 0.65f))),
			                   Justify.Right,
			                   0.5f,
			                   new Color(0.85f, 0.85f, 0.85f, Transition.Alpha),
			                   spriteBatch,
							   Time);

			ScreenManager.SpriteBatchEnd();
		}

		#endregion //Methods
	}
}