using Microsoft.Xna.Framework;

namespace MenuBuddy
{
	/// <summary>
	/// This is a little shim that can be added as a placeholder between items.
	/// </summary>
	public interface IShim : IWidget
	{
		Vector2 Size
		{
			set;
		}
	}
}