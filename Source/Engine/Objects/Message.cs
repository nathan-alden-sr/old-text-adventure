using System;
using System.Collections.Generic;
using System.Linq;

using Junior.Common;

using TextAdventure.Engine.Common;
using TextAdventure.Engine.Game.Messages;

namespace TextAdventure.Engine.Objects
{
	public class Message : IMessageWithBackgroundColor
	{
		private readonly Color _backgroundColor;
		private readonly Guid _id;
		private readonly IEnumerable<IMessagePart> _parts;

		public Message(
			Guid id,
			Color backgroundColor,
			IEnumerable<IMessagePart> parts)
		{
			parts.ThrowIfNull("parts");

			parts = parts.ToArray();

			IMessagePart question = parts.SingleOrDefault(arg => arg is MessageQueue);

			if (question != null && parts.Last() != question)
			{
				throw new ArgumentException("When a MessageQuestion is present, it must be the last part.", "parts");
			}

			_id = id;
			_backgroundColor = backgroundColor;
			_parts = parts;
		}

		public Color BackgroundColor
		{
			get
			{
				return _backgroundColor;
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

		public static MessageBuilder Build(Color backgroundColor)
		{
			return new MessageBuilder(backgroundColor);
		}

		public static MessageBuilder Build(Guid id, Color backgroundColor)
		{
			return new MessageBuilder(id, backgroundColor);
		}
	}
}