using System;

using TextAdventure.Engine.Game.Commands;
using TextAdventure.Engine.Game.Events;
using TextAdventure.Engine.Objects;

namespace TextAdventure.Samples.Introduction.EventHandlers
{
	public class PlayerTouchedActorInstanceEventHandler1 : Engine.Game.Events.EventHandler<PlayerTouchedActorInstanceEvent>
	{
		public override void HandleEvent(EventContext context, PlayerTouchedActorInstanceEvent @event)
		{
			SoundEffect soundEffect = context.GetSoundEffectById(Guid.Parse("4f095264-d8bb-49da-b1bb-38c81e4a90a9"));

			context.EnqueueCommand(Commands.PlaySoundEffect(soundEffect));

			Message message = context.GetMessageById(Guid.Parse("889d17fa-04ca-4eca-aa36-18589ce5f958"));

			context.EnqueueCommand(Commands.Message(message));
		}
	}
}