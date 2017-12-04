
namespace MenuBuddy
{
	public interface IStackLayout : IScreenItemContainer, IScreenItem, ILayout
	{
		StackAlignment Alignment { get; set; }

		void InsertItem(IScreenItem item, IScreenItem prevItem);

		void InsertItemBefore(IScreenItem item, IScreenItem nextItem);
	}
}