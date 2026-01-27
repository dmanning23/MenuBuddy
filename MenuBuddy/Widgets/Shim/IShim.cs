using Microsoft.Xna.Framework;

namespace MenuBuddy
{
	/// <summary>
	/// This is a little shim that can be added as a placeholder between items.
	/// </summary>
	public interface IShim : IWidget
	{
		/// <summary>
		/// The width and height of the shim.
		/// </summary>
		Vector2 Size
		{
			set;
		}
	}
}