using System;
using System.Collections.Generic;
using System.Linq;

using Junior.Common;

using TextAdventure.Engine.Game.Events;
using TextAdventure.Engine.Game.World;
using TextAdventure.Engine.Objects;

namespace TextAdventure.Engine.Game.Messages
{
	public class MessageMananger
	{
		private readonly List<IMessage> _list = new List<IMessage>();
		private readonly WorldInstance _worldInstance;

		public MessageMananger(WorldInstance worldInstance)
		{
			_worldInstance = worldInstance;
		}

		public int Count
		{
			get
			{
				return _list.Count;
			}
		}

		public void EnqueueMessage(IMessage message, MessageQueuePosition position)
		{
			message.ThrowIfNull("message");

			switch (position)
			{
				case MessageQueuePosition.First:
					_list.Insert(0, message);
					break;
				case MessageQueuePosition.Last:
					_list.Add(message);
					break;
				default:
					throw new ArgumentOutOfRangeException("position");
			}
		}

		public IMessage DequeueMessage()
		{
			if (_list.Any())
			{
				IMessage message = _list[0];

				_list.RemoveAt(0);

				return message;
			}

			return null;
		}

		public void SelectAnswer(MessageAnswer answer)
		{
			answer.ThrowIfNull("answer");

			_worldInstance.RaiseEvent(answer.OnSelected, new MessageAnswerSelectedEvent(answer));
			if (answer.Parts.Any())
			{
				EnqueueMessage(answer, MessageQueuePosition.First);
			}
		}

		public void MessageOpened(IMessage message)
		{
			message.ThrowIfNull("message");

			var messageObject = message as Message;
			var messageAnswerObject = message as MessageAnswer;

			if (messageObject != null)
			{
				_worldInstance.RaiseEvent(messageObject.OnOpened, new MessageOpenedEvent(messageObject));
			}
			else if (messageAnswerObject != null)
			{
				_worldInstance.RaiseEvent(messageAnswerObject.OnOpened, new MessageAnswerOpenedEvent(messageAnswerObject));
			}
		}

		public void MessageClosed(IMessage message)
		{
			message.ThrowIfNull("message");

			var messageObject = message as Message;
			var messageAnswerObject = message as MessageAnswer;

			if (messageObject != null)
			{
				_worldInstance.RaiseEvent(messageObject.OnClosed, new MessageClosedEvent(messageObject));
			}
			else if (messageAnswerObject != null)
			{
				_worldInstance.RaiseEvent(messageAnswerObject.OnClosed, new MessageAnswerClosedEvent(messageAnswerObject));
			}
		}
	}
}