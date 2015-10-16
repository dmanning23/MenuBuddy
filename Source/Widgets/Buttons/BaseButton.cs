using Microsoft.Xna.Framework;
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
		/// whether or not this dude is highlighted
		/// </summary>
		private bool _highlight;

		/// <summary>
		/// whether or not to draw this item when inactive
		/// </summary>
		private bool _drawWhenInactive;

		private Vector2 _size;

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

		/// <summary>
		/// The layout to add to this button
		/// </summary>
		public Layout Layout { get; protected set; }

		public override bool Highlight
		{
			protected get
			{
				return _highlight;
			}
			set
			{
				_highlight = value;
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

		/// <summary>
		/// Event raised when the menu entry is selected.
		/// </summary>
		public event EventHandler<PlayerIndexEventArgs> Selected;

		/// <summary>
		/// Method for raising the Selected event.
		/// </summary>
		public virtual void OnSelect(PlayerIndex? playerIndex)
		{
			if (Selected != null)
			{
				Selected(this, new PlayerIndexEventArgs(playerIndex));
			}

			//play the sound effect
			if (!Style.IsQuiet && (null != Style.SelectedSoundEffect))
			{
				Style.SelectedSoundEffect.Play();
			}
		}

		public IEnumerable<IButton> Buttons
		{
			get
			{
				//create the list to hold all the buttons
				var buttons = new List<IButton>();

				//add the layout buttons
				buttons.AddRange(Layout.Buttons);

				//add this dude
				buttons.Add(this);

				return buttons;
			}
		}

		#endregion //Properties

		#region Methods

		/// <summary>
		/// Constructs a new button with the specified text.
		/// </summary>
		protected BaseButton()
		{
		}

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

		#endregion //Methods
	}
}