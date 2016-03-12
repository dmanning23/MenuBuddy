using System.Collections.Generic;

namespace MenuBuddy
{
	public interface ITree<T> : ILayout
	{
		List<TreeItem<T>> Items { get; }
	}
}