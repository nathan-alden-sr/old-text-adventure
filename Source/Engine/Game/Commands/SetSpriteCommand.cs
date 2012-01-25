using Junior.Common;

using TextAdventure.Engine.Objects;

namespace TextAdventure.Engine.Game.Commands
{
	public class SetSpriteCommand : Command
	{
		private readonly Sprite _sprite;
		private readonly SpriteLayer _spriteLayer;

		public SetSpriteCommand(SpriteLayer spriteLayer, Sprite sprite)
		{
			spriteLayer.ThrowIfNull("spriteLayer");
			sprite.ThrowIfNull("sprite");

			_spriteLayer = spriteLayer;
			_sprite = sprite;
		}

		protected override CommandResult OnExecute(CommandContext context)
		{
			context.ThrowIfNull("context");

			_spriteLayer.SetTile(_sprite.Coordinate, _sprite);

			return CommandResult.Succeeded;
		}
	}
}