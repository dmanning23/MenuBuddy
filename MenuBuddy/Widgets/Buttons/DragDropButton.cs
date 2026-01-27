using InputHelper;
using System;

namespace MenuBuddy
{
	/// <summary>
	/// A button that supports drag-and-drop operations. Moves to the drag position while being dragged
	/// and fires events for drag and drop actions.
	/// </summary>
	public class DragDropButton : RelativeLayoutButton, IDraggable, IDroppable, IDisposable
	{
		#region Properties

		/// <summary>
		/// Raised during a drag operation on this button.
		/// </summary>
		public event EventHandler<DragEventArgs> OnDrag;

		/// <summary>
		/// Raised when a drop operation completes on this button.
		/// </summary>
		public event EventHandler<DropEventArgs> OnDrop;

		/// <summary>
		/// Whether this button is currently being dragged.
		/// </summary>
		private bool CurrentlyDragging { get; set; }

		#endregion //Properties

		#region Initialization

		/// <summary>
		/// Initializes a new <see cref="DragDropButton"/>.
		/// </summary>
		public DragDropButton()
		{
			CurrentlyDragging = false;
		}

		/// <summary>
		/// Initializes a new <see cref="DragDropButton"/> by copying values from an existing instance.
		/// </summary>
		/// <param name="inst">The button to copy from.</param>
		public DragDropButton(DragDropButton inst) : base(inst)
		{
			CurrentlyDragging = inst.CurrentlyDragging;
		}

		/// <summary>
		/// Creates a deep copy of this button.
		/// </summary>
		/// <returns>A new <see cref="DragDropButton"/> that is a copy of this instance.</returns>
		public override IScreenItem DeepCopy()
		{
			return new DragDropButton(this);
		}

		/// <summary>
		/// Unloads content and releases drag/drop event handlers.
		/// </summary>
		public override void UnloadContent()
		{
			base.UnloadContent();
			OnDrag = null;
			OnDrop = null;
		}

		#endregion //Initialization

		#region Methods

		/// <summary>
		/// Checks whether this button should handle the given drag event. If dragging is in progress
		/// or the drag started within this button's bounds, the button moves to the drag position.
		/// </summary>
		/// <param name="drag">The drag event arguments.</param>
		/// <returns><c>true</c> if this button handled the drag; otherwise, <c>false</c>.</returns>
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

		/// <summary>
		/// Moves this button to the current drag position and raises the <see cref="OnDrag"/> event.
		/// </summary>
		/// <param name="drag">The drag event arguments.</param>
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

		/// <summary>
		/// Checks whether this button should handle the given drop event. If this button was being dragged,
		/// it moves to the drop position and raises the <see cref="OnDrop"/> event.
		/// </summary>
		/// <param name="drop">The drop event arguments.</param>
		/// <returns><c>true</c> if this button handled the drop; otherwise, <c>false</c>.</returns>
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