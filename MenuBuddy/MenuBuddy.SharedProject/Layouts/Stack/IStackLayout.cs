namespace MenuBuddy
{
	public interface IStackLayout
	{
		StackAlignment Alignment { get; set; }

		void InsertItem(IScreenItem item, IScreenItem prevItem);

		void InsertItemBefore(IScreenItem item, IScreenItem nextItem);
	}
}