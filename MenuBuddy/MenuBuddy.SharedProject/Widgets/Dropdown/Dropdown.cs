using InputHelper;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MenuBuddy
{
	public class Dropdown<T> : RelativeLayoutButton, IDropdown<T>, IDisposable
	{
		#region Events

		public event EventHandler<SelectionChangeEventArgs<T>> OnSelectedItemChange;

		#endregion //Events

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
		private DropdownItem<T> _selectedDropdownItem;
		public DropdownItem<T> SelectedDropdownItem
		{
			get
			{
				return _selectedDropdownItem;
			}
			set
			{
				SetSelectedDropdownItem(value);

				//fire off the selected event
				if (null != _selectedDropdownItem && null != OnSelectedItemChange)
				{
					OnSelectedItemChange(this, new SelectionChangeEventArgs<T>(_selectedDropdownItem.Item));
				}
			}
		}

		public T SelectedItem
		{
			get
			{
				return SelectedDropdownItem.Item;
			}
			set
			{
				DropdownItem<T> item = null;
				if (null != value)
				{
					item = DropdownItems.First(x => x.Item.ToString() == value.ToString());
				}
				SetSelectedDropdownItem(item);
			}
		}

		/// <summary>
		/// When the dropdown button is clicked, this list of items is popped up in the dropdown list.
		/// </summary>
		public List<DropdownItem<T>> DropdownItems { get; private set; }

		/// <summary>
		/// the dropdown icon that is displayed at the right of the widget
		/// </summary>
		private Image DropImage { get; set; }

		public int SelectedIndex
		{
			set
			{
				if (0 <= value && value < DropdownItems.Count)
				{
					SelectedItem = DropdownItems[value].Item;
				}
			}
		}

		#endregion //Properties

		#region Methods

		public Dropdown()
		{
			DropdownItems = new List<DropdownItem<T>>();
			OnClick += CreateDropdownList;
		}

		public override void LoadContent(IScreen screen)
		{
			Screen = screen;
			base.LoadContent(screen);

			DropImage = new Image()
			{
				Texture = Screen.ScreenManager.Game.Content.Load<Texture2D>(StyleSheet.DropdownImageResource),
				Size = new Vector2(Rect.Height, Rect.Height) * 0.75f,
				Horizontal = HorizontalAlignment.Right,
				Vertical = VerticalAlignment.Center,
				FillRect = true,
			};
			AddItem(DropImage);
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

				if (null != selectedItem)
				{
					//set the new selected item
					_selectedDropdownItem = selectedItem?.DeepCopy() as DropdownItem<T>;
					_selectedDropdownItem.Position = Position;
				}

				//clear out the current item
				Layout.Items.Clear();

				//add the new item as the selected item
				if (null != selectedItem)
				{
					AddItem(selectedItem);
				}

				//add the expansion button
				if (null != DropImage)
				{
					AddItem(DropImage);
				}
			}
		}

		public void Clear()
		{
			SelectedItem = default(T);
			DropdownItems.Clear();
		}

		public override void Dispose()
		{
			base.Dispose();
			OnSelectedItemChange = null;
		}

		public void AddDropdownItem(DropdownItem<T> dropdownItem)
		{
			DropdownItems.Add(dropdownItem);
			if (DropdownItems.Count == 1)
			{
				SelectedItem = dropdownItem.Item;
			}
		}

		#endregion //Methods
	}
}