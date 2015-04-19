using GameTimer;
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
	}
}