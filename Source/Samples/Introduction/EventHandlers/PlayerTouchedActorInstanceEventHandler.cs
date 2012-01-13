using System;

using TextAdventure.Engine.Game.Commands;
using TextAdventure.Engine.Game.Events;
using TextAdventure.Engine.Objects;

namespace TextAdventure.Samples.Introduction.EventHandlers
{
	public class PlayerTouchedActorInstanceEventHandler : Engine.Game.Events.EventHandler<PlayerTouchedActorInstanceEvent>
	{
		public override void HandleEvent(EventContext context, PlayerTouchedActorInstanceEvent @event)
		{
			Message message = context.GetMessageById(Guid.Parse("889d17fa-04ca-4eca-aa36-18589ce5f958"));

			context.EnqueueCommand(Commands.Message(message));
		}
	}
}