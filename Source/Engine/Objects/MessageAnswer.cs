using System;
using System.Collections.Generic;

using Junior.Common;

using TextAdventure.Engine.Game.Events;
using TextAdventure.Engine.Game.Messages;

namespace TextAdventure.Engine.Objects
{
	public class MessageAnswer : IMessage
	{
		private readonly Guid _id;
		private readonly IEventHandler<MessageAnswerSelectedEvent> _messageAnswerSelectedEventHandler;
		private readonly IEnumerable<IMessagePart> _parts;
		private readonly string _text;

		public MessageAnswer(
			Guid id,
			string text,
			IEnumerable<IMessagePart> parts,
			IEventHandler<MessageAnswerSelectedEvent> messageAnswerSelectedEventHandler = null)
		{
			parts.ThrowIfNull("parts");
			text.ThrowIfNull("text");

			_id = id;
			_text = text;
			_parts = parts;
			_messageAnswerSelectedEventHandler = messageAnswerSelectedEventHandler;
		}

		public string Text
		{
			get
			{
				return _text;
			}
		}

		public IEventHandler<MessageAnswerSelectedEvent> MessageAnswerSelectedEventHandler
		{
			get
			{
				return _messageAnswerSelectedEventHandler;
			}
		}

		public IEnumerable<IMessagePart> Parts
		{
			get
			{
				return _parts;
			}
		}

		public Guid Id
		{
			get
			{
				return _id;
			}
		}

		public static MessageAnswerBuilder Build(string text, IEventHandler<MessageAnswerSelectedEvent> answerSelectedEventHandler = null)
		{
			return new MessageAnswerBuilder(text, answerSelectedEventHandler);
		}

		public static MessageAnswerBuilder Build(Guid id, string text, IEventHandler<MessageAnswerSelectedEvent> answerSelectedEventHandler = null)
		{
			return new MessageAnswerBuilder(id, text, answerSelectedEventHandler);
		}
	}
}