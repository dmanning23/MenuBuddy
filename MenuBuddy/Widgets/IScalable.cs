
namespace MenuBuddy
{
	/// <summary>
	/// This is an item that can be scaled
	/// </summary>
	public interface IScalable
	{
		/// <summary>
		/// How much to resize this widget.
		/// Default is 1.0
		/// </summary>
		float Scale { get; set; }
	}
}
