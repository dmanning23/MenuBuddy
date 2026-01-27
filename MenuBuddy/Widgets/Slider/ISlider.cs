using InputHelper;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace MenuBuddy
{
	/// <summary>
	/// Generic interface for a slider widget that allows the user to select a value by dragging a handle.
	/// </summary>
	/// <typeparam name="T">The type of the slider value (e.g., <c>float</c> or <c>int</c>).</typeparam>
	public interface ISlider<T> : IWidget, IDraggable, IHighlightable
	{
		/// <summary>
		/// The minimum value of the slider range.
		/// </summary>
		float Min { get; set; }

		/// <summary>
		/// The maximum value of the slider range.
		/// </summary>
		float Max { get; set; }

		/// <summary>
		/// The current position of the slider handle within the range.
		/// </summary>
		T SliderPosition { get; set; }

		/// <summary>
		/// The overall size of the slider widget in pixels.
		/// </summary>
		Vector2 Size
		{
			set;
		}

		/// <summary>
		/// The size of the draggable handle in pixels.
		/// </summary>
		Vector2 HandleSize
		{
			set;
		}

		/// <summary>
		/// A list of values at which hash marks are drawn on the slider track.
		/// </summary>
		List<float> Marks { get; }

		/// <summary>
		/// Whether the slider accepts user input. When <c>false</c>, the handle is not drawn and dragging is ignored.
		/// </summary>
		bool Enabled { get; set; }
	}
}