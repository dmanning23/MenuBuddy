using InputHelper;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;

namespace MenuBuddy
{
	/// <summary>
	/// A NumEdit control. 
	/// This is a widget with a number in it. 
	/// When the widget is clicked, a numpad pops up where the user can edit the number.
	/// Wehn the user edits the number with the 9pad, a special event is fired off.
	/// </summary>
	public class NumEdit : RelativeLayoutButton, INumEdit, IDisposable
	{
		#region Fields

		/// <summary>
		/// backgin field for the number of this dude
		/// </summary>
		private float _number;

		#endregion //Fields

		#region Properties

		/// <summary>
		/// the screen that holds this guy
		/// </summary>
		private IScreen Screen
		{
			get; set;
		}

		/// <summary>
		/// Property to get or set the number of this dude.
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
		/// The label that displays the text
		/// </summary>
		private Label NumLabel { get; set; }

		public FontSize FontSize
		{
			get
			{
				return NumLabel.FontSize;
			}
		}

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
		/// Event that gets fired when the user finishes changing the number from the numpad
		/// </summary>
		public event EventHandler<NumChangeEventArgs> OnNumberEdited;

		public float Min { get; set; }
		public float Max { get; set; }
		public bool AllowDecimal { get; set; }
		public bool AllowNegative { get; set; }

		#endregion //Properties

		#region Methods

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

		public NumEdit(ContentManager content, FontSize fontSize = FontSize.Medium) : this(0, content, fontSize)
		{
		}

		public override void LoadContent(IScreen screen)
		{
			Screen = screen;
			base.LoadContent(screen);
		}

		/// <summary>
		/// Method that gets called when the label is clicked to create the numpad.
		/// Adds a new screen with a numpad.
		/// </summary>
		/// <param name="obj"></param>
		/// <param name="e"></param>
		public void CreateNumPad(object obj, ClickEventArgs e)
		{
			//create the dropdown screen
			var numpad = new NumPadScreen(this, AllowDecimal, AllowNegative);

			//add the screen over the current one
			Screen.ScreenManager.AddScreen(numpad);
		}

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

		public override void Dispose()
		{
			base.Dispose();
			OnNumberEdited = null;
		}

		#endregion //Methods
	}
}
