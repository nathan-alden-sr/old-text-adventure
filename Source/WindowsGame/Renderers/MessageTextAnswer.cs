using Junior.Common;

using Microsoft.Xna.Framework;

using TextAdventure.Engine.Objects;

using Color = TextAdventure.Engine.Common.Color;

namespace TextAdventure.WindowsGame.Renderers
{
	public class MessageTextAnswer : MessageTextWord
	{
		private readonly MessageAnswer _answer;
		private readonly Color _selectedAnswerBackgroundColor;
		private readonly Color _selectedAnswerForegroundColor;
		private readonly Color _unselectedAnswerForegroundColor;

		public MessageTextAnswer(
			MessageAnswer answer,
			string text,
			Vector2 size,
			Color unselectedAnswerForegroundColor,
			Color selectedAnswerForegroundColor,
			Color selectedAnswerBackgroundColor)
			: base(text, size, false)
		{
			answer.ThrowIfNull("answer");

			_answer = answer;
			_unselectedAnswerForegroundColor = unselectedAnswerForegroundColor;
			_selectedAnswerForegroundColor = selectedAnswerForegroundColor;
			_selectedAnswerBackgroundColor = selectedAnswerBackgroundColor;
		}

		public MessageAnswer Answer
		{
			get
			{
				return _answer;
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