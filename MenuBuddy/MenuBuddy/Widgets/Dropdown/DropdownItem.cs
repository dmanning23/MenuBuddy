namespace MenuBuddy
{
	/// <summary>
	/// A single item in the dropdown list.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class DropdownItem<T> : RelativeLayoutButton, IDropdownItem<T>
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
		private IDropdown<T> Owner { get; set; }

		#endregion //Properties

		#region Methods

		public DropdownItem(T item, IDropdown<T> owner)
		{
			Item = item;
			Owner = owner;
			Clickable = false;

			OnClick += ((obj, e) =>
			{
				Owner.SelectedDropdownItem = this;
			});
		}

		public DropdownItem(DropdownItem<T> inst) : base(inst)
		{
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