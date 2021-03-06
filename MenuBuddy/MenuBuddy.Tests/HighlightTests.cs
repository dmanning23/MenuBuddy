﻿using GameTimer;
using InputHelper;
using Microsoft.Xna.Framework;
using Moq;
using NUnit.Framework;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouchScreenBuddy;

namespace MenuBuddy.Tests
{
	public class TestRelativeLayoutButton : RelativeLayoutButton
	{
		public void SetInputHelper(IInputHelper helper)
		{
			InputHelper = helper;
		}
	}

	[TestFixture]
	public class HighlightTests
	{
		#region Fields

		private List<HighlightEventArgs> _highlights;
		private IInputHelper _input;
		private AbsoluteLayout _layout;
		private TestRelativeLayoutButton _button1;
		private TestRelativeLayoutButton _button2;

		#endregion //Fields

		#region Setup

		[SetUp]
		public void Setup()
		{
			_highlights = new List<HighlightEventArgs>();
			var inputMock = new Mock<IInputHelper>();
			inputMock.Setup(x => x.Highlights).Returns(_highlights);
			_input = inputMock.Object;

			_layout = new AbsoluteLayout()
			{
				Horizontal = HorizontalAlignment.Left,
				Vertical = VerticalAlignment.Top,
				Size = new Vector2(40, 10),
				Position = new Point(0,0),
			};

			_button1 = new TestRelativeLayoutButton()
			{
				Horizontal = HorizontalAlignment.Left,
				Vertical = VerticalAlignment.Top,
				Size = new Vector2(20, 10),
				Position = new Point(0, 0),
			};
			_button1.SetInputHelper(_input);
			_layout.AddItem(_button1);

			_button2 = new TestRelativeLayoutButton()
			{
				Horizontal = HorizontalAlignment.Left,
				Vertical = VerticalAlignment.Top,
				Size = new Vector2(20, 10),
				Position = new Point(20, 0),
			};
			_button2.SetInputHelper(_input);
			_layout.AddItem(_button2);
		}

		#endregion //Setup

		#region Tests

		[Test]
		public void Highlight1()
		{
			_layout.CheckHighlight(new HighlightEventArgs(new Vector2(10, 5), _input));

			_button1.IsHighlighted.ShouldBe(true);
		}

		[Test]
		public void Highlight2()
		{
			_layout.CheckHighlight(new HighlightEventArgs(new Vector2(30, 5), _input));

			_button2.IsHighlighted.ShouldBe(true);
		}

		[Test]
		public void Highlight1_Then2()
		{
			var highlight1 = new HighlightEventArgs(new Vector2(10, 5), _input);
			var highlight2 = new HighlightEventArgs(new Vector2(30, 5), _input);

			_input.Highlights.Add(highlight1);
			_input.Highlights.Add(highlight2);

			_layout.CheckHighlight(highlight1);
			_layout.CheckHighlight(highlight2);

			_button1.IsHighlighted.ShouldBe(true);
			_button2.IsHighlighted.ShouldBe(true);
		}

		[Test]
		public void Highlight1_ThenNot1()
		{
			var highlight1 = new HighlightEventArgs(new Vector2(10, 5), _input);
			var highlight2 = new HighlightEventArgs(new Vector2(30, 5), _input);

			_layout.CheckHighlight(highlight1);
			_layout.CheckHighlight(highlight2);

			_button1.IsHighlighted.ShouldBe(false);
			_button2.IsHighlighted.ShouldBe(true);
		}

		[Test]
		public void Button1_NotTapped()
		{
			_button1.IsTappable = true;

			_button1.Update(null, new GameClock());

			_button1.WasTapped.ShouldBe(false);
		}

		[Test]
		public void Button1_WasTapped()
		{
			_button1.IsTappable = true;

			var highlight1 = new HighlightEventArgs(new Vector2(10, 5), _input);
			_input.Highlights.Add(highlight1);

			_button1.Update(null, new GameClock());

			_button1.WasTapped.ShouldBe(true);
		}

		[Test]
		public void Button1_NotHeld()
		{
			_button1.IsTappable = true;

			_button1.Update(null, new GameClock());

			_button1.IsTapHeld.ShouldBe(false);
		}

		[Test]
		public void Button1_IsHeld()
		{
			_button1.IsTappable = true;

			var highlight1 = new HighlightEventArgs(new Vector2(10, 5), _input);
			_input.Highlights.Add(highlight1);

			_button1.Update(null, new GameClock());

			_button1.IsTapHeld.ShouldBe(true);
		}

		#endregion Tests
	}
}
