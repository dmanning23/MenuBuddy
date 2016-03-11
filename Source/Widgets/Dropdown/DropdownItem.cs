namespace MenuBuddy
{
	/// <summary>
	/// A single item in the dropdown list.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class DropdownItem<T> : RelativeLayoutButton
	{
		#region Properties

		/// <summary>
		/// The item this dropdownitem is representing in the gui
		/// </summary>
		public T Item
		{
			get; set;
		}

		/// <summary>
		/// the dropdown control that owns this item
		/// </summary>
		private Dropdown<T> Owner { get; set; }

		#endregion //Properties

		#region Methods

		public DropdownItem(T item, Dropdown<T> owner)
		{
			//Style = DefaultStyles.Instance().MenuEntryStyle;
			Item = item;
			Owner = owner;

			OnClick += ((obj, e) =>
			{
				Owner.SelectedItem = Item;
			});
		}

		public DropdownItem(DropdownItem<T> inst) : base(inst)
		{
			//Style = DefaultStyles.Instance().MenuEntryStyle;
			Item = inst.Item;
			Owner = inst.Owner;
		}

		/// <summary>
		/// Get a deep copy of this item
		/// </summary>
		/// <returns></returns>
		public override IScreenItem DeepCopy()
		{
			return new DropdownItem<T>(this);
		}

		#endregion //Methods
	}
}