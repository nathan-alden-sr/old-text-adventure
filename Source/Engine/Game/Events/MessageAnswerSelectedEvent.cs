using System.Collections.Generic;

using Junior.Common;

using TextAdventure.Engine.Objects;

namespace TextAdventure.Engine.Game.Events
{
	public class MessageAnswerSelectedEvent : Event
	{
		private readonly MessageAnswer _messageAnswer;

		public MessageAnswerSelectedEvent(MessageAnswer messageAnswer)
		{
			messageAnswer.ThrowIfNull("messageAnswer");

			_messageAnswer = messageAnswer;
		}

		public override IEnumerable<string> Details
		{
			get
			{
				yield return FormatIdDetailText("Message answer", _messageAnswer.Id);
			}
		}

		public MessageAnswer MessageAnswer
		{
			get
			{
				return _messageAnswer;
			}
		}
	}
}