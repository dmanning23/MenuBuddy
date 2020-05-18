using GameTimer;
using InputHelper;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MenuBuddy
{
	/// <summary>
	/// This is a list of items on a screen
	/// </summary>
	public abstract class Layout : ILayout, IDisposable
	{
		#region Fields

		public event EventHandler<ClickEventArgs> OnClick;
		public event EventHandler<HighlightEventArgs> OnHighlight;
		public event EventHandler<DragEventArgs> OnDrag;
		public event EventHandler<DropEventArgs> OnDrop;

		private float _scale;

		#endregion //Fields

		#region Properties

		public string Name { get; set; } = "Layout";

		public bool Highlightable { get; set; }

		public bool Clickable { get; set; }

		public bool DrawWhenInactive
		{
			set
			{
				foreach (var item in Items)
				{
					item.DrawWhenInactive = value;
				}
			}
		}

		private bool _isHighlighted;
		public bool IsHighlighted
		{
			get
			{
				return _isHighlighted;
			}
			set 
			{
				_isHighlighted = value;
				foreach (var item in Items)
				{
					var highlightable = item as IHighlightable;
					if (null != highlightable)
					{
						highlightable.IsHighlighted = value;
					}
				}
			}
		}

		public bool IsClicked
		{
			get
			{
				return false;
			}
			set
			{
				foreach (var item in Items)
				{
					var clickable = item as IClickable;
					if (null != clickable && clickable.Clickable)
					{
						clickable.IsClicked = value;
					}
				}
			}
		}

		/// <summary>
		/// all the items that are in this layout
		/// </summary>
		public List<IScreenItem> Items { get; protected set; }

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

		public virtual Point Position { get; set; }

		/// <summary>
		/// Where to layer the item.
		/// low numbers go in the back, higher numbers in the front
		/// </summary>
		public float Layer { get; set; }

		public virtual HorizontalAlignment Horizontal { get; set; }

		public virtual VerticalAlignment Vertical { get; set; }

		public virtual float Scale
		{
			get
			{
				return _scale;
			}
			set
			{
				//grab the scale
				_scale = value;
			}
		}

		public bool HasOutline { get; set; }

		public virtual ITransitionObject TransitionObject
		{
			get
			{
				return null;
			}
			set
			{
				foreach (var item in Items)
				{
					var transitionableItem = item as ITransitionable;
					if (null != transitionableItem)
					{
						transitionableItem.TransitionObject = value;
					}
				}
			}
		}

		#endregion //Properties

		#region Initialzation

		protected Layout()
		{
			Highlightable = true;
			Clickable = true;
			Items = new List<IScreenItem>();
			Scale = 1.0f;
			HasOutline = false;
		}

		protected Layout(Layout inst) : this()
		{
			Highlightable = inst.Highlightable;
			Clickable = inst.Clickable;
			Position = new Point(inst.Position.X, inst.Position.Y);
			Horizontal = inst.Horizontal;
			Vertical = inst.Vertical;
			OnClick = inst.OnClick;
			OnHighlight = inst.OnHighlight;
			OnDrag = inst.OnDrag;
			_scale = inst._scale;
			Layer = inst.Layer;
			HasOutline = inst.HasOutline;

			//copy all the items in the list
			Items = new List<IScreenItem>();
			foreach (var item in inst.Items)
			{
				Items.Add(item.DeepCopy());
			}
		}

		public abstract IScreenItem DeepCopy();

		public virtual async Task LoadContent(IScreen screen)
		{
			for (int i = 0; i < Items.Count; i++)
			{
				await Items[i].LoadContent(screen);
			}
		}

		public virtual void UnloadContent()
		{
			for (int i = 0; i < Items?.Count; i++)
			{
				Items[i].UnloadContent();
			}
			Items = null;
		}

		#endregion //Initialzation

		#region Methods

		public abstract void AddItem(IScreenItem item);

		protected void Sort()
		{
			Items.Sort((x, y) => x.Layer.CompareTo(y.Layer));
		}

		public virtual bool RemoveItem(IScreenItem item)
		{
			return Items.Remove(item);
		}

		public bool RemoveItems<T>() where T:IScreenItem
		{
			//get a list of items
			var items = Items.Where(x => x is T).ToList();

			//remove all the items
			var result = false;
			foreach (var item in items)
			{
				if (RemoveItem(item))
				{
					result = true;
				}
			}

			return result;
		}

		public virtual void Update(IScreen screen, GameClock gameTime)
		{
			//update all the items
			for (int i = 0; i < Items.Count; i++)
			{
				Items[i].Update(screen, gameTime);
			}
		}

		public virtual void DrawBackground(IScreen screen, GameClock gameTime)
		{
			//draw the backgrounds of all the items
			for (int i = 0; i < Items.Count; i++)
			{
				Items[i].DrawBackground(screen, gameTime);
			}

			//draw the outline!
			if (HasOutline && screen.TransitionState == TransitionState.Active)
			{
				screen.ScreenManager.DrawHelper.DrawOutline(StyleSheet.NeutralOutlineColor, Rect);
			}
		}

		public virtual void Draw(IScreen screen, GameClock gameTime)
		{
			//draw all the items
			for (int i = 0; i < Items.Count; i++)
			{
				Items[i].Draw(screen, gameTime);
			}
		}

		public virtual bool CheckHighlight(HighlightEventArgs highlight)
		{
			var highlighted = false;

			if (Highlightable)
			{
				for (int i = Items.Count - 1; i >= 0; i--)
				{
					var highlightable = Items[i] as IHighlightable;
					if ((highlightable != null) && highlightable.CheckHighlight(highlight))
					{
						highlighted = true;
					}
				}
			}

			return highlighted;
		}

		public virtual bool CheckClick(ClickEventArgs click)
		{
			if (Rect.Contains(click.Position) && Clickable)
			{
				for (int i = Items.Count - 1; i >= 0; i--)
				{
					var clickable = Items[i] as IClickable;
					if ((clickable != null) && clickable.CheckClick(click))
					{
						return true;
					}
				}
			}

			//None of the items in this container were clicked
			return false;
		}

		protected virtual void Clicked(object obj, ClickEventArgs e)
		{
			if (OnClick != null)
			{
				OnClick(obj, e);
			}
		}

		public virtual bool CheckDrag(DragEventArgs drag)
		{
			if (Rect.Contains(drag.Start))
			{
				for (int i = Items.Count - 1; i >= 0; i--)
				{
					var draggable = Items[i] as IDraggable;
					if ((draggable != null) && draggable.CheckDrag(drag))
					{
						return true;
					}
				}
			}

			//None of the items in this container were clicked
			return false;
		}

		public virtual bool CheckDrop(DropEventArgs drop)
		{
			if (Rect.Contains(drop.Drop))
			{
				for (int i = Items.Count - 1; i >= 0; i--)
				{
					var droppable = Items[i] as IDroppable;
					if ((droppable != null) && droppable.CheckDrop(drop))
					{
						return true;
					}
				}
			}

			//None of the items in this container were clicked
			return false;
		}

		public virtual void Dispose()
		{
			OnClick = null;
			OnHighlight = null;
			OnDrag = null;
			OnDrop = null;
		}

		public override string ToString()
		{
			return Name;
		}

		#endregion //Methods
	}
}