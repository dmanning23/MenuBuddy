using GameTimer;
using InputHelper;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MenuBuddy
{
	/// <summary>
	/// A dropdown widget that displays a selected item and opens a <see cref="DropdownScreen{T}"/> with available options when clicked.
	/// </summary>
	/// <typeparam name="T">The type of the items in the dropdown.</typeparam>
	public class Dropdown<T> : RelativeLayout, IDropdown<T>, IDisposable
	{
		#region Events

		/// <summary>
		/// Raised when the selected item changes.
		/// </summary>
		public event EventHandler<SelectionChangeEventArgs<T>> OnSelectedItemChange;

		#endregion //Events

		#region Properties

		/// <summary>
		/// The screen that contains this dropdown widget.
		/// </summary>
		protected IScreen Screen
		{
			get; private set;
		}

		/// <summary>
		/// The currently selected dropdown item widget. Setting this fires <see cref="OnSelectedItemChange"/>.
		/// </summary>
		private IDropdownItem<T> _selectedDropdownItem;
		public IDropdownItem<T> SelectedDropdownItem
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

		/// <summary>
		/// The currently selected item value. Setting this finds the matching dropdown item in the list.
		/// </summary>
		public T SelectedItem
		{
			get
			{
				return (null != SelectedDropdownItem) ? SelectedDropdownItem.Item : default(T);
			}
			set
			{
				IDropdownItem<T> item = null;
				if (null != value)
				{
					item = DropdownItems.Where(x => x.Item != null).ToList().Find(x => x.Item.Equals(value));
				}
				SetSelectedDropdownItem(item);
			}
		}

		/// <summary>
		/// When the dropdown button is clicked, this list of items is popped up in the dropdown list.
		/// </summary>
		public List<IDropdownItem<T>> DropdownItems { get; private set; }

		/// <summary>
		/// The dropdown arrow button displayed at the right of the widget.
		/// </summary>
		private RelativeLayoutButton DropButton { get; set; }

		/// <summary>
		/// Sets the selected item by its index in <see cref="DropdownItems"/>.
		/// </summary>
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

		/// <summary>
		/// The transition object for this dropdown and its child items.
		/// </summary>
		private ITransitionObject _transitionObject;
		/// <inheritdoc/>
		public override ITransitionObject TransitionObject
		{
			get
			{
				return _transitionObject;
			}
			set
			{
				base.TransitionObject = value;
				_transitionObject = value;
			}
		}

		/// <inheritdoc/>
		public bool HasBackground { get; set; }

		/// <summary>
		/// The background renderer for this dropdown.
		/// </summary>
		protected Background Background { get; set; }

		#endregion //Properties

		#region Methods

		/// <summary>
		/// Initializes a new <see cref="Dropdown{T}"/> on the specified screen.
		/// </summary>
		/// <param name="screen">The screen that contains this dropdown.</param>
		public Dropdown(IScreen screen)
		{
			Screen = screen;
			DropdownItems = new List<IDropdownItem<T>>();
			TransitionObject = new WipeTransitionObject(StyleSheet.DefaultTransition);
			Background = new Background();
		}

		/// <inheritdoc/>
		public override async Task LoadContent(IScreen screen)
		{
			await Background.LoadContent(screen);

			DropButton = new RelativeLayoutButton()
			{
				Size = new Vector2(Rect.Height, Rect.Height),
				Horizontal = HorizontalAlignment.Right,
				Vertical = VerticalAlignment.Center,
				Highlightable = true,
				TransitionObject = this.TransitionObject,
			};
			DropButton.OnClick += CreateDropdownList;

			var dropImage = new Image()
			{
				Texture = Screen.Content.Load<Texture2D>(StyleSheet.DropdownImageResource),
				Size = new Vector2(Rect.Height, Rect.Height) * 0.75f,
				Horizontal = HorizontalAlignment.Right,
				Vertical = VerticalAlignment.Center,
				FillRect = true,
				TransitionObject = this.TransitionObject,
			};
			DropButton.AddItem(dropImage);
			AddItem(DropButton);

			for (int i = 0; i < DropdownItems.Count; i++)
			{
				await DropdownItems[i].LoadContent(screen);
			}

			await base.LoadContent(screen);
		}

		/// <inheritdoc/>
		public override void UnloadContent()
		{
			base.UnloadContent();
			OnSelectedItemChange = null;
			Screen = null;

			DropButton?.UnloadContent();
			DropButton = null;

			if (DropdownItems != null)
			{
				for (int i = 0; i < DropdownItems?.Count; i++)
				{
					DropdownItems[i].UnloadContent();
				}
			}
			DropdownItems = null;
		}

		/// <summary>
		/// Method that gets called when the dropdown button is clicked to create the droplist.
		/// Adds a new screen with the GetDropdownList content.
		/// </summary>
		/// <param name="obj">The source of the click event.</param>
		/// <param name="e">The click event arguments.</param>
		public async void CreateDropdownList(object obj, ClickEventArgs e)
		{
			if (e.Button == MouseButton.Left)
			{
				//create the dropdown screen
				var droplist = new DropdownScreen<T>(this);

				//add the screen over the current one
				await Screen.ScreenManager.AddScreen(droplist);
			}
		}

		/// <summary>
		/// Replaces the currently displayed item with the specified selection.
		/// </summary>
		/// <param name="selectedItem">The new selected dropdown item, or <c>null</c> to clear the selection.</param>
		private void SetSelectedDropdownItem(IDropdownItem<T> selectedItem)
		{
			if (selectedItem != SelectedDropdownItem)
			{
				//remove the old item
				RemoveItem(SelectedDropdownItem);

				if (null != selectedItem)
				{
					//set the new selected item
					_selectedDropdownItem = selectedItem?.DeepCopy() as IDropdownItem<T>;
					_selectedDropdownItem.Position = Position;
				}
				else
				{
					//set the selected item to null
					_selectedDropdownItem = null;
				}

				//clear out the current item
				Items.Clear();

				//add the new item as the selected item
				if (null != selectedItem)
				{
					AddItem(selectedItem);
				}

				//add the expansion button
				if (null != DropButton)
				{
					if (null != SelectedDropdownItem)
					{
						DropButton.Layer = SelectedDropdownItem.Layer - 100;
					}
					AddItem(DropButton);
				}
			}
		}

		/// <summary>
		/// Clears the selected item and removes all dropdown items.
		/// </summary>
		public void Clear()
		{
			SelectedItem = default(T);
			DropdownItems.Clear();
		}

		/// <inheritdoc/>
		public void AddDropdownItem(IDropdownItem<T> dropdownItem)
		{
			if (null == dropdownItem)
			{
				throw new ArgumentNullException("dropdownItem");
			}
			dropdownItem.TransitionObject = TransitionObject;
			DropdownItems.Add(dropdownItem);
		}

		/// <inheritdoc/>
		public override void DrawBackground(IScreen screen, GameClock gameTime)
		{
			Background.Draw(this, screen);

			base.DrawBackground(screen, gameTime);
		}

		/// <inheritdoc/>
		public override bool CheckClick(ClickEventArgs click)
		{
			var result = base.CheckClick(click);
			if (!result && Clickable && Rect.Contains(click.Position))
			{
				Clicked(this, click);
				return true;
			}

			return result;
		}

		#endregion //Methods
	}
}