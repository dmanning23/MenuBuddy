using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MenuBuddy
{
	/// <summary>
	/// The main menu screen is the first thing displayed when the game starts up.
	/// </summary>
	public class MenuScreenWithBackground : MenuScreen
	{
		#region Members

		/// <summary>
		/// The image file to use as background
		/// </summary>
		protected string ImageResource { get; set; }

		/// <summary>
		/// The texture loaded from the image file
		/// </summary>
		protected Texture2D Background { get; set; }

		#endregion //Members

		#region Initialization

		/// <summary>
		/// Constructor fills in the menu contents.
		/// </summary>
		public MenuScreenWithBackground(string menuTitle, string backgroundImage)
			: base(menuTitle)
		{
			ImageResource = backgroundImage;
		}

		#endregion //Initialization

		#region Methods

		public override void LoadContent()
		{
			Background = ScreenManager.Game.Content.Load<Texture2D>(ImageResource);
		}

		/// <summary>
		/// Draws the menu.
		/// </summary>
		public override void Draw(GameTime gameTime)
		{
			//draw the background image
			ScreenManager.SpriteBatchBegin();
			ScreenManager.SpriteBatch.Draw(Background, ResolutionBuddy.Resolution.ScreenArea, Color.White);
			ScreenManager.SpriteBatchEnd();

			base.Draw(gameTime);
		}

		#endregion //Methods
	}
}