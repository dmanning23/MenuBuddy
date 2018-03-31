using Microsoft.Xna.Framework;

namespace MenuBuddy
{
	/// <summary>
	/// any item that has content that needs to be loaded
	/// </summary>
	public interface IHasContent
	{
		/// <summary>
		/// Available load content method for child classes.
		/// </summary>
		/// <param name="screen"></param>
		void LoadContent(IScreen screen);

		void UnloadContent();
	}
}