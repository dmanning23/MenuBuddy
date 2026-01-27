using GameTimer;
using InputHelper;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace MenuBuddy
{
	/// <summary>
	/// A widget that displays a texture on screen, with optional pulsate animation when highlighted or always.
	/// </summary>
	public class Image : Widget, IImage, IDisposable
	{
		#region Fields

		/// <summary>
		/// The amplitude of the pulsate effect.
		/// </summary>
		private const float _pulsateSize = 1.0f;

		/// <summary>
		/// The speed of the pulsate oscillation.
		/// </summary>
		private const float _pulsateSpeed = 4.0f;

		private Vector2 _size;
		private bool _fillRect;
		private Texture2D _texture;

#pragma warning disable 0414
		/// <summary>
		/// Whether this image can be clicked.
		/// </summary>
		public bool Clickable { get; set; }

		/// <summary>
		/// Raised when this image is clicked.
		/// </summary>
		public event EventHandler<ClickEventArgs> OnClick;
#pragma warning restore 0414

		#endregion //Fields

		#region Properties

		/// <summary>
		/// The width of the texture in pixels, or 0 if no texture is set.
		/// </summary>
		public virtual int Width
		{
			get
			{
				return (null != Texture) ? Texture.Width : 0;
			}
		}

		/// <summary>
		/// The height of the texture in pixels, or 0 if no texture is set.
		/// </summary>
		public virtual int Height
		{
			get
			{
				return (null != Texture) ? Texture.Height : 0;
			}
		}

		/// <summary>
		/// The bounding rectangle of the texture, or <see cref="Rectangle.Empty"/> if no texture is set.
		/// </summary>
		public virtual Rectangle Bounds
		{
			get
			{
				return (null != Texture) ? Texture.Bounds : Rectangle.Empty;
			}
		}

		/// <summary>
		/// The target size for this image when <see cref="FillRect"/> is <c>true</c>. Setting this recalculates the bounding rectangle.
		/// </summary>
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

		/// <summary>
		/// The texture displayed by this image. Setting this recalculates the bounding rectangle.
		/// </summary>
		public Texture2D Texture
		{
			get { return _texture; }
			set
			{
				_texture = value;
				CalculateRect();
			}
		}

		/// <summary>
		/// Whether the image should stretch to fill the specified <see cref="Size"/>. Setting this recalculates the bounding rectangle.
		/// </summary>
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

		/// <summary>
		/// Whether this image plays a pulsate animation when highlighted or clicked.
		/// </summary>
		public bool PulsateOnHighlight
		{
			get; set;
		}

		/// <summary>
		/// Whether this image continuously plays a pulsate animation, regardless of highlight state.
		/// </summary>
		public bool AlwaysPulsate
		{
			get; set;
		}

		/// <summary>
		/// Whether this image is currently in a clicked visual state.
		/// </summary>
		public bool IsClicked { get; set; }

		/// <summary>
		/// The tint color applied when drawing this image. Use <see cref="Color.White"/> for no tint.
		/// </summary>
		public Color FillColor { private get; set; }

		#endregion //Properties

		#region Initialization

		/// <summary>
		/// Initializes a new <see cref="Image"/> with no texture. Intended for unit testing.
		/// </summary>
		public Image()
		{
			PulsateOnHighlight = false;
			FillColor = Color.White;
			Clickable = true;
			AlwaysPulsate = false;
		}

		/// <summary>
		/// Initializes a new <see cref="Image"/> with the specified texture.
		/// </summary>
		/// <param name="texture">The texture to display.</param>
		public Image(Texture2D texture) : this()
		{
			FillRect = false;
			Texture = texture;
		}

		/// <summary>
		/// Initializes a new <see cref="Image"/> by copying values from an existing instance.
		/// </summary>
		/// <param name="inst">The image to copy from.</param>
		public Image(Image inst) : base(inst)
		{
			Clickable = inst.Clickable;
			_size = new Vector2(inst._size.X, inst._size.Y);
			_fillRect = inst._fillRect;
			_texture = inst._texture;
			PulsateOnHighlight = inst.PulsateOnHighlight;
			AlwaysPulsate = inst.AlwaysPulsate;
		}

		/// <summary>
		/// Creates a deep copy of this image.
		/// </summary>
		/// <returns>A new <see cref="Image"/> that is a copy of this instance.</returns>
		public override IScreenItem DeepCopy()
		{
			return new Image(this);
		}

		/// <summary>
		/// Unloads content and releases the click event handler.
		/// </summary>
		public override void UnloadContent()
		{
			base.UnloadContent();
			OnClick = null;
		}

		#endregion //Initialization

		#region Methods

		/// <inheritdoc/>
		public override void Draw(IScreen screen, GameClock gameTime)
		{
			if (!ShouldDraw(screen) || null == Texture)
			{
				return;
			}

			//get the transition color
			var screenTransition = TransitionObject.GetScreenTransition(screen);
			var color = screenTransition.AlphaColor(FillColor);

			//Get the transition location
			var pos = DrawPosition(screen);
			Rectangle rect = DrawRect(pos.ToVector2());

			//draw the item with all the correct parameters
			screen.ScreenManager.SpriteBatch.Draw(Texture, rect, color);
		}

		/// <summary>
		/// Computes the draw rectangle, applying pulsate scaling if enabled.
		/// </summary>
		/// <param name="pos">The base draw position.</param>
		/// <returns>The rectangle to draw the image into.</returns>
		private Rectangle DrawRect(Vector2 pos)
		{
			Rectangle rect;

			if (((IsClicked || IsHighlighted) && PulsateOnHighlight) || AlwaysPulsate)
			{
				//multiply the time by the speed
				float currentTime = HighlightClock.CurrentTime;
				currentTime *= _pulsateSpeed;

				//Pulsate the size of the text
				float pulsate = _pulsateSize * (float)(Math.Sin(currentTime) + 1.0f);
				float pulseScale = (IsClicked ? 1.2f : 1f) + pulsate * 0.15f;

				//adjust the y position so it pulsates straight out
				Vector2 size = new Vector2(Rect.Width, Rect.Height);
				Vector2 adjust = ((size * pulseScale) - size) / 2.0f;
				pos -= adjust;
				size *= pulseScale;
				rect = new Rectangle((int)pos.X, (int)pos.Y, (int)size.X, (int)size.Y);
			}
			else
			{
				rect = new Rectangle((int)pos.X, (int)pos.Y, Rect.Width, Rect.Height);
			}

			return rect;
		}

		/// <inheritdoc/>
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
			size = size * Scale;

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

		/// <summary>
		/// Images do not handle clicks directly. Always returns <c>false</c>.
		/// </summary>
		/// <param name="click">The click event arguments.</param>
		/// <returns>Always <c>false</c>.</returns>
		public bool CheckClick(ClickEventArgs click)
		{
			return false;
		}

		#endregion //Methods
	}
}