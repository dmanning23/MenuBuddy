
namespace MenuBuddy
{
	public class IntSlider : BaseSlider<int>
    {
		#region Properties

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
		/// constructor
		/// </summary>
		public IntSlider()
		{
		}

		public IntSlider(IntSlider inst) : base(inst)
		{
		}

		/// <summary>
		/// Get a deep copy of this item
		/// </summary>
		/// <returns></returns>
		public override IScreenItem DeepCopy()
		{
			return new IntSlider(this);
		}

		#endregion //Methods
	}
}
