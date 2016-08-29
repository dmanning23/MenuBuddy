using InputHelper;
using Microsoft.Xna.Framework;
using System;

namespace MenuBuddy
{
	/// <summary>
	/// This is a button thhat contains a relaitve layout
	/// </summary>
	public class DragDropButton : RelativeLayoutButton, IDraggable, IDroppable
	{
		#region Properties

		public event EventHandler<DragEventArgs> OnDrag;
		public event EventHandler<DropEventArgs> OnDrop;

		private bool CurrentlyDragging { get; set; }

		#endregion //Properties

		#region Initialization

		/// <summary>
		/// Constructs a new menu entry with the specified text.
		/// </summary>
		public DragDropButton()
		{
			CurrentlyDragging = false;
		}

		public DragDropButton(DragDropButton inst) : base(inst)
		{
			CurrentlyDragging = inst.CurrentlyDragging;
		}

		/// <summary>
		/// Get a deep copy of this item
		/// </summary>
		/// <returns></returns>
		public override IScreenItem DeepCopy()
		{
			return new DragDropButton(this);
		}

		#endregion //Initialization

		#region Methods

		public bool CheckDrag(DragEventArgs drag)
		{
			var result = false;
			if (CurrentlyDragging)
			{
				result = true;

				//Set the position to the current drag position
				DragOperation(drag);
			}
			else
			{
				//Check if the user has started a drag operation
				result = Rect.Contains(drag.Start);
				if (result)
				{
					if (!CurrentlyDragging)
					{
						//Set the start rectangle
						CurrentlyDragging = true;
					}

					DragOperation(drag);
				}
			}

			return result;
		}

		private void DragOperation(DragEventArgs drag)
		{
			//Move the button to the current drag position
			Position = drag.Current.ToPoint();

			//fire off the event for any listeners
			if (OnDrag != null)
			{
				OnDrag(this, drag);
			}
		}

		public bool CheckDrop(DropEventArgs drop)
		{
			//check if we are currently dragging
			if (CurrentlyDragging)
			{
				//don't respond to anymore drop events
				CurrentlyDragging = false;

				//Move the button to the drop position
				Position = drop.Drop.ToPoint();

				//fire off the event for any listeners
				if (OnDrop != null)
				{
					OnDrop(this, drop);
				}

				return true;
			}

			return false;
		}

		#endregion
	}
}