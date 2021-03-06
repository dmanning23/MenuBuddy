using FontBuddyLib;
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
	public class MenuEntry : RelativeLayoutButton, ILabel, IDisposable
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

		#region Initialization

		/// <summary>
		/// Constructs a new menu entry with the specified text.
		/// </summary>
		public MenuEntry(string text, ContentManager content)
		{
			_text = text;
			Horizontal = HorizontalAlignment.Center;
			Vertical = VerticalAlignment.Top;

			Label = CreateLabel(content);

			Init();
		}

		/// <summary>
		/// Constructs a new menu entry with the specified text.
		/// </summary>
		public MenuEntry(string text, IFontBuddy font, IFontBuddy highlightedFont = null)
		{
			_text = text;
			Horizontal = HorizontalAlignment.Center;
			Vertical = VerticalAlignment.Top;

			Label = CreateLabel(font, highlightedFont);

			Init();
		}

		public MenuEntry(MenuEntry inst) : base(inst)
		{
			_text = inst._text;

			Label = CreateLabel(inst.Label);

			Init();
		}

		public virtual Label CreateLabel(ContentManager content)
		{
			return new Label(Text, content, FontSize.Medium)
			{
				Vertical = VerticalAlignment.Center,
				Horizontal = HorizontalAlignment.Center
			};
		}

		public virtual Label CreateLabel(IFontBuddy font, IFontBuddy highlightedFont = null)
		{
			return new Label(Text, font, highlightedFont)
			{
				Vertical = VerticalAlignment.Center,
				Horizontal = HorizontalAlignment.Center
			};
		}

		public virtual Label CreateLabel(Label inst)
		{
			return new Label(inst);
		}

		private void Init()
		{
			//get the label height from the font being used
			var labelHeight = Label.Rect.Height;
			var labelWidth = Resolution.TitleSafeArea.Width;

			var shim = new Shim(labelWidth, labelHeight * 0.7f)
			{
				Horizontal = HorizontalAlignment.Center,
				Vertical = VerticalAlignment.Bottom,
				HasBackground = this.HasBackground,
			};

			Size = new Vector2(labelWidth, labelHeight + shim.Rect.Height);

			AddItem(shim);
			AddItem(Label);
		}

		public override IScreenItem DeepCopy()
		{
			return new MenuEntry(this);
		}

		public override void UnloadContent()
		{
			base.UnloadContent();
			Label?.UnloadContent();
			Label = null;
		}

		#endregion //Initialization

		#region Methods

		public override string ToString()
		{
			return Text;
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