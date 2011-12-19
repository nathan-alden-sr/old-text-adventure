using System;
using System.Collections.Generic;
using System.Linq;

using Junior.Common;

namespace TextAdventure.Engine.Objects
{
	public class MessageQuestion : IMessagePart
	{
		private readonly IEnumerable<MessageAnswer> _answers;

		public MessageQuestion(IEnumerable<MessageAnswer> answers)
		{
			answers.ThrowIfNull("answers");
			answers = answers.ToArray();
			if (!answers.Any())
			{
				throw new ArgumentException("Must provide at least one answer.", "answers");
			}

			_answers = answers;
		}

		public IEnumerable<MessageAnswer> Answers
		{
			get
			{
				return _answers;
			}
		}
	}
}