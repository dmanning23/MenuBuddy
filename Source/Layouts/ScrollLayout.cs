using System.Collections.Generic;
using GameTimer;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MenuBuddy
{
	/// <summary>
	/// This is a abs layout that is in a window with scroll bars
	/// </summary>
	public class ScrollLayout : AbsoluteLayout
	{
		#region Delegates

		private delegate void DrawStuffDelegate(IScreen screen, GameClock gameTime);

		#endregion //Delegates

		#region Fields

		private Vector2 _scrollPos = Vector2.Zero;

		private RenderTarget2D _renderTarget = null;

		private Vector2 _minScroll = Vector2.Zero;

		private Vector2 _maxScroll = Vector2.Zero;

		#endregion //Fields

		#region Properties

		/// <summary>
		/// the current scorll position of this thing
		/// </summary>
		public Vector2 ScrollPosition
		{
			get
			{
				return _scrollPos;
			}
			set
			{
				//constrain the scroll to within the total rect
				value = ConstrainScroll(value);

				if (_scrollPos != value)
				{
					//set the scroll position
					var delta = (_scrollPos - value).ToPoint();
					_scrollPos = value;

					//update the position of all the items
					foreach (var item in Items)
					{
						item.Position += delta;
					}
				}
			}
		}

		public override Vector2 Size
		{
			get
			{
				return base.Size;
			}
			set
			{
				//make sure to redo the rendertarget
				_renderTarget = null;
                base.Size = value;
			}
		}

		public TransitionType Transition
		{
			get; set;
		}

		/// <summary>
		/// This is the total max rect, containing all the widgets.
		/// </summary>
		public Rectangle TotalRect
		{
			get
			{
				Rectangle result = Rect;

				//add all the widgets in this dude
				foreach (var item in Items)
				{
					result = Rectangle.Union(result, item.Rect);
				}

				return result;
			}
		}

		public Vector2 MinScroll
		{
			get
			{
				return _minScroll;
			}
		}

		public Vector2 MaxScroll
		{
			get
			{
				return _maxScroll;
			}
		}

		#endregion //Properties

		#region Methods

		public ScrollLayout()
		{
			Transition = TransitionType.SlideLeft;
		}

		public override void AddItem(IScreenItem item)
		{
			//Items in a scroll layout don't transition
			var widget = item as Widget;
			if (null != widget)
			{
				widget.Style.Transition = TransitionType.None;
			}

			base.AddItem(item);

			UpdateMinMaxScroll();
		}

		private void InitializeRenderTarget(IScreen screen)
		{
			if (null == _renderTarget)
			{
				_renderTarget = new RenderTarget2D(screen.ScreenManager.GraphicsDevice, 
					(int)Size.X,
					(int)Size.Y,
					false,
					screen.ScreenManager.GraphicsDevice.PresentationParameters.BackBufferFormat,
					screen.ScreenManager.GraphicsDevice.PresentationParameters.DepthStencilFormat,
					0,
					RenderTargetUsage.PreserveContents);
			}
		}

		private void DrawStuff(IScreen screen, GameClock gameTime, DrawStuffDelegate del)
		{
			//grab the old stuff
			var curPos = Position;
			var curHor = Horizontal;
			var curVert = Vertical;
			Position = Point.Zero;
			Horizontal = HorizontalAlignment.Left;
			Vertical = VerticalAlignment.Top;

			var screenManager = screen.ScreenManager;

			//initialize the render target if necessary
			InitializeRenderTarget(screen);

			//end the current draw loop
			screenManager.SpriteBatchEnd();

			var curRenderTarget = screenManager.GraphicsDevice.GetRenderTargets();

			//set the rendertarget
			screenManager.GraphicsDevice.SetRenderTarget(_renderTarget);

			screenManager.GraphicsDevice.Clear(screenManager.ClearColor);

			//start a new draw loop
			screenManager.SpriteBatchBegin(BlendState.AlphaBlend);

			//call the provided delegate to draw everything
			del(screen, gameTime);

			//end the loop
			screenManager.SpriteBatchEnd();

			//set the position back
			Position = curPos;
			Vertical = curVert;
			Horizontal = curHor;

			//set the render target back
			screenManager.GraphicsDevice.SetRenderTarget(null);

			//start a new loop
			screenManager.SpriteBatchBegin();

			//render the texture
			var rect = CalculateRect();
			screenManager.SpriteBatch.Draw(_renderTarget,
				screen.Transition.Position(rect, Transition).ToVector2(),
				Color.White);
		}

		public override void DrawBackground(IScreen screen, GameClock gameTime)
		{
			DrawStuff(screen, gameTime, base.DrawBackground);
		}

		public override void Draw(IScreen screen, GameClock gameTime)
		{
			DrawStuff(screen, gameTime, base.Draw);
		}

		public void UpdateMinMaxScroll()
		{
			//get the total rectangle
			var total = TotalRect;

			//get the layout rectangle
			var current = Rect;

			//set the min and max to be the diff between the two
			_minScroll = new Vector2(total.Left - current.Left, total.Top - current.Top);
			_maxScroll = new Vector2(total.Right- current.Right, total.Bottom - current.Bottom);
		}

		private Vector2 ConstrainScroll(Vector2 value)
		{
			//set the x value
			if (value.X < _minScroll.X)
			{
				value.X = _minScroll.X;
			}
			else if (value.X > _maxScroll.X)
			{
				value.X = _maxScroll.X;
			}

			//set the y value
			if (value.Y < _minScroll.Y)
			{
				value.Y = _minScroll.Y;
			}
			else if (value.Y > _maxScroll.Y)
			{
				value.Y = _maxScroll.Y;
			}

			return value;
		}

		#endregion //Methods
	}
}