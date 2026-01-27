using System;

namespace MenuBuddy
{
	/// <summary>
	/// Event arguments for number change events, containing the updated numeric value.
	/// </summary>
	public class NumChangeEventArgs : EventArgs
    {
		/// <summary>
		/// The updated numeric value.
		/// </summary>
		public float Num { get; set; }

		/// <summary>
		/// Initializes a new <see cref="NumChangeEventArgs"/> with the specified number.
		/// </summary>
		/// <param name="num">The updated numeric value.</param>
		public NumChangeEventArgs(float num)
		{
			Num = num;
		}
	}
}
