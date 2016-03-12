using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace MenuBuddy
{
	public class Tree<T> : ScrollLayout, ITree<T>
	{
		#region Fields

		/// <summary>
		/// The stack layout that will hold all the tree items
		/// </summary>

		#endregion //Fields

		#region Properties

		public List<TreeItem<T>> Items
		{
			get; private set;
		}

		#endregion //Properties

		#region Methods

		#endregion //Methods

	}
}