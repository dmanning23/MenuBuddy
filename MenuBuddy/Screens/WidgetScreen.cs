using GameTimer;
using InputHelper;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using ResolutionBuddy;
using System;
using System.Threading.Tasks;

namespace MenuBuddy
{
	/// <summary>
	/// A screen that hosts an <see cref="AbsoluteLayout"/> of widgets and handles pointer
	/// interaction (highlight, click, drag, drop). Supports modal blocking to prevent input
	/// from reaching screens below, and tracks idle time to trigger attract mode.
	/// </summary>
	public class WidgetScreen : Screen, IWidgetScreen, IDisposable
	{
		#region Properties

		/// <summary>
		/// Gets or sets whether this screen checks for widget highlights on pointer movement.
		/// </summary>
		public bool Highlightable { get; set; }

		/// <summary>
		/// Gets or sets whether this screen checks for click events.
		/// </summary>
		public bool Clickable { get; set; }

		/// <summary>
		/// Amount of time in seconds that passes before attract mode is activated.
		/// </summary>
		public float AttractModeTime { get; set; }

#pragma warning disable 0414
		/// <summary>Raised when a click event occurs on this screen.</summary>
		public event EventHandler<ClickEventArgs> OnClick;
		/// <summary>Raised when a highlight event occurs on this screen.</summary>
		public event EventHandler<HighlightEventArgs> OnHighlight;
		/// <summary>Raised when a drag event occurs on this screen.</summary>
		public event EventHandler<DragEventArgs> OnDrag;
		/// <summary>Raised when a drop event occurs on this screen.</summary>
		public event EventHandler<DropEventArgs> OnDrop;
#pragma warning restore 0414

		/// <summary>
		/// The root layout container that holds all widgets on this screen.
		/// </summary>
		protected AbsoluteLayout Layout { get; private set; }

		/// <summary>
		/// Gets the full screen rectangle. Widget screens always occupy the entire screen area.
		/// </summary>
		public Rectangle Rect
		{
			get { return ResolutionBuddy.Resolution.ScreenArea; }
		}

		/// <summary>
		/// Gets or sets the position of this screen. Always returns <see cref="Point.Zero"/> as screens are full-screen.
		/// </summary>
		public Point Position
		{
			get { return Point.Zero; }
			set { }
		}

		/// <summary>
		/// Countdown timer that is used to tell when to start attract mode.
		/// </summary>
		public CountdownTimer TimeSinceInput { get; private set; }

		/// <summary>
		/// Gets or sets whether this screen is currently highlighted.
		/// </summary>
		public bool Highlight { protected get; set; }

		/// <summary>
		/// Gets or sets whether this screen is in a clicked state. Always returns false; clicks are delegated to widgets.
		/// </summary>
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

		/// <summary>
		/// Gets or sets whether this screen is currently highlighted by a pointer.
		/// </summary>
		public bool IsHighlighted { get; set; }

		/// <summary>
		/// Gets or sets whether this screen is modal. Modal screens consume all input,
		/// preventing screens underneath from receiving clicks or other input events.
		/// </summary>
		public bool Modal { get; set; }

		#endregion //Properties

		#region Initialization

		/// <summary>
		/// Initializes a new instance of <see cref="WidgetScreen"/> with the given name.
		/// </summary>
		/// <param name="name">The display name for this screen.</param>
		/// <param name="content">Optional shared content manager. If null, one will be created on load.</param>
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

		/// <inheritdoc/>
		public override async Task LoadContent()
		{
			await base.LoadContent();
			ResetInputTimer();
		}

		/// <inheritdoc/>
		public override void UnloadContent()
		{
			base.UnloadContent();

			Layout?.UnloadContent();
			Layout = null;

			OnClick = null;
			OnHighlight = null;
			OnDrag = null;
			OnDrop = null;
		}

		#endregion Initialization

		#region Update & Draw

		/// <summary>
		/// Updates the screen. Advances the attract-mode countdown while active,
		/// then delegates per-frame logic to the widget layout.
		/// </summary>
		/// <param name="gameTime">Snapshot of the current game timing.</param>
		/// <param name="otherScreenHasFocus">True when the application window does not have OS focus.</param>
		/// <param name="coveredByOtherScreen">True when another screen is stacked on top of this one.</param>
		public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
		{
			base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);

			if (IsActive)
			{
				TimeSinceInput.Update(gameTime);
			}

