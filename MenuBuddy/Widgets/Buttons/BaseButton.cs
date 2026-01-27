using GameTimer;
using InputHelper;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using System;
using System.Threading.Tasks;

namespace MenuBuddy
{
	/// <summary>
	/// Abstract base class for clickable buttons. Provides click handling, sound effects,
	/// layout management, and left/right navigation support.
	/// </summary>
	public abstract class BaseButton : Widget, IButton, IDisposable
	{
		#region Fields

		/// <summary>
		/// Whether to draw this button when the parent screen is inactive.
		/// </summary>
		private bool _drawWhenInactive;

		private Vector2 _size;

		/// <summary>
		/// Raised when this button is clicked.
		/// </summary>
		public event EventHandler<ClickEventArgs> OnClick;

		/// <summary>
		/// Timer used to track the visual click state for a brief period after a click occurs.
		/// </summary>
		private CountdownTimer _clickTimer;

		/// <summary>
		/// The duration in seconds that the button remains in the "clicked" visual state after being clicked.
		/// </summary>
		public float ClickTimeDelta { get; set; } = 0.2f;

		private ILayout _layout;

		#endregion //Fields

		#region Properties

		/// <summary>
		/// Whether this button can be clicked. Set to <c>false</c> to disable click handling.
		/// </summary>
		public bool Clickable { get; set; }

		/// <summary>
		/// The size of this button. Setting this recalculates the bounding rectangle.
		/// </summary>
		public Vector2 Size
		{
			protected get
			{
				return _size;
			}
			set
			{
				if (_size != value)
				{
					_size = value;
					CalculateRect();
				}
			}
		}

		/// <summary>
		/// The scale factor for this button. Setting this also updates the inner layout's scale.
		/// </summary>
		public override float Scale
		{
			get
			{
				return base.Scale;
			}
			set
			{
				base.Scale = value;
				Layout.Scale = value;
			}
		}

		/// <summary>
		/// The layout used to arrange child items within this button.
		/// </summary>
		public ILayout Layout
		{
			get
			{
				return _layout;
			}
			protected set
			{
				_layout = value;
				if (null != _layout)
				{
					_layout.TransitionObject = TransitionObject;
				}
			}
		}

		/// <summary>
		/// Whether this button is highlighted. Setting this also updates the inner layout's highlight state.
		/// </summary>
		public override bool IsHighlighted
		{
			get
			{
				return base.IsHighlighted;
			}
			set
			{
				base.IsHighlighted = value;
				Layout.IsHighlighted = value;
			}
		}

		/// <inheritdoc/>
		public override bool DrawWhenInactive
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

		/// <inheritdoc/>
		public override Point Position
		{
			get { return base.Position; }
			set
			{
				if (base.Position != value)
				{
					base.Position = value;
					CalculateRect();
				}
			}
		}

		/// <summary>
		/// A text description of this button's function.
		/// </summary>
		public string Description { get; set; }

		/// <inheritdoc/>
		public bool IsQuiet { get; set; }

		/// <summary>
		/// The sound effect played when this button is highlighted.
		/// </summary>
		protected SoundEffect HighlightedSoundEffect { get; set; }

		/// <summary>
		/// The sound effect played when this button is clicked.
		/// </summary>
		protected SoundEffect ClickedSoundEffect { get; set; }

		/// <summary>
		/// Whether this button is currently in its clicked visual state.
		/// Setting this also updates the inner layout's clicked state.
		/// </summary>
		public bool IsClicked
		{
			get
			{
				return _clickTimer.HasTimeRemaining;
			}
			set
			{
				Layout.IsClicked = value;
			}
		}

		/// <summary>
		/// The transition object for this button. Setting this also updates the inner layout's transition.
		/// </summary>
		public override ITransitionObject TransitionObject
		{
			get
			{
				return base.TransitionObject;
			}

			set
			{
				base.TransitionObject = value;

				if (null != Layout)
				{
					Layout.TransitionObject = value;
				}
			}
		}

		/// <inheritdoc/>
		public string HighlightedSound { get; set; }

		/// <inheritdoc/>
		public string ClickedSound { get; set; }

		/// <summary>
		/// Raised when left navigation is triggered on this button.
		/// </summary>
		public event EventHandler OnLeft;

		/// <summary>
		/// Raised when right navigation is triggered on this button.
		/// </summary>
		public event EventHandler OnRight;

		#endregion //Properties

		#region Initialization

		/// <summary>
		/// Initializes a new <see cref="BaseButton"/> with default click handling and sound effects.
		/// </summary>
		protected BaseButton()
		{
			Clickable = true;
			IsQuiet = false;
			_clickTimer = new CountdownTimer();

			//by default, just play a sound when this item is selected
			OnClick += PlaySelectedSound;

			OnClick += ((obj, e) =>
			{
				_clickTimer.Start(ClickTimeDelta);
			});

			OnHighlight += PlayHighlightSound;
			OnHighlight += ((obj, e) =>
			{
				IsHighlighted = true;
			});

			HighlightedSound = StyleSheet.HighlightedSoundResource;
			ClickedSound = StyleSheet.ClickedSoundResource;
		}

