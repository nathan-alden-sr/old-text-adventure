using System;
using System.Collections.Generic;

using TextAdventure.Engine.Common;
using TextAdventure.Engine.Objects;

namespace TextAdventure.Engine.Game.Messages
{
	public class MessageAnswerBuilder
	{
		private readonly Guid _id;
		private readonly List<IMessagePart> _parts = new List<IMessagePart>();
		private readonly string _text;

		public MessageAnswerBuilder(string text)
			: this(Guid.NewGuid(), text)
		{
		}

		public MessageAnswerBuilder(Guid id, string text)
		{
			_id = id;
			_text = text;
		}

		public MessageAnswer MessageAnswer
		{
			get
			{
				return new MessageAnswer(_id, _text, _parts);
			}
		}

		public MessageAnswerBuilder Color(Color color)
		{
			_parts.Add(new MessageColor(color));

			return this;
		}

		public MessageAnswerBuilder Text(string text, int numberOfLineBreaksAfterText = 0)
		{
			_parts.Add(new MessageText(text));

			return LineBreak(numberOfLineBreaksAfterText);
		}

		public MessageAnswerBuilder LineBreak(int numberOfLineBreaks = 1)
		{
			for (int i = 0; i < numberOfLineBreaks; i++)
			{
				_parts.Add(new MessageLineBreak());
			}

			return this;
		}

		public MessageAnswerBuilder Question(
			string prompt,
			Color questionForegroundColor,
			Color unselectedAnswerForegroundColor,
			Color unselectedAnswerBackgroundColor,
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

		public static implicit operator MessageAnswer(MessageAnswerBuilder builder)
		{
			return builder.MessageAnswer;
		}
	}
}