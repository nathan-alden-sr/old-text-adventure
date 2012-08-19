using System;

using NUnit.Framework;

using TextAdventure.Engine.Common;

namespace TextAdventure.UnitTests.Engine.Common
{
	public static class ColorTester
	{
		[TestFixture]
		public class When_constructing_instance_with_byte_values
		{
			[Test]
			public void Must_expose_correct_rgb_values()
			{
				const byte r = 127;
				const byte g = 15;
				const byte b = 255;
				const byte a = 123;
				var systemUnderTest = new Color(r, g, b, a);

				Assert.That(systemUnderTest.R, Is.EqualTo(r / 255f));
				Assert.That(systemUnderTest.G, Is.EqualTo(g / 255f));
				Assert.That(systemUnderTest.B, Is.EqualTo(b / 255f));
				Assert.That(systemUnderTest.A, Is.EqualTo(a / 255f));
			}
		}

		[TestFixture]
		public class When_constructing_instance_with_existing_color_and_byte_alpha
		{
			[Test]
			public void Must_apply_alpha()
			{
				var color = new Color(0.25f, 0.5f, 0.75f, 0.1f);
				var systemUnderTest = new Color(color, 112);

				Assert.That(systemUnderTest.A, Is.EqualTo(112 / 255f));
			}
		}

		[TestFixture]
		public class When_constructing_instance_with_existing_color_and_float_alpha
		{
			[Test]
			public void Must_apply_alpha()
			{
				var color = new Color(0.25f, 0.5f, 0.75f, 0.1f);
				var systemUnderTest = new Color(color, 0.9f);

				Assert.That(systemUnderTest.A, Is.EqualTo(0.9f));
			}
		}

		[TestFixture]
		public class When_constructing_instance_with_float_values
		{
			[Test]
			[TestCase(0f)]
			[TestCase(0.5f)]
			[TestCase(1f)]
			public void Must_allow_in_range_values(float value)
			{
				Assert.DoesNotThrow(() => new Color(value, value, value, value));
			}

			[Test]
			[TestCase(Single.MinValue)]
			[TestCase(Single.MaxValue)]
			[TestCase(-0.1f)]
			[TestCase(1.1f)]
			public void Must_clamp_out_of_range_values(float value)
			{
				var systemUnderTest = new Color(value, value, value, value);

				Assert.That(systemUnderTest.R, Is.AtLeast(0f).And.AtMost(1.0f));
				Assert.That(systemUnderTest.G, Is.AtLeast(0f).And.AtMost(1.0f));
				Assert.That(systemUnderTest.B, Is.AtLeast(0f).And.AtMost(1.0f));
				Assert.That(systemUnderTest.A, Is.AtLeast(0f).And.AtMost(1.0f));
			}
		}

		[TestFixture]
		public class When_lerping_colors
		{
			[Test]
			public void Must_result_in_correct_color()
			{
				Assert.That(Color.Lerp(Color.White, Color.TransparentBlack, 0.4f), Is.EqualTo(new Color(0.6f, 0.6f, 0.6f, 0.6f)));
				Assert.That(Color.Lerp(Color.TransparentBlack, Color.White, 0.4f), Is.EqualTo(new Color(0.4f, 0.4f, 0.4f, 0.4f)));
			}
		}

		[TestFixture]
		public class When_lerping_colors_alpha
		{
			[Test]
			public void Must_result_in_correct_color()
			{
				Assert.That(Color.LerpAlpha(Color.White, Color.TransparentBlack, 0.4f), Is.EqualTo(new Color(1f, 1f, 1f, 0.6f)));
				Assert.That(Color.LerpAlpha(Color.TransparentBlack, Color.White, 0.4f), Is.EqualTo(new Color(0f, 0f, 0f, 0.4f)));
			}
		}

		[TestFixture]
		public class When_lerping_colors_rgb
		{
			[Test]
			public void Must_result_in_correct_color()
			{
				Assert.That(Color.LerpRgb(Color.White, Color.TransparentBlack, 0.4f), Is.EqualTo(new Color(0.6f, 0.6f, 0.6f)));
				Assert.That(Color.LerpRgb(Color.TransparentBlack, Color.White, 0.4f), Is.EqualTo(new Color(0.4f, 0.4f, 0.4f, 0f)));
			}
		}

		[TestFixture]
		public class When_multiplying_color
		{
			[Test]
			public void Must_result_in_correct_color()
			{
				Assert.That(Color.Multiply(Color.White, 0.75f), Is.EqualTo(new Color(0.75f, 0.75f, 0.75f, 0.75f)));
				Assert.That(Color.White * 0.75f, Is.EqualTo(new Color(0.75f, 0.75f, 0.75f, 0.75f)));
			}
		}

		[TestFixture]
		public class When_multiplying_color_alpha
		{
			[Test]
			public void Must_result_in_correct_color()
			{
				Assert.That(Color.MultiplyAlpha(Color.White, 0.75f), Is.EqualTo(new Color(1f, 1f, 1f, 0.75f)));
			}
		}

		[TestFixture]
		public class When_multiplying_color_rgb
		{
			[Test]
			public void Must_result_in_correct_color()
			{
				Assert.That(Color.MultiplyRgb(Color.White, 0.75f), Is.EqualTo(new Color(0.75f, 0.75f, 0.75f)));
			}
		}

		[TestFixture]
		public class When_testing_equality_with_equal_instances
		{
			[Test]
			public void Must_result_in_true()
			{
				var systemUnderTest1 = new Color(0f, 0.25f, 0.5f, 0.75f);
				var systemUnderTest2 = new Color(0f, 0.25f, 0.5f, 0.75f);

				Assert.That(systemUnderTest1 == systemUnderTest2, Is.True);
			}
		}

		[TestFixture]
		public class When_testing_equality_with_unequal_instances
		{
			[Test]
			public void Must_result_in_false()
			{
				var systemUnderTest1 = new Color(0f, 0.25f, 0.5f, 0.75f);
				var systemUnderTest2 = new Color(0f, 0.25f, 0.5f, 0.76f);

				Assert.That(systemUnderTest1 == systemUnderTest2, Is.False);
			}
		}

		[TestFixture]
		public class When_testing_inequality_with_equal_instances
		{
			[Test]
			public void Must_result_in_false()
			{
				var systemUnderTest1 = new Color(0f, 0.25f, 0.5f, 0.75f);
				var systemUnderTest2 = new Color(0f, 0.25f, 0.5f, 0.75f);

				Assert.That(systemUnderTest1 != systemUnderTest2, Is.False);
			}
		}

		[TestFixture]
		public class When_testing_inequality_with_unequal_instances
		{
			[Test]
			public void Must_result_in_true()
			{
				var systemUnderTest1 = new Color(0f, 0.25f, 0.5f, 0.75f);
				var systemUnderTest2 = new Color(0f, 0.25f, 0.5f, 0.76f);

				Assert.That(systemUnderTest1 != systemUnderTest2, Is.True);
			}
		}
	}
}