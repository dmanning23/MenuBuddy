
namespace MenuBuddy
{
	/// <summary>
	/// Interface for screen items that can have a background fill and/or outline drawn behind them.
	/// </summary>
	public interface IBackgroundable : IScreenItem
	{
		/// <summary>
		/// Whether to draw a filled background rectangle behind this item.
		/// </summary>
		bool HasBackground { get; }

		/// <summary>
		/// Whether to draw an outline rectangle around this item.
		/// </summary>
		bool HasOutline { get; }
	}
}
