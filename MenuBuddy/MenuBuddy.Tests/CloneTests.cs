using FontBuddyLib;
using Microsoft.Xna.Framework;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MenuBuddy.Tests
{
	[TestFixture]
	public class CloneTests
	{
		#region Fields

		private Mock<IFontBuddy> _font;

		#endregion //Fields

		#region Setup

		[SetUp]
		public void LabelTests_Setup()
		{
			DefaultStyles.InitUnitTests();
			var menuStyles = new StyleSheet();
			DefaultStyles.Instance().MainStyle = menuStyles;

			_font = new Mock<IFontBuddy>() { CallBase = true };
			_font.Setup(x => x.MeasureString(It.IsAny<string>()))
				.Returns(new Vector2(30f, 40f));
			menuStyles.SelectedFont = _font.Object;
		}

		#endregion //Setup

		#region Absolute Layout 

		[Test]
		public void Clone_AbsoluteLayout()
		{
			var layout = new AbsoluteLayout()
			{
				Size = new Vector2(20, 30),
				Position = new Point(40, 50),
				Horizontal = HorizontalAlignment.Center,
				Vertical = VerticalAlignment.Center,
				Scale = 0.5f,
			};

			var clone = layout.DeepCopy() as AbsoluteLayout;

			Assert.AreEqual(20, layout.Size.X);
			Assert.AreEqual(30, layout.Size.Y);
			Assert.AreEqual(40, layout.Position.X);
			Assert.AreEqual(50, layout.Position.Y);
			Assert.AreEqual(HorizontalAlignment.Center, layout.Horizontal);
			Assert.AreEqual(VerticalAlignment.Center, layout.Vertical);
			Assert.AreEqual(.5f, layout.Scale);

			Assert.AreEqual(20, clone.Size.X);
			Assert.AreEqual(30, clone.Size.Y);
			Assert.AreEqual(40, clone.Position.X);
			Assert.AreEqual(50, clone.Position.Y);
			Assert.AreEqual(HorizontalAlignment.Center, clone.Horizontal);
			Assert.AreEqual(VerticalAlignment.Center, clone.Vertical);
			Assert.AreEqual(.5f, clone.Scale);
		}

		[Test]
		public void Clone_AbsoluteLayout_Move()
		{
			var layout = new AbsoluteLayout()
			{
				Size = new Vector2(20, 30),
				Position = new Point(40, 50),
				Horizontal = HorizontalAlignment.Center,
				Vertical = VerticalAlignment.Center,
				Scale = 0.5f,
			};

			var clone = layout.DeepCopy() as AbsoluteLayout;

			layout.Size = new Vector2(60, 70);
			layout.Position = new Point(80, 90);
			layout.Horizontal = HorizontalAlignment.Left;
			layout.Vertical = VerticalAlignment.Bottom;
			layout.Scale = 2.5f;

			Assert.AreEqual(20, clone.Size.X);
			Assert.AreEqual(30, clone.Size.Y);
			Assert.AreEqual(40, clone.Position.X);
			Assert.AreEqual(50, clone.Position.Y);
			Assert.AreEqual(HorizontalAlignment.Center, clone.Horizontal);
			Assert.AreEqual(VerticalAlignment.Center, clone.Vertical);
			Assert.AreEqual(.5f, clone.Scale);
		}

		#endregion //Absolute Layout 

		#region Rel Layout 

		[Test]
		public void Clone_RelativeLayout()
		{
			var layout = new RelativeLayout()
			{
				Size = new Vector2(20, 30),
				Position = new Point(40, 50),
				Horizontal = HorizontalAlignment.Center,
				Vertical = VerticalAlignment.Center,
				Scale = 0.5f,
				Layer = 1000
			};

			var clone = layout.DeepCopy() as RelativeLayout;

			Assert.AreEqual(20, layout.Size.X);
			Assert.AreEqual(30, layout.Size.Y);
			Assert.AreEqual(40, layout.Position.X);
			Assert.AreEqual(50, layout.Position.Y);
			Assert.AreEqual(HorizontalAlignment.Center, layout.Horizontal);
			Assert.AreEqual(VerticalAlignment.Center, layout.Vertical);
			Assert.AreEqual(.5f, layout.Scale);

			Assert.AreEqual(20, clone.Size.X);
			Assert.AreEqual(30, clone.Size.Y);
			Assert.AreEqual(40, clone.Position.X);
			Assert.AreEqual(50, clone.Position.Y);
			Assert.AreEqual(HorizontalAlignment.Center, clone.Horizontal);
			Assert.AreEqual(VerticalAlignment.Center, clone.Vertical);
			Assert.AreEqual(.5f, clone.Scale);
			Assert.AreEqual(1000, clone.Layer);
		}

		[Test]
		public void Clone_RelativeLayout_Move()
		{
			var layout = new RelativeLayout()
			{
				Size = new Vector2(20, 30),
				Position = new Point(40, 50),
				Horizontal = HorizontalAlignment.Center,
				Vertical = VerticalAlignment.Center,
				Scale = 0.5f,
				Layer = 1000
			};

			var clone = layout.DeepCopy() as RelativeLayout;

			layout.Size = new Vector2(60, 70);
			layout.Position = new Point(80, 90);
			layout.Horizontal = HorizontalAlignment.Left;
			layout.Vertical = VerticalAlignment.Bottom;
			layout.Scale = 2.5f;
			layout.Layer = 2000;

			Assert.AreEqual(20, clone.Size.X);
			Assert.AreEqual(30, clone.Size.Y);
			Assert.AreEqual(40, clone.Position.X);
			Assert.AreEqual(50, clone.Position.Y);
			Assert.AreEqual(HorizontalAlignment.Center, clone.Horizontal);
			Assert.AreEqual(VerticalAlignment.Center, clone.Vertical);
			Assert.AreEqual(.5f, clone.Scale);
			Assert.AreEqual(1000, clone.Layer);
		}

		#endregion //Absolute Layout 

		#region Scroll Layout 

		[Test]
		public void Clone_ScrollLayout()
		{
			var layout = new ScrollLayout()
			{
				Size = new Vector2(20, 30),
				Position = new Point(40, 50),
				Horizontal = HorizontalAlignment.Center,
				Vertical = VerticalAlignment.Center,
				Scale = 0.5f,
				ScrollPosition = new Vector2(60, 70),
				Transition = TransitionType.PopTop,
				MaxScroll = new Vector2(80, 90),
				MinScroll = new Vector2(100, 200),
			};

			var clone = layout.DeepCopy() as ScrollLayout;

			Assert.AreEqual(20, clone.Size.X);
			Assert.AreEqual(30, clone.Size.Y);
			Assert.AreEqual(40, clone.Position.X);
			Assert.AreEqual(50, clone.Position.Y);
			Assert.AreEqual(HorizontalAlignment.Center, clone.Horizontal);
			Assert.AreEqual(VerticalAlignment.Center, clone.Vertical);
			Assert.AreEqual(.5f, clone.Scale);
			Assert.AreEqual(TransitionType.PopTop, clone.Transition);
			Assert.AreEqual(100, clone.MinScroll.X);
			Assert.AreEqual(200, clone.MinScroll.Y);
			Assert.AreEqual(80, clone.MaxScroll.X);
			Assert.AreEqual(90, clone.MaxScroll.Y);
		}

		[Test]
		public void Clone_ScrollLayout_Move()
		{
			var layout = new ScrollLayout()
			{
				Size = new Vector2(20, 30),
				Position = new Point(40, 50),
				Horizontal = HorizontalAlignment.Center,
				Vertical = VerticalAlignment.Center,
				Scale = 0.5f,
				ScrollPosition = new Vector2(60, 70),
				Transition = TransitionType.PopTop,
				MaxScroll = new Vector2(80, 90),
				MinScroll = new Vector2(100, 200),
			};

			var clone = layout.DeepCopy() as ScrollLayout;

			layout.Size = new Vector2(60, 70);
			layout.Position = new Point(80, 90);
			layout.Horizontal = HorizontalAlignment.Left;
			layout.Vertical = VerticalAlignment.Bottom;
			layout.Scale = 2.5f;
			layout.Transition = TransitionType.PopLeft;
			layout.MinScroll = new Vector2(400, 500);
			layout.MaxScroll = new Vector2(600, 700);

			Assert.AreEqual(20, clone.Size.X);
			Assert.AreEqual(30, clone.Size.Y);
			Assert.AreEqual(40, clone.Position.X);
			Assert.AreEqual(50, clone.Position.Y);
			Assert.AreEqual(HorizontalAlignment.Center, clone.Horizontal);
			Assert.AreEqual(VerticalAlignment.Center, clone.Vertical);
			Assert.AreEqual(.5f, clone.Scale);
			Assert.AreEqual(TransitionType.PopTop, clone.Transition);
			Assert.AreEqual(100, clone.MinScroll.X);
			Assert.AreEqual(200, clone.MinScroll.Y);
			Assert.AreEqual(80, clone.MaxScroll.X);
			Assert.AreEqual(90, clone.MaxScroll.Y);
		}

		#endregion //Scroll Layout 

		#region Shim

		[Test]
		public void Clone_Shim()
		{
			var layout = new Shim()
			{
				Size = new Vector2(20, 30),
				Position = new Point(40, 50),
				Horizontal = HorizontalAlignment.Center,
				Vertical = VerticalAlignment.Center,
				Scale = 0.5f,
				DrawWhenInactive = false,
				Layer = 1000,
				Padding = new Vector2(100, 200)
			};

			var clone = layout.DeepCopy() as Shim;

			Assert.AreEqual(20, clone.Size.X);
			Assert.AreEqual(30, clone.Size.Y);
			Assert.AreEqual(40, clone.Position.X);
			Assert.AreEqual(50, clone.Position.Y);
			Assert.AreEqual(HorizontalAlignment.Center, clone.Horizontal);
			Assert.AreEqual(VerticalAlignment.Center, clone.Vertical);
			Assert.AreEqual(.5f, clone.Scale);
			Assert.AreEqual(false, clone.DrawWhenInactive);
			Assert.AreEqual(1000, clone.Layer);
			Assert.AreEqual(100, clone.Padding.X);
			Assert.AreEqual(200, clone.Padding.Y);
		}

		[Test]
		public void Clone_Shim2()
		{
			var layout = new Shim()
			{
				DrawWhenInactive = true,
			};

			var clone = layout.DeepCopy() as Shim;

			Assert.AreEqual(true, clone.DrawWhenInactive);
		}

		[Test]
		public void Clone_Shim_Move()
		{
			var layout = new Shim()
			{
				Size = new Vector2(20, 30),
				Position = new Point(40, 50),
				Horizontal = HorizontalAlignment.Center,
				Vertical = VerticalAlignment.Center,
				Scale = 0.5f,
				DrawWhenInactive = false,
				Layer = 1000,
				Padding = new Vector2(100, 200)
			};

			var clone = layout.DeepCopy() as Shim;

			layout.Size = new Vector2(60, 70);
			layout.Position = new Point(80, 90);
			layout.Horizontal = HorizontalAlignment.Left;
			layout.Vertical = VerticalAlignment.Bottom;
			layout.Scale = 2.5f;
			layout.DrawWhenInactive = true;
			layout.Layer = 2000;
			layout.Padding = new Vector2(300, 400);

			Assert.AreEqual(20, clone.Size.X);
			Assert.AreEqual(30, clone.Size.Y);
			Assert.AreEqual(40, clone.Position.X);
			Assert.AreEqual(50, clone.Position.Y);
			Assert.AreEqual(HorizontalAlignment.Center, clone.Horizontal);
			Assert.AreEqual(VerticalAlignment.Center, clone.Vertical);
			Assert.AreEqual(.5f, clone.Scale);
			Assert.AreEqual(false, clone.DrawWhenInactive);
			Assert.AreEqual(1000, clone.Layer);
			Assert.AreEqual(100, clone.Padding.X);
			Assert.AreEqual(200, clone.Padding.Y);
		}

		#endregion //Shim

		#region Add a thing

		[Test]
		public void AddThingAndClone1()
		{
			var layout = new AbsoluteLayout()
			{
				Size = new Vector2(20, 30),
				Position = new Point(0, 0),
				Horizontal = HorizontalAlignment.Left,
				Vertical = VerticalAlignment.Top,
				Scale = 0.5f,
			};

			var original = new Shim()
			{
				Size = new Vector2(20, 30),
				Position = new Point(40, 50),
				Horizontal = HorizontalAlignment.Center,
				Vertical = VerticalAlignment.Center,
				Scale = 0.5f,
				DrawWhenInactive = false,
				Layer = 1000,
				Padding = new Vector2(100, 200)
			};

			layout.AddItem(original);

			Assert.AreEqual(20, original.Size.X);
			Assert.AreEqual(30, original.Size.Y);
			Assert.AreEqual(40, original.Position.X);
			Assert.AreEqual(50, original.Position.Y);
			Assert.AreEqual(HorizontalAlignment.Center, original.Horizontal);
			Assert.AreEqual(VerticalAlignment.Center, original.Vertical);
			Assert.AreEqual(.5f, original.Scale);
			Assert.AreEqual(false, original.DrawWhenInactive);
			Assert.AreEqual(1000, original.Layer);
			Assert.AreEqual(100, original.Padding.X);
			Assert.AreEqual(200, original.Padding.Y);
		}

		[Test]
		public void AddThingAndClone2()
		{
			var layout = new AbsoluteLayout()
			{
				Size = new Vector2(20, 30),
				Position = new Point(0, 0),
				Horizontal = HorizontalAlignment.Left,
				Vertical = VerticalAlignment.Top,
				Scale = 0.5f,
			};

			var original = new Shim()
			{
				Size = new Vector2(20, 30),
				Position = new Point(40, 50),
				Horizontal = HorizontalAlignment.Center,
				Vertical = VerticalAlignment.Center,
				Scale = 0.5f,
				DrawWhenInactive = false,
				Layer = 1000,
				Padding = new Vector2(100, 200)
			};

			layout.AddItem(original);

			var clone = layout.DeepCopy() as AbsoluteLayout;
			Assert.NotNull(clone.Items);
		}

		[Test]
		public void AddThingAndClone3()
		{
			var layout = new AbsoluteLayout()
			{
				Size = new Vector2(20, 30),
				Position = new Point(0, 0),
				Horizontal = HorizontalAlignment.Left,
				Vertical = VerticalAlignment.Top,
				Scale = 0.5f,
			};

			var original = new Shim()
			{
				Size = new Vector2(20, 30),
				Position = new Point(40, 50),
				Horizontal = HorizontalAlignment.Center,
				Vertical = VerticalAlignment.Center,
				Scale = 0.5f,
				DrawWhenInactive = false,
				Layer = 1000,
				Padding = new Vector2(100, 200)
			};

			layout.AddItem(original);

			var clone = layout.DeepCopy() as AbsoluteLayout;
			Assert.AreEqual(1, clone.Items.Count);
		}

		[Test]
		public void AddThingAndClone4()
		{
			var layout = new AbsoluteLayout()
			{
				Size = new Vector2(20, 30),
				Position = new Point(0, 0),
				Horizontal = HorizontalAlignment.Left,
				Vertical = VerticalAlignment.Top,
				Scale = 0.5f,
			};

			var original = new Shim()
			{
				Size = new Vector2(20, 30),
				Position = new Point(40, 50),
				Horizontal = HorizontalAlignment.Center,
				Vertical = VerticalAlignment.Center,
				Scale = 0.5f,
				DrawWhenInactive = false,
				Layer = 1000,
				Padding = new Vector2(100, 200)
			};

			layout.AddItem(original);

			var clone = layout.DeepCopy() as AbsoluteLayout;
			var copiedShim = clone.Items[0] as Shim;
			Assert.NotNull(copiedShim);
		}

		[Test]
		public void AddThingAndClone5()
		{
			var layout = new AbsoluteLayout()
			{
				Size = new Vector2(20, 30),
				Position = new Point(0, 0),
				Horizontal = HorizontalAlignment.Left,
				Vertical = VerticalAlignment.Top,
				Scale = 0.5f,
			};

			var original = new Shim()
			{
				Size = new Vector2(20, 30),
				Position = new Point(40, 50),
				Horizontal = HorizontalAlignment.Center,
				Vertical = VerticalAlignment.Center,
				Scale = 0.5f,
				DrawWhenInactive = false,
				Layer = 1000,
				Padding = new Vector2(100, 200)
			};

			layout.AddItem(original);

			var clone = layout.DeepCopy() as AbsoluteLayout;

			original.Size = new Vector2(60, 70);
			original.Position = new Point(80, 90);
			original.Horizontal = HorizontalAlignment.Left;
			original.Vertical = VerticalAlignment.Bottom;
			original.Scale = 2.5f;
			original.DrawWhenInactive = true;
			original.Layer = 2000;
			original.Padding = new Vector2(300, 400);

			var copiedShim = clone.Items[0] as Shim;

			Assert.AreEqual(20, copiedShim.Size.X);
			Assert.AreEqual(30, copiedShim.Size.Y);
			Assert.AreEqual(40, copiedShim.Position.X);
			Assert.AreEqual(50, copiedShim.Position.Y);
			Assert.AreEqual(HorizontalAlignment.Center, copiedShim.Horizontal);
			Assert.AreEqual(VerticalAlignment.Center, copiedShim.Vertical);
			Assert.AreEqual(.5f, copiedShim.Scale);
			Assert.AreEqual(false, copiedShim.DrawWhenInactive);
			Assert.AreEqual(1000, copiedShim.Layer);
			Assert.AreEqual(100, copiedShim.Padding.X);
			Assert.AreEqual(200, copiedShim.Padding.Y);
		}

		#endregion //Add a thing

		#region button

		[Test]
		public void RelButtonClone_baseline()
		{
			var button = new RelativeLayoutButton()
			{
				Size = new Vector2(20, 30),
				Position = new Point(40, 50),
				Horizontal = HorizontalAlignment.Center,
				Vertical = VerticalAlignment.Center,
				Scale = 0.5f,
				Layer = 1000,
				Highlight = true,
				DrawWhenInactive = true,
				Description = "catpants!"
			};

			var original = new Shim()
			{
				Size = new Vector2(20, 30),
				Position = new Point(40, 50),
				Horizontal = HorizontalAlignment.Center,
				Vertical = VerticalAlignment.Center,
				Scale = 0.5f,
				DrawWhenInactive = false,
				Layer = 1000,
				Padding = new Vector2(100, 200)
			};

			button.AddItem(original);

			var copiedShim = button.Layout.Items[0] as Shim;

			Assert.AreEqual(20, copiedShim.Size.X);
			Assert.AreEqual(30, copiedShim.Size.Y);
			Assert.AreEqual(40, copiedShim.Position.X);
			Assert.AreEqual(49, copiedShim.Position.Y);
			Assert.AreEqual(HorizontalAlignment.Center, copiedShim.Horizontal);
			Assert.AreEqual(VerticalAlignment.Center, copiedShim.Vertical);
			Assert.AreEqual(.5f, copiedShim.Scale);
			Assert.AreEqual(false, copiedShim.DrawWhenInactive);
			Assert.AreEqual(1000, copiedShim.Layer);
			Assert.AreEqual(100, copiedShim.Padding.X);
			Assert.AreEqual(200, copiedShim.Padding.Y);
		}

		[Test]
		public void RelButtonClone_baselinemove()
		{
			var button = new RelativeLayoutButton()
			{
				Size = new Vector2(20, 30),
				Position = new Point(40, 50),
				Horizontal = HorizontalAlignment.Center,
				Vertical = VerticalAlignment.Center,
				Scale = 0.5f,
				Layer = 1000,
				Highlight = true,
				DrawWhenInactive = true,
				Description = "catpants!"
			};

			var original = new Shim()
			{
				Size = new Vector2(20, 30),
				Position = new Point(40, 50),
				Horizontal = HorizontalAlignment.Center,
				Vertical = VerticalAlignment.Center,
				Scale = 0.5f,
				DrawWhenInactive = false,
				Layer = 1000,
				Padding = new Vector2(100, 200)
			};

			button.AddItem(original);

			button.Position = new Point(50, 60);

			var copiedShim = button.Layout.Items[0] as Shim;

			Assert.AreEqual(20, copiedShim.Size.X);
			Assert.AreEqual(30, copiedShim.Size.Y);
			Assert.AreEqual(50, copiedShim.Position.X);
			Assert.AreEqual(59, copiedShim.Position.Y);
			Assert.AreEqual(HorizontalAlignment.Center, copiedShim.Horizontal);
			Assert.AreEqual(VerticalAlignment.Center, copiedShim.Vertical);
			Assert.AreEqual(.5f, copiedShim.Scale);
			Assert.AreEqual(false, copiedShim.DrawWhenInactive);
			Assert.AreEqual(1000, copiedShim.Layer);
			Assert.AreEqual(100, copiedShim.Padding.X);
			Assert.AreEqual(200, copiedShim.Padding.Y);
		}

		[Test]
		public void RelButtonClone_move()
		{
			var button = new RelativeLayoutButton()
			{
				Size = new Vector2(20, 30),
				Position = new Point(40, 50),
				Horizontal = HorizontalAlignment.Center,
				Vertical = VerticalAlignment.Center,
				Scale = 0.5f,
				Layer = 1000,
				Highlight = true,
				DrawWhenInactive = true,
				Description = "catpants!"
			};

			var original = new Shim()
			{
				Size = new Vector2(20, 30),
				Position = new Point(40, 50),
				Horizontal = HorizontalAlignment.Center,
				Vertical = VerticalAlignment.Center,
				Scale = 0.5f,
				DrawWhenInactive = false,
				Layer = 1000,
				Padding = new Vector2(100, 200)
			};

			button.AddItem(original);

			var buttonClone = button.DeepCopy() as RelativeLayoutButton;
			buttonClone.Position = new Point(50, 60);

			Assert.AreEqual(20, original.Size.X);
			Assert.AreEqual(30, original.Size.Y);
			Assert.AreEqual(40, original.Position.X);
			Assert.AreEqual(49, original.Position.Y);
			Assert.AreEqual(HorizontalAlignment.Center, original.Horizontal);
			Assert.AreEqual(VerticalAlignment.Center, original.Vertical);
			Assert.AreEqual(.5f, original.Scale);
			Assert.AreEqual(false, original.DrawWhenInactive);
			Assert.AreEqual(1000, original.Layer);
			Assert.AreEqual(100, original.Padding.X);
			Assert.AreEqual(200, original.Padding.Y);
		}

		[Test]
		public void RelButtonClone2()
		{
			var button = new RelativeLayoutButton()
			{
				Size = new Vector2(20, 30),
				Position = new Point(40, 50),
				Horizontal = HorizontalAlignment.Center,
				Vertical = VerticalAlignment.Center,
				Scale = 0.5f,
				Layer = 1000,
				Highlight = true,
				DrawWhenInactive = true,
				Description = "catpants!"
			};

			var original = new Shim()
			{
				Size = new Vector2(20, 30),
				Position = new Point(40, 50),
				Horizontal = HorizontalAlignment.Center,
				Vertical = VerticalAlignment.Center,
				Scale = 0.5f,
				DrawWhenInactive = false,
				Layer = 1000,
				Padding = new Vector2(100, 200)
			};

			button.AddItem(original);

			var clone = button.DeepCopy() as RelativeLayoutButton;

			original.Size = new Vector2(60, 70);
			original.Position = new Point(80, 90);
			original.Horizontal = HorizontalAlignment.Left;
			original.Vertical = VerticalAlignment.Bottom;
			original.Scale = 2.5f;
			original.DrawWhenInactive = true;
			original.Layer = 2000;
			original.Padding = new Vector2(300, 400);

			var copiedShim = clone.Layout.Items[0] as Shim;

			Assert.AreEqual(20, copiedShim.Size.X);
			Assert.AreEqual(30, copiedShim.Size.Y);
			Assert.AreEqual(40, copiedShim.Position.X);
			Assert.AreEqual(49, copiedShim.Position.Y);
			Assert.AreEqual(HorizontalAlignment.Center, copiedShim.Horizontal);
			Assert.AreEqual(VerticalAlignment.Center, copiedShim.Vertical);
			Assert.AreEqual(.5f, copiedShim.Scale);
			Assert.AreEqual(false, copiedShim.DrawWhenInactive);
			Assert.AreEqual(1000, copiedShim.Layer);
			Assert.AreEqual(100, copiedShim.Padding.X);
			Assert.AreEqual(200, copiedShim.Padding.Y);
		}

		#endregion //button

		#region DropdownItem

		[Test]
		public void CloneDropdownItem()
		{
			var drop = new Dropdown<string>();
			drop.Vertical = VerticalAlignment.Center;
			drop.Horizontal = HorizontalAlignment.Center;
			drop.Size = new Vector2(350, 128);
			var dropitem = new DropdownItem<string>("catpants", drop)
			{
				Vertical = VerticalAlignment.Center,
				Horizontal = HorizontalAlignment.Center,
				Size = new Vector2(350, 128)
			};

			var label = new Label("catpants")
			{
				Vertical = VerticalAlignment.Center,
				Horizontal = HorizontalAlignment.Center
			};

			dropitem.AddItem(label);

			//layout.AddItem(original);

			//Assert.AreEqual(20, original.Size.X);
			//Assert.AreEqual(30, original.Size.Y);
			//Assert.AreEqual(40, original.Position.X);
			//Assert.AreEqual(50, original.Position.Y);
			//Assert.AreEqual(HorizontalAlignment.Center, original.Horizontal);
			//Assert.AreEqual(VerticalAlignment.Center, original.Vertical);
			//Assert.AreEqual(.5f, original.Scale);
			//Assert.AreEqual(false, original.DrawWhenInactive);
			//Assert.AreEqual(1000, original.Layer);
			//Assert.AreEqual(100, original.Padding.X);
			//Assert.AreEqual(200, original.Padding.Y);
		}

		#endregion //Dropdown
	}
}
