using System.Collections.Generic;

using TextAdventure.Engine.Common;

namespace TextAdventure.Engine.Objects
{
	public interface IMessageQuestion : IMessagePart
	{
		string Prompt
		{
			get;
		}
		Color QuestionForegroundColor
		{
			get;
		}
		Color UnselectedAnswerForegroundColor
		{
			get;
		}
		Color SelectedAnswerForegroundColor
		{
			get;
		}
		Color SelectedAnswerBackgroundColor
		{
			get;
		}
		IEnumerable<IMessageAnswer> Answers
		{
			get;
		}
	}
}