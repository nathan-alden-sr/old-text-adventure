using System.Collections.Generic;

using TextAdventure.Engine.Objects;

namespace TextAdventure.Engine.Game.Events
{
	public class MessageOpenedEvent : TargetedEvent<Message>
	{
		public MessageOpenedEvent(Message target)
			: base(target)
		{
		}

		public override IEnumerable<string> Details
		{
			get
			{
				yield return FormatNamedObjectDetailText("Message", Target);
			}
		}
	}
}