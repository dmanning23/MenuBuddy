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
		#region Fields

		private Vector2 _size;
		private bool _fillRect;

		#endregion //Fields

		#region Properties

		public virtual int Width
		{
			get
			{
				return Texture.Width;
			}
		}

		public virtual int Height
		{
			get
			{
				return Texture.Height;
			}
		}

		public virtual Rectangle Bounds
		{
			get
			{
				return Texture.Bounds;
			}
		}

		public Vector2 Size
		{
			protected get
			{
				return _size;
			}
			set
			{
				if (_size != value)
				{
					_size = value;
					CalculateRect();
				}
			}
		}

		public Texture2D Texture
		{
			get { return Style.Texture; }
			set
			{
				Style.Texture = value;
				CalculateRect();
			}
		}

		public bool FillRect
		{
			get
			{
				return _fillRect;
            }
			set
			{
				if (_fillRect != value)
				{
					_fillRect = value;
					CalculateRect();
				}
			}
		}

		#endregion //Properties

		#region Methods

		/// <summary>
		/// constructor!
		/// </summary>
		public Image()
		{
			FillRect = false;
		}

		/// <summary>
		/// constructor!
		/// </summary>
		public Image(Texture2D texture)
			: this()
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

			//draw the item with all the correct parameters
			screen.ScreenManager.SpriteBatch.Draw(Texture, new Rectangle((int)pos.X, (int)pos.Y, Rect.Width, Rect.Height), color);
		}

		protected override void CalculateRect()
		{
			//get the size of the rect
			Vector2 size;
			if (FillRect)
			{
				size = Size;
			}
			else
			{
				size = new Vector2(Width, Height);
			}
			size = (size + (Padding * 2f)) * Scale;

			//set the x component
			Vector2 pos = Position.ToVector2();
			switch (Horizontal)
			{
				case HorizontalAlignment.Center: { pos.X -= size.X / 2f; } break;
				case HorizontalAlignment.Right: { pos.X -= size.X; } break;
			}

			//set the y component
			switch (Vertical)
			{
				case VerticalAlignment.Center: { pos.Y -= size.Y / 2f; } break;
				case VerticalAlignment.Bottom: { pos.Y -= size.Y; } break;
			}

			_rect = new Rectangle((int)pos.X, (int)pos.Y, (int)size.X, (int)size.Y);
		}

		#endregion //Methods
	}
}