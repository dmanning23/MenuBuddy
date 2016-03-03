using GameTimer;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MouseBuddy;
using System;

namespace MenuBuddy
{
	/// <summary>
	/// An image that is displayed onteh screen
	/// </summary>
	public class Image : Widget, IImage
	{
		#region Fields

		private const float _pulsateSize = 1.0f;
		private const float _pulsateSpeed = 4.0f;

		private Vector2 _size;
		private bool _fillRect;
		private Texture2D _texture;

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

		#endregion //Properties

		#region Initialization

		/// <summary>
		/// only for unit testing
		/// </summary>
		public Image()
		{
			PulsateOnHighlight = true;
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
			_size = new Vector2(inst._size.X, inst._size.Y);
			_fillRect = inst._fillRect;
			_texture = inst._texture;
			PulsateOnHighlight = inst.PulsateOnHighlight;
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
			if (!ShouldDraw(screen))
			{
				return;
			}

			//get the transition color
			var color = screen.Transition.AlphaColor(Color.White);

			//Get the transition location
			var pos = DrawPosition(screen);
			Rectangle rect = DrawRect(pos);

			//draw the item with all the correct parameters
			screen.ScreenManager.SpriteBatch.Draw(Texture, rect, color);
		}

		private Rectangle DrawRect(Vector2 pos)
		{
			Rectangle rect;

			if (Highlight && PulsateOnHighlight)
			{
				//multiply the time by the speed
				float currentTime = HighlightClock.CurrentTime;
				currentTime *= _pulsateSpeed;

				//Pulsate the size of the text
				float pulsate = _pulsateSize * (float)(Math.Sin(currentTime) + 1.0f);
				float pulseScale = 1 + pulsate * 0.15f;

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

		public override bool CheckClick(ClickEventArgs click)
		{
			return false;
		}

		#endregion //Methods
	}
}