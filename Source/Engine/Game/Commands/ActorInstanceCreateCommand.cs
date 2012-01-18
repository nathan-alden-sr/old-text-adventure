using System;

using Junior.Common;

using TextAdventure.Engine.Common;
using TextAdventure.Engine.Game.Events;
using TextAdventure.Engine.Game.World;
using TextAdventure.Engine.Objects;

namespace TextAdventure.Engine.Game.Commands
{
	public class ActorInstanceCreateCommand : Command
	{
		private readonly Actor _actor;
		private readonly IEventHandler<ActorInstanceCreatedEvent> _actorInstanceCreatedEventHandler;
		private readonly IEventHandler<ActorInstanceDestroyedEvent> _actorInstanceDestroyedEventHandler;
		private readonly Guid _actorInstanceId;
		private readonly IEventHandler<ActorInstanceMovedEvent> _actorInstanceMovedEventHandler;
		private readonly IEventHandler<ActorInstanceTouchedActorInstanceEvent> _actorInstanceTouchedActorInstanceEventHandler;
		private readonly Character _character;
		private readonly Coordinate _coordinate;
		private readonly IEventHandler<PlayerTouchedActorInstanceEvent> _playerTouchedActorInstanceEventHandler;

		public ActorInstanceCreateCommand(
			Actor actor,
			Guid actorInstanceId,
			Coordinate coordinate,
			Character character,
			IEventHandler<ActorInstanceCreatedEvent> actorInstanceCreatedEventHandler = null,
			IEventHandler<ActorInstanceDestroyedEvent> actorInstanceDestroyedEventHandler = null,
			IEventHandler<ActorInstanceTouchedActorInstanceEvent> actorInstanceTouchedActorInstanceEventHandler = null,
			IEventHandler<PlayerTouchedActorInstanceEvent> playerTouchedActorInstanceEventHandler = null,
			IEventHandler<ActorInstanceMovedEvent> actorInstanceMovedEventHandler = null)
		{
			actor.ThrowIfNull("actor");
			character.ThrowIfNull("character");

			_actor = actor;
			_actorInstanceId = actorInstanceId;
			_coordinate = coordinate;
			_character = character;
			_actorInstanceCreatedEventHandler = actorInstanceCreatedEventHandler;
			_actorInstanceDestroyedEventHandler = actorInstanceDestroyedEventHandler;
			_actorInstanceTouchedActorInstanceEventHandler = actorInstanceTouchedActorInstanceEventHandler;
			_playerTouchedActorInstanceEventHandler = playerTouchedActorInstanceEventHandler;
			_actorInstanceMovedEventHandler = actorInstanceMovedEventHandler;
		}

		protected override CommandResult OnExecute(CommandContext context)
		{
			context.ThrowIfNull("context");

			ActorInstanceLayer actorInstanceLayer = context.CurrentBoard.ActorInstanceLayer;
			Sprite foregroundSprite = context.CurrentBoard.ForegroundLayer[_coordinate];
			ActorInstance existingActorInstance = actorInstanceLayer[_coordinate];

			if (foregroundSprite != null || existingActorInstance != null || context.Player.Coordinate == _coordinate)
			{
				return CommandResult.Failed;
			}

			var actorInstance = new ActorInstance(
				_actorInstanceId,
				_actor.Name,
				_actor.Description,
				_actor.Id,
				_coordinate,
				_character,
				_actorInstanceCreatedEventHandler,
				_actorInstanceDestroyedEventHandler,
				_actorInstanceTouchedActorInstanceEventHandler,
				_playerTouchedActorInstanceEventHandler,
				_actorInstanceMovedEventHandler);

			EventResult result = context.RaiseEvent(actorInstance.ActorInstanceCreatedEventHandler, new ActorInstanceCreatedEvent(actorInstance));

			if (result == EventResult.Canceled)
			{
				return CommandResult.Failed;
			}

			actorInstanceLayer.AddActorInstance(context.CurrentBoard, context.Player, actorInstance);

			return CommandResult.Succeeded;
		}
	}
}