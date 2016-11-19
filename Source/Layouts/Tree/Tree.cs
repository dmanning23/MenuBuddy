
using System;

namespace MenuBuddy
{
	public class Tree : ScrollLayout, ITree, IHasContent
	{
		#region Fields

		/// <summary>
		/// The stack layout that will hold all the tree items
		/// </summary>
		public StackLayout Stack { get; private set; }

		private IScreen Screen
		{
			get; set;
		}

		public StackAlignment Alignment
		{
			get
			{
				return ((IStackLayout)Stack).Alignment;
			}

			set
			{
				((IStackLayout)Stack).Alignment = value;
			}
		}

		#endregion //Fields

		#region Properties

		//private List<TreeItem> TreeItems
		//{
		//	get; set;
		//}

		#endregion //Properties

		#region Methods

		public Tree(IScreen screen)
		{
			//TreeItems = new List<TreeItem>();
			Stack = new StackLayout()
			{
				Alignment = StackAlignment.Top
			};
			Screen = screen;
		}

		public Tree(Tree inst) : base(inst)
		{
			Stack = new StackLayout(inst.Stack);
			Screen = inst.Screen;
			//TreeItems = new List<TreeItem>();
			//foreach (var item in inst.TreeItems)
			//{
			//	TreeItems.Add(item.DeepCopy() as TreeItem);
			//}
		}

		public override void LoadContent(IScreen screen)
		{
			base.AddItem(Stack);
		}

		public override IScreenItem DeepCopy()
		{
			return new Tree(this);
		}

		public override void AddItem(IScreenItem item)
		{
			//Make sure the thing is in the tree
			var treeItem = item as TreeItem;
			if (null != treeItem)
			{
				treeItem.AddToTree(Screen);
			}

			//add to the stack control
			Stack.AddItem(item);

			UpdateMinMaxScroll();
			UpdateScrollBars();
		}

		public void InsertItem(IScreenItem item, IScreenItem prevItem)
		{
			//Make sure the thing is in the tree
			var treeItem = item as TreeItem;
			if (null != treeItem)
			{
				treeItem.AddToTree(Screen);
			}

			//add to the stack control
			Stack.InsertItem(item, prevItem);

			UpdateMinMaxScroll();
			UpdateScrollBars();
		}

		public override bool RemoveItem(IScreenItem item)
		{
			var removed = Stack.RemoveItem(item);

			if (removed)
			{
				UpdateMinMaxScroll();
				UpdateScrollBars();
			}

			return removed;
		}

		#endregion //Methods
	}
}