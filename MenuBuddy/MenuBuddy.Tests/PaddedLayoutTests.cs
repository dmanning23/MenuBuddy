using Microsoft.Xna.Framework;
using NUnit.Framework;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MenuBuddy.Tests
{
	[TestFixture]
	public class PaddedLayoutTests
	{
		#region Fields

		private Shim _shim;

		#endregion //Fields

		#region Setup

		[SetUp]
		public void Setup()
		{
			StyleSheet.InitUnitTests();
			_shim = new Shim(10, 20);
		}

		#endregion //Setup

		#region Tests

		[Test]
		public void Default()
		{
			var layout = new PaddedLayout(0, 0, 0, 0, _shim);
			layout.Left.ShouldBe(0);
			layout.Right.ShouldBe(0);
			layout.Top.ShouldBe(0);
			layout.Bottom.ShouldBe(0);
			layout.Rect.Left.ShouldBe(0);
			layout.Rect.Right.ShouldBe(10);
			layout.Rect.Top.ShouldBe(0);
			layout.Rect.Bottom.ShouldBe(20);
			layout.Rect.Width.ShouldBe(10);
			layout.Rect.Height.ShouldBe(20);

			_shim.Rect.Left.ShouldBe(0);
			_shim.Rect.Right.ShouldBe(10);
			_shim.Rect.Top.ShouldBe(0);
			_shim.Rect.Bottom.ShouldBe(20);
			_shim.Rect.Width.ShouldBe(10);
			_shim.Rect.Height.ShouldBe(20);
		}

		[Test]
		public void Position()
		{
			var layout = new PaddedLayout(0, 0, 0, 0, _shim);
			layout.Position = new Point(10, 20);
			layout.Left.ShouldBe(0);
			layout.Right.ShouldBe(0);
			layout.Top.ShouldBe(0);
			layout.Bottom.ShouldBe(0);
			layout.Rect.Left.ShouldBe(10);
			layout.Rect.Right.ShouldBe(20);
			layout.Rect.Top.ShouldBe(20);
			layout.Rect.Bottom.ShouldBe(40);
			layout.Rect.Width.ShouldBe(10);
			layout.Rect.Height.ShouldBe(20);

			_shim.Rect.Left.ShouldBe(10);
			_shim.Rect.Right.ShouldBe(20);
			_shim.Rect.Top.ShouldBe(20);
			_shim.Rect.Bottom.ShouldBe(40);
			_shim.Rect.Width.ShouldBe(10);
			_shim.Rect.Height.ShouldBe(20);
		}

		[Test]
		public void Left()
		{
			var layout = new PaddedLayout(10, 0, 0, 0, _shim);
			layout.Position = new Point(10, 20);
			layout.Left.ShouldBe(10);
			layout.Right.ShouldBe(0);
			layout.Top.ShouldBe(0);
			layout.Bottom.ShouldBe(0);
			layout.Rect.Left.ShouldBe(10);
			layout.Rect.Right.ShouldBe(30);
			layout.Rect.Top.ShouldBe(20);
			layout.Rect.Bottom.ShouldBe(40);
			layout.Rect.Width.ShouldBe(20);
			layout.Rect.Height.ShouldBe(20);

			_shim.Rect.Left.ShouldBe(20);
			_shim.Rect.Right.ShouldBe(30);
			_shim.Rect.Top.ShouldBe(20);
			_shim.Rect.Bottom.ShouldBe(40);
			_shim.Rect.Width.ShouldBe(10);
			_shim.Rect.Height.ShouldBe(20);
		}

		[Test]
		public void Right()
		{
			var layout = new PaddedLayout(0, 10, 0, 0, _shim);
			layout.Position = new Point(10, 20);
			layout.Left.ShouldBe(0);
			layout.Right.ShouldBe(10);
			layout.Top.ShouldBe(0);
			layout.Bottom.ShouldBe(0);
			layout.Rect.Left.ShouldBe(10);
			layout.Rect.Right.ShouldBe(30);
			layout.Rect.Top.ShouldBe(20);
			layout.Rect.Bottom.ShouldBe(40);
			layout.Rect.Width.ShouldBe(20);
			layout.Rect.Height.ShouldBe(20);

			_shim.Rect.Left.ShouldBe(10);
			_shim.Rect.Right.ShouldBe(20);
			_shim.Rect.Top.ShouldBe(20);
			_shim.Rect.Bottom.ShouldBe(40);
			_shim.Rect.Width.ShouldBe(10);
			_shim.Rect.Height.ShouldBe(20);
		}

		[Test]
		public void Top()
		{
			var layout = new PaddedLayout(0, 0, 10, 0, _shim);
			layout.Position = new Point(10, 20);
			layout.Left.ShouldBe(0);
			layout.Right.ShouldBe(0);
			layout.Top.ShouldBe(10);
			layout.Bottom.ShouldBe(0);
			layout.Rect.Left.ShouldBe(10);
			layout.Rect.Right.ShouldBe(20);
			layout.Rect.Top.ShouldBe(20);
			layout.Rect.Bottom.ShouldBe(50);
			layout.Rect.Width.ShouldBe(10);
			layout.Rect.Height.ShouldBe(30);

			_shim.Rect.Left.ShouldBe(10);
			_shim.Rect.Right.ShouldBe(20);
			_shim.Rect.Top.ShouldBe(30);
			_shim.Rect.Bottom.ShouldBe(50);
			_shim.Rect.Width.ShouldBe(10);
			_shim.Rect.Height.ShouldBe(20);
		}

		[Test]
		public void Bottom()
		{
			var layout = new PaddedLayout(0, 0, 0, 10, _shim);
			layout.Position = new Point(10, 20);
			layout.Left.ShouldBe(0);
			layout.Right.ShouldBe(0);
			layout.Top.ShouldBe(0);
			layout.Bottom.ShouldBe(10);
			layout.Rect.Left.ShouldBe(10);
			layout.Rect.Right.ShouldBe(20);
			layout.Rect.Top.ShouldBe(20);
			layout.Rect.Bottom.ShouldBe(50);
			layout.Rect.Width.ShouldBe(10);
			layout.Rect.Height.ShouldBe(30);

			_shim.Rect.Left.ShouldBe(10);
			_shim.Rect.Right.ShouldBe(20);
			_shim.Rect.Top.ShouldBe(20);
			_shim.Rect.Bottom.ShouldBe(40);
			_shim.Rect.Width.ShouldBe(10);
			_shim.Rect.Height.ShouldBe(20);
		}

		[Test]
		public void All()
		{
			var layout = new PaddedLayout(10, 10, 10, 10, _shim);
			layout.Position = new Point(10, 20);
			layout.Left.ShouldBe(10);
			layout.Right.ShouldBe(10);
			layout.Top.ShouldBe(10);
			layout.Bottom.ShouldBe(10);
			layout.Rect.Left.ShouldBe(10);
			layout.Rect.Right.ShouldBe(40);
			layout.Rect.Top.ShouldBe(20);
			layout.Rect.Bottom.ShouldBe(60);
			layout.Rect.Width.ShouldBe(30);
			layout.Rect.Height.ShouldBe(40);

			_shim.Rect.Left.ShouldBe(20);
			_shim.Rect.Right.ShouldBe(30);
			_shim.Rect.Top.ShouldBe(30);
			_shim.Rect.Bottom.ShouldBe(50);
			_shim.Rect.Width.ShouldBe(10);
			_shim.Rect.Height.ShouldBe(20);
		}

		[Test]
		public void Scale()
		{
			var layout = new PaddedLayout(10, 10, 10, 10, _shim);
			layout.Position = new Point(10, 20);
			layout.Scale = 2f;
			layout.Left.ShouldBe(10);
			layout.Right.ShouldBe(10);
			layout.Top.ShouldBe(10);
			layout.Bottom.ShouldBe(10);
			layout.Rect.Left.ShouldBe(10);
			layout.Rect.Right.ShouldBe(70);
			layout.Rect.Top.ShouldBe(20);
			layout.Rect.Bottom.ShouldBe(100);
			layout.Rect.Width.ShouldBe(60);
			layout.Rect.Height.ShouldBe(80);

			_shim.Rect.Left.ShouldBe(30);
			_shim.Rect.Right.ShouldBe(50);
			_shim.Rect.Top.ShouldBe(40);
			_shim.Rect.Bottom.ShouldBe(80);
			_shim.Rect.Width.ShouldBe(20);
			_shim.Rect.Height.ShouldBe(40);
		}

		[Test]
		public void Centered()
		{
			var layout = new PaddedLayout(10, 10, 10, 10, _shim);
			layout.Position = new Point(10, 20);
			layout.Scale = 2f;
			layout.Horizontal = HorizontalAlignment.Center;
			layout.Vertical = VerticalAlignment.Center;
			layout.Left.ShouldBe(10);
			layout.Right.ShouldBe(10);
			layout.Top.ShouldBe(10);
			layout.Bottom.ShouldBe(10);
			layout.Rect.Left.ShouldBe(-20);
			layout.Rect.Right.ShouldBe(40);
			layout.Rect.Top.ShouldBe(-20);
			layout.Rect.Bottom.ShouldBe(60);
			layout.Rect.Width.ShouldBe(60);
			layout.Rect.Height.ShouldBe(80);

			_shim.Rect.Left.ShouldBe(0);
			_shim.Rect.Right.ShouldBe(20);
			_shim.Rect.Top.ShouldBe(0);
			_shim.Rect.Bottom.ShouldBe(40);
			_shim.Rect.Width.ShouldBe(20);
			_shim.Rect.Height.ShouldBe(40);
		}

		#endregion //Tests
	}
}
