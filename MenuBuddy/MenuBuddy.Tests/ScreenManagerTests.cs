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
	public class ScreenManagerTests
	{
		interface ITest
		{
			string ScreenName { get; }
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
	}
}
