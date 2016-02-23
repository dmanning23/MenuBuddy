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
			Item = item;
			Owner = owner;
		}

		#endregion //Methods
	}
}