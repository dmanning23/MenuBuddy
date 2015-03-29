using System;

namespace MenuBuddy
{
	/// <summary>
	/// This is a menu entry that changes the integer value on left/right.
	/// </summary>
	public class MenuEntryInt : MenuEntry
	{
		#region Fields

		/// <summary>
		/// The text of this menu entry without the value of it
		/// </summary>
		public string Label { get; set; }

		/// <summary>
		/// The current value of this menu entry.
		/// </summary>
		public int Value { get; set; }

		/// <summary>
		/// How much to subtract/add on left/right
		/// </summary>
		public int Step { get; set; }

		/// <summary>
		/// The min allowed value of this item.
		/// </summary>
		public int Min { get; set; }

		/// <summary>
		/// The max allowed value of this item
		/// </summary>
		public int Max { get; set; }

		#endregion //Fields

		#region Methods

		/// <summary>
		/// Constructs a new menu entry with the specified text.
		/// </summary>
		public MenuEntryInt(StyleSheet style, string text, int startValue)
			: base(style, text)
		{
			Label = text;
			Value = startValue;
			Step = 1;
			Min = 0;
			Max = 10;

			SetMenuEntryText();

			Left += Decrement;
			Right += Increment;
		}

		public void Increment(object sender, EventArgs e)
		{
			Value = Math.Min(Value + Step, Max);
			SetMenuEntryText();
		}

		public void Decrement(object sender, EventArgs e)
		{
			Value = Math.Max(Value - Step, Min);
			SetMenuEntryText();
		}

		/// <summary>
		/// Fills in the latest values for the options screen menu text.
		/// </summary>
		private void SetMenuEntryText()
		{
			Text = string.Format("{0}: {1}", Label, Value);
		}

		#endregion //Methods
	}
}