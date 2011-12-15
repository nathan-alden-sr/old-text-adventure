using System.Collections.Generic;

using TextAdventure.Engine.Common;

namespace TextAdventure.Engine.Game.Commands
{
	public class PlayerTransportCommand : Command
	{
		private readonly Coordinate _coordinate;

		public PlayerTransportCommand(Coordinate coordinate)
		{
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
			return context.Player.ChangeLocation(context.CurrentBoard, _coordinate) ? CommandResult.Succeeded : CommandResult.Failed;
		}
	}
}