		/// <summary>
		/// Initializes a new <see cref="BaseButton"/> by copying values from an existing instance.
		/// </summary>
		/// <param name="inst">The button to copy from.</param>
		protected BaseButton(BaseButton inst) : base(inst)
		{
			Clickable = true;
			_drawWhenInactive = inst._drawWhenInactive;
			_size = inst._size;
			Description = inst.Description;
			OnClick = inst.OnClick;
			IsQuiet = inst.IsQuiet;
			HighlightedSoundEffect = inst.HighlightedSoundEffect;
			ClickedSoundEffect = inst.ClickedSoundEffect;
			_clickTimer = inst._clickTimer;
			OnLeft = inst.OnLeft;
			OnRight = inst.OnRight;

			HighlightedSound = inst.HighlightedSound;
			ClickedSound = inst.ClickedSound;
		}

		/// <summary>
		/// Loads content for this button, including sound effects and the inner layout.
		/// </summary>
		/// <param name="screen">The screen whose content manager is used for loading.</param>
		public override async Task LoadContent(IScreen screen)
		{
			if (null != screen.ScreenManager)
			{
				if (!string.IsNullOrEmpty(HighlightedSound))
				{
					HighlightedSoundEffect = screen.Content.Load<SoundEffect>(HighlightedSound);
				}

				if (!string.IsNullOrEmpty(ClickedSound))
				{
					ClickedSoundEffect = screen.Content.Load<SoundEffect>(ClickedSound);
				}
			}

			await Layout.LoadContent(screen);
			await base.LoadContent(screen);
		}

		/// <summary>
		/// Unloads content and releases references held by this button, including the layout and event handlers.
		/// </summary>
		public override void UnloadContent()
		{
			base.UnloadContent();

			Layout?.UnloadContent();
			Layout = null;

			OnClick = null;
			OnLeft = null;
			OnRight = null;
		}

		#endregion //Initialization

		#region Methods

		/// <summary>
		/// Adds a screen item to this button's layout and assigns it the button's transition object.
		/// </summary>
		/// <param name="item">The screen item to add.</param>
		public void AddItem(IScreenItem item)
		{
			Layout.AddItem(item);
			CalculateRect();

			var widget = item as ITransitionable;
			if (null != widget)
			{
				widget.TransitionObject = TransitionObject;
			}

		}

		/// <summary>
		/// Removes a screen item from this button's layout.
		/// </summary>
		/// <param name="item">The screen item to remove.</param>
		/// <returns><c>true</c> if the item was found and removed; otherwise, <c>false</c>.</returns>
		public bool RemoveItem(IScreenItem item)
		{
			var result = Layout.RemoveItem(item);
			CalculateRect();
			return result;
		}

		/// <inheritdoc/>
		public override void Update(IScreen screen, GameClock gameTime)
		{
			base.Update(screen, gameTime);
			_clickTimer.Update(gameTime);
			Layout.Update(screen, gameTime);
			Layout.IsClicked = IsClicked;
		}

		/// <inheritdoc/>
		public override void DrawBackground(IScreen screen, GameClock gameTime)
		{
			base.DrawBackground(screen, gameTime);

			Layout.DrawBackground(screen, gameTime);
		}

		/// <inheritdoc/>
		public override void Draw(IScreen screen, GameClock gameTime)
		{
			Layout.Draw(screen, gameTime);
		}

		/// <inheritdoc/>
		protected override void CalculateRect()
		{
			_rect = Layout.Rect;
		}

		/// <summary>
		/// Plays the clicked sound effect, unless the button is quiet or no sound is loaded.
		/// </summary>
		/// <param name="obj">The source of the event.</param>
		/// <param name="e">The click event arguments.</param>
		public void PlaySelectedSound(object obj, ClickEventArgs e)
		{
			//play the sound effect
			if (!IsQuiet && (null != ClickedSoundEffect))
			{
				ClickedSoundEffect.Play();
			}
		}

		/// <summary>
		/// Plays the highlighted sound effect, unless the button is quiet or no sound is loaded.
		/// </summary>
		/// <param name="obj">The source of the event.</param>
		/// <param name="e">The highlight event arguments.</param>
		public void PlayHighlightSound(object obj, HighlightEventArgs e)
		{
			if (!IsQuiet && (null != HighlightedSoundEffect))
			{
				//play menu noise
				HighlightedSoundEffect.Play();
			}
		}

		/// <summary>
		/// Checks whether the click position is within this button's bounds, and triggers a click if so.
		/// </summary>
		/// <param name="click">The click event arguments containing the click position.</param>
		/// <returns><c>true</c> if this button was clicked; otherwise, <c>false</c>.</returns>
		public virtual bool CheckClick(ClickEventArgs click)
		{
			//check if the widget was clicked
			if (Rect.Contains(click.Position) && Clickable)
			{
				Clicked(this, click);
				return true;
			}

			return false;
		}

		/// <inheritdoc/>
		public virtual void Clicked(object obj, ClickEventArgs e)
		{
			if (OnClick != null)
			{
				OnClick(obj, e);
			}
		}

		/// <summary>
		/// Raises the <see cref="OnLeft"/> event and plays the highlight sound.
		/// </summary>
		public virtual void OnLeftEntry()
		{
			if (OnLeft != null)
			{
				//play menu noise
				PlayHighlightSound(this, new HighlightEventArgs(null));

				OnLeft(this, new EventArgs());
			}
		}

		/// <summary>
		/// Raises the <see cref="OnRight"/> event and plays the highlight sound.
		/// </summary>
		public virtual void OnRightEntry()
		{
			if (OnRight != null)
			{
				//play menu noise
				PlayHighlightSound(this, new HighlightEventArgs(null));

				OnRight(this, new EventArgs());
			}
		}

		#endregion //Methods
	}
}