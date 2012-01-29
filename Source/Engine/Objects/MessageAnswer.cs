using System;
using System.Collections.Generic;

using Junior.Common;

using TextAdventure.Engine.Game.Events;
using TextAdventure.Engine.Game.Messages;
using TextAdventure.Engine.Game.World;

namespace TextAdventure.Engine.Objects
{
	public class MessageAnswer : IMessage
	{
		private readonly EventHandlerCollection _eventHandlerCollection;
		private readonly Guid _id;
		private readonly IEnumerable<IMessagePart> _parts;
		private readonly string _text;

		public MessageAnswer(
			Guid id,
			string text,
			IEnumerable<IMessagePart> parts,
			EventHandlerCollection eventHandlerCollection = null)
		{
			parts.ThrowIfNull("parts");
			text.ThrowIfNull("text");

			_id = id;
			_text = text;
			_parts = parts;
			_eventHandlerCollection = eventHandlerCollection;
		}

		public string Text
		{
			get
			{
				return _text;
			}
		}

		protected internal EventHandlerCollection EventHandlerCollection
		{
			get
			{
				return _eventHandlerCollection;
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

		public static MessageAnswerBuilder Build(string text, EventHandlerCollection eventHandlerCollection = null)
		{
			return new MessageAnswerBuilder(text, eventHandlerCollection);
		}

		public static MessageAnswerBuilder Build(Guid id, string text, EventHandlerCollection eventHandlerCollection = null)
		{
			return new MessageAnswerBuilder(id, text, eventHandlerCollection);
		}

		protected internal virtual EventResult OnSelected(EventContext context, MessageAnswerSelectedEvent @event)
		{
			context.ThrowIfNull("context");
			@event.ThrowIfNull("event");

			return _eventHandlerCollection.SafeInvoke(context, @event);
		}

		protected internal virtual EventResult OnOpened(EventContext context, MessageAnswerOpenedEvent @event)
		{
			context.ThrowIfNull("context");
			@event.ThrowIfNull("event");

			return _eventHandlerCollection.SafeInvoke(context, @event);
		}

		protected internal virtual EventResult OnClosed(EventContext context, MessageAnswerClosedEvent @event)
		{
			context.ThrowIfNull("context");
			@event.ThrowIfNull("event");

			return _eventHandlerCollection.SafeInvoke(context, @event);
		}
	}
}