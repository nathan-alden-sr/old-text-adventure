using System;
using System.Collections.Generic;
using System.Linq;

using Junior.Common;

using TextAdventure.Engine.Objects;

namespace TextAdventure.WindowsGame.Managers
{
	public class MessageAnswerSelectionManager
	{
		private readonly MessageAnswer[] _answers;
		private MessageAnswer _selectedAnswer;

		public MessageAnswerSelectionManager(IEnumerable<MessageAnswer> answers)
		{
			answers.ThrowIfNull("answers");

			_answers = answers.ToArray();

			if (!_answers.Any())
			{
				throw new ArgumentException("Must provide at least one answer.", "answers");
			}

			_selectedAnswer = _answers.First();
		}

		public IEnumerable<MessageAnswer> Answers
		{
			get
			{
				return _answers;
			}
		}

		public MessageAnswer SelectedAnswer
		{
			get
			{
				return _selectedAnswer;
			}
		}

		public void SelectNextAnswer()
		{
			int index = Array.IndexOf(_answers, _selectedAnswer);

			if (index == _answers.Length - 1)
			{
				index = 0;
			}
			else
			{
				index++;
			}

			_selectedAnswer = _answers[index];
		}

		public void SelectPreviousAnswer()
		{
			int index = Array.IndexOf(_answers, _selectedAnswer);

			if (index == 0)
			{
				index = _answers.Length - 1;
			}
			else
			{
				index--;
			}

			_selectedAnswer = _answers[index];
		}
	}
}