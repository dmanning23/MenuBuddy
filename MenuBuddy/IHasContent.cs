using Microsoft.Xna.Framework;
using System.Threading.Tasks;

namespace MenuBuddy
{
	/// <summary>
	/// Interface for items that have content requiring loading and unloading.
	/// </summary>
	public interface IHasContent
	{
		/// <summary>
		/// Loads content required by this item.
		/// </summary>
		/// <param name="screen">The screen providing access to content managers and services.</param>
		/// <returns>A task representing the asynchronous load operation.</returns>
		Task LoadContent(IScreen screen);

		/// <summary>
		/// Unloads and releases content held by this item.
		/// </summary>
		void UnloadContent();
	}
}