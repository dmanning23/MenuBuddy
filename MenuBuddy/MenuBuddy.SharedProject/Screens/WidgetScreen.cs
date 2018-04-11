using GameTimer;
using InputHelper;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using ResolutionBuddy;
using System;

namespace MenuBuddy
{
	/// <summary>
	/// This is a screen that can display a bunch of widgets.
	/// </summary>
	public class WidgetScreen : Screen, IWidgetScreen, IDisposable
	{
		#region Fields

		public bool Highlightable { get; set; }

		public bool Clickable { get; set; }

		/// <summary>
		/// Ammount of time that passes before attract mode is activated
		/// </summary>
		public float AttractModeTime { get; set; }

#pragma warning disable 0414
		public event EventHandler<ClickEventArgs> OnClick;
		public event EventHandler<HighlightEventArgs> OnHighlight;
		public event EventHandler<DragEventArgs> OnDrag;
		public event EventHandler<DropEventArgs> OnDrop;
#pragma warning restore 0414

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

		public bool Highlight { protected get; set; }

		public bool IsClicked
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		public bool IsHighlighted { get; set; }

		public bool Modal { get; set; }

		#endregion //Properties

		#region Initialization

		public WidgetScreen(string name, ContentManager content = null)
			: base(name, content)
		{
			Highlightable = true;
			Clickable = true;
			Layout = new AbsoluteLayout()
			{
				Size = new Vector2(Resolution.ScreenArea.Width, Resolution.ScreenArea.Height)
			};

			TimeSinceInput = new CountdownTimer();
			ResetInputTimer();
			AttractModeTime = 15f;
			Modal = false;
		}

		public override void LoadContent()
		{
			base.LoadContent();
			ResetInputTimer();
		}

		public override void UnloadContent()
		{
			base.UnloadContent();
			Layout.UnloadContent();
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

			if (IsActive)
			{
				TimeSinceInput.Update(gameTime);
			}

			Layout.Update(this, Time);
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
			cancelButton.OnClick += ((obj, e) => {
				ExitScreen();
			});
			AddItem(cancelButton);
			return cancelButton;
		}

		public virtual void AddItem(IScreenItem item)
		{
			Layout.AddItem(item);

			//If the item is a widget, load it's content too.
			var widget = item as IHasContent;
			if (null != widget)
			{
				widget.LoadContent(this);
			}

			//if the item is transitionable, set its screentransition
			var transitionable = item as ITransitionable;
			if (null != transitionable && null != transitionable.TransitionObject)
			{
				transitionable.TransitionObject.LoadContent(this);
			}
		}

		public bool RemoveItem(IScreenItem item)
		{
			return Layout.RemoveItem(item);
		}

		public virtual bool CheckHighlight(HighlightEventArgs highlight)
		{
			if (!IsActive || !Highlightable)
			{
				return false;
			}

			return Layout.CheckHighlight(highlight) || Modal;
		}

		/// <summary>
		/// User clicked somewhere in this screen.
		/// </summary>
		/// <param name="point"></param>
		public virtual bool CheckClick(ClickEventArgs click)
		{
			if (!IsActive || !Clickable)
			{
				return false;
			}

			//restart the input timer thing
			ResetInputTimer();

			//check if they clicked in the layout
			return Layout.CheckClick(click) || Modal;
		}

		/// <summary>
		/// This gets called when the input timer needs to be reset.
		/// Used by menu screens to pop up attract mode
		/// </summary>
		public void ResetInputTimer()
		{
			TimeSinceInput.Start(AttractModeTime);
		}

		public virtual bool CheckDrag(DragEventArgs drag)
		{
			if (!IsActive)
			{
				return false;
			}

			//restart the input timer thing
			ResetInputTimer();

			//check if they clicked in the layout
			return Layout.CheckDrag(drag) || Modal;
		}

		public virtual bool CheckDrop(DropEventArgs drop)
		{
			if (!IsActive)
			{
				return false;
			}

			//check if they clicked in the layout
			return Layout.CheckDrop(drop) || Modal;
		}

		public override void Dispose()
		{
			OnClick = null;
			OnHighlight = null;
			OnDrag = null;
			OnDrop = null;
		}

		#endregion //Methods
	}
}
