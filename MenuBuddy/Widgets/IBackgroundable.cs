
namespace MenuBuddy
{
	public interface IBackgroundable : IScreenItem
	{
		bool HasBackground { get; }

		bool HasOutline { get; }
	}
}
