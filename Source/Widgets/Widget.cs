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

		#endregion //Fields

		#region Properties

		public virtual bool Highlight { protected get; set; }

		/// <summary>
		/// The area of this item
		/// </summary>
		public virtual Rectangle Rect { get; set; }

		/// <summary>
		/// set the position of the item
		/// </summary>
		public virtual Point Position
		{
			get { return Rect.Location; }
			set
			{
				//set the x component
				switch (Horizontal)
				{
					case HorizontalAlignment.Center: { value.X -= Rect.Width / 2; } break;
					case HorizontalAlignment.Right: { value.X -= Rect.Width; } break;
				}

				//set the y component
				switch (Vertical)
				{
					case VerticalAlignment.Center: { value.Y -= Rect.Height / 2; } break;
					case VerticalAlignment.Bottom: { value.Y -= Rect.Height; } break;
				}

				Rect = new Rectangle(value.X, value.Y, Rect.Width, Rect.Height);
			}
		}

		/// <summary>
		/// The stylesheet of this item
		/// </summary>
		public StyleSheet Style {
			get
			{
				return _style;
			}
			private set
			{
				_style = new StyleSheet(value);
			}
		}

		public HorizontalAlignment Horizontal
		{
			get { return _horizontal; }
			set
			{
				//need to update the position based on the old alignment
				var currentPos = Position;
				switch (_horizontal)
				{
					case HorizontalAlignment.Left:
					{
						switch (value)
						{
							case HorizontalAlignment.Center:
							{
								currentPos.X -= Rect.Width/2;
							}
							break;
							case HorizontalAlignment.Right:
							{
								currentPos.X -= Rect.Width;
							}
							break;
						}
					}
					break;
					case HorizontalAlignment.Center:
					{
						switch (value)
						{
							case HorizontalAlignment.Left:
							{
								currentPos.X += Rect.Width / 2;
							}
							break;
							case HorizontalAlignment.Right:
							{
								currentPos.X -= Rect.Width / 2;
							}
							break;
						}
					}
					break;
					case HorizontalAlignment.Right:
					{
						switch (value)
						{
							case HorizontalAlignment.Left:
							{
								currentPos.X += Rect.Width;
							}
							break;
							case HorizontalAlignment.Center:
							{
								currentPos.X += Rect.Width / 2;
							}
							break;
						}
					}
					break;
				}

				//set teh new rect
				Rect = new Rectangle(currentPos.X, currentPos.Y, Rect.Width, Rect.Height);

				//grab the new alignment
				_horizontal = value;
			}
		}

		public VerticalAlignment Vertical
		{
			get { return _vertical; }
			set
			{
				//need to update the position based on the old alignment
				var currentPos = Position;
				switch (_vertical)
				{
					case VerticalAlignment.Top:
					{
						switch (value)
						{
							case VerticalAlignment.Center:
							{
								currentPos.Y -= Rect.Height / 2;
							}
							break;
							case VerticalAlignment.Bottom:
							{
								currentPos.Y -= Rect.Height;
							}
							break;
						}
					}
					break;
					case VerticalAlignment.Center:
					{
						switch (value)
						{
							case VerticalAlignment.Top:
							{
								currentPos.Y += Rect.Height / 2;
							}
							break;
							case VerticalAlignment.Bottom:
							{
								currentPos.Y -= Rect.Height / 2;
							}
							break;
						}
					}
					break;
					case VerticalAlignment.Bottom:
					{
						switch (value)
						{
							case VerticalAlignment.Top:
							{
								currentPos.Y += Rect.Height;
							}
							break;
							case VerticalAlignment.Center:
							{
								currentPos.Y += Rect.Height / 2;
							}
							break;
						}
					}
					break;
				}

				//set teh new rect
				Rect = new Rectangle(currentPos.X, currentPos.Y, Rect.Width, Rect.Height);

				//grab the new alignment
				_vertical = value;
			}
		}

		public virtual bool DrawWhenInactive {
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

		public virtual float Scale { get; set; }

		public Vector2 Padding { get; set; }

		#endregion //Properties

		#region Methods

		/// <summary>
		/// constructor!
		/// </summary>
		/// <param name="style"></param>
		protected Widget(StyleSheet style)
		{
			Style = style;
			_horizontal = HorizontalAlignment.Left;
			_vertical = VerticalAlignment.Top;
			Scale = 1.0f;
			Padding = Vector2.Zero;
		}

		/// <summary>
		/// Available load content method for child classes.
		/// </summary>
		/// <param name="screen"></param>
		public virtual void LoadContent(IScreen screen)
		{
		}

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
			var pos = Position.ToVector2() + Padding;
			pos = GetTransition(screen).Position(pos, Style.Transition);
			return pos;
		}

		protected Vector2 TextPosition(IScreen screen)
		{
			//get the draw position
			var pos = Position.ToVector2() + Padding;
			pos = GetTransition(screen).Position(pos, Style.Transition);

			//text is always centered
			pos.X += Rect.Width / 2f;

			return pos;
		}

		#endregion //Methods
	}
}