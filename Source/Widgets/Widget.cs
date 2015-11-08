using System;
using GameTimer;
using Microsoft.Xna.Framework;

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

		#endregion //Fields

		#region Properties

		public virtual bool Highlight { protected get; set; }

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

		/// <summary>
		/// The stylesheet of this item
		/// </summary>
		public StyleSheet Style
		{
			get
			{
				return _style;
			}
			set
			{
				_style = new StyleSheet(value);
				CalculateRect();
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

		#endregion //Properties

		#region Methods

		/// <summary>
		/// constructor!
		/// </summary>
		protected Widget()
		{
			_style = new StyleSheet(DefaultStyles.Instance().MainStyle);
			_horizontal = HorizontalAlignment.Left;
			_vertical = VerticalAlignment.Top;
			_scale = 1.0f;
			_padding = Vector2.Zero;
		}

		/// <summary>
		/// Available load content method for child classes.
		/// </summary>
		/// <param name="screen"></param>
		public virtual void LoadContent(IScreen screen)
		{
		}

		/// <summary>
		/// Recalculate the rect of this widget
		/// </summary>
		protected abstract void CalculateRect();

		public abstract void Update(IScreen screen, GameClock gameTime);

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
				screen.ScreenManager.DrawHelper.DrawBackground(screen.Transition, Style, Rect);

				//draw the outline!
				screen.ScreenManager.DrawHelper.DrawOutline(screen.Transition, Style, Rect);
			}
		}

		public abstract void Draw(IScreen screen, GameClock gameTime);

		/// <summary>
		/// Get the transition this dude will use.
		/// Defaults to the screen transition object.
		/// Can be overloaded by child classes to use special transition objects.
		/// </summary>
		/// <param name="screen"></param>
		/// <returns></returns>
		protected virtual Transition GetTransition(IScreen screen)
		{
			return screen.Transition;
		}

		/// <summary>
		/// Get teh position to draw this widget
		/// </summary>
		/// <returns></returns>
		protected Vector2 DrawPosition(IScreen screen)
		{
			//take the transition position into account
			return GetTransition(screen).Position(new Point(Rect.X, Rect.Y), Style.Transition);
		}

		protected Vector2 TextPosition(IScreen screen)
		{
			//get the draw position
			return GetTransition(screen).Position(new Point(Position.X, Rect.Y), Style.Transition);
		}

		public void CheckHighlight(Vector2 position)
		{
			Highlight = Rect.Contains(position);
		}

		public virtual bool CheckClick(Vector2 position)
		{
			//dont need to do anything here
			return false;
		}

		#endregion //Methods
	}
}