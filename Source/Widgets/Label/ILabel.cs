namespace MenuBuddy
{
	/// <summary>
	/// Interface of a peice of text that can be displayed on the screen
	/// </summary>
	public interface ILabel : IWidget
	{
		/// <summary>
		/// The text of this label
		/// </summary>
		string Text { get; set; }
	}
}