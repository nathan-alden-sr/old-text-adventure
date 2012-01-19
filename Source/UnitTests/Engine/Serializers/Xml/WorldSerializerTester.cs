using System;
using System.IO;
using System.Xml;
using System.Xml.Linq;

using NUnit.Framework;

using TextAdventure.Engine.Objects;
using TextAdventure.Engine.Serializers.Xml;

namespace TextAdventure.UnitTests.Engine.Serializers.Xml
{
	public static class WorldSerializerTester
	{
		[TestFixture]
		public class When_serializing_and_deserializing_instance : WorldSerializerTestFixture
		{
			private static void AssertWorldElement(XElement worldElement)
			{
				string xml = SerializeXElement(worldElement);

				NUnit.Framework.Assert.That(xml, Is.EqualTo(SerializerResources.World));
			}

			// ReSharper disable SuggestBaseTypeForParameter
			private static string SerializeXElement(XElement element)
				// ReSharper restore SuggestBaseTypeForParameter
			{
				using (var stringWriter = new StringWriter())
				{
					var settings = new XmlWriterSettings
					               	{
					               		Indent = true,
					               		IndentChars = "\t",
					               		OmitXmlDeclaration = true,
					               	};

					using (XmlWriter xmlWriter = XmlWriter.Create(stringWriter, settings))
					{
						element.WriteTo(xmlWriter);
						xmlWriter.Flush();

						return stringWriter.ToString();
					}
				}
			}

			[Test]
			public void Must_serialize_and_deserialize_correctly()
			{
				XElement worldElement = XElement.Parse(SerializerResources.World);
				World world = WorldSerializer.Instance.Deserialize(worldElement);

				Assert(world);

				XElement serializedWorldElement = WorldSerializer.Instance.Serialize(world);

				Console.Write(SerializeXElement(serializedWorldElement));

				AssertWorldElement(serializedWorldElement);
			}
		}
	}
}