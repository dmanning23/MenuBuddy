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
	public class MenuEntry : RelativeLayoutButton, ILabel, IMenuEntry
	{
		#region Fields

		private string _text;

		#endregion //Fields

		#region Properties

		public Label Label { get; private set; }
		
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
			Label = new Label(text)
			{
				Horizontal = HorizontalAlignment.Center,
				Vertical = VerticalAlignment.Center,
			};
			_text = text;
			Horizontal = HorizontalAlignment.Center;
			Vertical = VerticalAlignment.Top;
		}

		public MenuEntry(MenuEntry inst) : base(inst)
		{
			Label = new Label(inst.Label);
			_text = inst._text;
			Left = inst.Left;
			Right = inst.Right;
		}

		public override IScreenItem DeepCopy()
		{
			return new MenuEntry(this);
		}

		public override void LoadContent(IScreen screen)
		{
			base.LoadContent(screen);

			//Add the text label
			Label = new Label(Text)
			{
                Vertical = VerticalAlignment.Top,
				Horizontal = HorizontalAlignment.Center
			};

			//get the label rect
			var labelRect = Label.Rect;
			Size = new Vector2(768f, labelRect.Size.Y);

			AddItem(Label);
		}

		#endregion //Initialization

		#region Methods

		public override string ToString()
		{
			return Text;
		}

		#endregion //Methods
	}
}