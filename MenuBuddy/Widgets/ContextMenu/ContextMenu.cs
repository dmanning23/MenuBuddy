using InputHelper;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ResolutionBuddy;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;

namespace MenuBuddy
{
	/// <summary>
	/// A modal screen that displays a context menu at the position where the user clicked.
	/// </summary>
	public class ContextMenu : WidgetScreen, IContextMenu
	{
		#region Properties

		/// <summary>
		/// The scroll layout containing the context menu items.
		/// </summary>
		private ScrollLayout _layout;

		/// <summary>
		/// The stack layout holding the menu items vertically.
		/// </summary>
		private StackLayout _stack;

		/// <summary>
		/// The screen position where the context menu was invoked.
		/// </summary>
		private Vector2 _clickPos;

		/// <summary>
		/// The list of items to display in the context menu.
		/// </summary>
		private List<ContextMenuItem> ContextMenuItems { get; set; }

		/// <summary>
		/// The transition object controlling the pop animation direction.
		/// </summary>
		ITransitionObject TransitionObject { get; set; }

		#endregion //Properties

		#region Methods

		/// <summary>
		/// Initializes a new <see cref="ContextMenu"/> at the specified screen position.
		/// </summary>
		/// <param name="clickPos">The screen position where the context menu should appear.</param>
		public ContextMenu(Vector2 clickPos) : base("ContextMenuScreen")
		{
			ContextMenuItems = new List<ContextMenuItem>();
			_clickPos = clickPos;

			CoverOtherScreens = false;
			CoveredByOtherScreens = false;
			Transition.OnTime = 0.2f;
			Transition.OffTime = 0.2f;
		}

		/// <inheritdoc/>
		public void AddItem(Texture2D icon, string iconText, ClickDelegate clickEvent)
		{
			ContextMenuItems.Add(new ContextMenuItem(icon, iconText, clickEvent));
		}

		/// <summary>
		/// Loads the context menu layout, positioning it relative to the click position and adjusting direction based on screen bounds.
		/// </summary>
		public override async Task LoadContent()
		{
			await base.LoadContent();

			_stack = new StackLayout()
			{
				Alignment = StackAlignment.Top,
				Position = Point.Zero,
				Horizontal = HorizontalAlignment.Left,
				Vertical = VerticalAlignment.Top,
			};

			//add each menu item below this
			foreach (var menuItem in ContextMenuItems)
			{
				CreateButton(menuItem, _stack);
			}

			//figure out if we should do left or right
			var horiz = HorizontalAlignment.Left;
			if ((_clickPos.X + _stack.Rect.Width) > Resolution.ScreenArea.Right)
			{
				horiz = HorizontalAlignment.Right;
			}

			//figure out if we should do top or bottom
			var vert = VerticalAlignment.Top;
			if ((_clickPos.Y + _stack.Rect.Height) > Resolution.ScreenArea.Bottom)
			{
				vert = VerticalAlignment.Bottom;

				foreach (var item in _stack.Items)
				{
					var transitionable = item as ITransitionable;
					if (null != transitionable)
					{
						transitionable.TransitionObject = new WipeTransitionObject(TransitionWipeType.PopBottom);
					}
				}
			}

			//create the scroll layout
			_layout = new ScrollLayout()
			{
				Position = _clickPos.ToPoint(),
				Horizontal = horiz,
				Vertical = vert,
				TransitionObject = new WipeTransitionObject(vert == VerticalAlignment.Top ? TransitionWipeType.PopTop : TransitionWipeType.PopBottom),
				Size = new Vector2(_stack.Rect.Width, _stack.Rect.Height)
			};
			_layout.AddItem(_stack);
			AddItem(_layout);

			//set the transition object for this layout so the background will follow correctly
			TransitionObject = new WipeTransitionObject(vert == VerticalAlignment.Top ? TransitionWipeType.PopTop : TransitionWipeType.PopBottom)
			{
				ScreenTransition = Transition
			};
		}

		/// <summary>
		/// Creates a button widget for the specified menu item and adds it to the stack layout.
		/// </summary>
		/// <param name="hamburgerItem">The menu item data to create a button for.</param>
		/// <param name="stack">The stack layout to add the button to.</param>
		private void CreateButton(ContextMenuItem hamburgerItem, StackLayout stack)
		{
			var button = new StackLayoutButton()
			{
				Vertical = VerticalAlignment.Center,
				Horizontal = HorizontalAlignment.Left,
				TransitionObject = new WipeTransitionObject(TransitionWipeType.PopTop),
				Alignment = StackAlignment.Left,
			};

			if (null != hamburgerItem.Icon)
			{
				button.AddItem(new Image(hamburgerItem.Icon)
				{
					Size = new Vector2(24f, 24f),
					Vertical = VerticalAlignment.Center,
					Horizontal = HorizontalAlignment.Left,
					FillRect = true,
					TransitionObject = new WipeTransitionObject(TransitionWipeType.PopTop)
				});
			}
			else
			{
				button.AddItem(new Shim()
				{
					Size = new Vector2(24f, 24f),
					Vertical = VerticalAlignment.Center,
					Horizontal = HorizontalAlignment.Left,
					TransitionObject = new WipeTransitionObject(TransitionWipeType.PopTop)
				});
			}

			button.AddItem(new Shim()
			{
				Size = new Vector2(8f, 8f)
			});

			button.AddItem(new Label(hamburgerItem.IconText, Content, FontSize.Small)
			{
				Vertical = VerticalAlignment.Center,
				Horizontal = HorizontalAlignment.Left,
				TransitionObject = new WipeTransitionObject(TransitionWipeType.PopTop)
			});

			button.AddItem(new Shim()
			{
				Size = new Vector2(24f, 24f),
			});
			
			button.OnClick += (obj, e) => ExitScreen();
			button.OnClick += (obj, e) => hamburgerItem.ClickEvent(obj, e);

			stack.AddItem(button);
			stack.AddItem(new Shim()
			{
				Size = new Vector2(4f, 4f)
			});
		}

		/// <summary>
		/// Handles clicks. Clicking outside the menu dismisses it; clicking an item activates it.
		/// </summary>
		/// <param name="click">The click event arguments.</param>
		/// <returns>Always <c>true</c>, consuming the click.</returns>
		public override bool CheckClick(ClickEventArgs click)
		{
			//check if the user clicked one of the items
			if (!base.CheckClick(click))
			{
				//Once the user clicks anywhere on this screen, it gets popped off
				ExitScreen();
			}

			return true;
		}

		/// <summary>
		/// Draws the context menu background and items.
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
