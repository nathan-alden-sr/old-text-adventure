using TextAdventure.Engine.Common;
using TextAdventure.Engine.Game.Commands;
using TextAdventure.Engine.Game.Events;
using TextAdventure.Engine.Objects;

namespace TextAdventure.SampleWorld.EventHandlers
{
	public class TimerElapsedEventHandler : EventHandler<TimerElapsedEvent>
	{
		public override void HandleEvent(EventContext context, TimerElapsedEvent @event)
		{
			Message message = Message.Build(new Color(0f, 0f, 0.5f))
				.Text("Timer!")
				.Message;

			context.EnqueueCommand(Commands.Message(message));
		}
	}
}