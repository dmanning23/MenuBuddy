using Microsoft.Xna.Framework;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MenuBuddy.Tests
{
	[TestFixture]
	public class StackAlignments
	{
		#region Fields

		private StackLayout _stack;

		#endregion //Fields

		#region Setup

		[SetUp]
		public void Setup()
		{
			StyleSheet.InitUnitTests();
			_stack = new StackLayout();
		}

		#endregion //Setup

		#region Defaults

		[Test]
		public void StackAlignments_Default()
		{
			Assert.AreEqual(StackAlignment.Top, _stack.Alignment);
			Assert.AreEqual(HorizontalAlignment.Center, _stack.Horizontal);
			Assert.AreEqual(VerticalAlignment.Top, _stack.Vertical);
		}

		#endregion //Defaults

		#region BottomAlignment

		[Test]
		public void StackAlignments_BottomAlignment_TopLeft()
		{
			_stack.Alignment = StackAlignment.Bottom;
			_stack.Vertical = VerticalAlignment.Top;
			_stack.Horizontal = HorizontalAlignment.Left;

			Assert.AreEqual(StackAlignment.Bottom, _stack.Alignment);
			Assert.AreEqual(HorizontalAlignment.Left, _stack.Horizontal);
			Assert.AreEqual(VerticalAlignment.Bottom, _stack.Vertical);
		}

		[Test]
		public void StackAlignments_BottomAlignment_TopCenter()
		{
			_stack.Alignment = StackAlignment.Bottom;
			_stack.Vertical = VerticalAlignment.Top;
			_stack.Horizontal = HorizontalAlignment.Center;

			Assert.AreEqual(StackAlignment.Bottom, _stack.Alignment);
			Assert.AreEqual(HorizontalAlignment.Center, _stack.Horizontal);
			Assert.AreEqual(VerticalAlignment.Bottom, _stack.Vertical);
		}

		[Test]
		public void StackAlignments_BottomAlignment_TopRight()
		{
			_stack.Alignment = StackAlignment.Bottom;
			_stack.Vertical = VerticalAlignment.Top;
			_stack.Horizontal = HorizontalAlignment.Right;

			Assert.AreEqual(StackAlignment.Bottom, _stack.Alignment);
			Assert.AreEqual(HorizontalAlignment.Right, _stack.Horizontal);
			Assert.AreEqual(VerticalAlignment.Bottom, _stack.Vertical);
		}

		[Test]
		public void StackAlignments_BottomAlignment_CenterLeft()
		{
			_stack.Alignment = StackAlignment.Bottom;
			_stack.Vertical = VerticalAlignment.Center;
			_stack.Horizontal = HorizontalAlignment.Left;

			Assert.AreEqual(StackAlignment.Bottom, _stack.Alignment);
			Assert.AreEqual(HorizontalAlignment.Left, _stack.Horizontal);
			Assert.AreEqual(VerticalAlignment.Bottom, _stack.Vertical);
		}

		[Test]
		public void StackAlignments_BottomAlignment_CenterCenter()
		{
			_stack.Alignment = StackAlignment.Bottom;
			_stack.Vertical = VerticalAlignment.Center;
			_stack.Horizontal = HorizontalAlignment.Center;

			Assert.AreEqual(StackAlignment.Bottom, _stack.Alignment);
			Assert.AreEqual(HorizontalAlignment.Center, _stack.Horizontal);
			Assert.AreEqual(VerticalAlignment.Bottom, _stack.Vertical);
		}

		[Test]
		public void StackAlignments_BottomAlignment_CenterRight()
		{
			_stack.Alignment = StackAlignment.Bottom;
			_stack.Vertical = VerticalAlignment.Center;
			_stack.Horizontal = HorizontalAlignment.Right;

			Assert.AreEqual(StackAlignment.Bottom, _stack.Alignment);
			Assert.AreEqual(HorizontalAlignment.Right, _stack.Horizontal);
			Assert.AreEqual(VerticalAlignment.Bottom, _stack.Vertical);
		}

		[Test]
		public void StackAlignments_BottomAlignment_BottomLeft()
		{
			_stack.Alignment = StackAlignment.Bottom;
			_stack.Vertical = VerticalAlignment.Bottom;
			_stack.Horizontal = HorizontalAlignment.Left;

			Assert.AreEqual(StackAlignment.Bottom, _stack.Alignment);
			Assert.AreEqual(HorizontalAlignment.Left, _stack.Horizontal);
			Assert.AreEqual(VerticalAlignment.Bottom, _stack.Vertical);
		}

		[Test]
		public void StackAlignments_BottomAlignment_BottomCenter()
		{
			_stack.Alignment = StackAlignment.Bottom;
			_stack.Vertical = VerticalAlignment.Bottom;
			_stack.Horizontal = HorizontalAlignment.Center;

			Assert.AreEqual(StackAlignment.Bottom, _stack.Alignment);
			Assert.AreEqual(HorizontalAlignment.Center, _stack.Horizontal);
			Assert.AreEqual(VerticalAlignment.Bottom, _stack.Vertical);
		}

		[Test]
		public void StackAlignments_BottomAlignment_BottomRight()
		{
			_stack.Alignment = StackAlignment.Bottom;
			_stack.Vertical = VerticalAlignment.Bottom;
			_stack.Horizontal = HorizontalAlignment.Right;

			Assert.AreEqual(StackAlignment.Bottom, _stack.Alignment);
			Assert.AreEqual(HorizontalAlignment.Right, _stack.Horizontal);
			Assert.AreEqual(VerticalAlignment.Bottom, _stack.Vertical);
		}

		#endregion //BottomAlignment

		#region TopAlignment

		[Test]
		public void StackAlignments_TopAlignment_TopLeft()
		{
			_stack.Alignment = StackAlignment.Top;
			_stack.Vertical = VerticalAlignment.Top;
			_stack.Horizontal = HorizontalAlignment.Left;

			Assert.AreEqual(StackAlignment.Top, _stack.Alignment);
			Assert.AreEqual(HorizontalAlignment.Left, _stack.Horizontal);
			Assert.AreEqual(VerticalAlignment.Top, _stack.Vertical);
		}

		[Test]
		public void StackAlignments_TopAlignment_TopCenter()
		{
			_stack.Alignment = StackAlignment.Top;
			_stack.Vertical = VerticalAlignment.Top;
			_stack.Horizontal = HorizontalAlignment.Center;

			Assert.AreEqual(StackAlignment.Top, _stack.Alignment);
			Assert.AreEqual(HorizontalAlignment.Center, _stack.Horizontal);
			Assert.AreEqual(VerticalAlignment.Top, _stack.Vertical);
		}

		[Test]
		public void StackAlignments_TopAlignment_TopRight()
		{
			_stack.Alignment = StackAlignment.Top;
			_stack.Vertical = VerticalAlignment.Top;
			_stack.Horizontal = HorizontalAlignment.Right;

			Assert.AreEqual(StackAlignment.Top, _stack.Alignment);
			Assert.AreEqual(HorizontalAlignment.Right, _stack.Horizontal);
			Assert.AreEqual(VerticalAlignment.Top, _stack.Vertical);
		}

		[Test]
		public void StackAlignments_TopAlignment_CenterLeft()
		{
			_stack.Alignment = StackAlignment.Top;
			_stack.Vertical = VerticalAlignment.Center;
			_stack.Horizontal = HorizontalAlignment.Left;

			Assert.AreEqual(StackAlignment.Top, _stack.Alignment);
			Assert.AreEqual(HorizontalAlignment.Left, _stack.Horizontal);
			Assert.AreEqual(VerticalAlignment.Top, _stack.Vertical);
		}

		[Test]
		public void StackAlignments_TopAlignment_CenterCenter()
		{
			_stack.Alignment = StackAlignment.Top;
			_stack.Vertical = VerticalAlignment.Center;
			_stack.Horizontal = HorizontalAlignment.Center;

			Assert.AreEqual(StackAlignment.Top, _stack.Alignment);
			Assert.AreEqual(HorizontalAlignment.Center, _stack.Horizontal);
			Assert.AreEqual(VerticalAlignment.Top, _stack.Vertical);
		}

		[Test]
		public void StackAlignments_TopAlignment_CenterRight()
		{
			_stack.Alignment = StackAlignment.Top;
			_stack.Vertical = VerticalAlignment.Center;
			_stack.Horizontal = HorizontalAlignment.Right;

			Assert.AreEqual(StackAlignment.Top, _stack.Alignment);
			Assert.AreEqual(HorizontalAlignment.Right, _stack.Horizontal);
			Assert.AreEqual(VerticalAlignment.Top, _stack.Vertical);
		}

		[Test]
		public void StackAlignments_TopAlignment_BottomLeft()
		{
			_stack.Alignment = StackAlignment.Top;
			_stack.Vertical = VerticalAlignment.Bottom;
			_stack.Horizontal = HorizontalAlignment.Left;

			Assert.AreEqual(StackAlignment.Top, _stack.Alignment);
			Assert.AreEqual(HorizontalAlignment.Left, _stack.Horizontal);
			Assert.AreEqual(VerticalAlignment.Top, _stack.Vertical);
		}

		[Test]
		public void StackAlignments_TopAlignment_BottomCenter()
		{
			_stack.Alignment = StackAlignment.Top;
			_stack.Vertical = VerticalAlignment.Bottom;
			_stack.Horizontal = HorizontalAlignment.Center;

			Assert.AreEqual(StackAlignment.Top, _stack.Alignment);
			Assert.AreEqual(HorizontalAlignment.Center, _stack.Horizontal);
			Assert.AreEqual(VerticalAlignment.Top, _stack.Vertical);
		}

		[Test]
		public void StackAlignments_TopAlignment_BottomRight()
		{
			_stack.Alignment = StackAlignment.Top;
			_stack.Vertical = VerticalAlignment.Bottom;
			_stack.Horizontal = HorizontalAlignment.Right;

			Assert.AreEqual(StackAlignment.Top, _stack.Alignment);
			Assert.AreEqual(HorizontalAlignment.Right, _stack.Horizontal);
			Assert.AreEqual(VerticalAlignment.Top, _stack.Vertical);
		}

		#endregion //TopAlignment

		#region LeftAlignment

		[Test]
		public void StackAlignments_LeftAlignment_TopLeft()
		{
			_stack.Alignment = StackAlignment.Left;
			_stack.Vertical = VerticalAlignment.Top;
			_stack.Horizontal = HorizontalAlignment.Left;

			Assert.AreEqual(StackAlignment.Left, _stack.Alignment);
			Assert.AreEqual(HorizontalAlignment.Left, _stack.Horizontal);
			Assert.AreEqual(VerticalAlignment.Top, _stack.Vertical);
		}

		[Test]
		public void StackAlignments_LeftAlignment_TopCenter()
		{
			_stack.Alignment = StackAlignment.Left;
			_stack.Vertical = VerticalAlignment.Top;
			_stack.Horizontal = HorizontalAlignment.Center;

			Assert.AreEqual(StackAlignment.Left, _stack.Alignment);
			Assert.AreEqual(HorizontalAlignment.Left, _stack.Horizontal);
			Assert.AreEqual(VerticalAlignment.Top, _stack.Vertical);
		}

		[Test]
		public void StackAlignments_LeftAlignment_TopRight()
		{
			_stack.Alignment = StackAlignment.Left;
			_stack.Vertical = VerticalAlignment.Top;
			_stack.Horizontal = HorizontalAlignment.Right;

			Assert.AreEqual(StackAlignment.Left, _stack.Alignment);
			Assert.AreEqual(HorizontalAlignment.Left, _stack.Horizontal);
			Assert.AreEqual(VerticalAlignment.Top, _stack.Vertical);
		}

		[Test]
		public void StackAlignments_LeftAlignment_CenterLeft()
		{
			_stack.Alignment = StackAlignment.Left;
			_stack.Vertical = VerticalAlignment.Center;
			_stack.Horizontal = HorizontalAlignment.Left;

			Assert.AreEqual(StackAlignment.Left, _stack.Alignment);
			Assert.AreEqual(HorizontalAlignment.Left, _stack.Horizontal);
			Assert.AreEqual(VerticalAlignment.Center, _stack.Vertical);
		}

		[Test]
		public void StackAlignments_LeftAlignment_CenterCenter()
		{
			_stack.Alignment = StackAlignment.Left;
			_stack.Vertical = VerticalAlignment.Center;
			_stack.Horizontal = HorizontalAlignment.Center;

			Assert.AreEqual(StackAlignment.Left, _stack.Alignment);
			Assert.AreEqual(HorizontalAlignment.Left, _stack.Horizontal);
			Assert.AreEqual(VerticalAlignment.Center, _stack.Vertical);
		}

		[Test]
		public void StackAlignments_LeftAlignment_CenterRight()
		{
			_stack.Alignment = StackAlignment.Left;
			_stack.Vertical = VerticalAlignment.Center;
			_stack.Horizontal = HorizontalAlignment.Right;

			Assert.AreEqual(StackAlignment.Left, _stack.Alignment);
			Assert.AreEqual(HorizontalAlignment.Left, _stack.Horizontal);
			Assert.AreEqual(VerticalAlignment.Center, _stack.Vertical);
		}

		[Test]
		public void StackAlignments_LeftAlignment_BottomLeft()
		{
			_stack.Alignment = StackAlignment.Left;
			_stack.Vertical = VerticalAlignment.Bottom;
			_stack.Horizontal = HorizontalAlignment.Left;

			Assert.AreEqual(StackAlignment.Left, _stack.Alignment);
			Assert.AreEqual(HorizontalAlignment.Left, _stack.Horizontal);
			Assert.AreEqual(VerticalAlignment.Bottom, _stack.Vertical);
		}

		[Test]
		public void StackAlignments_LeftAlignment_BottomCenter()
		{
			_stack.Alignment = StackAlignment.Left;
			_stack.Vertical = VerticalAlignment.Bottom;
			_stack.Horizontal = HorizontalAlignment.Center;

			Assert.AreEqual(StackAlignment.Left, _stack.Alignment);
			Assert.AreEqual(HorizontalAlignment.Left, _stack.Horizontal);
			Assert.AreEqual(VerticalAlignment.Bottom, _stack.Vertical);
		}

		[Test]
		public void StackAlignments_LeftAlignment_BottomRight()
		{
			_stack.Alignment = StackAlignment.Left;
			_stack.Vertical = VerticalAlignment.Bottom;
			_stack.Horizontal = HorizontalAlignment.Right;

			Assert.AreEqual(StackAlignment.Left, _stack.Alignment);
			Assert.AreEqual(HorizontalAlignment.Left, _stack.Horizontal);
			Assert.AreEqual(VerticalAlignment.Bottom, _stack.Vertical);
		}

		#endregion //LeftAlignment

		#region RightAlignment

		[Test]
		public void StackAlignments_RightAlignment_TopLeft()
		{
			_stack.Alignment = StackAlignment.Right;
			_stack.Vertical = VerticalAlignment.Top;
			_stack.Horizontal = HorizontalAlignment.Left;

			Assert.AreEqual(StackAlignment.Right, _stack.Alignment);
			Assert.AreEqual(HorizontalAlignment.Right, _stack.Horizontal);
			Assert.AreEqual(VerticalAlignment.Top, _stack.Vertical);
		}

		[Test]
		public void StackAlignments_RightAlignment_TopCenter()
		{
			_stack.Alignment = StackAlignment.Right;
			_stack.Vertical = VerticalAlignment.Top;
			_stack.Horizontal = HorizontalAlignment.Center;

			Assert.AreEqual(StackAlignment.Right, _stack.Alignment);
			Assert.AreEqual(HorizontalAlignment.Right, _stack.Horizontal);
			Assert.AreEqual(VerticalAlignment.Top, _stack.Vertical);
		}

		[Test]
		public void StackAlignments_RightAlignment_TopRight()
		{
			_stack.Alignment = StackAlignment.Right;
			_stack.Vertical = VerticalAlignment.Top;
			_stack.Horizontal = HorizontalAlignment.Right;

			Assert.AreEqual(StackAlignment.Right, _stack.Alignment);
			Assert.AreEqual(HorizontalAlignment.Right, _stack.Horizontal);
			Assert.AreEqual(VerticalAlignment.Top, _stack.Vertical);
		}

		[Test]
		public void StackAlignments_RightAlignment_CenterLeft()
		{
			_stack.Alignment = StackAlignment.Right;
			_stack.Vertical = VerticalAlignment.Center;
			_stack.Horizontal = HorizontalAlignment.Left;

			Assert.AreEqual(StackAlignment.Right, _stack.Alignment);
			Assert.AreEqual(HorizontalAlignment.Right, _stack.Horizontal);
			Assert.AreEqual(VerticalAlignment.Center, _stack.Vertical);
		}

		[Test]
		public void StackAlignments_RightAlignment_CenterCenter()
		{
			_stack.Alignment = StackAlignment.Right;
			_stack.Vertical = VerticalAlignment.Center;
			_stack.Horizontal = HorizontalAlignment.Center;

			Assert.AreEqual(StackAlignment.Right, _stack.Alignment);
			Assert.AreEqual(HorizontalAlignment.Right, _stack.Horizontal);
			Assert.AreEqual(VerticalAlignment.Center, _stack.Vertical);
		}

		[Test]
		public void StackAlignments_RightAlignment_CenterRight()
		{
			_stack.Alignment = StackAlignment.Right;
			_stack.Vertical = VerticalAlignment.Center;
			_stack.Horizontal = HorizontalAlignment.Right;

			Assert.AreEqual(StackAlignment.Right, _stack.Alignment);
			Assert.AreEqual(HorizontalAlignment.Right, _stack.Horizontal);
			Assert.AreEqual(VerticalAlignment.Center, _stack.Vertical);
		}

		[Test]
		public void StackAlignments_RightAlignment_BottomLeft()
		{
			_stack.Alignment = StackAlignment.Right;
			_stack.Vertical = VerticalAlignment.Bottom;
			_stack.Horizontal = HorizontalAlignment.Left;

			Assert.AreEqual(StackAlignment.Right, _stack.Alignment);
			Assert.AreEqual(HorizontalAlignment.Right, _stack.Horizontal);
			Assert.AreEqual(VerticalAlignment.Bottom, _stack.Vertical);
		}

		[Test]
		public void StackAlignments_RightAlignment_BottomCenter()
		{
			_stack.Alignment = StackAlignment.Right;
			_stack.Vertical = VerticalAlignment.Bottom;
			_stack.Horizontal = HorizontalAlignment.Center;

			Assert.AreEqual(StackAlignment.Right, _stack.Alignment);
			Assert.AreEqual(HorizontalAlignment.Right, _stack.Horizontal);
			Assert.AreEqual(VerticalAlignment.Bottom, _stack.Vertical);
		}

		[Test]
		public void StackAlignments_RightAlignment_BottomRight()
		{
			_stack.Alignment = StackAlignment.Right;
			_stack.Vertical = VerticalAlignment.Bottom;
			_stack.Horizontal = HorizontalAlignment.Right;

			Assert.AreEqual(StackAlignment.Right, _stack.Alignment);
			Assert.AreEqual(HorizontalAlignment.Right, _stack.Horizontal);
			Assert.AreEqual(VerticalAlignment.Bottom, _stack.Vertical);
		}

		#endregion //RightAlignment

		#region BottomAlignment1

		[Test]
		public void StackAlignments_BottomAlignment1_TopLeft()
		{
			_stack.Alignment = StackAlignment.Top;
			_stack.Vertical = VerticalAlignment.Top;
			_stack.Horizontal = HorizontalAlignment.Left;
			_stack.Alignment = StackAlignment.Bottom;

			Assert.AreEqual(StackAlignment.Bottom, _stack.Alignment);
			Assert.AreEqual(HorizontalAlignment.Left, _stack.Horizontal);
			Assert.AreEqual(VerticalAlignment.Bottom, _stack.Vertical);
		}

		[Test]
		public void StackAlignments_BottomAlignment1_TopCenter()
		{
			_stack.Alignment = StackAlignment.Top;
			_stack.Vertical = VerticalAlignment.Top;
			_stack.Horizontal = HorizontalAlignment.Center;
			_stack.Alignment = StackAlignment.Bottom;

			Assert.AreEqual(StackAlignment.Bottom, _stack.Alignment);
			Assert.AreEqual(HorizontalAlignment.Center, _stack.Horizontal);
			Assert.AreEqual(VerticalAlignment.Bottom, _stack.Vertical);
		}

		[Test]
		public void StackAlignments_BottomAlignment1_TopRight()
		{
			_stack.Alignment = StackAlignment.Top;
			_stack.Vertical = VerticalAlignment.Top;
			_stack.Horizontal = HorizontalAlignment.Right;
			_stack.Alignment = StackAlignment.Bottom;

			Assert.AreEqual(StackAlignment.Bottom, _stack.Alignment);
			Assert.AreEqual(HorizontalAlignment.Right, _stack.Horizontal);
			Assert.AreEqual(VerticalAlignment.Bottom, _stack.Vertical);
		}

		[Test]
		public void StackAlignments_BottomAlignment1_CenterLeft()
		{
			_stack.Alignment = StackAlignment.Top;
			_stack.Vertical = VerticalAlignment.Center;
			_stack.Horizontal = HorizontalAlignment.Left;
			_stack.Alignment = StackAlignment.Bottom;

			Assert.AreEqual(StackAlignment.Bottom, _stack.Alignment);
			Assert.AreEqual(HorizontalAlignment.Left, _stack.Horizontal);
			Assert.AreEqual(VerticalAlignment.Bottom, _stack.Vertical);
		}

		[Test]
		public void StackAlignments_BottomAlignment1_CenterCenter()
		{
			_stack.Alignment = StackAlignment.Top;
			_stack.Vertical = VerticalAlignment.Center;
			_stack.Horizontal = HorizontalAlignment.Center;
			_stack.Alignment = StackAlignment.Bottom;

			Assert.AreEqual(StackAlignment.Bottom, _stack.Alignment);
			Assert.AreEqual(HorizontalAlignment.Center, _stack.Horizontal);
			Assert.AreEqual(VerticalAlignment.Bottom, _stack.Vertical);
		}

		[Test]
		public void StackAlignments_BottomAlignment1_CenterRight()
		{
			_stack.Alignment = StackAlignment.Top;
			_stack.Vertical = VerticalAlignment.Center;
			_stack.Horizontal = HorizontalAlignment.Right;
			_stack.Alignment = StackAlignment.Bottom;

			Assert.AreEqual(StackAlignment.Bottom, _stack.Alignment);
			Assert.AreEqual(HorizontalAlignment.Right, _stack.Horizontal);
			Assert.AreEqual(VerticalAlignment.Bottom, _stack.Vertical);
		}

		[Test]
		public void StackAlignments_BottomAlignment1_BottomLeft()
		{
			_stack.Alignment = StackAlignment.Top;
			_stack.Vertical = VerticalAlignment.Bottom;
			_stack.Horizontal = HorizontalAlignment.Left;
			_stack.Alignment = StackAlignment.Bottom;

			Assert.AreEqual(StackAlignment.Bottom, _stack.Alignment);
			Assert.AreEqual(HorizontalAlignment.Left, _stack.Horizontal);
			Assert.AreEqual(VerticalAlignment.Bottom, _stack.Vertical);
		}

		[Test]
		public void StackAlignments_BottomAlignment1_BottomCenter()
		{
			_stack.Alignment = StackAlignment.Top;
			_stack.Vertical = VerticalAlignment.Bottom;
			_stack.Horizontal = HorizontalAlignment.Center;
			_stack.Alignment = StackAlignment.Bottom;

			Assert.AreEqual(StackAlignment.Bottom, _stack.Alignment);
			Assert.AreEqual(HorizontalAlignment.Center, _stack.Horizontal);
			Assert.AreEqual(VerticalAlignment.Bottom, _stack.Vertical);
		}

		[Test]
		public void StackAlignments_BottomAlignment1_BottomRight()
		{
			_stack.Alignment = StackAlignment.Top;
			_stack.Vertical = VerticalAlignment.Bottom;
			_stack.Horizontal = HorizontalAlignment.Right;
			_stack.Alignment = StackAlignment.Bottom;

			Assert.AreEqual(StackAlignment.Bottom, _stack.Alignment);
			Assert.AreEqual(HorizontalAlignment.Right, _stack.Horizontal);
			Assert.AreEqual(VerticalAlignment.Bottom, _stack.Vertical);
		}

		#endregion //BottomAlignment
	}
}
