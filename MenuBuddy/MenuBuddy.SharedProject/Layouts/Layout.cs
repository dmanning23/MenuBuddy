using GameTimer;
using InputHelper;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

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

		public bool IsHighlighted
		{
			set 
			{
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
					if (null != clickable)
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

				//rescale all the child items
				foreach (var item in Items)
				{
					var scalable = item as IScalable;
					if (null != scalable)
					{
						scalable.Scale = value;
					}
				}
			}
		}

		public bool HasOutline { get; set; }

		#endregion //Properties

		#region Initialzation

		protected Layout()
		{
			Items = new List<IScreenItem>();
			Scale = 1.0f;
			HasOutline = false;
		}

		protected Layout(Layout inst) : this()
		{
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

		public virtual void LoadContent(IScreen screen)
		{
			for (int i = 0; i < Items.Count; i++)
			{
				Items[i].LoadContent(screen);
			}
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

			foreach (var item in Items)
			{
				var highlightable = item as IHighlightable;
				if ((highlightable != null) && highlightable.CheckHighlight(highlight))
				{
					highlighted = true;
				}
			}

			return highlighted;
		}

		public virtual bool CheckClick(ClickEventArgs click)
		{
			if (Rect.Contains(click.Position))
			{
				foreach (var item in Items)
				{
					var clickable = item as IClickable;
					if ((clickable != null) && clickable.CheckClick(click))
					{
						return true;
					}
				}
			}

			//None of the items in this container were clicked
			return false;
		}

		public virtual bool CheckDrag(DragEventArgs drag)
		{
			if (Rect.Contains(drag.Start))
			{
				foreach (var item in Items)
				{
					var draggable = item as IDraggable;
					if ((draggable != null) && draggable.CheckDrag(drag))
					{
						return true;
					}
				}
			}

			//None of the items in this container were clicked
			return false;
		}

		public bool CheckDrop(DropEventArgs drop)
		{
			if (Rect.Contains(drop.Drop))
			{
				foreach (var item in Items)
				{
					var droppable = item as IDroppable;
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

		#endregion //Methods
	}
}