using System;
using GameTimer;

namespace MenuBuddy
{
	public class Tree<T> : ScrollLayout, ITree<T>, IHasContent, IDisposable
	{
		#region Events

		public event EventHandler<SelectionChangeEventArgs<T>> OnSelectedItemChange;

		#endregion //Events

		#region Fields

		/// <summary>
		/// The stack layout that will hold all the tree items
		/// </summary>
		public StackLayout Stack { get; private set; }

		protected IScreen Screen
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

		/// <summary>
		/// The currently selected item
		/// </summary>
		private TreeItem<T> _selectedTreeItem;
		public TreeItem<T> SelectedTreeItem
		{
			private get
			{
				return _selectedTreeItem;
			}
			set
			{
				_selectedTreeItem = value;

				//fire off the selected event
				if (null != OnSelectedItemChange)
				{
					OnSelectedItemChange(this, new SelectionChangeEventArgs<T>(_selectedTreeItem.Item));
				}
			}
		}

		public T SelectedItem
		{
			get
			{
				return SelectedTreeItem.Item;
			}
		}

		public bool HasBackground { get; set; }

		protected Background Background { get; set; }
		
		public override ITransitionObject TransitionObject
		{
			get
			{
				return base.TransitionObject;
			}
			set
			{
				base.TransitionObject = value;
				foreach (var item in Items)
				{
					var transitionableItem = item as ITransitionable;
					if (null != transitionableItem)
					{
						transitionableItem.TransitionObject = TransitionObject;
					}
				}
			}
		}

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
			Background = new Background();
		}

		public Tree(Tree<T> inst) : base(inst)
		{
			Stack = new StackLayout(inst.Stack);
			Screen = inst.Screen;
			SelectedTreeItem = inst.SelectedTreeItem;
			Background = inst.Background;
		}

		public override void LoadContent(IScreen screen)
		{
			base.LoadContent(screen);

			base.AddItem(Stack);

			Background.LoadContent(screen);
		}

		public override IScreenItem DeepCopy()
		{
			return new Tree<T>(this);
		}

		protected void PrepareAdd(IScreenItem item)
		{
			//Make sure the thing is in the tree
			var treeItem = item as TreeItem<T>;
			if (null != treeItem)
			{
				treeItem.AddToTree(Screen);
			}

			var transitionable = item as ITransitionable;
			if (null != transitionable)
			{
				transitionable.TransitionObject = this.TransitionObject;
			}
		}

		public override void AddItem(IScreenItem item)
		{
			PrepareAdd(item);

			//add to the stack control
			Stack.AddItem(item);

			UpdateMinMaxScroll();
			UpdateScrollBars();
		}

		public void InsertItem(IScreenItem item, IScreenItem prevItem)
		{
			PrepareAdd(item);

			//add to the stack control
			Stack.InsertItem(item, prevItem);

			UpdateMinMaxScroll();
			UpdateScrollBars();
		}

		public void InsertItemBefore(IScreenItem item, IScreenItem nextItem)
		{
			PrepareAdd(item);

			//add to the stack control
			Stack.InsertItemBefore(item, nextItem);

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

		public override void Dispose()
		{
			base.Dispose();
			OnSelectedItemChange = null;
		}

		public override void DrawBackground(IScreen screen, GameClock gameTime)
		{
			Background.Draw(this, screen);

			base.DrawBackground(screen, gameTime);
		}

		#endregion //Methods
	}
}