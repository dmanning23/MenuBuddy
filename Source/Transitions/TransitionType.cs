
namespace MenuBuddy
{
	/// <summary>
	/// The different ways for screen items to transition on and off the screen
	/// </summary>
	public enum TransitionType
	{
		/// <summary>
		/// in left, out left
		/// </summary>
		PopLeft,

		/// <summary>
		/// in right, out right
		/// </summary>
		PopRight,

		/// <summary>
		/// in top, out top
		/// </summary>
		PopTop,

		/// <summary>
		/// in bottom, out bottom
		/// </summary>
		PopBottom,

		/// <summary>
		/// in left, out right
		/// </summary>
		SlideLeft,

		/// <summary>
		/// in right, out left
		/// </summary>
		SlideRight,

		/// <summary>
		/// This item doesn't transition at all.
		/// </summary>
		None
	}
}