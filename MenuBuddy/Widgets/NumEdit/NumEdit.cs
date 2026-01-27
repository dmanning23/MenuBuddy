using InputHelper;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Threading.Tasks;

namespace MenuBuddy
{
	/// <summary>
	/// A numeric input widget that displays a number and opens a <see cref="NumPadScreen"/> for editing when clicked.
	/// </summary>
	public class NumEdit : RelativeLayoutButton, INumEdit, IDisposable
	{
		#region Fields

		/// <summary>
		/// Backing field for <see cref="Number"/>.
		/// </summary>
		private float _number;

		#endregion //Fields

		#region Properties

		/// <summary>
		/// The screen that contains this numeric edit widget.
		/// </summary>
		private IScreen Screen
		{
			get; set;
		}

		/// <summary>
		/// The current numeric value. Setting this updates the label text.
		/// </summary>
		public float Number
		{
			get
			{
				return _number;
			}
			set
			{
				_number = value;
				if (null != NumLabel)
				{
					NumLabel.Text = _number.ToString();
				}
			}
		}

		/// <summary>
		/// The text representation of the number, used during numpad editing. May contain partial input such as a trailing decimal point.
		/// </summary>
		public string NumberText
		{
			get
			{
				return (null != NumLabel) ? NumLabel.Text : string.Empty;
			}
			set
			{
				if (NumLabel != null)
				{
					//check for empty string
					if (string.IsNullOrEmpty(value))
					{
						NumLabel.Text = "0";
					}
					else
					{
						//set the text to the provided value, whether or not it is a valid number
						NumLabel.Text = value;
					}
				}
			}
		}

		/// <summary>
		/// The label widget that displays the current number.
		/// </summary>
		private Label NumLabel { get; set; }

		/// <inheritdoc/>
		public FontSize FontSize
		{
			get
			{
				return NumLabel.FontSize;
			}
		}

		/// <inheritdoc/>
		public Color? ShadowColor
		{
			get
			{
				return NumLabel.ShadowColor;
			}
			set
			{
				NumLabel.ShadowColor = value;
			}
		}

		/// <inheritdoc/>
		public Color? TextColor
		{
			get
			{
				return NumLabel.TextColor;
			}
			set
			{
				NumLabel.TextColor = value;
			}
		}

		/// <summary>
		/// Raised when the user finishes editing and the number value has changed.
		/// </summary>
		public event EventHandler<NumChangeEventArgs> OnNumberEdited;

		/// <summary>
		/// The minimum allowed value for this numeric input.
		/// </summary>
		public float Min { get; set; }

		/// <summary>
		/// The maximum allowed value for this numeric input.
		/// </summary>
		public float Max { get; set; }

		/// <summary>
		/// Whether the numpad allows decimal point input.
		/// </summary>
		public bool AllowDecimal { get; set; }

		/// <summary>
		/// Whether the numpad allows toggling the sign to negative.
		/// </summary>
		public bool AllowNegative { get; set; }

		#endregion //Properties

		#region Methods

		/// <summary>
		/// Initializes a new <see cref="NumEdit"/> with the specified initial number.
		/// </summary>
		/// <param name="num">The initial numeric value.</param>
		/// <param name="content">The content manager used to load font resources.</param>
		/// <param name="fontSize">The font size category. Defaults to <see cref="FontSize.Medium"/>.</param>
		public NumEdit(float num, ContentManager content, FontSize fontSize = FontSize.Medium)
		{
			Min = float.MinValue;
			Max = float.MaxValue;
			AllowDecimal = true;
			AllowNegative = true;
			_number = num;
			OnClick += CreateNumPad;
			NumLabel = new Label(num.ToString(), content, fontSize)
			{
				Horizontal = HorizontalAlignment.Center,
				Vertical = VerticalAlignment.Center,
			};
			AddItem(NumLabel);
		}

		/// <summary>
		/// Initializes a new <see cref="NumEdit"/> with a default value of 0.
		/// </summary>
		/// <param name="content">The content manager used to load font resources.</param>
		/// <param name="fontSize">The font size category. Defaults to <see cref="FontSize.Medium"/>.</param>
		public NumEdit(ContentManager content, FontSize fontSize = FontSize.Medium) : this(0, content, fontSize)
		{
		}

		/// <inheritdoc/>
		public override async Task LoadContent(IScreen screen)
		{
			Screen = screen;
			await base.LoadContent(screen);
		}

		/// <summary>
		/// Unloads content and releases the <see cref="OnNumberEdited"/> event handler.
		/// </summary>
		public override void UnloadContent()
		{
			base.UnloadContent();
			OnNumberEdited = null;
		}

		/// <summary>
		/// Creates and displays a <see cref="NumPadScreen"/> for numeric input.
		/// </summary>
		/// <param name="obj">The source of the click event.</param>
		/// <param name="e">The click event arguments.</param>
		public async void CreateNumPad(object obj, ClickEventArgs e)
		{
			//create the dropdown screen
			var numpad = new NumPadScreen(this, AllowDecimal, AllowNegative);

			//add the screen over the current one
			await Screen.ScreenManager.AddScreen(numpad);
		}

		/// <inheritdoc/>
		public void SetNumber(float num)
		{
			if (Number != num &&
				(Min <= num && num <= Max))
			{
				Number = num;
				if (null != OnNumberEdited)
				{
					OnNumberEdited(this, new NumChangeEventArgs(Number));
				}

				PlaySelectedSound(this, new ClickEventArgs());
			}
			else
			{
				//you can't set it to the provided number.
				Number = Number;
			}
		}

		#endregion //Methods
	}
}
