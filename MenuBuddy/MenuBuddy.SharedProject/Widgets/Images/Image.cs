using GameTimer;
using InputHelper;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace MenuBuddy
{
	/// <summary>
	/// An image that is displayed onteh screen
	/// </summary>
	public class Image : Widget, IImage, IDisposable
	{
		#region Fields

		private const float _pulsateSize = 1.0f;
		private const float _pulsateSpeed = 4.0f;

		private Vector2 _size;
		private bool _fillRect;
		private Texture2D _texture;

#pragma warning disable 0414
		public bool Clickable { get; set; }
		public event EventHandler<ClickEventArgs> OnClick;
#pragma warning restore 0414

		#endregion //Fields

		#region Properties

		public virtual int Width
		{
			get
			{
				return (null != Texture) ? Texture.Width : 0;
			}
		}

		public virtual int Height
		{
			get
			{
				return (null != Texture) ? Texture.Height : 0;
			}
		}

		public virtual Rectangle Bounds
		{
			get
			{
				return (null != Texture) ? Texture.Bounds : Rectangle.Empty;
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
			get { return _texture; }
			set
			{
				_texture = value;
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

		public bool PulsateOnHighlight
		{
			get; set;
		}

		public bool AlwaysPulsate
		{
			get; set;
		}

		public bool IsClicked { get; set; }

		public Color FillColor { private get; set; }

		#endregion //Properties

		#region Initialization

		/// <summary>
		/// only for unit testing
		/// </summary>
		public Image()
		{
			PulsateOnHighlight = false;
			FillColor = Color.White;
			Clickable = true;
			AlwaysPulsate = false;
		}

		/// <summary>
		/// constructor!
		/// </summary>
		public Image(Texture2D texture) : this()
		{
			FillRect = false;
			Texture = texture;
		}

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
		/// Get a deep copy of this item
		/// </summary>
		/// <returns></returns>
		public override IScreenItem DeepCopy()
		{
			return new Image(this);
		}

		#endregion //Initialization

		#region Methods

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

		public bool CheckClick(ClickEventArgs click)
		{
			return false;
		}

		public override void Dispose()
		{
			base.Dispose();
			OnClick = null;
		}

		#endregion //Methods
	}
}