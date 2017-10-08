
namespace MenuBuddy
{
	/// <summary>
	/// The different ways for transition objects to wipe on and off the screen
	/// </summary>
	public enum TransitionWipeType
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
		/// in top, out bottom
		/// </summary>
		SlideTop,

		/// <summary>
		/// in bottom, out top
		/// </summary>
		SlideBottom,

		/// <summary>
		/// This item doesn't transition at all.
		/// </summary>
		None
	}
}