using InputHelper;
using Microsoft.Xna.Framework;
using System;
using System.Threading.Tasks;

namespace MenuBuddy
{
	public class DropdownScreen<T> : WidgetScreen
	{
		#region Fields

		private ScrollLayout _layout;
		private StackLayout _stack;

		#endregion //Fields

		#region Properties

		/// <summary>
		/// The dropdown that created this screen
		/// </summary>
		public IDropdown<T> DropdownWidget { get; set; }

		private ITransitionObject TransitionObject { get; set; }

		#endregion //Properties

		#region Methods

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="widget"></param>
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

		public override void AddItem(IScreenItem item)
		{
			var widget = item as ITransitionable;
			if (null != widget)
			{
				widget.TransitionObject = new WipeTransitionObject(TransitionWipeType.None);
			}
			base.AddItem(item);
		}

		public override bool CheckClick(ClickEventArgs click)
		{
			//Once the user clicks anywhere on this screen, it gets popped off
			ExitScreen();

			//check if the user clicked one of the items
			return base.CheckClick(click);
		}

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