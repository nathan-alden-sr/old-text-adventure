using System;
using System.Collections.Generic;
using System.Linq;

using Junior.Common;

using TextAdventure.Engine.Common;
using TextAdventure.Engine.Game.Messages;

namespace TextAdventure.Engine.Objects
{
	public class Message : IMessageWithBackgroundColor, INamedObject, IDescribedObject
	{
		private readonly Color _backgroundColor;
		private readonly Guid _id;
		private readonly IEnumerable<IMessagePart> _parts;
		private string _description;
		private string _name;

		public Message(
			Guid id,
			string name,
			string description,
			Color backgroundColor,
			IEnumerable<IMessagePart> parts)
		{
			name.ThrowIfNull("name");
			description.ThrowIfNull("description");
			parts.ThrowIfNull("parts");

			parts = parts.ToArray();

			IMessagePart question = parts.SingleOrDefault(arg => arg is MessageQueue);

			if (question != null && parts.Last() != question)
			{
				throw new ArgumentException("When a MessageQuestion is present, it must be the last part.", "parts");
			}

			_id = id;
			Name = name;
			Description = description;
			_backgroundColor = backgroundColor;
			_parts = parts;
		}

		public string Description
		{
			get
			{
				return _description;
			}
			protected internal set
			{
				value.ThrowIfNull("value");

				_description = value;
			}
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

		public string Name
		{
			get
			{
				return _name;
			}
			protected internal set
			{
				value.ThrowIfNull("value");

				_name = value;
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