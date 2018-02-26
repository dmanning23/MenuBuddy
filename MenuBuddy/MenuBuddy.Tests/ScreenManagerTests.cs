using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shouldly;

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
			screenStack.Screens.Add(new Screen1());
			screenStack.Screens.Add(new Screen2());
			screenStack.Screens.Add(new Screen3());

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
			screenStack.Screens.Add(screen1);
			var screen2 = new Screen2();
			screenStack.Screens.Add(screen2);
			var screen3 = new Screen3();
			screenStack.Screens.Add(screen3);

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
			screenStack.Screens.Add(screen1);
			var screen2 = new Screen2();
			screenStack.Screens.Add(screen2);
			var screen3 = new Screen3();
			screenStack.Screens.Add(screen3);

			screenStack.PopToScreen<Screen2>();

			screen1.IsExiting.ShouldBeFalse();
			screen2.IsExiting.ShouldBeFalse();
			screen3.IsExiting.ShouldBeTrue();
		}
	}
}
