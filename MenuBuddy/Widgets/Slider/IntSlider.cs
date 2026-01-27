
namespace MenuBuddy
{
	/// <summary>
	/// A slider widget that operates on <c>int</c> values, snapping the handle to integer positions.
	/// </summary>
	public class IntSlider : BaseSlider<int>, ISlider<int>
    {
		#region Properties

		/// <summary>
		/// The handle position, truncated to an integer when set.
		/// </summary>
		protected override float HandlePosition
		{
			get
			{
				return base.HandlePosition;
			}

			set
			{
				base.HandlePosition = (int)value;
			}
		}

		/// <inheritdoc/>
		public override int SliderPosition
		{
			get
			{
				return (int)HandlePosition;
			}
			set
			{
				HandlePosition = value;
			}
		}

		#endregion //Properties

		#region Methods

		/// <summary>
		/// Initializes a new <see cref="IntSlider"/> with default values.
		/// </summary>
		public IntSlider()
		{
		}

		/// <summary>
		/// Initializes a new <see cref="IntSlider"/> by copying values from an existing instance.
		/// </summary>
		/// <param name="inst">The slider to copy from.</param>
		public IntSlider(IntSlider inst) : base(inst)
		{
		}

		/// <summary>
		/// Creates a deep copy of this slider.
		/// </summary>
		/// <returns>A new <see cref="IntSlider"/> that is a copy of this instance.</returns>
		public override IScreenItem DeepCopy()
		{
			return new IntSlider(this);
		}

		#endregion //Methods
	}
}
