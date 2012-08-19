using System;
using System.Collections.Generic;

using Junior.Common;

using TextAdventure.Engine.Objects;

namespace TextAdventure.Engine.Game.Events
{
	public abstract class Event : ILoggable
	{
		public virtual string Title
		{
			get
			{
				string typeName = GetType().Name;

				if (typeName.EndsWith("Event"))
				{
					typeName = typeName.Substring(0, typeName.Length - 5);
				}

				return typeName;
			}
		}

		public virtual IEnumerable<string> Details
		{
			get
			{
				yield break;
			}
		}

		protected static string FormatIdDetailText(string prefix, Guid id)
		{
			prefix.ThrowIfNull("prefix");

			return DetailTextFormatter.Instance.FormatId(prefix, id);
		}

		protected static string FormatUniqueDetailText(string prefix, IUnique unique)
		{
			prefix.ThrowIfNull("prefix");
			unique.ThrowIfNull("unique");

			return DetailTextFormatter.Instance.FormatUnique(prefix, unique);
		}

		protected static string FormatNamedObjectDetailText(string prefix, INamedObject namedObject)
		{
			prefix.ThrowIfNull("prefix");
			namedObject.ThrowIfNull("namedObject");

			return DetailTextFormatter.Instance.FormatNamedObject(prefix, namedObject);
		}
	}
}