using System;

using TextAdventure.Engine.Common;
using TextAdventure.Engine.Game.Events;
using TextAdventure.Engine.Objects;

namespace TextAdventure.Samples.Factories
{
	public class ActorInstanceFactory
	{
		public static readonly ActorInstanceFactory Instance = new ActorInstanceFactory();

		private ActorInstanceFactory()
		{
		}

		public ActorInstance CreateActorInstance(
			Actor actor,
			Coordinate coordinate,
			IEventHandler<ActorInstanceCreatedEvent> actorInstanceCreatedEventHandler = null,
			IEventHandler<ActorInstanceDestroyedEvent> actorInstanceDestroyedEventHandler = null,
			IEventHandler<ActorInstanceTouchedActorInstanceEvent> actorInstanceTouchedActorInstanceEventHandler = null,
			IEventHandler<PlayerTouchedActorInstanceEvent> playerTouchedActorInstanceEventHandler = null,
			IEventHandler<ActorInstanceMovedEvent> actorInstanceMovedEventHandler = null)
		{
			return new ActorInstance(
				Guid.NewGuid(),
				actor.Name,
				actor.Description,
				actor.Id,
				coordinate,
				actor.Character,
				actorInstanceCreatedEventHandler,
				actorInstanceDestroyedEventHandler,
				actorInstanceTouchedActorInstanceEventHandler,
				playerTouchedActorInstanceEventHandler,
				actorInstanceMovedEventHandler);
		}
	}
}