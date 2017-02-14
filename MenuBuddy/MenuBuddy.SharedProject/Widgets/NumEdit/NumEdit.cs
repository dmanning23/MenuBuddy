using InputHelper;
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
				if (_number == value)
				{
					return;
				}

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

		private FontSize _fontSize;

		/// <summary>
		/// Event that gets fired when the user finishes changing the number from the numpad
		/// </summary>
		public event EventHandler<NumChangeEventArgs> OnNumberEdited;

		#endregion //Properties

		#region Methods

		public NumEdit(FontSize fontSize = FontSize.Medium)
		{
			OnClick += CreateNumPad;
			_fontSize = fontSize;
		}

		public override void LoadContent(IScreen screen)
		{
			Screen = screen;
			base.LoadContent(screen);

			NumLabel = new Label(Number.ToString(), _fontSize)
			{
				Horizontal = HorizontalAlignment.Center,
				Vertical = VerticalAlignment.Center,
			};
			AddItem(NumLabel);
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
			var numpad = new NumPadScreen(this);

			//add the screen over the current one
			Screen.ScreenManager.AddScreen(numpad);
		}

		public void SetNumber(float num)
		{
			if (Number != num)
			{
				Number = num;
				if (null != OnNumberEdited)
				{
					OnNumberEdited(this, new NumChangeEventArgs(Number));
				}

				PlaySelectedSound(this, new ClickEventArgs());
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
