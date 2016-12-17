using GameTimer;
using InputHelper;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace MenuBuddy
{
	public abstract class BaseSlider<T> : Widget, ISlider<T>
	{
		#region Fields

		private Vector2 _size;

		/// <summary>
		/// the minimum value of the slider
		/// </summary>
		private float _min;

		/// <summary>
		/// the maximum location of the slider
		/// </summary>
		private float _max;

		/// <summary>
		/// the current value of the handle
		/// </summary>
		private float _handlePosition;

		private Vector2 _handleSize;

		private Rectangle _slideRect;
		private Rectangle _handleRect;

		public event EventHandler<DragEventArgs> OnDrag;

		public List<float> Marks { get; set; }

		public bool Enabled { get; set; }

		#endregion //Fields

		#region Properties

		public Vector2 Size
		{
			get
			{
				return _size;
			}
			set
			{
				if (_size != value)
				{
					_size = value;
					CalculateRect();
				}
			}
		}

		public float Min
		{
			get
			{
				return _min;
			}
			set
			{
				if (!_min.Equals(value))
				{
					_min = value;
					CalculateRect();
				}
			}
		}

		public float Max
		{
			get
			{
				return _max;
			}
			set
			{
				if (!_max.Equals(value))
				{
					_max = value;
					CalculateRect();
				}
			}
		}

		protected virtual float HandlePosition
		{
			get
			{
				return _handlePosition;
			}
			set
			{
				//constrain the scroll to within the total rect
				value = ConstrainSliderPos(value);

				if (_handlePosition != value)
				{
					//set the scroll position
					_handlePosition = value;
					CalculateHandleRect();
				}
			}
		}

		/// <summary>
		/// the position of the handle of the slider
		/// </summary>
		public abstract T SliderPosition { get; set; }

		public Vector2 HandleSize
		{
			private get
			{
				return _handleSize;
			}
			set
			{
				if (_handleSize != value)
				{
					_handleSize = value;
					CalculateRect();
				}
			}
		}

		#endregion //Properties

		#region Initialization

		/// <summary>
		/// constructor
		/// </summary>
		public BaseSlider()
		{
			HasBackground = false;
			HasOutline = true;
			Marks = new List<float>();
			Enabled = true;
		}

		public BaseSlider(BaseSlider<T> inst) : base(inst)
		{
			Marks = new List<float>();
			foreach (var mark in inst.Marks)
			{
				Marks.Add(mark);
			}

			_min = inst._min;
			_max = inst._max;
			_handlePosition = inst._handlePosition;
			_handleSize = new Vector2(inst._handleSize.X, inst._handleSize.Y);
			_size = new Vector2(inst._size.X, inst._size.Y);
			_handleRect = new Rectangle(inst._handleRect.Location, inst._handleRect.Size);
			_slideRect = new Rectangle(inst._slideRect.Location, inst._slideRect.Size);
			OnDrag = inst.OnDrag;
		}

		#endregion //Initialization

		#region Methods

		public override void Update(IScreen screen, GameClock gameTime)
		{
		}

		public override void DrawBackground(IScreen screen, GameClock gameTime)
		{
			base.DrawBackground(screen, gameTime);

			//draw the slide rect
			screen.ScreenManager.DrawHelper.DrawRect(
				IsHighlighted ? StyleSheet.HighlightedBackgroundColor : StyleSheet.NeutralBackgroundColor,
				_slideRect, screen.Transition, Transition);

			//draw the marks on the background
			foreach (var mark in Marks)
			{
				var markPos = SolveSliderPos(Min, Max, mark, _slideRect.Left, _slideRect.Right);
				screen.ScreenManager.DrawHelper.Prim.Line(
					new Vector2(markPos, _slideRect.Top),
					new Vector2(markPos, _slideRect.Bottom),
					Color.DarkRed);
			}
		}

		public override void Draw(IScreen screen, GameClock gameTime)
		{
			if (Enabled)
			{
				//draw the handle rect
				screen.ScreenManager.DrawHelper.DrawRect(
					IsHighlighted ? StyleSheet.HighlightedTextColor : StyleSheet.NeutralTextColor,
					_handleRect, screen.Transition, Transition);
			}
		}

		protected override void CalculateRect()
		{
			//get the size of the rect
			var size = (Size + (Padding * 2f)) * Scale;

			//set the x component
			Vector2 pos = Position.ToVector2();
			switch (Horizontal)
			{
				case HorizontalAlignment.Center: { pos.X -= size.X / 2f; } break;
				case HorizontalAlignment.Right: { pos.X -= size.X; } break;
			}

			//set the y component
			switch (Vertical)
			{
				case VerticalAlignment.Center: { pos.Y -= size.Y / 2f; } break;
				case VerticalAlignment.Bottom: { pos.Y -= size.Y; } break;
			}

			_rect = new Rectangle((int)pos.X, (int)pos.Y, (int)size.X, (int)size.Y);

			//update the handle rect after the rect has been changed
			CalculateHandleRect();
		}

		private void CalculateHandleRect()
		{
			//get the slide rect
			_slideRect = new Rectangle((int)(Rect.Left + (HandleSize.X * .5f)),
				(int)(Rect.Center.Y - (HandleSize.Y * .25f)),
				(int)(Rect.Width - HandleSize.X),
				(int)(HandleSize.Y - (HandleSize.Y * .5f)));

			//get the location of the handle
			var yLoc = Rect.Center.Y - (HandleSize.Y * .5f);
			var xLoc = SolveSliderPos(Min, Max, HandlePosition, _slideRect.Left, _slideRect.Right) - (HandleSize.X * .5f);

			_handleRect = new Rectangle((int)xLoc, (int)yLoc, (int)HandleSize.X, (int)HandleSize.Y);
		}

		public float ConstrainSliderPos(float pos)
		{
			if (pos < Min)
			{
				pos = Min;
			}
			else if (pos > Max)
			{
				pos = Max;
			}

			return pos;
		}

		/// <summary>
		/// solve for pos2:
		/// 
		/// max1 - min1       max2 - min2
		/// -----------   =   ------------
		/// max1 - pos1       max2 - pos2
		///                           ^
		///                           
		/// pos2 = (((range2 * (max1 - pos1)) / range1) - max2) * -1
		/// 
		/// </summary>
		/// <param name="min1"></param>
		/// <param name="max1"></param>
		/// <param name="pos1"></param>
		/// <param name="min2"></param>
		/// <param name="max2"></param>
		/// <returns></returns>
		public static float SolveSliderPos(float min1, float max1, float pos1, float min2, float max2)
		{
			var range1 = max1 - min1;
			var range2 = max2 - min2;
			var ratio1 = max1 - pos1;

			return (((range2 * ratio1) / range1) - max2) * -1;
		}

		public bool CheckDrag(DragEventArgs drag)
		{
			var result = Rect.Contains(drag.Start);
			if (result && Enabled)
			{
				//convert back to slider coords and set the slider pos
				HandlePosition = SolveSliderPos(_slideRect.Left, _slideRect.Right, drag.Current.X, Min, Max);

				//fire off the event for any listeners
				if (OnDrag != null)
				{
					OnDrag(this, drag);
				}
			}

			return result;
		}

		#endregion //Methods
	}
}
