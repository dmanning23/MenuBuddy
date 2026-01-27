namespace MenuBuddy
{
	/// <summary>
	/// A single item in the dropdown list.
	/// </summary>
	/// <typeparam name="T">The type of the data item this widget represents.</typeparam>
	public class DropdownItem<T> : RelativeLayoutButton, IDropdownItem<T>
	{
		#region Properties

		/// <summary>
		/// The data item that this dropdown entry represents.
		/// </summary>
		public T Item
		{
			get; set;
		}

		/// <summary>
		/// The dropdown control that owns this item.
		/// </summary>
		private IDropdown<T> Owner { get; set; }

		#endregion //Properties

		#region Methods

		/// <summary>
		/// Initializes a new <see cref="DropdownItem{T}"/> with the specified data item and owner dropdown.
		/// </summary>
		/// <param name="item">The data item this entry represents.</param>
		/// <param name="owner">The dropdown control that owns this item.</param>
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

		/// <summary>
		/// Initializes a new <see cref="DropdownItem{T}"/> by copying values from an existing instance.
		/// </summary>
		/// <param name="inst">The dropdown item to copy from.</param>
		public DropdownItem(DropdownItem<T> inst) : base(inst)
		{
			Item = inst.Item;
			Owner = inst.Owner;
		}

		/// <summary>
		/// Creates a deep copy of this dropdown item.
		/// </summary>
		/// <returns>A new <see cref="DropdownItem{T}"/> that is a copy of this instance.</returns>
		public override IScreenItem DeepCopy()
		{
			return new DropdownItem<T>(this);
		}

		#endregion //Methods
	}
}