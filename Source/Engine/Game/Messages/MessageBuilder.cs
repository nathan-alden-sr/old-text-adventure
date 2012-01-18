using System;
using System.Collections.Generic;

using Junior.Common;

using TextAdventure.Engine.Common;
using TextAdventure.Engine.Objects;

namespace TextAdventure.Engine.Game.Messages
{
	public class MessageBuilder
	{
		private readonly Color _backgroundColor;
		private readonly string _description;
		private readonly Guid _id;
		private readonly string _name;
		private readonly List<IMessagePart> _parts = new List<IMessagePart>();

		public MessageBuilder(Color backgroundColor, string name = "", string description = "")
			: this(Guid.NewGuid(), backgroundColor, name, description)
		{
			_backgroundColor = backgroundColor;
			_id = Guid.NewGuid();
		}

		public MessageBuilder(Guid id, Color backgroundColor, string name = "", string description = "")
		{
			name.ThrowIfNull("name");
			description.ThrowIfNull("description");

			_id = id;
			_backgroundColor = backgroundColor;
			_name = name;
			_description = description;
		}

		public Message Message
		{
			get
			{
				return new Message(_id, _name, _description, _backgroundColor, _parts);
			}
		}

		public MessageBuilder Color(Color color)
		{
			_parts.Add(new MessageColor(color));

			return this;
		}

		public MessageBuilder Text(string text, int numberOfLineBreaksAfterText = 0)
		{
			_parts.Add(new MessageText(text));

			return LineBreak(numberOfLineBreaksAfterText);
		}

		public MessageBuilder LineBreak(int numberOfLineBreaks = 1)
		{
			for (int i = 0; i < numberOfLineBreaks; i++)
			{
				_parts.Add(new MessageLineBreak());
			}

			return this;
		}

		public MessageBuilder Question(
			string prompt,
			Color questionForegroundColor,
			Color unselectedAnswerForegroundColor,
			Color selectedAnswerForegroundColor,
			Color selectedAnswerBackgroundColor,
			params MessageAnswer[] answers)
		{
			_parts.Add(new MessageQuestion(
			           	prompt,
			           	questionForegroundColor,
			           	unselectedAnswerForegroundColor,
			           	selectedAnswerForegroundColor,
			           	selectedAnswerBackgroundColor,
			           	answers));

			return this;
		}

		public static implicit operator Message(MessageBuilder builder)
		{
			return builder.Message;
		}
	}
}