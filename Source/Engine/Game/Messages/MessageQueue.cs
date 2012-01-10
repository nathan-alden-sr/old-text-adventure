using System;
using System.Collections.Generic;
using System.Linq;

using Junior.Common;

using TextAdventure.Engine.Objects;

namespace TextAdventure.Engine.Game.Messages
{
	public class MessageQueue
	{
		private readonly List<IMessage> _list = new List<IMessage>();

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
	}
}