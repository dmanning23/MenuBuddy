using GameTimer;
using Microsoft.Xna.Framework;

namespace MenuBuddy
{
	/// <summary>
	/// An invisible spacer widget used as a placeholder between items in a layout.
	/// </summary>
	public class Shim : Widget, IShim
	{
		#region Fields

		/// <summary>
		/// Backing field for <see cref="Size"/>.
		/// </summary>
		private Vector2 _size;

		#endregion //Fields

		#region Properties

		/// <summary>
		/// The width and height of the shim. Recalculates the bounding rectangle when changed.
		/// </summary>
		public Vector2 Size
		{
			get
			{
				return _size;
			}
			set
			{
				if (_size != value)
				{
					_size = value;
					CalculateRect();
				}
			}
		}

		#endregion //Properties

		#region Initialization

		/// <summary>
		/// Initializes a new <see cref="Shim"/> with zero size.
		/// </summary>
		public Shim()
		{
		}

		/// <summary>
		/// Initializes a new <see cref="Shim"/> with the specified width and height.
		/// </summary>
		/// <param name="x">The width of the shim.</param>
		/// <param name="y">The height of the shim.</param>
		public Shim(float x, float y)
		{
			Size = new Vector2(x, y);
		}

		/// <summary>
		/// Initializes a new <see cref="Shim"/> by copying values from an existing instance.
		/// </summary>
		/// <param name="inst">The shim to copy from.</param>
		public Shim(Shim inst) : base(inst)
		{
			_size = inst._size;
		}

		/// <summary>
		/// Creates a deep copy of this shim.
		/// </summary>
		/// <returns>A new <see cref="Shim"/> that is a copy of this instance.</returns>
		public override IScreenItem DeepCopy()
		{
			return new Shim(this);
		}

		#endregion //Initialization

		#region Methods

		/// <summary>
		/// No-op. Shims have no update logic.
		/// </summary>
		public override void Update(IScreen screen, GameClock gameTime)
		{
		}

		/// <summary>
		/// No-op. Shims are invisible and do not draw anything.
		/// </summary>
		public override void Draw(IScreen screen, GameClock gameTime)
		{
		}

		/// <summary>
		/// Calculates the bounding rectangle based on size, scale, position, and alignment.
		/// </summary>
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
		}

		#endregion //Methods
	}
}