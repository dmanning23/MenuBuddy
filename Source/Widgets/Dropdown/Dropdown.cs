using InputHelper;
using System.Collections.Generic;

namespace MenuBuddy
{
	public class Dropdown<T> : RelativeLayoutButton, IDropdown<T>
	{
		#region Properties

		/// <summary>
		/// the screen that holds this guy
		/// </summary>
		private IScreen Screen
		{
			get; set;
		}

		/// <summary>
		/// The currently selected item
		/// </summary>
		private DropdownItem<T> SelectedDropdownItem
		{
			get; set;
		}

		/// <summary>
		/// When the dropdown button is clicked, this list of items is popped up in the dropdown list.
		/// </summary>
		public List<DropdownItem<T>> DropdownList { get; private set; }

		public T SelectedItem
		{
			get
			{
				return SelectedDropdownItem.Item;
			}
			set
			{
				var item = DropdownList.Find(x => x.Item.ToString() == value.ToString());
				SetSelectedDropdownItem(item);
			}
		}

		#endregion //Properties

		#region Methods

		public Dropdown()
		{
			DropdownList = new List<DropdownItem<T>>();
			OnClick += CreateDropdownList;
		}

		public override void LoadContent(IScreen screen)
		{
			Screen = screen;
			base.LoadContent(screen);
		}

		/// <summary>
		/// Method that gets called when the dropdown button is clicked to create the droplist.
		/// Adds a new screen with the GetDropdownList content.
		/// </summary>
		/// <param name="obj"></param>
		/// <param name="e"></param>
		public void CreateDropdownList(object obj, ClickEventArgs e)
		{
			//create the dropdown screen
			var droplist = new DropdownScreen<T>(this);

			//add the screen over the current one
			Screen.ScreenManager.AddScreen(droplist);
		}

		private void SetSelectedDropdownItem(DropdownItem<T> selectedItem)
		{
			if (selectedItem != SelectedDropdownItem)
			{
				//remove the old item
				RemoveItem(SelectedDropdownItem);

				//set the new selected item
				SelectedDropdownItem = selectedItem.DeepCopy() as DropdownItem<T>;
				SelectedDropdownItem.Position = Position;

				//clear out the current item
				Layout.Items.Clear();

				//add the new item as the selected item
				if (null != selectedItem)
				{
					AddItem(selectedItem);
				}
			}
		}

		public void Clear()
		{
			SetSelectedDropdownItem(null);
			DropdownList.Clear();
		}

		#endregion //Methods
	}
}