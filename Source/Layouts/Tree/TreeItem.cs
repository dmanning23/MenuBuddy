using System;
using System.Collections.Generic;

namespace MenuBuddy
{
	public class TreeItem<T> : RelativeLayoutButton
	{
		#region Fields

		#endregion //Fields

		#region Properties

		T Item
		{
			get;
		}

		public List<TreeItem<T>> Items
		{
			get; private set;
		}

		#endregion //Properties

		#region Methods

		public TreeItem<T> FindItem(T item)
		{
			throw new NotImplementedException();
		}

		#endregion //Methods
	}
}