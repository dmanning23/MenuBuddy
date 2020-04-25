using Microsoft.Xna.Framework.Content;
using System;

namespace MenuBuddy
{
	/// <summary>
	/// This is a menu entry that changes the boolean value on left/right.
	/// </summary>
	public class MenuEntryBool : MenuEntry
	{
		#region Fields

		/// <summary>
		/// The text of this menu entry without the value of it
		/// </summary>
		public string LabelText { get; set; }

		/// <summary>
		/// The current value of this menu entry.
		/// </summary>
		public bool Value { get; set; }

		#endregion //Fields

		#region Methods

		/// <summary>
		/// Constructs a new menu entry with the specified text.
		/// </summary>
		public MenuEntryBool(string text, bool startValue, ContentManager content)
			: base(text, content)
		{
			LabelText = text;
			Value = startValue;

			SetMenuEntryText();

			OnLeft += ChangeBool;
			OnRight += ChangeBool;
		}

		public void ChangeBool(object sender, EventArgs e)
		{
			Value = !Value;
			SetMenuEntryText();
		}

		/// <summary>
		/// Fills in the latest values for the options screen menu text.
		/// </summary>
		private void SetMenuEntryText()
		{
			Text = string.Format("{0}: {1}", LabelText, Value);
		}

		#endregion //Methods
	}
}