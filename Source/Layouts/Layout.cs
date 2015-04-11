using GameTimer;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;

namespace MenuBuddy
{
	/// <summary>
	/// This is a list of items on a screen
	/// </summary>
	public abstract class Layout : IScreenItemContainer
	{
		#region Properties

		/// <summary>
		/// all the items that are stacked
		/// </summary>
		public List<IScreenItem> Items { get; private set; }

		public virtual Rectangle Rect
		{
			get
			{
				//check if there are any items at all
				if (0 < Items.Count)
				{
					//add up all the rectangles of the contained items
					var result = Items[0].Rect;
					for (int i = 1; i < Items.Count; i++)
					{
						//get the rect of the item
						var itemRect = Items[i].Rect;

						//check if the current rect is bogus
						if (result.IsEmpty)
						{
							result = itemRect;
						}

						//check if it is valid
						if (!itemRect.IsEmpty)
						{
							result = Rectangle.Union(result, itemRect);
						}
					}

					return result;
				}
				else
				{
					return new Rectangle(Position.X, Position.Y, 0, 0);
				}
			}
		}

		public Point Position { get; set; }

		#endregion //Properties

		#region Methods

		protected Layout()
		{
			Items = new List<IScreenItem>();
		}

		public abstract void AddItem(IScreenItem item);

		public bool RemoveItem(IScreenItem item)
		{
			return Items.Remove(item);
		}

		public IEnumerable<IButton> Buttons
		{
			get
			{
				//create the list to hold all the buttons
				var buttons = new List<IButton>();

				//let each screen item add it's buttons
				var containers = Items.OfType<IScreenItemContainer>();
				foreach (var item in containers)
				{
					buttons.AddRange(item.Buttons);
				}

				//add the items that are themselves buttons
				buttons.AddRange(Items.OfType<IButton>());

				return buttons;
			}
		}

		public void Update(IScreen screen, GameClock gameTime)
		{
			//update all the items
			for (int i = 0; i < Items.Count; i++)
			{
				Items[i].Update(screen, gameTime);
			}
		}

		public void DrawBackground(IScreen screen, GameClock gameTime)
		{
			//draw the backgrounds of all the items
			for (int i = 0; i < Items.Count; i++)
			{
				Items[i].DrawBackground(screen, gameTime);
			}
		}

		public void Draw(IScreen screen, GameClock gameTime)
		{
			//draw all the items
			for (int i = 0; i < Items.Count; i++)
			{
				Items[i].Draw(screen, gameTime);
			}
		}

		#endregion //Methods
	}
}