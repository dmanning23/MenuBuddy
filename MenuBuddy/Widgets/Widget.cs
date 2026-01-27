using GameTimer;
using InputHelper;
using Microsoft.Xna.Framework;
using System;
using System.Threading.Tasks;

namespace MenuBuddy
{
	/// <summary>
	/// Abstract base class for all displayable widgets. Provides positioning, scaling, alignment,
	/// highlighting, transitions, background rendering, and tap detection.
	/// </summary>
	public abstract class Widget : IWidget, IDisposable
	{
		#region Fields

		private HorizontalAlignment _horizontal;
		private VerticalAlignment _vertical;
		private bool _drawWhenInactive = true;
		protected Rectangle _rect;

		private Point _position;

		private float _scale;

		/// <summary>
		/// Raised when this widget becomes highlighted.
		/// </summary>
		public event EventHandler<HighlightEventArgs> OnHighlight;

		/// <summary>
		/// Whether this widget is currently highlighted.
		/// </summary>
		private bool _highlight = false;

		#endregion //Fields

		#region Properties

		/// <summary>
		/// A name for this widget, used for debugging purposes.
		/// </summary>
		public string Name { get; set; } = "Widget";

		/// <summary>
		/// A clock that tracks elapsed time since the widget was last highlighted, used for pulsate effects.
		/// </summary>
		public GameClock HighlightClock
		{
			get; protected set;
		}

		/// <inheritdoc/>
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
		/// The bounding rectangle occupied by this widget on screen.
		/// </summary>
		public virtual Rectangle Rect
		{
			get
			{
				return _rect;
			}
		}

		/// <summary>
		/// The position of this widget, used as the anchor point for alignment. Setting this recalculates the bounding rectangle.
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
		/// The horizontal alignment of this widget relative to its position. Setting this recalculates the bounding rectangle.
		/// </summary>
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

		/// <summary>
		/// The vertical alignment of this widget relative to its position. Setting this recalculates the bounding rectangle.
		/// </summary>
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

		/// <summary>
		/// Whether to draw this widget when its parent screen is inactive.
		/// </summary>
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

		/// <summary>
		/// The scale factor applied to this widget. Default is 1.0. Setting this recalculates the bounding rectangle.
		/// </summary>
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

		/// <inheritdoc/>
		public bool HasBackground { get; set; }

		/// <inheritdoc/>
		public bool HasOutline { get; set; }

		/// <summary>
		/// The transition object controlling how this widget animates during screen transitions.
		/// </summary>
		public virtual ITransitionObject TransitionObject { get; set; }

		/// <summary>
		/// Whether this widget can be highlighted by user input.
		/// </summary>
		public bool Highlightable { get; set; }

		/// <summary>
		/// The background renderer for this widget.
		/// </summary>
		protected Background Background { get; set; }

		/// <summary>
		/// Whether a tap was held on the previous frame, used to detect rising edges.
		/// </summary>
		private bool _prevTapHeld;

		/// <inheritdoc/>
		public bool WasTapped { get; private set; }

		/// <inheritdoc/>
		public bool IsTapHeld { get; private set; }

		/// <inheritdoc/>
		public bool IsTappable { get; set; }

		/// <summary>
		/// The input helper service, used for tap and highlight detection.
		/// </summary>
		protected IInputHelper InputHelper { get; set; }

		/// <inheritdoc/>
		public bool IsHidden { get; set; } = false;

		#endregion //Properties

		#region Initialization

		/// <summary>
		/// Initializes a new <see cref="Widget"/> with default values.
		/// </summary>
		protected Widget()
		{
			Highlightable = true;
			_horizontal = HorizontalAlignment.Left;
			_vertical = VerticalAlignment.Top;
			_scale = 1.0f;
			HighlightClock = new GameClock();
			TransitionObject = new WipeTransitionObject(StyleSheet.DefaultTransition);
			Background = new Background();
			IsTappable = false;
			_prevTapHeld = false;
		}

		/// <summary>
		/// Initializes a new <see cref="Widget"/> by copying values from an existing instance.
		/// </summary>
		/// <param name="inst">The widget to copy from.</param>
		protected Widget(Widget inst)
		{
			_horizontal = inst.Horizontal;
			_vertical = inst.Vertical;
			_scale = inst.Scale;
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
			IsTappable = inst.IsTappable;
		}

		/// <summary>
		/// Creates a deep copy of this widget.
		/// </summary>
		/// <returns>A new <see cref="IScreenItem"/> that is a copy of this instance.</returns>
		public abstract IScreenItem DeepCopy();

