using System.Collections.Generic;

using Junior.Common;

using TextAdventure.Engine.Common;
using TextAdventure.Engine.Objects;

namespace TextAdventure.Engine.Game.Commands
{
	public class ActorInstanceTransportCommand : Command
	{
		private readonly ActorInstance _actorInstance;
		private readonly Coordinate _coordinate;

		public ActorInstanceTransportCommand(ActorInstance actorInstance, Coordinate coordinate)
		{
			actorInstance.ThrowIfNull("actorInstance");

			_actorInstance = actorInstance;
			_coordinate = coordinate;
		}

		public override IEnumerable<string> Details
		{
			get
			{
				yield return "Coordinate: " + _coordinate;
			}
		}

		protected override CommandResult OnExecute(CommandContext context)
		{
			context.ThrowIfNull("context");

			return _actorInstance.ChangeCoordinate(context.CurrentBoard, context.Player, _coordinate) ? CommandResult.Succeeded : CommandResult.Failed;
		}
	}
}