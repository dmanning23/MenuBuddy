
namespace MenuBuddy
{
	/// <summary>
	/// This is just a list of items that are on the screen.
	/// The user has to take care of placing them etc.
	/// </summary>
	public class AbsoluteLayout : Layout
	{
		#region Methods

		public AbsoluteLayout()
		{
		}

		public override void AddItem(IScreenItem item)
		{
			//store the new item
			Items.Add(item);
			Sort();
		}

		#endregion //Methods
	}
}