			Layout?.Update(this, Time);
		}

		/// <summary>
		/// Draws the screen. Opens a sprite batch, renders widget backgrounds followed by
		/// widget foregrounds, then closes the sprite batch.
		/// </summary>
		/// <param name="gameTime">Snapshot of the current game timing.</param>
		public override void Draw(GameTime gameTime)
		{
			base.Draw(gameTime);

			ScreenManager.SpriteBatchBegin();
			DrawBackground(this, Time);
			Draw(this, Time);
			ScreenManager.SpriteBatchEnd();
		}

		/// <summary>
		/// Draws the background layer of every widget in the layout.
		/// Called before <see cref="Draw(IScreen, GameClock)"/> so backgrounds render beneath widget content.
		/// </summary>
		/// <param name="screen">The screen context passed to each widget.</param>
		/// <param name="gameTime">The game clock used for transition-aware rendering.</param>
		public void DrawBackground(IScreen screen, GameClock gameTime)
		{
			Layout?.DrawBackground(screen, gameTime);
		}

		/// <summary>
		/// Draws the foreground content of every widget in the layout.
		/// </summary>
		/// <param name="screen">The screen context passed to each widget.</param>
		/// <param name="gameTime">The game clock used for transition-aware rendering.</param>
		public void Draw(IScreen screen, GameClock gameTime)
		{
			Layout?.Draw(screen, gameTime);
		}

		#endregion //Update & Draw

		#region Methods

		/// <summary>
		/// Creates a <see cref="CancelButton"/> and adds it to the screen layout.
		/// </summary>
		/// <param name="customSize">Optional override for the button size. Defaults to the stylesheet value when null.</param>
		/// <returns>The cancel button that was added.</returns>
		protected CancelButton AddCancelButton(int? customSize = null)
		{
			var cancelButton = new CancelButton(customSize);
			AddItem(cancelButton);
			return cancelButton;
		}

		/// <summary>
		/// Adds a widget to this screen's layout and loads its content if needed.
		/// </summary>
		/// <param name="item">The widget or layout item to add.</param>
		public virtual void AddItem(IScreenItem item)
		{
			Layout.AddItem(item);

			//If the item is a widget, load it's content too.
			var widget = item as IHasContent;
			if (null != widget)
			{
				widget.LoadContent(this);
			}
		}

		/// <summary>
		/// Removes a widget from this screen's layout.
		/// </summary>
		/// <param name="item">The widget or layout item to remove.</param>
		/// <returns>True if the item was found and removed; otherwise false.</returns>
		public bool RemoveItem(IScreenItem item)
		{
			return Layout.RemoveItem(item);
		}

		/// <summary>
		/// Checks whether any widget in this screen should receive the highlight event.
		/// </summary>
		/// <param name="highlight">The highlight event arguments.</param>
		/// <returns>True if the highlight was consumed (by a widget or by modal blocking).</returns>
		public virtual bool CheckHighlight(HighlightEventArgs highlight)
		{
			if (!IsActive || !Highlightable)
			{
				return false;
			}

			return Layout.CheckHighlight(highlight) || Modal;
		}

		/// <summary>
		/// Checks whether any widget in this screen should receive the click event.
		/// Resets the attract-mode countdown on every handled click.
		/// </summary>
		/// <param name="click">The click event arguments.</param>
		/// <returns>True if the click was consumed (by a widget or by modal blocking).</returns>
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
		/// Restarts the attract-mode countdown to <see cref="AttractModeTime"/> seconds.
		/// Called automatically on every click or drag event.
		/// </summary>
		public void ResetInputTimer()
		{
			TimeSinceInput.Start(AttractModeTime);
		}

		/// <summary>
		/// Checks whether any widget in this screen should receive the drag event.
		/// </summary>
		/// <param name="drag">The drag event arguments.</param>
		/// <returns>True if the drag was consumed (by a widget or by modal blocking).</returns>
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

		/// <summary>
		/// Checks whether any widget in this screen should receive the drop event.
		/// </summary>
		/// <param name="drop">The drop event arguments.</param>
		/// <returns>True if the drop was consumed (by a widget or by modal blocking).</returns>
		public virtual bool CheckDrop(DropEventArgs drop)
		{
			if (!IsActive)
			{
				return false;
			}

			//check if they clicked in the layout
			return Layout.CheckDrop(drop) || Modal;
		}

		#endregion //Methods
	}
}
