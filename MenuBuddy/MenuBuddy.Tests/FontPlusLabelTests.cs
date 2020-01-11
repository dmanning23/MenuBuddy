using FakeItEasy;
using Microsoft.Xna.Framework.Content;
using NUnit.Framework;

namespace MenuBuddy.Tests
{
	public class TestLabel : Label
	{
		public TestLabel(string text, ContentManager content, FontSize fontSize = FontSize.Medium, string fontResource = null, bool? useFontPlus = null, int? fontPlusSize = 48) : base(text, content, fontSize, fontResource, useFontPlus, fontPlusSize)
		{
		}

		public virtual void InitializeFontsCalled()
		{
		}

		public virtual void InitializeFontPlusCalled()
		{
		}

		protected override void InitializeFonts(ContentManager content, string fontResource)
		{
			InitializeFontsCalled();
			//base.InitializeFonts(content, fontResource);
		}

		protected override void InitializeFontPlus(ContentManager content, string fontResource, int? fontPlusSize)
		{
			InitializeFontPlusCalled();
			//base.InitializeFontPlus(content, fontResource, fontPlusSize);
		}
	}

	public class FontPlusLabelTests
	{
		[TestCase(true, true, true)]
		[TestCase(true, false, true)]
		[TestCase(false, true, false)]
		[TestCase(false, false, false)]
		public void UseFontPlus(bool useFontPlus, bool styleSheetDefault, bool expectedResult)
		{
			StyleSheet.UseFontPlus = styleSheetDefault;
			var label = A.Fake<TestLabel>(x =>
			{
				x.WithArgumentsForConstructor(() => new TestLabel(string.Empty, null, FontSize.Medium, string.Empty, useFontPlus, 48));
				x.CallsBaseMethods();
			});

			if (expectedResult)
			{
				A.CallTo(() => label.InitializeFontPlusCalled()).MustHaveHappenedOnceExactly();
			}
			else
			{
				A.CallTo(() => label.InitializeFontPlusCalled()).MustNotHaveHappened();
			}
		}

		[TestCase(true, true, false)]
		[TestCase(true, false, false)]
		[TestCase(false, true, true)]
		[TestCase(false, false, true)]
		public void UseFont(bool useFontPlus, bool styleSheetDefault, bool expectedResult)
		{
			StyleSheet.UseFontPlus = styleSheetDefault;
			var label = A.Fake<TestLabel>(x =>
			{
				x.WithArgumentsForConstructor(() => new TestLabel(string.Empty, null, FontSize.Medium, string.Empty, useFontPlus, 48));
				x.CallsBaseMethods();
			});

			if (expectedResult)
			{
				A.CallTo(() => label.InitializeFontsCalled()).MustHaveHappenedOnceExactly();
			}
			else
			{
				A.CallTo(() => label.InitializeFontsCalled()).MustNotHaveHappened();
			}
		}

		[TestCase(true, true)]
		[TestCase(false, false)]
		public void UseStyleSheetDefault(bool styleSheetDefault, bool expectedResult)
		{
			StyleSheet.UseFontPlus = styleSheetDefault;
			var label = A.Fake<TestLabel>(x =>
			{
				x.WithArgumentsForConstructor(() => new TestLabel(string.Empty, null, FontSize.Medium, string.Empty, null, 48));
				x.CallsBaseMethods();
			});

			if (expectedResult)
			{
				A.CallTo(() => label.InitializeFontPlusCalled()).MustHaveHappenedOnceExactly();
			}
			else
			{
				A.CallTo(() => label.InitializeFontsCalled()).MustHaveHappenedOnceExactly();
			}
		}
	}
}
