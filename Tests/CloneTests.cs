using FontBuddyLib;
using Microsoft.Xna.Framework;
using Moq;
using NUnit.Framework;
using ResolutionBuddy;
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
			var resolution = new Mock<IResolution>();
			resolution.Setup(x => x.ScreenArea).Returns(new Rectangle(0, 0, 1280, 720));
			Resolution.Init(resolution.Object);

			_font = new Mock<IFontBuddy>() { CallBase = true };
			_font.Setup(x => x.MeasureString(It.IsAny<string>()))
				.Returns(new Vector2(30f, 40f));
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

			layout.Size.X.ShouldBe(20);
			layout.Size.Y.ShouldBe(30);
			layout.Position.X.ShouldBe(40);
			layout.Position.Y.ShouldBe(50);
			layout.Horizontal.ShouldBe(HorizontalAlignment.Center);
			layout.Vertical.ShouldBe(VerticalAlignment.Center);
			layout.Scale.ShouldBe(.5f);

			clone.Size.X.ShouldBe(20);
			clone.Size.Y.ShouldBe(30);
			clone.Position.X.ShouldBe(40);
			clone.Position.Y.ShouldBe(50);
			clone.Horizontal.ShouldBe(HorizontalAlignment.Center);
			clone.Vertical.ShouldBe(VerticalAlignment.Center);
			clone.Scale.ShouldBe(.5f);
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

			clone.Size.X.ShouldBe(20);
			clone.Size.Y.ShouldBe(30);
			clone.Position.X.ShouldBe(40);
			clone.Position.Y.ShouldBe(50);
			clone.Horizontal.ShouldBe(HorizontalAlignment.Center);
			clone.Vertical.ShouldBe(VerticalAlignment.Center);
			clone.Scale.ShouldBe(.5f);
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

			layout.Size.X.ShouldBe(20);
			layout.Size.Y.ShouldBe(30);
			layout.Position.X.ShouldBe(40);
			layout.Position.Y.ShouldBe(50);
			layout.Horizontal.ShouldBe(HorizontalAlignment.Center);
			layout.Vertical.ShouldBe(VerticalAlignment.Center);
			layout.Scale.ShouldBe(.5f);

			clone.Size.X.ShouldBe(20);
			clone.Size.Y.ShouldBe(30);
			clone.Position.X.ShouldBe(40);
			clone.Position.Y.ShouldBe(50);
			clone.Horizontal.ShouldBe(HorizontalAlignment.Center);
			clone.Vertical.ShouldBe(VerticalAlignment.Center);
			clone.Scale.ShouldBe(.5f);
			clone.Layer.ShouldBe(1000);
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

			clone.Size.X.ShouldBe(20);
			clone.Size.Y.ShouldBe(30);
			clone.Position.X.ShouldBe(40);
			clone.Position.Y.ShouldBe(50);
			clone.Horizontal.ShouldBe(HorizontalAlignment.Center);
			clone.Vertical.ShouldBe(VerticalAlignment.Center);
			clone.Scale.ShouldBe(.5f);
			clone.Layer.ShouldBe(1000);
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
				TransitionObject = new WipeTransitionObject(TransitionWipeType.PopTop),
				MaxScroll = new Vector2(80, 90),
				MinScroll = new Vector2(100, 200),
			};

			var clone = layout.DeepCopy() as ScrollLayout;

			clone.Size.X.ShouldBe(20);
			clone.Size.Y.ShouldBe(30);
			clone.Position.X.ShouldBe(40);
			clone.Position.Y.ShouldBe(50);
			clone.Horizontal.ShouldBe(HorizontalAlignment.Center);
			clone.Vertical.ShouldBe(VerticalAlignment.Center);
			clone.Scale.ShouldBe(.5f);
			(clone.TransitionObject as WipeTransitionObject).WipeType.ShouldBe(TransitionWipeType.PopTop);
			clone.MinScroll.X.ShouldBe(100);
			clone.MinScroll.Y.ShouldBe(200);
			clone.MaxScroll.X.ShouldBe(80);
			clone.MaxScroll.Y.ShouldBe(90);
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
				TransitionObject = new WipeTransitionObject(TransitionWipeType.PopTop),
				MaxScroll = new Vector2(80, 90),
				MinScroll = new Vector2(100, 200),
			};

			var clone = layout.DeepCopy() as ScrollLayout;

			layout.Size = new Vector2(60, 70);
			layout.Position = new Point(80, 90);
			layout.Horizontal = HorizontalAlignment.Left;
			layout.Vertical = VerticalAlignment.Bottom;
			layout.Scale = 2.5f;
			layout.TransitionObject = new WipeTransitionObject(TransitionWipeType.PopLeft);
			layout.MinScroll = new Vector2(400, 500);
			layout.MaxScroll = new Vector2(600, 700);

			clone.Size.X.ShouldBe(20);
			clone.Size.Y.ShouldBe(30);
			clone.Position.X.ShouldBe(40);
			clone.Position.Y.ShouldBe(50);
			clone.Horizontal.ShouldBe(HorizontalAlignment.Center);
			clone.Vertical.ShouldBe(VerticalAlignment.Center);
			clone.Scale.ShouldBe(.5f);
			(clone.TransitionObject as WipeTransitionObject).WipeType.ShouldBe(TransitionWipeType.PopTop);
			clone.MinScroll.X.ShouldBe(100);
			clone.MinScroll.Y.ShouldBe(200);
			clone.MaxScroll.X.ShouldBe(80);
			clone.MaxScroll.Y.ShouldBe(90);
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
			};

			var clone = layout.DeepCopy() as Shim;

			clone.Size.X.ShouldBe(20);
			clone.Size.Y.ShouldBe(30);
			clone.Position.X.ShouldBe(40);
			clone.Position.Y.ShouldBe(50);
			clone.Horizontal.ShouldBe(HorizontalAlignment.Center);
			clone.Vertical.ShouldBe(VerticalAlignment.Center);
			clone.Scale.ShouldBe(.5f);
			clone.DrawWhenInactive.ShouldBe(false);
			clone.Layer.ShouldBe(1000);
		}

		[Test]
		public void Clone_Shim2()
		{
			var layout = new Shim()
			{
				DrawWhenInactive = true,
			};

			var clone = layout.DeepCopy() as Shim;

			clone.DrawWhenInactive.ShouldBe(true);
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
			};

			var clone = layout.DeepCopy() as Shim;

			layout.Size = new Vector2(60, 70);
			layout.Position = new Point(80, 90);
			layout.Horizontal = HorizontalAlignment.Left;
			layout.Vertical = VerticalAlignment.Bottom;
			layout.Scale = 2.5f;
			layout.DrawWhenInactive = true;
			layout.Layer = 2000;

			clone.Size.X.ShouldBe(20);
			clone.Size.Y.ShouldBe(30);
			clone.Position.X.ShouldBe(40);
			clone.Position.Y.ShouldBe(50);
			clone.Horizontal.ShouldBe(HorizontalAlignment.Center);
			clone.Vertical.ShouldBe(VerticalAlignment.Center);
			clone.Scale.ShouldBe(.5f);
			clone.DrawWhenInactive.ShouldBe(false);
			clone.Layer.ShouldBe(1000);
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
			};

			layout.AddItem(original);

			original.Size.X.ShouldBe(20);
			original.Size.Y.ShouldBe(30);
			original.Position.X.ShouldBe(40);
			original.Position.Y.ShouldBe(50);
			original.Horizontal.ShouldBe(HorizontalAlignment.Center);
			original.Vertical.ShouldBe(VerticalAlignment.Center);
			original.Scale.ShouldBe(.5f);
			original.DrawWhenInactive.ShouldBe(false);
			original.Layer.ShouldBe(1000);
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
			};

			layout.AddItem(original);

			var clone = layout.DeepCopy() as AbsoluteLayout;
			clone.Items.ShouldNotBeNull();
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
			};

			layout.AddItem(original);

			var clone = layout.DeepCopy() as AbsoluteLayout;
			clone.Items.Count.ShouldBe(1);
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
			};

			layout.AddItem(original);

			var clone = layout.DeepCopy() as AbsoluteLayout;
			var copiedShim = clone.Items[0] as Shim;
			copiedShim.ShouldNotBeNull();
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

			var copiedShim = clone.Items[0] as Shim;

			copiedShim.Size.X.ShouldBe(20);
			copiedShim.Size.Y.ShouldBe(30);
			copiedShim.Position.X.ShouldBe(40);
			copiedShim.Position.Y.ShouldBe(50);
			copiedShim.Horizontal.ShouldBe(HorizontalAlignment.Center);
			copiedShim.Vertical.ShouldBe(VerticalAlignment.Center);
			copiedShim.Scale.ShouldBe(.5f);
			copiedShim.DrawWhenInactive.ShouldBe(false);
			copiedShim.Layer.ShouldBe(1000);
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
				IsHighlighted = true,
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
			};

			button.AddItem(original);

			var copiedShim = button.Layout.Items[0] as Shim;

			copiedShim.Size.X.ShouldBe(20);
			copiedShim.Size.Y.ShouldBe(30);
			copiedShim.Position.X.ShouldBe(40);
			copiedShim.Position.Y.ShouldBe(49);
			copiedShim.Horizontal.ShouldBe(HorizontalAlignment.Center);
			copiedShim.Vertical.ShouldBe(VerticalAlignment.Center);
			copiedShim.Scale.ShouldBe(.5f);
			copiedShim.DrawWhenInactive.ShouldBe(false);
			copiedShim.Layer.ShouldBe(1000);
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
				IsHighlighted = true,
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
			};

			button.AddItem(original);

			button.Position = new Point(50, 60);

			var copiedShim = button.Layout.Items[0] as Shim;

			copiedShim.Size.X.ShouldBe(20);
			copiedShim.Size.Y.ShouldBe(30);
			copiedShim.Position.X.ShouldBe(50);
			copiedShim.Position.Y.ShouldBe(59);
			copiedShim.Horizontal.ShouldBe(HorizontalAlignment.Center);
			copiedShim.Vertical.ShouldBe(VerticalAlignment.Center);
			copiedShim.Scale.ShouldBe(.5f);
			copiedShim.DrawWhenInactive.ShouldBe(false);
			copiedShim.Layer.ShouldBe(1000);
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
				IsHighlighted = true,
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
			};

			button.AddItem(original);

			var buttonClone = button.DeepCopy() as RelativeLayoutButton;
			buttonClone.Position = new Point(50, 60);

			original.Size.X.ShouldBe(20);
			original.Size.Y.ShouldBe(30);
			original.Position.X.ShouldBe(40);
			original.Position.Y.ShouldBe(49);
			original.Horizontal.ShouldBe(HorizontalAlignment.Center);
			original.Vertical.ShouldBe(VerticalAlignment.Center);
			original.Scale.ShouldBe(.5f);
			original.DrawWhenInactive.ShouldBe(false);
			original.Layer.ShouldBe(1000);
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
				IsHighlighted = true,
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

			var copiedShim = clone.Layout.Items[0] as Shim;

			copiedShim.Size.X.ShouldBe(20);
			copiedShim.Size.Y.ShouldBe(30);
			copiedShim.Position.X.ShouldBe(40);
			copiedShim.Position.Y.ShouldBe(49);
			copiedShim.Horizontal.ShouldBe(HorizontalAlignment.Center);
			copiedShim.Vertical.ShouldBe(VerticalAlignment.Center);
			copiedShim.Scale.ShouldBe(.5f);
			copiedShim.DrawWhenInactive.ShouldBe(false);
			copiedShim.Layer.ShouldBe(1000);
		}

		#endregion //button

		#region DropdownItem

		[Test]
		public void CloneDropdownItem()
		{
			var screen = new WidgetScreen("test screen");
			var drop = new Dropdown<string>(screen);
			drop.Vertical = VerticalAlignment.Center;
			drop.Horizontal = HorizontalAlignment.Center;
			drop.Size = new Vector2(350, 128);
			var dropitem = new DropdownItem<string>("catpants", drop)
			{
				Vertical = VerticalAlignment.Center,
				Horizontal = HorizontalAlignment.Center,
				Size = new Vector2(350, 128)
			};

			var label = new Label("catpants", _font.Object)
			{
				Vertical = VerticalAlignment.Center,
				Horizontal = HorizontalAlignment.Center
			};

			dropitem.AddItem(label);

			//layout.AddItem(original);

			//original.Size.X.ShouldBe(20);
			//original.Size.Y.ShouldBe(30);
			//original.Position.X.ShouldBe(40);
			//original.Position.Y.ShouldBe(50);
			//original.Horizontal.ShouldBe(HorizontalAlignment.Center);
			//original.Vertical.ShouldBe(VerticalAlignment.Center);
			//original.Scale.ShouldBe(.5f);
			//original.DrawWhenInactive.ShouldBe(false);
			//original.Layer.ShouldBe(1000);
			//original.Padding.X.ShouldBe(100);
			//original.Padding.Y.ShouldBe(200);
		}

		#endregion //Dropdown
	}
}
