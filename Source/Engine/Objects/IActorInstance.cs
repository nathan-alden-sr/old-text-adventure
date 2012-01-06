using System;

using TextAdventure.Engine.Game.Events;

namespace TextAdventure.Engine.Objects
{
	public interface IActorInstance : ISprite, IUnique
	{
		Guid ActorId
		{
			get;
		}
		IEventHandler<ActorInstanceCreatedEvent> ActorInstanceCreatedEventHandler
		{
			get;
		}
		IEventHandler<ActorInstanceDestroyedEvent> ActorInstanceDestroyedEventHandler
		{
			get;
		}
		IEventHandler<ActorInstanceTouchedActorInstanceEvent> ActorInstanceTouchedActorInstanceEventHandler
		{
			get;
		}
		IEventHandler<PlayerTouchedActorInstanceEvent> PlayerTouchedActorInstanceEventHandler
		{
			get;
		}
		IEventHandler<ActorInstanceMovedEvent> ActorInstanceMovedEventHandler
		{
			get;
		}
	}
}