		#endregion //Initialization

		#region Methods

		/// <summary>
		/// Loads content for this widget, including the background texture and input helper service.
		/// </summary>
		/// <param name="screen">The screen whose services and content manager are used for loading.</param>
		public virtual async Task LoadContent(IScreen screen)
		{
			await Background.LoadContent(screen);

			InputHelper = screen.ScreenManager.Game.Services.GetService<IInputHelper>();
		}

		/// <summary>
		/// Unloads content and releases references held by this widget.
		/// </summary>
		public virtual void UnloadContent()
		{
			TransitionObject = null;
			OnHighlight = null;
		}

		/// <summary>
		/// Recalculates the bounding rectangle based on the current position, size, scale, and alignment.
		/// </summary>
		protected abstract void CalculateRect();

		/// <summary>
		/// Updates this widget for the current frame, including highlight clock and tap detection.
		/// </summary>
		/// <param name="screen">The screen this widget belongs to.</param>
		/// <param name="gameTime">The current game time.</param>
		public virtual void Update(IScreen screen, GameClock gameTime)
		{
			HighlightClock.Update(gameTime);

			if (IsTappable)
			{
				IsTapHeld = InputHelper.Highlights.Exists(x => Rect.Contains(x.Position));
				WasTapped = !_prevTapHeld && IsTapHeld;
				_prevTapHeld = IsTapHeld;
			}
		}

		/// <summary>
		/// Determines whether this widget should be drawn, based on its hidden state and the screen's active state.
		/// </summary>
		/// <param name="screen">The screen this widget belongs to.</param>
		/// <returns><c>true</c> if the widget should be drawn; otherwise, <c>false</c>.</returns>
		protected bool ShouldDraw(IScreen screen)
		{
			//check if we don't want to draw this widget when inactive
			return (!IsHidden && (DrawWhenInactive || screen.IsActive));
		}

		/// <summary>
		/// Draws the background fill and outline for this widget when the screen is active.
		/// </summary>
		/// <param name="screen">The screen this widget belongs to.</param>
		/// <param name="gameTime">The current game time.</param>
		public virtual void DrawBackground(IScreen screen, GameClock gameTime)
		{
			if (!ShouldDraw(screen))
			{
				return;
			}

			//draw the background rectangle if in touch mode
			if (screen.IsActive)
			{
				Background.Draw(this, screen);
			}
		}

		/// <summary>
		/// Draws this widget. Subclasses must implement this to render their content.
		/// </summary>
		/// <param name="screen">The screen this widget belongs to.</param>
		/// <param name="gameTime">The current game time.</param>
		public abstract void Draw(IScreen screen, GameClock gameTime);

		/// <summary>
		/// Gets the draw position of this widget, adjusted for screen transitions.
		/// </summary>
		/// <param name="screen">The screen used to compute the transition offset.</param>
		/// <returns>The adjusted position to draw this widget at.</returns>
		protected virtual Point DrawPosition(IScreen screen)
		{
			//take the transition position into account
			return TransitionObject.Position(screen, Rect);
		}

		/// <summary>
		/// Checks whether this widget should become highlighted based on the current input position,
		/// and raises the <see cref="OnHighlight"/> event when newly highlighted.
		/// </summary>
		/// <param name="highlight">The highlight event arguments containing the input position.</param>
		/// <returns><c>true</c> if this widget is highlighted; otherwise, <c>false</c>.</returns>
		public virtual bool CheckHighlight(HighlightEventArgs highlight)
		{
			if (Highlightable)
			{
				//get the previous value
				var prev = IsHighlighted;

				//Check if this dude should be highlighted
				var currentHighlight = Rect.Contains(highlight.Position);

				if (!prev && currentHighlight)
				{
					//This is a new highlight
					IsHighlighted = true;

					//Do we need to run the highlight event?
					if (null != OnHighlight)
					{
						OnHighlight(this, highlight);
					}
				}
				else if (prev && !currentHighlight)
				{
					//Check if this thing is still highlighted
					IsHighlighted = highlight.InputHelper.Highlights.Exists(x => Rect.Contains(x.Position));
				}
			}

			return IsHighlighted;
		}

		/// <summary>
		/// Disposes this widget by unloading its content.
		/// </summary>
		public virtual void Dispose()
		{
			UnloadContent();
		}

		/// <summary>
		/// Returns the name of this widget.
		/// </summary>
		/// <returns>The <see cref="Name"/> of this widget.</returns>
		public override string ToString()
		{
			return Name;
		}

		#endregion //Methods
	}
}