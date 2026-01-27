using GameTimer;
using InputHelper;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace MenuBuddy
{
	/// <summary>
	/// Abstract base class for slider widgets. Provides track rendering, handle positioning, drag and click handling,
	/// and hash mark display.
	/// </summary>
	/// <typeparam name="T">The type of the slider value.</typeparam>
	public abstract class BaseSlider<T> : Widget, ISlider<T>, IDisposable, IClickable
	{
		#region Fields

		private Vector2 _size;

		/// <summary>
		/// The minimum value of the slider range.
		/// </summary>
		private float _min;

		/// <summary>
		/// The maximum value of the slider range.
		/// </summary>
		private float _max;

		/// <summary>
		/// The current handle position within the slider range.
		/// </summary>
		private float _handlePosition;

		private Vector2 _handleSize;

		private Rectangle _slideRect;
		private Rectangle _handleRect;

		/// <summary>
		/// Raised during a drag operation on this slider.
		/// </summary>
		public event EventHandler<DragEventArgs> OnDrag;

		/// <summary>
		/// Raised when this slider is clicked.
		/// </summary>
		public event EventHandler<ClickEventArgs> OnClick;

		/// <inheritdoc/>
		public List<float> Marks { get; set; }

		/// <inheritdoc/>
		public bool Enabled { get; set; }

		#endregion //Fields

		#region Properties

		/// <summary>
		/// The overall size of the slider widget in pixels. Setting this recalculates the bounding rectangle.
		/// </summary>
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

		/// <inheritdoc/>
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

		/// <inheritdoc/>
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

		/// <summary>
		/// The current handle position, constrained within the slider range. Setting this recalculates the handle rectangle.
		/// </summary>
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
		/// The current slider value, expressed in the slider's value type.
		/// </summary>
		public abstract T SliderPosition { get; set; }

		/// <summary>
		/// The size of the draggable handle in pixels. Setting this recalculates the bounding rectangle.
		/// </summary>
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

		/// <summary>
		/// Whether this slider can be clicked to set the handle position.
		/// </summary>
		public bool Clickable { get; set; }

		/// <summary>
		/// Whether this slider is currently in a clicked visual state.
		/// </summary>
		public bool IsClicked { get; set; }

		#endregion //Properties

		#region Initialization

		/// <summary>
		/// Initializes a new <see cref="BaseSlider{T}"/> with default values.
		/// </summary>
		public BaseSlider()
		{
			HasBackground = false;
			HasOutline = true;
			Marks = new List<float>();
			Enabled = true;
			Clickable = true;
		}

		/// <summary>
		/// Initializes a new <see cref="BaseSlider{T}"/> by copying values from an existing instance.
		/// </summary>
		/// <param name="inst">The slider to copy from.</param>
		public BaseSlider(BaseSlider<T> inst) : base(inst)
		{
			Marks = new List<float>();
			foreach (var mark in inst.Marks)
			{
				Marks.Add(mark);
			}

			Clickable = inst.Clickable;
			IsClicked = inst.IsClicked;
			_min = inst._min;
			_max = inst._max;
			_handlePosition = inst._handlePosition;
			_handleSize = new Vector2(inst._handleSize.X, inst._handleSize.Y);
			_size = new Vector2(inst._size.X, inst._size.Y);
			_handleRect = new Rectangle(inst._handleRect.Location, inst._handleRect.Size);
			_slideRect = new Rectangle(inst._slideRect.Location, inst._slideRect.Size);
			OnDrag = inst.OnDrag;
			OnClick = inst.OnClick;
		}

		/// <summary>
		/// Unloads content and releases drag and click event handlers.
		/// </summary>
		public override void UnloadContent()
		{
			base.UnloadContent();
			OnDrag = null;
			OnClick = null;
		}

		#endregion //Initialization

		#region Methods

		/// <inheritdoc/>
		public override void Update(IScreen screen, GameClock gameTime)
		{
		}

		/// <summary>
		/// Draws the slider track background and any hash marks.
		/// </summary>
		/// <param name="screen">The screen this slider belongs to.</param>
		/// <param name="gameTime">The current game time.</param>
		public override void DrawBackground(IScreen screen, GameClock gameTime)
		{
			base.DrawBackground(screen, gameTime);

			//draw the slide rect
			screen.ScreenManager.DrawHelper.DrawRect(screen,
				IsHighlighted ? StyleSheet.HighlightedBackgroundColor : StyleSheet.NeutralBackgroundColor,
				_slideRect, TransitionObject);

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

		/// <summary>
		/// Draws the slider handle if the slider is enabled.
		/// </summary>
		/// <param name="screen">The screen this slider belongs to.</param>
		/// <param name="gameTime">The current game time.</param>
		public override void Draw(IScreen screen, GameClock gameTime)
		{
			if (Enabled)
			{
				//draw the handle rect
				screen.ScreenManager.DrawHelper.DrawRect(screen,
					IsHighlighted ? StyleSheet.HighlightedTextColor : StyleSheet.NeutralTextColor,
					_handleRect, TransitionObject);
			}
		}

		/// <inheritdoc/>
		protected override void CalculateRect()
		{
			//get the size of the rect
			var size = Size * Scale;

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

		/// <summary>
		/// Recalculates the slide track rectangle and handle rectangle based on the current position and size.
		/// </summary>
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

		/// <summary>
		/// Clamps the given position to be within the slider's <see cref="Min"/> and <see cref="Max"/> range.
		/// </summary>
		/// <param name="pos">The position to constrain.</param>
		/// <returns>The clamped position.</returns>
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
		/// Maps a position from one range to another using linear interpolation.
		/// Given a position within [min1, max1], returns the corresponding position within [min2, max2].
		/// </summary>
		/// <param name="min1">The minimum of the source range.</param>
		/// <param name="max1">The maximum of the source range.</param>
		/// <param name="pos1">The position within the source range.</param>
		/// <param name="min2">The minimum of the target range.</param>
		/// <param name="max2">The maximum of the target range.</param>
		/// <returns>The corresponding position within the target range.</returns>
		public static float SolveSliderPos(float min1, float max1, float pos1, float min2, float max2)
		{
			var range1 = max1 - min1;
			var range2 = max2 - min2;
			var ratio1 = max1 - pos1;

			return (((range2 * ratio1) / range1) - max2) * -1;
		}

		/// <summary>
		/// Checks whether the drag started within this slider's bounds, and if so, updates the handle position.
		/// </summary>
		/// <param name="drag">The drag event arguments.</param>
		/// <returns><c>true</c> if the drag started within this slider; otherwise, <c>false</c>.</returns>
		public bool CheckDrag(DragEventArgs drag)
		{
			var result = Rect.Contains(drag.Start);
			if (result && Enabled)
			{
				//convert back to slider coords and set the slider pos
				UpdateHandlePosition(drag.Current);

				//fire off the event for any listeners
				if (OnDrag != null)
				{
					OnDrag(this, drag);
				}
			}

			return result;
		}

		/// <summary>
		/// Updates the handle position by converting screen coordinates back to slider value coordinates.
		/// </summary>
		/// <param name="pos">The screen position to convert.</param>
		public void UpdateHandlePosition(Vector2 pos)
		{
			//convert back to slider coords and set the slider pos
			HandlePosition = SolveSliderPos(_slideRect.Left, _slideRect.Right, pos.X, Min, Max);
		}

		/// <summary>
		/// Checks whether the click is within this slider's bounds, and if so, updates the handle position.
		/// </summary>
		/// <param name="click">The click event arguments.</param>
		/// <returns><c>true</c> if this slider was clicked; otherwise, <c>false</c>.</returns>
		public bool CheckClick(ClickEventArgs click)
		{
			//check if the widget was clicked
			if (Rect.Contains(click.Position) && Clickable)
			{
				UpdateHandlePosition(click.Position);

				if (OnClick != null)
				{
					OnClick(this, click);
				}

				return true;
			}

			return false;
		}

		#endregion //Methods
	}
}
