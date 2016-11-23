using System;

namespace MenuBuddy
{
	public interface ITree<T> : ILayout, IStackLayout
	{
		event EventHandler<SelectionChangeEventArgs<T>> OnSelectedItemChange;

		T SelectedItem { get; }

		TreeItem<T> SelectedTreeItem { set; }

		StackLayout Stack
		{
			get;
		}
	}
}