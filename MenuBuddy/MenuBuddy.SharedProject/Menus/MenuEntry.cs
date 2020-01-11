using FontBuddyLib;
using InputHelper;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using ResolutionBuddy;
using System;
using System.Threading.Tasks;

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

		public bool IsPassword
		{
			get
			{
				return null != Label ? Label.IsPassword : false;
			}
			set
			{
				if (null != Label)
				{
					Label.IsPassword = value;
				}
			}
		}

		public FontSize FontSize
		{
			get
			{
				return Label.FontSize;
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

		public IFontBuddy Font
		{
			get
			{
				return Label.Font;
			}
			set
			{
				Label.Font = value;
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
				Label.Scale = Scale;
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
				PlayHighlightSound(this, new HighlightEventArgs(null));

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
				PlayHighlightSound(this, new HighlightEventArgs(null));

				Right(this, new EventArgs());
			}
		}

		#endregion

		#region Initialization

		/// <summary>
		/// Constructs a new menu entry with the specified text.
		/// </summary>
		public MenuEntry(string text, ContentManager content)
		{
			_text = text;
			Horizontal = HorizontalAlignment.Center;
			Vertical = VerticalAlignment.Top;

			Label = new Label(Text, content, FontSize.Medium)
			{
				Vertical = VerticalAlignment.Center,
				Horizontal = HorizontalAlignment.Center
			};
		}

		/// <summary>
		/// Constructs a new menu entry with the specified text.
		/// </summary>
		public MenuEntry(string text, IFontBuddy font, IFontBuddy highlightedFont = null)
		{
			_text = text;
			Horizontal = HorizontalAlignment.Center;
			Vertical = VerticalAlignment.Top;

			Label = new Label(Text, font, highlightedFont)
			{
				Vertical = VerticalAlignment.Center,
				Horizontal = HorizontalAlignment.Center
			};
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

		public override async Task LoadContent(IScreen screen)
		{
			//get the label height from the font being used
			var labelHeight = Label.Rect.Height;
			Size = new Vector2(Resolution.TitleSafeArea.Width, labelHeight * 2.5f);

			AddItem(new Shim(Size.X, labelHeight * 2f)
			{
				Horizontal = HorizontalAlignment.Center,
				Vertical = VerticalAlignment.Center,
				HasBackground = true,
			});

			AddItem(Label);

			await base.LoadContent(screen);
		}

		#endregion //Initialization

		#region Methods

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

		public void ScaleToFit(int rowWidth)
		{
			Label.ScaleToFit(rowWidth);
		}

		public void ShrinkToFit(int rowWidth)
		{
			Label.ShrinkToFit(rowWidth);
		}

		#endregion //Methods
	}
}