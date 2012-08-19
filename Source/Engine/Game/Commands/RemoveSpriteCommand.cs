using Junior.Common;

using TextAdventure.Engine.Common;
using TextAdventure.Engine.Objects;

namespace TextAdventure.Engine.Game.Commands
{
	public class RemoveSpriteCommand : Command
	{
		private readonly Coordinate _coordinate;
		private readonly SpriteLayer _spriteLayer;

		public RemoveSpriteCommand(SpriteLayer spriteLayer, Coordinate coordinate)
		{
			spriteLayer.ThrowIfNull("spriteLayer");

			_spriteLayer = spriteLayer;
			_coordinate = coordinate;
		}

		protected override CommandResult OnExecute(CommandContext context)
		{
			context.ThrowIfNull("context");

			_spriteLayer.RemoveTile(_coordinate);

			return CommandResult.Succeeded;
		}
	}
}