using InputHelper;
using System;

namespace MenuBuddy
{
	/// <summary>
	/// This is a button thhat contains a relaitve layout
	/// </summary>
	public class DragDropButton : RelativeLayoutButton, IDraggable
	{
		#region Properties

		public event EventHandler<DragEventArgs> OnDrag;

		#endregion //Properties

		#region Initialization

		/// <summary>
		/// Constructs a new menu entry with the specified text.
		/// </summary>
		public DragDropButton()
		{
		}

		public DragDropButton(DragDropButton inst) : base(inst)
		{
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
			var result = Rect.Contains(drag.Current);
			if (result)
			{
				//Move the button to the current drag position
				Position = drag.Current.ToPoint();

				//fire off the event for any listeners
				if (OnDrag != null)
				{
					OnDrag(this, drag);
				}
			}

			return result;
		}

		#endregion
	}
}