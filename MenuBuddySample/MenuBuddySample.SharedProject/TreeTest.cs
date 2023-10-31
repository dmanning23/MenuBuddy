using MenuBuddy;
using Microsoft.Xna.Framework;
using ResolutionBuddy;
using System.Threading.Tasks;

namespace MenuBuddySample
{
	public class TreeTest : WidgetScreen
	{
		public TreeTest() : base("Tree Test")
		{
			CoverOtherScreens = true;
			CoveredByOtherScreens = true;
		}

		public override async Task LoadContent()
		{
			await base.LoadContent();

			AddCancelButton();

			//create the tree layout
			var tree = new Tree<string>(this)
			{
				Vertical = VerticalAlignment.Center,
				Horizontal = HorizontalAlignment.Center,
				Size = new Vector2(350, 350),
				Position = Resolution.ScreenArea.Center
			};

			string[] words = { "cat", "pants", "buttnuts", "cat1", "pants1", "whoa", "test1", "test2" };
			string[] subWords = { "sub1", "sub2", "sub3" };
			foreach (var word in words)
			{
				//create the item
				var treeItem = new TreeItem<string>(word, tree, null);

				//just add a label for now
				var label = new Label(word, Content)
				{
					Vertical = VerticalAlignment.Center,
					Horizontal = HorizontalAlignment.Center
				};
				treeItem.SetButton(label);

				//add the sub items
				foreach (var subWord in subWords)
				{
					//create the item
					var subTreeItem = new TreeItem<string>(subWord, tree, treeItem);

					//just add a label for now
					var subLabel = new Label(subWord, Content)
					{
						Vertical = VerticalAlignment.Center,
						Horizontal = HorizontalAlignment.Center
					};
					subTreeItem.SetButton(subLabel);
					treeItem.ChildItems.Add(subTreeItem);
				}

				tree.AddItem(treeItem);
			}

			AddItem(tree);
		}
	}
}
