using System;
using System.Collections.Generic;
using System.Linq;

using Junior.Common;

using TextAdventure.Engine.Common;

namespace TextAdventure.Engine.Objects
{
	public class MessageQuestion : IMessagePart
	{
		private readonly IEnumerable<MessageAnswer> _answers;
		private readonly string _prompt;
		private readonly Color _questionForegroundColor;
		private readonly Color _selectedAnswerBackgroundColor;
		private readonly Color _selectedAnswerForegroundColor;
		private readonly Color _unselectedAnswerForegroundColor;

		public MessageQuestion(
			string prompt,
			Color questionForegroundColor,
			Color unselectedAnswerForegroundColor,
			Color selectedAnswerForegroundColor,
			Color selectedAnswerBackgroundColor,
			IEnumerable<MessageAnswer> answers)
		{
			prompt.ThrowIfNull("prompt");
			answers.ThrowIfNull("answers");
			answers = answers.ToArray();
			if (!answers.Any())
			{
				throw new ArgumentException("Must provide at least one answer.", "answers");
			}

			_prompt = prompt;
			_questionForegroundColor = questionForegroundColor;
			_unselectedAnswerForegroundColor = unselectedAnswerForegroundColor;
			_selectedAnswerForegroundColor = selectedAnswerForegroundColor;
			_selectedAnswerBackgroundColor = selectedAnswerBackgroundColor;
			_answers = answers;
		}

		public IEnumerable<MessageAnswer> Answers
		{
			get
			{
				return _answers;
			}
		}

		public string Prompt
		{
			get
			{
				return _prompt;
			}
		}

		public Color QuestionForegroundColor
		{
			get
			{
				return _questionForegroundColor;
			}
		}

		public Color UnselectedAnswerForegroundColor
		{
			get
			{
				return _unselectedAnswerForegroundColor;
			}
		}

		public Color SelectedAnswerForegroundColor
		{
			get
			{
				return _selectedAnswerForegroundColor;
			}
		}

		public Color SelectedAnswerBackgroundColor
		{
			get
			{
				return _selectedAnswerBackgroundColor;
			}
		}
	}
}