using InputHelper;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace MenuBuddy
{
	public interface ISlider<T> : IWidget, IDraggable, IHighlightable
	{
		/// <summary>
		/// the min value of teh slider
		/// </summary>
		float Min { get; set; }

		/// <summary>
		/// the max value of the slider
		/// </summary>
		float Max { get; set; }

		/// <summary>
		/// the position of the handle of the slider
		/// </summary>
		T SliderPosition { get; set; }

		/// <summary>
		/// The size of this widget in pixels
		/// </summary>
		Vector2 Size
		{
			set;
		}

		/// <summary>
		/// The size of the slider handle in pixels
		/// </summary>
		Vector2 HandleSize
		{
			set;
		}

		/// <summary>
		/// A list of hask marks to draw on the slider
		/// </summary>
		List<float> Marks { get; }
	}
}