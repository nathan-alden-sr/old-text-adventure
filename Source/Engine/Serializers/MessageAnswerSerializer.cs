﻿using System;
using System.Xml.Linq;

using Junior.Common;

using TextAdventure.Engine.Objects;

namespace TextAdventure.Engine.Serializers
{
	public class MessageAnswerSerializer
	{
		public static readonly MessageAnswerSerializer Instance = new MessageAnswerSerializer();

		private MessageAnswerSerializer()
		{
		}

		public XElement Serialize(MessageAnswer messageAnswer, string elementName = "answer")
		{
			messageAnswer.ThrowIfNull("messageAnswer");
			elementName.ThrowIfNull("elementName");

			return new XElement(
				elementName,
				MessagePartSerializer.Instance.Serialize(messageAnswer.Parts),
				new XAttribute("id", messageAnswer.Id),
				new XAttribute("text", messageAnswer.Text));
		}

		public MessageAnswer Deserialize(XElement answerElement)
		{
			answerElement.ThrowIfNull("answerElement");

			return new MessageAnswer(
				(Guid)answerElement.Attribute("id"),
				(string)answerElement.Attribute("text"),
				MessagePartSerializer.Instance.Deserialize(answerElement));
		}
	}
}