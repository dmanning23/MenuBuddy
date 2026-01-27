using Microsoft.Xna.Framework;
using Vector2Extensions;

namespace MenuBuddy
{
	/// <summary>
	/// A button that uses a <see cref="RelativeLayout"/> to position its child items.
	/// </summary>
	public class RelativeLayoutButton : LayoutButton<RelativeLayout>
	{
		#region Initialization

		/// <summary>
		/// Initializes a new <see cref="RelativeLayoutButton"/> with a default relative layout.
		/// </summary>
		public RelativeLayoutButton()
		{
			Layout = new RelativeLayout();
		}

		/// <summary>
		/// Initializes a new <see cref="RelativeLayoutButton"/> by copying values from an existing instance.
		/// </summary>
		/// <param name="inst">The button to copy from.</param>
		public RelativeLayoutButton(RelativeLayoutButton inst) : base(inst)
		{
			Layout = new RelativeLayout(inst.Layout as RelativeLayout);
		}

		/// <summary>
		/// Creates a deep copy of this button.
		/// </summary>
		/// <returns>A new <see cref="RelativeLayoutButton"/> that is a copy of this instance.</returns>
		public override IScreenItem DeepCopy()
		{
			return new RelativeLayoutButton(this);
		}

		#endregion //Initialization

		#region Methods

		/// <inheritdoc/>
		protected override void CalculateRect()
		{
			//get the size of the rect
			var size = Size * Scale;

			//set the x component
			Vector2 pos = Position.ToVector2();
			switch (Horizontal)
			{
				case HorizontalAlignment.Center: { pos.X -= size.X / 2f; } break;
				case HorizontalAlignment.Right: { pos.X -= size.X; } break;
			}

			//set the y component
			switch (Vertical)
			{
				case VerticalAlignment.Center: { pos.Y -= size.Y / 2f; } break;
				case VerticalAlignment.Bottom: { pos.Y -= size.Y; } break;
			}

			_rect = new Rectangle((int)pos.X, (int)pos.Y, (int)size.X, (int)size.Y);

			//Set the position of the internal layout
			var relLayout = Layout as RelativeLayout;
			if (null != relLayout)
			{
				relLayout.Scale = Scale;
				relLayout.Vertical = VerticalAlignment.Top;
				relLayout.Horizontal = HorizontalAlignment.Left;
				relLayout.Position = pos.ToPoint();
				relLayout.Size = size;
			}
		}

		#endregion
	}
}