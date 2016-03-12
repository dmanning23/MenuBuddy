using System.Collections.Generic;

namespace MenuBuddy
{
	public interface ITree<T> : IWidget
	{
		List<TreeItem<T>> Items { get; }
	}
}