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
				messageQuestion.Answers.Select(arg => MessageAnswerSerializer.Instance.Serialize(arg)));
		}

		public MessageQuestion Deserialize(XElement questionElement)
		{
			questionElement.ThrowIfNull("questionElement");

			return new MessageQuestion(
				questionElement.Elements("answer").Select(MessageAnswerSerializer.Instance.Deserialize));
		}
	}
}