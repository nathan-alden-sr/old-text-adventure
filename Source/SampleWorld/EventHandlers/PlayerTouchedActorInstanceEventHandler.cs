using TextAdventure.Engine.Common;
using TextAdventure.Engine.Game.Commands;
using TextAdventure.Engine.Game.Events;
using TextAdventure.Engine.Objects;

namespace TextAdventure.SampleWorld.EventHandlers
{
	public class PlayerTouchedActorInstanceEventHandler : EventHandler<PlayerTouchedActorInstanceEvent>
	{
		public override void HandleEvent(EventContext context, PlayerTouchedActorInstanceEvent @event)
		{
			Message message = Message.Build(new Color(0.5f, 0f, 0f))
				.Text("Blah blah blah", 1)
				.Text("Blah blah blah", 1)
				.Text("Blah blah blah", 1)
				.Text("Blah blah blah", 1)
				.Text("Blah blah blah");

			context.EnqueueCommand(Commands.Message(message));
		}
	}
}