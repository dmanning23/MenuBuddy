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

		public override IEnumerable<IButton> Buttons
		{
			get
			{
				//will hold the final list of buttons
				var result = new List<IButton>();

				//the list of all the buttons
				var buttons = base.Buttons;

				//go through the list of buttons and only return the visible ones
				foreach (var button in buttons)
				{
					if (button.Rect.Intersects(Rect))
					{
						result.Add(button);
					}
				}

				return result;
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
		}

		private void InitializeRenderTarget(IScreen screen)
		{
			if (null == _renderTarget)
			{
				_renderTarget = new RenderTarget2D(screen.ScreenManager.GraphicsDevice, 
					(int)Size.X, (int)Size.Y);
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

			//set the rendertarget
			screenManager.GraphicsDevice.SetRenderTarget(_renderTarget);

			//start a new draw loop
			screenManager.SpriteBatchBegin();

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

		#endregion //Methods
	}
}