using Microsoft.Xna.Framework;

namespace MenuBuddy
{
	public interface ISlider : IWidget, IDraggable
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
		float HandlePosition { get; set; }

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
	}
}