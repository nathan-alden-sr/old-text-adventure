using System.Collections.Generic;

using TextAdventure.Engine.Objects;

namespace TextAdventure.Engine.Game.Events
{
	public class MessageClosedEvent : TargetedEvent<Message>
	{
		public MessageClosedEvent(Message target)
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