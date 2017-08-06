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
	public abstract class Widget : IWidget, IDisposable
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

		public GameClock HighlightClock
		{
			get; protected set;
		}

		#endregion //Fields

		#region Properties

		public virtual bool IsHighlighted
		{
			get
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
		public virtual Rectangle Rect
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

		public virtual ITransitionObject TransitionObject { get; set; }

		public bool Highlightable { get; set; }

		protected Background Background { get; set; }

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
			TransitionObject = new WipeTransitionObject(StyleSheet.Transition);
			Background = new Background();
		}

		protected Widget(Widget inst)
		{
			_horizontal = inst.Horizontal;
			_vertical = inst.Vertical;
			_scale = inst.Scale;
			_padding = new Vector2(inst.Padding.X, inst.Padding.Y);
			_drawWhenInactive = inst.DrawWhenInactive;
			_rect = new Rectangle(inst.Rect.Location, inst.Rect.Size);
			_position = new Point(inst.Position.X, inst.Position.Y);
			Layer = inst.Layer;
			HighlightClock = new GameClock(inst.HighlightClock);
			OnHighlight = inst.OnHighlight;
			HasBackground = inst.HasBackground;
			HasOutline = inst.HasOutline;
			TransitionObject = inst.TransitionObject;
			Background = inst.Background;
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
			Background.LoadContent(screen);
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
				Background.Draw(this, screen);
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
			return TransitionObject.Position(screen.Transition, Rect);
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

		public virtual void Dispose()
		{
			OnHighlight = null;
		}

		#endregion //Methods
	}
}