using GameTimer;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MenuBuddy
{
	/// <summary>
	/// Helper class represents a single entry in a touch screen. 
	/// </summary>
	public class ImageButton : Button
	{
		#region Properties

		public override Point Position
		{
			get { return base.Position; }
			set
			{
				base.Position = value;

				//set the image position
				var center = new Point(
					Rect.Center.X - (Image.Texture.Bounds.Width / 2),
					Rect.Center.Y - (Image.Texture.Bounds.Height / 2));
				Image.Position = center;
			}
		}

		/// <summary>
		/// The image to draw on this button
		/// </summary>
		public Image Image { get; set; }

		/// <summary>
		/// You can set this flag to prevent drawing this button when the screen is inactive.
		/// </summary>
		public bool DrawWhenInactive { get; set; }

		#endregion

		#region Initialization

		/// <summary>
		/// Constructs a new menu entry with the specified text.
		/// </summary>
		public ImageButton(StyleSheet style, string text)
			: base(style, text)
		{
			DrawWhenInactive = true;
			Image = new Image(style);
		}

		/// <summary>
		/// Constructs a new menu entry with the specified text.
		/// </summary>
		public ImageButton(StyleSheet style, string text, Texture2D image)
			: this(style, text)
		{
			Image.Texture = image;
		}

		#endregion

		#region Update and Draw

		public override void DrawBackground(IScreen screen, GameClock gameTime)
		{
			//check if we don't want to draw this button when inactive
			if (!DrawWhenInactive && !screen.IsActive)
			{
				return;
			}

			base.DrawBackground(screen, gameTime);
		}

		public override void Draw(IScreen screen, GameClock gameTime)
		{
			//check if we don't want to draw this button when inactive
			if (!DrawWhenInactive && !screen.IsActive)
			{
				return;
			}

			base.Draw(screen, gameTime);

			//Draw the image
			Image.Draw(screen, gameTime);
		}

		#endregion
	}
}