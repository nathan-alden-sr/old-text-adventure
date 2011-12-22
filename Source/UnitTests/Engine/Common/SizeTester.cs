using System;

using NUnit.Framework;

using TextAdventure.Engine.Common;

namespace TextAdventure.UnitTests.Engine.Common
{
	public static class SizeTester
	{
		[TestFixture]
		public class When_constructing_instance_with_negative_height
		{
			[Test]
			[TestCase(Int32.MinValue)]
			[TestCase(-1)]
			public void Must_throw_exception(int height)
			{
				Assert.Throws<ArgumentOutOfRangeException>(() => new Size(10, height));
			}
		}

		[TestFixture]
		public class When_constructing_instance_with_negative_width
		{
			[Test]
			[TestCase(Int32.MinValue)]
			[TestCase(-1)]
			public void Must_throw_exception(int width)
			{
				Assert.Throws<ArgumentOutOfRangeException>(() => new Size(width, 10));
			}
		}

		[TestFixture]
		public class When_constructing_instance_with_nonnegative_width_and_height
		{
			[Test]
			[TestCase(0, 0)]
			[TestCase(10, 0)]
			[TestCase(0, 10)]
			[TestCase(Int32.MaxValue, Int32.MaxValue)]
			public void TestName(int width, int height)
			{
				var systemUnderTest = new Size(width, height);

				Assert.That(systemUnderTest.Width, Is.EqualTo(width));
				Assert.That(systemUnderTest.Height, Is.EqualTo(height));
			}
		}

		[TestFixture]
		public class When_converting_to_coordinate
		{
			[Test]
			public void Resulting_object_must_have_x_equal_to_width_and_y_equal_to_height()
			{
				var systemUnderTest = new Size(5, 10);
				var coordinate = (Coordinate)systemUnderTest;

				Assert.That(coordinate.X, Is.EqualTo(systemUnderTest.Width));
				Assert.That(coordinate.Y, Is.EqualTo(systemUnderTest.Height));
			}
		}

		[TestFixture]
		public class When_setting_negative_height
		{
			[Test]
			[TestCase(Int32.MinValue)]
			[TestCase(-1)]
			public void Must_throw_exception(int height)
			{
				var systemUnderTest = new Size(5, 10);

				Assert.Throws<ArgumentOutOfRangeException>(() => systemUnderTest.Height = height);
			}
		}

		[TestFixture]
		public class When_setting_negative_width
		{
			[Test]
			[TestCase(Int32.MinValue)]
			[TestCase(-1)]
			public void Must_throw_exception(int width)
			{
				var systemUnderTest = new Size(5, 10);

				Assert.Throws<ArgumentOutOfRangeException>(() => systemUnderTest.Width = width);
			}
		}

		[TestFixture]
		public class When_setting_nonnegative_height
		{
			[Test]
			[TestCase(0)]
			[TestCase(1)]
			[TestCase(Int32.MaxValue)]
			public void Must_throw_exception(int height)
			{
				var systemUnderTest = new Size(5, 10);

				Assert.DoesNotThrow(() => systemUnderTest.Height = height);
			}
		}

		[TestFixture]
		public class When_setting_nonnegative_width
		{
			[Test]
			[TestCase(0)]
			[TestCase(1)]
			[TestCase(Int32.MaxValue)]
			public void Must_throw_exception(int width)
			{
				var systemUnderTest = new Size(5, 10);

				Assert.DoesNotThrow(() => systemUnderTest.Width = width);
			}
		}

		[TestFixture]
		public class When_testing_equality_with_equal_instances
		{
			[Test]
			public void Must_result_in_true()
			{
				var systemUnderTest1 = new Size(5, 10);
				var systemUnderTest2 = new Size(5, 10);

				Assert.That(systemUnderTest1 == systemUnderTest2, Is.True);
			}
		}

		[TestFixture]
		public class When_testing_equality_with_unequal_instances
		{
			[Test]
			public void Must_result_in_false()
			{
				var systemUnderTest1 = new Size(5, 10);
				var systemUnderTest2 = new Size(10, 5);

				Assert.That(systemUnderTest1 == systemUnderTest2, Is.False);
			}
		}

		[TestFixture]
		public class When_testing_inequality_with_equal_instances
		{
			[Test]
			public void Must_result_in_false()
			{
				var systemUnderTest1 = new Size(5, 10);
				var systemUnderTest2 = new Size(5, 10);

				Assert.That(systemUnderTest1 != systemUnderTest2, Is.False);
			}
		}

		[TestFixture]
		public class When_testing_inequality_with_unequal_instances
		{
			[Test]
			public void Must_result_in_true()
			{
				var systemUnderTest1 = new Size(5, 10);
				var systemUnderTest2 = new Size(10, 5);

				Assert.That(systemUnderTest1 != systemUnderTest2, Is.True);
			}
		}
	}
}