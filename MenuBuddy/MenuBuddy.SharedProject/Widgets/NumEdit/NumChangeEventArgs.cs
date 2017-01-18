using System;

namespace MenuBuddy
{
	/// <summary>
	/// The event argumnets that get fired off when the number is manually edited in a NumEdit using the numpad
	/// </summary>
	public class NumChangeEventArgs : EventArgs
    {
		public float Num { get; set; }

		public NumChangeEventArgs(float num)
		{
			Num = num;
		}
	}
}
