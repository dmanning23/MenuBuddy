
namespace MenuBuddy
{
	/// <summary>
	/// A slider widget that operates on <c>float</c> values.
	/// </summary>
	public class Slider : BaseSlider<float>, ISlider<float>
	{
		#region Properties

		/// <inheritdoc/>
		public override float SliderPosition
		{
			get
			{
				return HandlePosition;
			}
			set
			{
				HandlePosition = value;
			}
		}

		#endregion //PropertiesS

		#region Methods

		/// <summary>
		/// Initializes a new <see cref="Slider"/> with default values.
		/// </summary>
		public Slider()
		{
		}

		/// <summary>
		/// Initializes a new <see cref="Slider"/> by copying values from an existing instance.
		/// </summary>
		/// <param name="inst">The slider to copy from.</param>
		public Slider(Slider inst) : base(inst)
		{
		}

		/// <summary>
		/// Creates a deep copy of this slider.
		/// </summary>
		/// <returns>A new <see cref="Slider"/> that is a copy of this instance.</returns>
		public override IScreenItem DeepCopy()
		{
			return new Slider(this);
		}

		#endregion //Methods
	}
}