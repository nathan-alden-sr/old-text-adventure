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
			public void Must_not_allow_out_of_range_values(float value)
			{
				Assert.Throws<ArgumentOutOfRangeException>(() => new Color(value, value, value, value));
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