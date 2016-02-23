using Microsoft.Xna.Framework;
using MouseBuddy;

namespace MenuBuddy
{
	public class DropdownScreen<T> : WidgetScreen
	{
		#region Fields

		private ScrollLayout _layout;

		#endregion //Fields

		#region Properties

		/// <summary>
		/// The dropdown that created this screen
		/// </summary>
		public IDropdown<T> DropdownWidget { get; set; }

		#endregion //Properties

		#region Methods

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="widget"></param>
		public DropdownScreen(IDropdown<T> widget) : base("Dropdown")
		{
			DropdownWidget = widget;
			CoverOtherScreens = false;
		}

		public override void LoadContent()
		{
			base.LoadContent();

			Style.Transition = TransitionType.None;

			//create the stack layout that will hold all the droplist items
			var stack = new StackLayout()
			{
				Alignment = StackAlignment.Top,
				Horizontal = HorizontalAlignment.Left,
				Vertical = VerticalAlignment.Top
			};

			//Add all the items to the stack layout
			foreach (var item in DropdownWidget.DropdownList)
			{
				stack.AddItem(item);
			}

			//create the scroll layout and add the stack
			_layout = new ScrollLayout()
			{
				Horizontal = HorizontalAlignment.Right,
				Vertical = VerticalAlignment.Bottom,
				Size = new Vector2(DropdownWidget.Rect.Width, ResolutionBuddy.Resolution.ScreenArea.Bottom - DropdownWidget.Rect.Bottom)
			};
			_layout.Position = new Point(DropdownWidget.Rect.Right, DropdownWidget.Rect.Bottom);
		}

		public override bool CheckClick(ClickEventArgs click)
		{
			//Once the user clicks anywhere on this screen, it gets popped off
			ExitScreen();

			//check if the user clicked one of the items
			return base.CheckClick(click);
		}

		#endregion //Methods
	}
}