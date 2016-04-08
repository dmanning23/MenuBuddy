using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MenuBuddy
{
	/// <summary>
	/// An image that is displayed onteh screen
	/// </summary>
	public interface IImage : IWidget
	{
		/// <summary>
		/// The image to display
		/// </summary>
		Texture2D Texture { get; set; }

		/// <summary>
		/// If true, the image will fill the rect
		/// </summary>
		bool FillRect { get; set; }

		/// <summary>
		/// if fillrect is true, will use this to set the size of the image
		/// </summary>
		Vector2 Size
		{
			set;
		}
	}
}