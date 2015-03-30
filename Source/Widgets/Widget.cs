using System.Diagnostics;
using GameTimer;
using Microsoft.Xna.Framework;
using Vector2Extensions;

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

		#endregion //Fields

		#region Properties

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
		public StyleSheet Style { get; private set; }

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
		}

		public abstract void Update(IScreen screen, GameClock gameTime);

		public virtual void DrawBackground(IScreen screen, GameClock gameTime)
		{
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
		/// Get teh position to draw this widget
		/// </summary>
		/// <returns></returns>
		protected Vector2 DrawPosition(IScreen screen)
		{
			//take the transition position into account
			return screen.Transition.Position(Position.ToVector2(), Style.Transition);
		}

		protected Vector2 TextPosition(IScreen screen)
		{
			//get the draw position
			var pos = screen.Transition.Position(Position.ToVector2(), Style.Transition);

			//text is always centered
			pos.X += Rect.Width / 2f;

			return pos;
		}

		#endregion //Methods
	}
}