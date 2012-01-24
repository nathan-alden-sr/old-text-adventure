using System;
using System.Collections.Generic;

using Junior.Common;

using TextAdventure.Engine.Game.Events;
using TextAdventure.Engine.Game.Messages;

namespace TextAdventure.Engine.Objects
{
	public class MessageAnswer : IMessage
	{
		private readonly IEventHandler<AnswerSelectedEvent> _answerSelectedEventHandler;
		private readonly Guid _id;
		private readonly IEnumerable<IMessagePart> _parts;
		private readonly string _text;

		public MessageAnswer(
			Guid id,
			string text,
			IEnumerable<IMessagePart> parts,
			IEventHandler<AnswerSelectedEvent> answerSelectedEventHandler = null)
		{
			parts.ThrowIfNull("parts");
			text.ThrowIfNull("text");

			_id = id;
			_text = text;
			_parts = parts;
			_answerSelectedEventHandler = answerSelectedEventHandler;
		}

		public string Text
		{
			get
			{
				return _text;
			}
		}

		public IEventHandler<AnswerSelectedEvent> AnswerSelectedEventHandler
		{
			get
			{
				return _answerSelectedEventHandler;
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

		public static MessageAnswerBuilder Build(string text, IEventHandler<AnswerSelectedEvent> answerSelectedEventHandler = null)
		{
			return new MessageAnswerBuilder(text, answerSelectedEventHandler);
		}

		public static MessageAnswerBuilder Build(Guid id, string text, IEventHandler<AnswerSelectedEvent> answerSelectedEventHandler = null)
		{
			return new MessageAnswerBuilder(id, text, answerSelectedEventHandler);
		}
	}
}