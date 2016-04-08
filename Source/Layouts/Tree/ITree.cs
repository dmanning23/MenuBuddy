
namespace MenuBuddy
{
	public interface ITree<T> : ILayout, IStackLayout
	{
		StackLayout Stack
		{
			get;
		}
	}
}