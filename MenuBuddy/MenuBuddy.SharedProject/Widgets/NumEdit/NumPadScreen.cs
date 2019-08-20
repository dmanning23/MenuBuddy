using InputHelper;
using Microsoft.Xna.Framework;
using ResolutionBuddy;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MenuBuddy
{
    public class NumPadScreen : WidgetScreen, IHighlightable
	{
		#region Fields

		private ScrollLayout _layout;
		private StackLayout _rows;
		private bool AllowDecimal { get; set; }
		private bool AllowNegative { get; set; }

		#endregion //Fields

		#region Properties

		/// <summary>
		/// The dropdown that created this screen
		/// </summary>
		public INumEdit NumEditWidget { get; set; }

		private ITransitionObject TransitionObject { get; set; }

		#endregion //Properties

		#region Methods

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="widget"></param>
		public NumPadScreen(INumEdit widget, bool allowDecimal = true, bool allowNegative = true) : base("NumPad")
		{
			TransitionObject = new WipeTransitionObject(TransitionWipeType.None);
			Transition.OnTime = 0f;
			Transition.OffTime = 0f;
			NumEditWidget = widget;
			CoverOtherScreens = false;
			AllowDecimal = allowDecimal;
			AllowNegative = allowNegative;
		}

		public override async Task LoadContent()
		{
			await base.LoadContent();

			//create the stack layout that will hold all the droplist items
			_rows = new StackLayout()
			{
				Alignment = StackAlignment.Top,
				Horizontal = HorizontalAlignment.Left,
				Vertical = VerticalAlignment.Top,
			};

			//Add all the items to the stack layout
			
			var firstRow = GetRow();
			firstRow.AddItem(new NumPadButton("1", NumEditWidget));
			firstRow.AddItem(new NumPadButton("2", NumEditWidget));
			firstRow.AddItem(new NumPadButton("3", NumEditWidget));
			_rows.AddItem(firstRow);

			var secondRow = GetRow();
			secondRow.AddItem(new NumPadButton("4", NumEditWidget));
			secondRow.AddItem(new NumPadButton("5", NumEditWidget));
			secondRow.AddItem(new NumPadButton("6", NumEditWidget));
			_rows.AddItem(secondRow);

			var thirdRow = GetRow();
			thirdRow.AddItem(new NumPadButton("7", NumEditWidget));
			thirdRow.AddItem(new NumPadButton("8", NumEditWidget));
			thirdRow.AddItem(new NumPadButton("9", NumEditWidget));
			_rows.AddItem(thirdRow);

			var fourthRow = GetRow();
			if (AllowDecimal)
			{
				fourthRow.AddItem(new NumPadButton(".", NumEditWidget));
			}
			else
			{
				fourthRow.AddItem(new Shim(48f, 32f));
			}
			fourthRow.AddItem(new NumPadButton("0", NumEditWidget));
			if (AllowNegative)
			{
				fourthRow.AddItem(new NumPadButton("-", NumEditWidget));
			}
			else
			{
				fourthRow.AddItem(new Shim(48f, 32f));
			}
			_rows.AddItem(fourthRow);

			var fifthhRow = GetRow();
			fifthhRow.AddItem(new NumPadButton("<", NumEditWidget));
			_rows.AddItem(fifthhRow);

			//create the scroll layout and add the stack
			_layout = new ScrollLayout()
			{
				Horizontal = HorizontalAlignment.Left,
				Vertical = VerticalAlignment.Top,
				Size = new Vector2((48f * 3), 32f * 5)
			};
			_layout.Position = new Point(NumEditWidget.Rect.Left, NumEditWidget.Rect.Bottom);

			if (_layout.Rect.Bottom > Resolution.ScreenArea.Bottom)
			{
				_layout.Vertical = VerticalAlignment.Bottom;
				_layout.Position = new Point(NumEditWidget.Rect.Left, NumEditWidget.Rect.Top);
			};

			_layout.AddItem(_rows);
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

		public override bool CheckHighlight(HighlightEventArgs highlight)
		{
			base.CheckHighlight(highlight);

			//don't highlight other items when this dude is on top
			return IsActive;
		}

		public override bool CheckClick(ClickEventArgs click)
		{
			//check if the user clicked one of the items
			if (!base.CheckClick(click))
			{
				//set the number of the numEdit
				var numFloat = NumEditWidget.Number;
				try
				{
					numFloat = Convert.ToSingle(NumEditWidget.NumberText);
				}
				catch (Exception)
				{
				}

				NumEditWidget.SetNumber(numFloat);

				//Once the user clicks anywhere on this screen, it gets popped off
				ExitScreen();
			}

			return true;
		}

		public override void Draw(GameTime gameTime)
		{
			ScreenManager.SpriteBatchBegin();

			//draw the background
			ScreenManager.DrawHelper.DrawRect(this,
				StyleSheet.NeutralBackgroundColor,
				Rectangle.Intersect(_rows.Rect, _layout.Rect),
				TransitionObject);

			ScreenManager.SpriteBatchEnd();

			base.Draw(gameTime);
		}

		private StackLayout GetRow()
		{
			return new StackLayout(StackAlignment.Left)
			{
				Horizontal = HorizontalAlignment.Left,
				Vertical = VerticalAlignment.Top
			};
		}

		#endregion //Methods

		class NumPadButton : RelativeLayoutButton
		{
			private string Num { get; set; }
			private INumEdit NumWidget { get; set; }
			public NumPadButton(string num, INumEdit widget)
			{
				Num = num;
				NumWidget = widget;
				IsQuiet = true;
			}

			public override async Task LoadContent(IScreen screen)
			{
				await base.LoadContent(screen);

				Size = new Vector2(48f, 32f);
				Horizontal = HorizontalAlignment.Left;
				Vertical = VerticalAlignment.Top;

				AddItem(new Label(Num, screen.Content, FontSize.Small)
				{
					Horizontal = HorizontalAlignment.Center,
					Vertical = VerticalAlignment.Center
				});

				OnClick += ((obj, e) =>
				{
					NumWidget.NumberText = AppendNumber(NumWidget.NumberText);
				});
			}

			public string AppendNumber(string originalNum)
			{
				var result = originalNum;
				switch (Num)
				{
					case "-":
						{
							//multiply the number by -1
							try
							{
								var numFloat = Convert.ToSingle(originalNum);
								numFloat *= -1f;
								result = numFloat.ToString();
							}
							catch (Exception)
							{
							}
						}
						break;
					case ".":
						{
							//If there isn't already a decimal, add one
							if (!originalNum.Contains("."))
							{
								result = originalNum + ".";
							}
						}
						break;
					case "<":
						{
							//remove the last character from the end
							if (!string.IsNullOrEmpty(originalNum))
							{
								result = originalNum.Substring(0, originalNum.Length - 1);
							}
						}
						break;
					default:
						{
							if (originalNum == "0")
							{
								result = Num;
							}
							else
							{
								result = originalNum + Num;
							}
						}
						break;
				}

				return result;
			}
		}
	}
}
