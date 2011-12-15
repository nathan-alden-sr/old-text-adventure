using System.Collections.Generic;

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
	}
}