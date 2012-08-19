using System.Collections.Generic;

using TextAdventure.Engine.Objects;

namespace TextAdventure.Engine.Game.Events
{
	public class MessageAnswerOpenedEvent : TargetedEvent<MessageAnswer>
	{
		public MessageAnswerOpenedEvent(MessageAnswer target)
			: base(target)
		{
		}

		public override IEnumerable<string> Details
		{
			get
			{
				yield return FormatUniqueDetailText("Message", Target);
			}
		}
	}
}