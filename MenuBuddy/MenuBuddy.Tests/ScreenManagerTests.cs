using NUnit.Framework;
using Shouldly;
using System.Linq;

namespace MenuBuddy.Tests
{
	[TestFixture]
	public class ScreenManagerTests
	{
		interface ITest : IScreen
		{
		}

		class Screen1 : Screen, ITest
		{
			public Screen1() : base("First")
			{
			}
		}

		class Screen2 : Screen, ITest
		{
			public Screen2() : base("Second")
			{
			}
		}

		class Screen3 : Screen
		{
			public Screen3() : base("Third")
			{
			}
		}

		[Test]
		public void FindScreens()
		{
			//create teh screenstack
			var screenStack = new ScreenStack();

			//add three test screens
			screenStack.AddScreen(new Screen1());
			screenStack.AddScreen(new Screen2());
			screenStack.AddScreen(new Screen3());

			var screens = screenStack.FindScreens<ITest>();

			Assert.AreEqual(2, screens.Count());
			Assert.AreEqual(1, screens.Where(x => x.ScreenName == "First").Count());
			Assert.AreEqual(1, screens.Where(x => x.ScreenName == "Second").Count());
			Assert.AreEqual(0, screens.Where(x => x.ScreenName == "Third").Count());
		}

		[Test]
		public void ClearToScreens()
		{
			//create teh screenstack
			var screenStack = new ScreenStack();

			//add three test screens
			var screen1 = new Screen1();
			screenStack.AddScreen(screen1);
			var screen2 = new Screen2();
			screenStack.AddScreen(screen2);
			var screen3 = new Screen3();
			screenStack.AddScreen(screen3);

			screenStack.PopToScreen<Screen1>();

			screen1.IsExiting.ShouldBeFalse();
			screen2.IsExiting.ShouldBeTrue();
			screen3.IsExiting.ShouldBeTrue();
		}

		[Test]
		public void ClearToScreens2()
		{
			//create teh screenstack
			var screenStack = new ScreenStack();

			//add three test screens
			var screen1 = new Screen1();
			screenStack.AddScreen(screen1);
			var screen2 = new Screen2();
			screenStack.AddScreen(screen2);
			var screen3 = new Screen3();
			screenStack.AddScreen(screen3);

			screenStack.PopToScreen<Screen2>();

			screen1.IsExiting.ShouldBeFalse();
			screen2.IsExiting.ShouldBeFalse();
			screen3.IsExiting.ShouldBeTrue();
		}

		[Test]
		public void AddedOrder()
		{
			//create teh screenstack
			var screenStack = new ScreenStack();

			//add three test screens
			var screen1 = new Screen1();
			screenStack.AddScreen(screen1);
			var screen2 = new Screen2();
			screenStack.AddScreen(screen2);
			var screen3 = new Screen3();
			screenStack.AddScreen(screen3);

			var screens = screenStack.GetScreens();
			screens[0].ShouldBeOfType(typeof(Screen1));
			screens[1].ShouldBeOfType(typeof(Screen2));
			screens[2].ShouldBeOfType(typeof(Screen3));
		}

		[Test]
		public void SortedByLayer()
		{
			//create teh screenstack
			var screenStack = new ScreenStack();

			//add three test screens
			var screen1 = new Screen1()
			{
				Layer = 3
			};
			screenStack.AddScreen(screen1);
			var screen2 = new Screen2()
			{
				Layer = 2
			};
			screenStack.AddScreen(screen2);
			var screen3 = new Screen3()
			{
				Layer = 1
			};
			screenStack.AddScreen(screen3);

			var screens = screenStack.GetScreens();
			screens[0].ShouldBeOfType(typeof(Screen3));
			screens[1].ShouldBeOfType(typeof(Screen2));
			screens[2].ShouldBeOfType(typeof(Screen1));
		}

		[Test]
		public void SortedByLayer_EmptyLayer()
		{
			//create teh screenstack
			var screenStack = new ScreenStack();

			//add three test screens
			var screen1 = new Screen1()
			{
				Layer = 3
			};
			screenStack.AddScreen(screen1);
			var screen2 = new Screen2()
			{
				Layer = 2
			};
			screenStack.AddScreen(screen2);
			var screen3 = new Screen3();
			screenStack.AddScreen(screen3);

			var screens = screenStack.GetScreens();
			
			screens[0].ShouldBeOfType(typeof(Screen2));
			screens[1].ShouldBeOfType(typeof(Screen1));
			screens[2].ShouldBeOfType(typeof(Screen3));
		}

		[Test]
		public void SortedBySublayer()
		{
			//create teh screenstack
			var screenStack = new ScreenStack();

			//add three test screens
			var screen1 = new Screen1()
			{
				Layer = 3
			};
			screenStack.AddScreen(screen1);
			var screen2 = new Screen2()
			{
				Layer = 1
			};
			screenStack.AddScreen(screen2);
			var screen3 = new Screen3()
			{
				Layer = 1
			};
			screenStack.AddScreen(screen3);

			var screens = screenStack.GetScreens();
			screens[0].ShouldBeOfType(typeof(Screen2));
			screens[1].ShouldBeOfType(typeof(Screen3));
			screens[2].ShouldBeOfType(typeof(Screen1));
		}

		[Test]
		public void SortedBySublayer_AllEmpty()
		{
			//create teh screenstack
			var screenStack = new ScreenStack();

			//add three test screens
			var screen1 = new Screen1();
			screenStack.AddScreen(screen1);
			var screen2 = new Screen2();
			screenStack.AddScreen(screen2);
			var screen3 = new Screen3();
			screenStack.AddScreen(screen3);

			var screens = screenStack.GetScreens();
			screens[0].ShouldBeOfType(typeof(Screen1));
			screens[1].ShouldBeOfType(typeof(Screen2));
			screens[2].ShouldBeOfType(typeof(Screen3));
		}
	}
}
