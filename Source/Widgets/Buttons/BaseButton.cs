using Microsoft.Xna.Framework;
using MouseBuddy;
using System;
using System.Collections.Generic;

namespace MenuBuddy
{
	/// <summary>
	/// This is a button that can be clicked on
	/// </summary>
	public abstract class BaseButton : Widget, IButton
	{
		#region Fields

		/// <summary>
		/// whether or not to draw this item when inactive
		/// </summary>
		private bool _drawWhenInactive;

		private Vector2 _size;

		public event EventHandler<SelectedEventArgs> OnSelect;

		public event EventHandler<ClickEventArgs> OnClick;

		#endregion //Fields

		#region Properties

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
		/// The layout to add to this button
		/// </summary>
		public ILayout Layout { get; protected set; }

		public override bool Highlight
		{
			protected get
			{
				return base.Highlight;
			}
			set
			{
				base.Highlight = value;
				Layout.Highlight = value;
			}
		}

		public override bool DrawWhenInactive
		{
			get
			{
				return _drawWhenInactive;
			}
			set
			{
				_drawWhenInactive = value;
				Layout.Highlight = value;
			}
		}

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
		/// A description of the function of the menu entry.
		/// </summary>
		public string Description { get; set; }

		#endregion //Properties

		#region Initialization

		/// <summary>
		/// Constructs a new button with the specified text.
		/// </summary>
		protected BaseButton()
		{
			//when this item is clicked, run the OnSelect event
			OnClick += ((obj, e) =>
			{
				if (null != OnSelect)
				{
					Selected(obj, PlayerIndex.One);
				}
			});

			//by default, just play a sound when this item is selected
			OnSelect += PlaySelectedSound;
			OnHighlight += PlayHighlightSound;
			OnHighlight += ((obj, e) =>
			{
				Highlight = true;
			});
		}

		/// <summary>
		/// Constructs a new button with the specified text.
		/// </summary>
		protected BaseButton(BaseButton inst) : base(inst)
		{
			_drawWhenInactive = inst._drawWhenInactive;
			_size = inst._size;
			OnSelect = inst.OnSelect;
			Description = inst.Description;
			OnClick = inst.OnClick;
		}

		#endregion //Initialization

		#region Methods

		public void AddItem(IScreenItem item)
		{
			Layout.AddItem(item);
			CalculateRect();
		}

		public bool RemoveItem(IScreenItem item)
		{
			var result = Layout.RemoveItem(item);
			CalculateRect();
			return result;
		}

		public override void Update(IScreen screen, GameTimer.GameClock gameTime)
		{
			Layout.Update(screen, gameTime);
		}

		public override void Draw(IScreen screen, GameTimer.GameClock gameTime)
		{
			Layout.Draw(screen, gameTime);
		}

		protected override void CalculateRect()
		{
			_rect = Layout.Rect;
		}

		/// <summary>
		/// Method for raising the Selected event.
		/// </summary>
		public void PlaySelectedSound(object obj, SelectedEventArgs e)
		{
			//play the sound effect
			if (!Style.IsQuiet && (null != Style.SelectedSoundEffect))
			{
				Style.SelectedSoundEffect.Play();
			}
		}

		public void PlayHighlightSound(object obj, HighlightEventArgs e)
		{
			if (!Style.IsQuiet && (null != Style.SelectionChangeSoundEffect))
			{
				//play menu noise
				Style.SelectionChangeSoundEffect.Play();
			}
		}

		public void Selected(object obj, PlayerIndex player)
		{
			if (null != OnSelect)
			{
				OnSelect(obj, new SelectedEventArgs(player));
			}
		}

		public virtual bool CheckClick(ClickEventArgs click)
		{
			//check if the widget was clicked
			if (Rect.Contains(click.Position))
			{
				if (OnClick != null)
				{
					OnClick(this, click);
				}

				return true;
			}

			return false;
		}

		#endregion //Methods
	}
}