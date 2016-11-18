
namespace MenuBuddy
{
	public interface ITree : ILayout, IStackLayout
	{
		StackLayout Stack
		{
			get;
		}
	}
}