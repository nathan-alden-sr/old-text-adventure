using System.Linq;
using System.Xml.Linq;

using Junior.Common;

using TextAdventure.Engine.Objects;

namespace TextAdventure.Engine.Serializers
{
	public class MessageQuestionSerializer
	{
		public static readonly MessageQuestionSerializer Instance = new MessageQuestionSerializer();

		private MessageQuestionSerializer()
		{
		}

		public XElement Serialize(MessageQuestion messageQuestion, string elementName = "question")
		{
			messageQuestion.ThrowIfNull("messageQuestion");
			elementName.ThrowIfNull("elementName");

			return new XElement(
				elementName,
				messageQuestion.Answers.Select(arg => MessageAnswerSerializer.Instance.Serialize(arg)),
				new XAttribute("prompt", messageQuestion.Prompt),
				new XAttribute("questionForegroundColor", ColorSerializer.Instance.Serialize(messageQuestion.QuestionForegroundColor)),
				new XAttribute("unselectedAnswerForegroundColor", ColorSerializer.Instance.Serialize(messageQuestion.UnselectedAnswerForegroundColor)),
				new XAttribute("selectedAnswerForegroundColor", ColorSerializer.Instance.Serialize(messageQuestion.SelectedAnswerForegroundColor)),
				new XAttribute("selectedAnswerBackgroundColor", ColorSerializer.Instance.Serialize(messageQuestion.SelectedAnswerBackgroundColor)));
		}

		public MessageQuestion Deserialize(XElement questionElement)
		{
			questionElement.ThrowIfNull("questionElement");

			return new MessageQuestion(
				(string)questionElement.Attribute("prompt"),
				ColorSerializer.Instance.Deserialize((string)questionElement.Attribute("questionForegroundColor")),
				ColorSerializer.Instance.Deserialize((string)questionElement.Attribute("unselectedAnswerForegroundColor")),
				ColorSerializer.Instance.Deserialize((string)questionElement.Attribute("selectedAnswerForegroundColor")),
				ColorSerializer.Instance.Deserialize((string)questionElement.Attribute("selectedAnswerBackgroundColor")),
				questionElement.Elements("answer").Select(MessageAnswerSerializer.Instance.Deserialize));
		}
	}
}