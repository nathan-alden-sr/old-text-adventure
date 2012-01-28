using System.Collections.Generic;

using Junior.Common;

using TextAdventure.Engine.Common;
using TextAdventure.Engine.Game.Events;
using TextAdventure.Engine.Game.World;
using TextAdventure.Engine.Objects;

namespace TextAdventure.Engine.Game.Commands
{
	public class ActorInstanceCreateCommand : Command
	{
		private readonly ActorInstance _actorInstance;
		private readonly Board _board;

		public ActorInstanceCreateCommand(Board board, ActorInstance actorInstance)
		{
			board.ThrowIfNull("board");
			actorInstance.ThrowIfNull("actorInstance");

			_board = board;
			_actorInstance = actorInstance;
		}

		public override IEnumerable<string> Details
		{
			get
			{
				yield return FormatNamedObjectDetailText("Actor instance", _actorInstance);
				yield return FormatIdDetailText("Actor", _actorInstance.ActorId);
			}
		}

		protected override CommandResult OnExecute(CommandContext context)
		{
			context.ThrowIfNull("context");

			ActorInstanceLayer actorInstanceLayer = _board.ActorInstanceLayer;
			Coordinate coordinate = _actorInstance.Coordinate;
			Sprite foregroundSprite = _board.ForegroundLayer[coordinate];
			ActorInstance existingActorInstance = actorInstanceLayer[coordinate];

			if (foregroundSprite != null || existingActorInstance != null || (context.CurrentBoard == _board && context.Player.Coordinate == coordinate))
			{
				return CommandResult.Failed;
			}

			EventResult result = context.RaiseEvent(_actorInstance.OnCreated, new ActorInstanceCreatedEvent(_actorInstance));

			if (result == EventResult.Canceled)
			{
				return CommandResult.Failed;
			}

			actorInstanceLayer.AddActorInstance(context.CurrentBoard, context.Player, _actorInstance);

			return CommandResult.Succeeded;
		}
	}
}