using GameTimer;
using InputHelper;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace MenuBuddy
{
	/// <summary>
	/// A widget is a screen item that can be displayed
	/// </summary>
	public abstract class Widget : IWidget
	{
		#region Fields

		private HorizontalAlignment _horizontal;
		private VerticalAlignment _vertical;
		private StyleSheet _style;
		private bool _drawWhenInactive = true;
		protected Rectangle _rect;

		private Point _position;

		private float _scale;

		private Vector2 _padding;

		public event EventHandler<HighlightEventArgs> OnHighlight;

		/// <summary>
		/// whether or not this dude is highlighted
		/// </summary>
		private bool _highlight = false;

		protected GameClock HighlightClock
		{
			get; set;
		}

		#endregion //Fields

		#region Properties

		public virtual bool IsHighlighted
		{
			protected get
			{
				return _highlight;
			}
			set
			{
				var prev = _highlight;
				_highlight = value;
				if (!prev && _highlight)
				{
					HighlightClock.Start();
				}
			}
		}

		/// <summary>
		/// The area of this item
		/// </summary>
		public Rectangle Rect
		{
			get
			{
				return _rect;
			}
		}

		/// <summary>
		/// set the position of the item
		/// </summary>
		public virtual Point Position
		{
			get
			{
				return _position;
			}
			set
			{
				if (_position != value)
				{
					_position = value;
					CalculateRect();
				}
			}
		}

		public virtual HorizontalAlignment Horizontal
		{
			get
			{
				return _horizontal;
			}
			set
			{
				if (_horizontal != value)
				{
					_horizontal = value;
					CalculateRect();
				}
			}
		}

		public virtual VerticalAlignment Vertical
		{
			get
			{
				return _vertical;
			}
			set
			{
				if (_vertical != value)
				{
					_vertical = value;
					CalculateRect();
				}
			}
		}

		public virtual bool DrawWhenInactive
		{
			get
			{
				return _drawWhenInactive;
			}
			set
			{
				_drawWhenInactive = value;
			}
		}

		/// <summary>
		/// Where to layer the item.
		/// low numbers go in the back, higher numbers in the front
		/// </summary>
		public float Layer { get; set; }

		public virtual float Scale
		{
			get
			{
				return _scale;
			}
			set
			{
				if (_scale != value)
				{
					_scale = value;
					CalculateRect();
				}
			}
		}

		public virtual Vector2 Padding
		{
			get
			{
				return _padding;
			}
			set
			{
				if (_padding != value)
				{
					_padding = value;
					CalculateRect();
				}
			}
		}

		public bool HasBackground { get; set; }

		public bool HasOutline { get; set; }

		public virtual ITransitionObject Transition { get; set; }

		public Texture2D BackgroundImage { get; set; }

		public bool Highlightable { get; set; }

		#endregion //Properties

		#region Initialization

		/// <summary>
		/// constructor!
		/// </summary>
		protected Widget()
		{
			Highlightable = true;
			_horizontal = HorizontalAlignment.Left;
			_vertical = VerticalAlignment.Top;
			_scale = 1.0f;
			_padding = Vector2.Zero;
			HighlightClock = new GameClock();
			Transition = new WipeTransitionObject(StyleSheet.Transition);
		}

		protected Widget(Widget inst)
		{
			_horizontal = inst._horizontal;
			_vertical = inst._vertical;
			_scale = inst._scale;
			_padding = new Vector2(inst._padding.X, inst._padding.Y);
			_drawWhenInactive = inst._drawWhenInactive;
			_rect = new Rectangle(inst._rect.Location, inst._rect.Size);
			_position = new Point(inst._position.X, inst._position.Y);
			Layer = inst.Layer;
			HighlightClock = new GameClock(inst.HighlightClock);
			OnHighlight = inst.OnHighlight;
			HasBackground = inst.HasBackground;
			HasOutline = inst.HasOutline;
			Transition = inst.Transition;
			BackgroundImage = inst.BackgroundImage;
		}

		/// <summary>
		/// Get a deep copy of this item
		/// </summary>
		/// <returns></returns>
		public abstract IScreenItem DeepCopy();

		#endregion //Initialization

		#region Methods

		/// <summary>
		/// Available load content method for child classes.
		/// </summary>
		/// <param name="screen"></param>
		public virtual void LoadContent(IScreen screen)
		{
			if (null != screen.ScreenManager)
			{
				BackgroundImage = screen.ScreenManager.Game.Content.Load<Texture2D>(StyleSheet.ButtonBackgroundImageResource);
			}
		}

		/// <summary>
		/// Recalculate the rect of this widget
		/// </summary>
		protected abstract void CalculateRect();

		public virtual void Update(IScreen screen, GameClock gameTime)
		{
			HighlightClock.Update(gameTime);
		}

		/// <summary>
		/// Check if we should draw this widget.
		/// Widgets can be hidden is the screen is inactive
		/// </summary>
		/// <param name="screen"></param>
		/// <returns></returns>
		protected bool ShouldDraw(IScreen screen)
		{
			//check if we don't want to draw this widget when inactive
			return (DrawWhenInactive || screen.IsActive);
		}

		public virtual void DrawBackground(IScreen screen, GameClock gameTime)
		{
			if (!ShouldDraw(screen))
			{
				return;
			}

			//darw the background rectangle if in touch mode
			if (screen.IsActive)
			{
				//draw the rect!
				if (HasBackground)
				{
					var color = IsHighlighted ? StyleSheet.HighlightedBackgroundColor : StyleSheet.NeutralBackgroundColor;
					screen.ScreenManager.DrawHelper.DrawRect(color, Rect, screen.Transition, Transition, BackgroundImage);
				}

				//draw the outline!
				if (HasOutline)
				{
					var color = IsHighlighted ? StyleSheet.HighlightedOutlineColor : StyleSheet.NeutralOutlineColor;
					screen.ScreenManager.DrawHelper.DrawOutline(color, Rect, screen.Transition, Transition);
				}
			}
		}

		public abstract void Draw(IScreen screen, GameClock gameTime);

		/// <summary>
		/// Get teh position to draw this widget
		/// </summary>
		/// <returns></returns>
		protected Point DrawPosition(IScreen screen)
		{
			//take the transition position into account
			return Transition.Position(screen.Transition, Rect);
		}

		public virtual bool CheckHighlight(HighlightEventArgs highlight)
		{
			if (Highlightable)
			{
				//get the previous value
				var prev = IsHighlighted;

				//Check if this dude should be highlighted
				IsHighlighted = Rect.Contains(highlight.Position);

				//Do we need to run the highlight event?
				if (IsHighlighted &&
					!prev &&
					null != OnHighlight)
				{
					OnHighlight(this, highlight);
				}
			}

			return IsHighlighted;
		}

		#endregion //Methods
	}
}