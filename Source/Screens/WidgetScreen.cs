using GameTimer;
using Microsoft.Xna.Framework;
using ResolutionBuddy;
using System.Collections.Generic;

namespace MenuBuddy
{
	/// <summary>
	/// This is a screen that can display a bunch of widgets.
	/// </summary>
	public class WidgetScreen : Screen, IWidgetScreen
	{
		#region Fields

		/// <summary>
		/// Ammount of time that passes before attract mode is activated
		/// </summary>
		private const float _AttractModeTime = 15.0f;

		#endregion

		#region Properties

		protected AbsoluteLayout Layout { get; private set; }

		public Rectangle Rect
		{
			get { return ResolutionBuddy.Resolution.ScreenArea; }
		}

		public Point Position
		{
			get { return Point.Zero; }
			set { }
		}

		/// <summary>
		/// Countdown timer that is used to tell when to start attract mode
		/// </summary>
		/// <value>The time since input.</value>
		public CountdownTimer TimeSinceInput { get; private set; }

		#endregion //Properties

		#region Initialization

		public WidgetScreen(string name)
			: base(name)
		{
			Layout = new AbsoluteLayout()
			{
				Size = new Vector2(Resolution.ScreenArea.Width, Resolution.ScreenArea.Height)
			};

			TimeSinceInput = new CountdownTimer();
			ResetInputTimer();
		}

		public override void LoadContent()
		{
			base.LoadContent();
			ResetInputTimer();
		}

		#endregion Initialization

		#region Update & Draw

		/// <summary>
		/// The screen update
		/// </summary>
		/// <param name="gameTime"></param>
		/// <param name="otherScreenHasFocus"></param>
		/// <param name="coveredByOtherScreen"></param>
		public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
		{
			base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);
			Update(this, Time);
		}

		/// <summary>
		/// The item container update
		/// </summary>
		/// <param name="screen"></param>
		/// <param name="gameTime"></param>
		public void Update(IScreen screen, GameClock gameTime)
		{
			Layout.Update(screen, gameTime);
		}

		/// <summary>
		/// The screen draw
		/// </summary>
		/// <param name="gameTime"></param>
		public override void Draw(GameTime gameTime)
		{
			base.Draw(gameTime);

			ScreenManager.SpriteBatchBegin();
			DrawBackground(this, Time);
			Draw(this, Time);
			ScreenManager.SpriteBatchEnd();
		}

		/// <summary>
		/// Draw the background of all the items
		/// </summary>
		/// <param name="screen"></param>
		/// <param name="gameTime"></param>
		public void DrawBackground(IScreen screen, GameClock gameTime)
		{
			Layout.DrawBackground(screen, gameTime);
		}

		/// <summary>
		/// Draw all the items
		/// </summary>
		/// <param name="screen"></param>
		/// <param name="gameTime"></param>
		public void Draw(IScreen screen, GameClock gameTime)
		{
			Layout.Draw(screen, gameTime);
		}

		#endregion //Update & Draw

		#region Methods

		/// <summary>
		/// This method adds a continue button to the menu and attachs it to OnCancel
		/// </summary>
		protected CancelButton AddCancelButton()
		{
			var cancelButton = new CancelButton();
			cancelButton.Selected += OnCancel;
			AddItem(cancelButton);
			return cancelButton;
		}

		public void AddItem(IScreenItem item)
		{
			Layout.AddItem(item);

			//If the item is a widget, load it's content too.
			var widget = item as IWidget;
			if (null != widget)
			{
				widget.LoadContent(this);
			}
		}

		public bool RemoveItem(IScreenItem item)
		{
			return Layout.RemoveItem(item);
		}

		/// <summary>
		/// User hit the "menu select" button.
		/// </summary>
		/// <param name="playerIndex"></param>
		public virtual void OnSelect(object obj, PlayerIndexEventArgs playerIndex)
		{
			ResetInputTimer();
		}

		public virtual void OnCancel(object obj, PlayerIndexEventArgs playerIndex)
		{
			ExitScreen();
		}

		public void CheckHighlight(Vector2 point)
		{
			Layout.CheckHighlight(point);
		}

		/// <summary>
		/// User clicked somewhere in this screen.
		/// </summary>
		/// <param name="point"></param>
		public bool CheckClick(Vector2 point)
		{
			//restart the input timer thing
			TimeSinceInput.Start(_AttractModeTime);

			//check if they clicked in the layout
			return Layout.CheckClick(point);
        }

		/// <summary>
		/// This gets called when the input timer needs to be reset.
		/// Used by menu screens to pop up attract mode
		/// </summary>
		public void ResetInputTimer()
		{
			TimeSinceInput.Start(_AttractModeTime);
		}

		#endregion //Methods
	}
}