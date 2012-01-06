using TextAdventure.Engine.Objects;

namespace TextAdventure.Engine.Game.Messages
{
	public interface IMessageQueue
	{
		int Count
		{
			get;
		}
		void EnqueueMessage(IMessage message, MessageQueuePosition position);
		IMessage DequeueMessage();
	}
}