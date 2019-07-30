using GameTimer;
using InputHelper;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MenuBuddy
{
	public class Dropdown<T> : RelativeLayout, IDropdown<T>, IDisposable
	{
		#region Events

		public event EventHandler<SelectionChangeEventArgs<T>> OnSelectedItemChange;

		#endregion //Events

		#region Properties

		/// <summary>
		/// the screen that holds this guy
		/// </summary>
		protected IScreen Screen
		{
			get; private set;
		}

		/// <summary>
		/// The currently selected item
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
		/// the dropdown icon that is displayed at the right of the widget
		/// </summary>
		private RelativeLayoutButton DropButton { get; set; }

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

		private ITransitionObject _transitionObject;
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

		public bool HasBackground { get; set; }

		protected Background Background { get; set; }

		#endregion //Properties

		#region Methods

		public Dropdown(IScreen screen)
		{
			Screen = screen;
			DropdownItems = new List<IDropdownItem<T>>();
			TransitionObject = new WipeTransitionObject(StyleSheet.DefaultTransition);
			Background = new Background();
		}

		public override void LoadContent(IScreen screen)
		{
			Background.LoadContent(screen);

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
				DropdownItems[i].LoadContent(screen);
			}

			base.LoadContent(screen);
		}

		/// <summary>
		/// Method that gets called when the dropdown button is clicked to create the droplist.
		/// Adds a new screen with the GetDropdownList content.
		/// </summary>
		/// <param name="obj"></param>
		/// <param name="e"></param>
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

		public void AddDropdownItem(IDropdownItem<T> dropdownItem)
		{
			if (null == dropdownItem)
			{
				throw new ArgumentNullException("dropdownItem");
			}
			dropdownItem.TransitionObject = TransitionObject;
			DropdownItems.Add(dropdownItem);
		}

		public override void DrawBackground(IScreen screen, GameClock gameTime)
		{
			Background.Draw(this, screen);

			base.DrawBackground(screen, gameTime);
		}

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