using InputHelper;
using Microsoft.Xna.Framework;
using System;

namespace MenuBuddy
{
	/// <summary>
	/// Helper class represents a single entry in a MenuScreen. By default this
	/// just draws the entry text string, but it can be customized to display menu
	/// entries in different ways. This also provides an event that will be raised
	/// when the menu entry is selected.
	/// </summary>
	public class MenuEntry : RelativeLayoutButton, ILabel, IMenuEntry, IDisposable
	{
		#region Fields

		private string _text;

		#endregion //Fields

		#region Properties

		public Label Label { get; protected set; }

		public string Text
		{
			get
			{
				return _text;
			}
			set
			{
				_text = value;
				if (null != Label)
				{
					Label.Text = value;
				}
			}
		}

		public FontSize FontSize
		{
			get
			{
				return Label.FontSize;
			}
			set
			{
				Label.FontSize = value;
			}
		}

		public Color? ShadowColor
		{
			get
			{
				return Label.ShadowColor;
			}

			set
			{
				Label.ShadowColor = value;
			}
		}

		public Color? TextColor
		{
			get
			{
				return Label.TextColor;
			}

			set
			{
				Label.TextColor = value;
			}
		}

		#endregion //Properties

		#region Events

		/// <summary>
		/// Event raised when the menu entry is selected.
		/// </summary>
		public event EventHandler Left;

		/// <summary>
		/// Event raised when the menu entry is selected.
		/// </summary>
		public event EventHandler Right;

		/// <summary>
		/// Method for raising the Selected event.
		/// </summary>
		public virtual void OnLeftEntry()
		{
			if (Left != null)
			{
				//play menu noise
				PlayHighlightSound(this, new HighlightEventArgs());

				Left(this, new EventArgs());
			}
		}

		/// <summary>
		/// Method for raising the Selected event.
		/// </summary>
		public virtual void OnRightEntry()
		{
			if (Right != null)
			{
				//play menu noise
				PlayHighlightSound(this, new HighlightEventArgs());

				Right(this, new EventArgs());
			}
		}

		#endregion

		#region Initialization

		/// <summary>
		/// Constructs a new menu entry with the specified text.
		/// </summary>
		public MenuEntry(string text)
		{
			HasBackground = true;
			_text = text;
			Horizontal = HorizontalAlignment.Center;
			Vertical = VerticalAlignment.Top;

			Label = CreateLabel();
		}

		public MenuEntry(MenuEntry inst) : base(inst)
		{
			_text = inst._text;
			Left = inst.Left;
			Right = inst.Right;

			Label = new Label(inst.Label);
		}

		public override IScreenItem DeepCopy()
		{
			return new MenuEntry(this);
		}

		public override void LoadContent(IScreen screen)
		{
			//get the label rect
			var labelRect = Label.Rect;
			Size = new Vector2(768f, labelRect.Size.Y);

			AddItem(Label);

			base.LoadContent(screen);
		}

		#endregion //Initialization

		#region Methods

		/// <summary>
		/// Create the text label for this menu item.
		/// </summary>
		/// <returns></returns>
		protected virtual Label CreateLabel()
		{
			return new Label(Text)
			{
				Vertical = VerticalAlignment.Top,
				Horizontal = HorizontalAlignment.Center
			};
		}

		public override string ToString()
		{
			return Text;
		}

		public override void Dispose()
		{
			base.Dispose();
			Left = null;
			Right = null;
		}

		#endregion //Methods
	}
}