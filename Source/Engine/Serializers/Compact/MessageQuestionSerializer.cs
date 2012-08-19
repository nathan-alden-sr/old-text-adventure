using System.Collections.Generic;
using System.Linq;
using System.Text;

using Junior.Common;

using TextAdventure.Engine.Common;
using TextAdventure.Engine.Objects;

namespace TextAdventure.Engine.Serializers.Compact
{
	public class MessageQuestionSerializer
	{
		public static readonly MessageQuestionSerializer Instance = new MessageQuestionSerializer();

		private MessageQuestionSerializer()
		{
		}

		public byte[] Serialize(MessageQuestion messageQuestion)
		{
			messageQuestion.ThrowIfNull("messageQuestion");

			var serializer = new CompactSerializer();

			serializer[0] = Encoding.UTF8.GetBytes(messageQuestion.Prompt);
			serializer[1] = ColorSerializer.Instance.Serialize(messageQuestion.QuestionForegroundColor);
			serializer[2] = ColorSerializer.Instance.Serialize(messageQuestion.UnselectedAnswerForegroundColor);
			serializer[3] = ColorSerializer.Instance.Serialize(messageQuestion.SelectedAnswerForegroundColor);
			serializer[4] = ColorSerializer.Instance.Serialize(messageQuestion.SelectedAnswerBackgroundColor);

			var answerSerializer = new CompactSerializer();
			int index = 0;

			foreach (MessageAnswer answer in messageQuestion.Answers)
			{
				answerSerializer[index++] = MessageAnswerSerializer.Instance.Serialize(answer);
			}

			serializer[5] = answerSerializer.Serialize();

			return serializer.Serialize();
		}

		public MessageQuestion Deserialize(byte[] serializedData)
		{
			serializedData.ThrowIfNull("serializedData");

			var serializer = new CompactSerializer(serializedData);
			string prompt = Encoding.UTF8.GetString(serializer[0]);
			Color questionForegroundColor = ColorSerializer.Instance.Deserialize(serializer[1]);
			Color unselectedAnswerForegroundColor = ColorSerializer.Instance.Deserialize(serializer[2]);
			Color selectedAnswerForegroundColor = ColorSerializer.Instance.Deserialize(serializer[3]);
			Color selectedAnswerBackgroundColor = ColorSerializer.Instance.Deserialize(serializer[4]);
			var answerSerializer = new CompactSerializer(serializer[5]);
			IEnumerable<MessageAnswer> answers = answerSerializer.FieldIndices.Select(arg => MessageAnswerSerializer.Instance.Deserialize(answerSerializer[arg]));

			return new MessageQuestion(prompt, questionForegroundColor, unselectedAnswerForegroundColor, selectedAnswerForegroundColor, selectedAnswerBackgroundColor, answers);
		}
	}
}