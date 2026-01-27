using GameTimer;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MenuBuddy
{
	/// <summary>
	/// Background image is an image that is drawn behind all the button backgrounds and screen items
	/// </summary>
	public class BackgroundImage : Image
	{
		#region Initialization

		/// <summary>
		/// Initializes a new <see cref="BackgroundImage"/> with the specified texture, drawn at layer -1.
		/// </summary>
		/// <param name="texture">The texture to display as a background.</param>
		public BackgroundImage(Texture2D texture)
			: base(texture)
		{
			Layer = -1.0f;
			PulsateOnHighlight = false;
			HasBackground = true;
		}

		/// <summary>
		/// Initializes a new <see cref="BackgroundImage"/> by copying values from an existing instance.
		/// </summary>
		/// <param name="inst">The background image to copy from.</param>
		public BackgroundImage(BackgroundImage inst) : base(inst)
		{
		}

		#endregion //Initialization

		#region Methods

		/// <summary>
		/// Draws this image during the background pass by delegating to the base <see cref="Image.Draw"/> method.
		/// </summary>
		/// <param name="screen">The screen this image belongs to.</param>
		/// <param name="gameTime">The current game time.</param>
		public override void DrawBackground(IScreen screen, GameClock gameTime)
		{
			//Draw the image as the background item
			base.Draw(screen, gameTime);
		}

		/// <summary>
		/// Overridden to do nothing. This image is drawn during the background pass instead.
		/// </summary>
		/// <param name="screen">The screen this image belongs to.</param>
		/// <param name="gameTime">The current game time.</param>
		public override void Draw(IScreen screen, GameClock gameTime)
		{
			//Do nothing when this item is asked to "draw"
		}

		#endregion //Methods
	}
}