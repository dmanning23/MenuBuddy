using InputHelper;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MenuBuddy
{
	/// <summary>
	/// Interface for a widget that displays a texture on screen.
	/// </summary>
	public interface IImage : IWidget, IClickable
	{
		/// <summary>
		/// The texture displayed by this image widget.
		/// </summary>
		Texture2D Texture { get; set; }

		/// <summary>
		/// Whether the image should stretch to fill the specified <see cref="Size"/> rather than using the texture's native dimensions.
		/// </summary>
		bool FillRect { get; set; }

		/// <summary>
		/// The target size for this image when <see cref="FillRect"/> is <c>true</c>.
		/// </summary>
		Vector2 Size
		{
			set;
		}

		/// <summary>
		/// The tint color applied when drawing this image. Use <see cref="Color.White"/> for no tint.
		/// </summary>
		Color FillColor { set; }

		/// <summary>
		/// Whether this image continuously plays a pulsate animation, regardless of highlight state.
		/// </summary>
		bool AlwaysPulsate { get; set; }
	}
}