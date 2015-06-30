using System;
using Microsoft.Xna.Framework;

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
		private Label _label;

		#endregion //Fields

		#region Properties

		public override Rectangle Rect
		{
			get
			{
				var rect = base.Rect;
				rect.Width = 768;
				return rect;
			}
		}

		public string Text 
		{
			get
			{
				return _text;
			}
			set
			{
				_text = value;
				if (null != _label)
				{
					_label.Text = value;
				}
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
				if (!Style.IsQuiet && (null != Style.SelectionChangeSoundEffect))
				{
					Style.SelectionChangeSoundEffect.Play();
				}

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
				if (!Style.IsQuiet && (null != Style.SelectionChangeSoundEffect))
				{
					Style.SelectionChangeSoundEffect.Play();
				}

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
			Style = DefaultStyles.Instance().MenuEntryStyle;
			Text = text;
		}

		public override void LoadContent(IScreen screen)
		{
			base.LoadContent(screen);

			//Add the text label
			_label = new Label(Text)
			{
				Vertical = VerticalAlignment.Center,
				Horizontal = HorizontalAlignment.Center,
				Style = this.Style
			};
			AddItem(_label);
		}

		public override string ToString()
		{
			return Text;
		}

		#endregion
	}
}