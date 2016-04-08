using NUnit.Framework;

namespace MenuBuddy.Tests
{
	[TestFixture]
	public class StyleItemTests
	{
		[Test]
		public void Default()
		{
			var item = new BoolStyleItem(StyleItemType.HasOutline, true);
			Assert.AreEqual(true, item.Style);
			Assert.AreEqual(StyleItemType.HasOutline, item.StyleType);
		}

		[Test]
		public void ChangeStyle()
		{
			var item = new BoolStyleItem(StyleItemType.HasOutline, true);
			item.Style = false;
            Assert.AreEqual(false, item.Style);
			Assert.AreEqual(StyleItemType.HasOutline, item.StyleType);
		}

		[Test]
		public void Nested_Default()
		{
			var item = new BoolStyleItem(StyleItemType.HasOutline, true);
			var item2 = new BoolStyleItem(item);
			Assert.AreEqual(true, item2.Style);
			Assert.AreEqual(StyleItemType.HasOutline, item2.StyleType);
		}

		[Test]
		public void Nested_Default2()
		{
			var item = new BoolStyleItem(StyleItemType.HasOutline, false);
			var item2 = new BoolStyleItem(item);
			Assert.AreEqual(false, item2.Style);
			Assert.AreEqual(StyleItemType.HasOutline, item2.StyleType);
		}

		[Test]
		public void Nested_DoesntChange()
		{
			var item = new BoolStyleItem(StyleItemType.HasOutline, false);
			var item2 = new BoolStyleItem(item);
			item.Style = true;
			Assert.AreEqual(false, item2.Style);
			Assert.AreEqual(StyleItemType.HasOutline, item2.StyleType);
		}

		[Test]
		public void Nested_StopFollowing()
		{
			var item = new BoolStyleItem(StyleItemType.HasOutline, false);
			var item2 = new BoolStyleItem(item);
			item2.Style = true;
			Assert.AreEqual(false, item.Style);
			Assert.AreEqual(true, item2.Style);
			Assert.AreEqual(StyleItemType.HasOutline, item2.StyleType);
		}
	}
}
