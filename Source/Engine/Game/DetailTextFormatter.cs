using System;

using Junior.Common;

using TextAdventure.Engine.Objects;

namespace TextAdventure.Engine.Game
{
	public class DetailTextFormatter
	{
		public static readonly DetailTextFormatter Instance = new DetailTextFormatter();

		private DetailTextFormatter()
		{
		}

		public string FormatId(string prefix, Guid id)
		{
			prefix.ThrowIfNull("prefix");

			return String.Format("{0} ID: {1}", prefix, id);
		}

		public string FormatUnique(string prefix, IUnique unique)
		{
			prefix.ThrowIfNull("prefix");
			unique.ThrowIfNull("unique");

			return String.Format("{0} ID: {1}", prefix, unique.Id);
		}

		public string FormatNamedObject(string prefix, INamedObject namedObject)
		{
			prefix.ThrowIfNull("prefix");
			namedObject.ThrowIfNull("namedObject");

			return String.Format("{0}: {1} ({2})", prefix, namedObject.Name, namedObject.Id);
		}
	}
}