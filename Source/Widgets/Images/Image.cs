using GameTimer;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MenuBuddy
{
	/// <summary>
	/// An image that is displayed onteh screen
	/// </summary>
	public class Image : Widget, IImage
	{
		#region Properties

		public Texture2D Texture
		{
			get { return Style.Texture; }
			set
			{
				Style.Texture = value;
				Rect = new Rectangle(Rect.X, Rect.Y, Style.Texture.Width, Style.Texture.Height);
			}
		}

		public override Rectangle Rect
		{
			get
			{
				return base.Rect;
			}
			set
			{
				Vector2 size;
				if (FillRect)
				{
					size = new Vector2(value.Width, value.Height);
				}
				else
				{
					size = new Vector2(Style.Texture.Width, Style.Texture.Height);
					size *= Scale;
					size += Padding * 2;
				}
				base.Rect = new Rectangle(value.X, value.Y, (int)size.X, (int)size.Y);
			}
		}

		public bool FillRect { get; set; }

		#endregion //Properties

		#region Methods

		/// <summary>
		/// constructor!
		/// </summary>
		/// <param name="style"></param>
		public Image(StyleSheet style)
			: base(style)
		{
			FillRect = false;
		}

		/// <summary>
		/// constructor!
		/// </summary>
		public Image(StyleSheet style, Texture2D texture)
			: this(style)
		{
			Texture = texture;
		}

		public override void Update(IScreen screen, GameClock gameTime)
		{
		}

		public override void Draw(IScreen screen, GameClock gameTime)
		{
			if (!ShouldDraw(screen))
			{
				return;
			}

			//get the transition color
			var color = screen.Transition.AlphaColor(Color.White);

			//Get the transition location
			var pos = DrawPosition(screen);

			var width = Texture.Width * Scale;
			var height = Texture.Height * Scale;
			if (FillRect)
			{
				width = Rect.Width;
				height = Rect.Height;
			}

			//draw the item with all the correct parameters
			screen.ScreenManager.SpriteBatch.Draw(Texture, new Rectangle((int)pos.X, (int)pos.Y, (int)width, (int)height), color);
		}

		#endregion //Methods
	}
}