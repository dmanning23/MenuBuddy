using InputHelper;
using Microsoft.Xna.Framework;
using System;
using System.Threading.Tasks;

namespace MenuBuddy
{
	/// <summary>
	/// A modal screen that displays a scrollable list of dropdown items for selection.
	/// </summary>
	/// <typeparam name="T">The type of the items in the dropdown.</typeparam>
	public class DropdownScreen<T> : WidgetScreen
	{
		#region Fields

		/// <summary>
		/// The scroll layout containing the dropdown list.
		/// </summary>
		private ScrollLayout _layout;

		/// <summary>
		/// The stack layout holding the dropdown items vertically.
		/// </summary>
		private StackLayout _stack;

		#endregion //Fields

		#region Properties

		/// <summary>
		/// The dropdown widget that created this screen.
		/// </summary>
		public IDropdown<T> DropdownWidget { get; set; }

		/// <summary>
		/// The transition object for this screen (set to no transition).
		/// </summary>
		private ITransitionObject TransitionObject { get; set; }

		#endregion //Properties

		#region Methods

		/// <summary>
		/// Initializes a new <see cref="DropdownScreen{T}"/> for the specified dropdown widget.
		/// </summary>
		/// <param name="widget">The dropdown widget whose items will be displayed.</param>
		public DropdownScreen(IDropdown<T> widget) : base("Dropdown")
		{
			TransitionObject = new WipeTransitionObject(TransitionWipeType.None)
			{
				ScreenTransition = Transition
			};
			Transition.OnTime = 0f;
			Transition.OffTime = 0f;
			DropdownWidget = widget;
			CoverOtherScreens = false;
		}

		/// <summary>
		/// Loads the dropdown list layout, creating a scrollable stack of all dropdown items positioned below the widget.
		/// </summary>
		public override async Task LoadContent()
		{
			await base.LoadContent();

			//create the stack layout that will hold all the droplist items
			_stack = new StackLayout()
			{
				Alignment = StackAlignment.Top,
				Horizontal = HorizontalAlignment.Left,
				Vertical = VerticalAlignment.Top,
			};

			//Add all the items to the stack layout
			foreach (var item in DropdownWidget.DropdownItems)
			{
				_stack.AddItem(item.DeepCopy());
			}

			//create the scroll layout and add the stack
			_layout = new ScrollLayout()
			{
				Horizontal = HorizontalAlignment.Left,
				Vertical = VerticalAlignment.Top,
				Size = new Vector2(DropdownWidget.Rect.Width, ResolutionBuddy.Resolution.ScreenArea.Bottom - DropdownWidget.Rect.Bottom)
			};
			_layout.Position = new Point(DropdownWidget.Rect.Left, DropdownWidget.Rect.Bottom);
			_layout.AddItem(_stack);
			AddItem(_layout);
		}

		/// <summary>
		/// Adds a screen item with its transition set to none.
		/// </summary>
		/// <param name="item">The screen item to add.</param>
		public override void AddItem(IScreenItem item)
		{
			var widget = item as ITransitionable;
			if (null != widget)
			{
				widget.TransitionObject = new WipeTransitionObject(TransitionWipeType.None);
			}
			base.AddItem(item);
		}

		/// <summary>
		/// Handles clicks. Any click exits the dropdown screen, and if an item was clicked it is selected.
		/// </summary>
		/// <param name="click">The click event arguments.</param>
		/// <returns>Always <c>true</c>, consuming the click.</returns>
		public override bool CheckClick(ClickEventArgs click)
		{
			//Once the user clicks anywhere on this screen, it gets popped off
			ExitScreen();

			//check if the user clicked one of the items
			return base.CheckClick(click);
		}

		/// <summary>
		/// Draws the dropdown list background and items.
		/// </summary>
		public override void Draw(GameTime gameTime)
		{
			ScreenManager.SpriteBatchBegin();

			//draw the background
			ScreenManager.DrawHelper.DrawRect(this, 
				StyleSheet.NeutralBackgroundColor,
				Rectangle.Intersect(_stack.Rect, _layout.Rect),
				TransitionObject);

			ScreenManager.SpriteBatchEnd();

			base.Draw(gameTime);
		}

		#endregion //Methods
	}
}