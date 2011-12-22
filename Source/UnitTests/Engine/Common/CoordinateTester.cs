using NUnit.Framework;

using TextAdventure.Engine.Common;

namespace TextAdventure.UnitTests.Engine.Common
{
	public static class CoordinateTester
	{
		[TestFixture]
		public class When_converting_to_size
		{
			[Test]
			public void Resulting_object_must_have_width_equal_to_x_and_height_equal_to_y()
			{
				var systemUnderTest = new Coordinate(5, 10);
				var size = (Size)systemUnderTest;

				Assert.That(size.Width, Is.EqualTo(systemUnderTest.X));
				Assert.That(size.Height, Is.EqualTo(systemUnderTest.Y));
			}
		}

		[TestFixture]
		public class When_testing_equality_with_equal_instances
		{
			[Test]
			public void Must_evaluate_to_true()
			{
				var systemUnderTest1 = new Coordinate(1, 2);
				var systemUnderTest2 = new Coordinate(1, 2);

				Assert.That(systemUnderTest1 == systemUnderTest2, Is.True);
			}
		}

		[TestFixture]
		public class When_testing_equality_with_unequal_instances
		{
			[Test]
			public void Must_evaluate_to_false()
			{
				var systemUnderTest1 = new Coordinate(1, 2);
				var systemUnderTest2 = new Coordinate(3, 4);

				Assert.That(systemUnderTest1 == systemUnderTest2, Is.False);
			}
		}

		[TestFixture]
		public class When_testing_inequality_with_equal_instances
		{
			[Test]
			public void Must_evaluate_to_false()
			{
				var systemUnderTest1 = new Coordinate(1, 2);
				var systemUnderTest2 = new Coordinate(1, 2);

				Assert.That(systemUnderTest1 != systemUnderTest2, Is.False);
			}
		}

		[TestFixture]
		public class When_testing_inequality_with_unequal_instances
		{
			[Test]
			public void Must_evaluate_to_true()
			{
				var systemUnderTest1 = new Coordinate(1, 2);
				var systemUnderTest2 = new Coordinate(3, 4);

				Assert.That(systemUnderTest1 != systemUnderTest2, Is.True);
			}
		}
	}
}