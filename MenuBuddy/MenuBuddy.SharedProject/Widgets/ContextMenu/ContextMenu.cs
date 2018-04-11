using InputHelper;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ResolutionBuddy;
using System.Collections.Generic;
using System;

namespace MenuBuddy
{
	public class ContextMenu : WidgetScreen, IContextMenu
	{
		#region Properties

		private ScrollLayout _layout;
		private StackLayout _stack;

		private Vector2 _clickPos;

		private List<ContextMenuItem> ContextMenuItems { get; set; }

		ITransitionObject TransitionObject { get; set; }

		#endregion //Properties

		#region Methods

		public ContextMenu(Vector2 clickPos) : base("ContextMenuScreen")
		{
			ContextMenuItems = new List<ContextMenuItem>();
			_clickPos = clickPos;

			CoverOtherScreens = false;
			CoveredByOtherScreens = false;
			Transition.OnTime = 0.2f;
			Transition.OffTime = 0.2f;
		}

		public void AddItem(Texture2D icon, string iconText, ClickDelegate clickEvent)
		{
			ContextMenuItems.Add(new ContextMenuItem(icon, iconText, clickEvent));
		}

		public override void LoadContent()
		{
			base.LoadContent();

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

		public override void Draw(GameTime gameTime)
		{
			ScreenManager.SpriteBatchBegin();

			//draw the background
			ScreenManager.DrawHelper.DrawRect(
				StyleSheet.NeutralBackgroundColor,
				Rectangle.Intersect(_stack.Rect, _layout.Rect),
				TransitionObject);

			ScreenManager.SpriteBatchEnd();

			base.Draw(gameTime);
		}

		#endregion //Methods
	}
}
