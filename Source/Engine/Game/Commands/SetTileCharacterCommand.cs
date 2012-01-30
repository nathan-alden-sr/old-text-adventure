using Junior.Common;

using TextAdventure.Engine.Objects;

namespace TextAdventure.Engine.Game.Commands
{
	public class SetTileCharacterCommand : Command
	{
		private readonly Character _character;
		private readonly Tile _tile;

		public SetTileCharacterCommand(Tile tile, Character character)
		{
			tile.ThrowIfNull("sprite");
			character.ThrowIfNull("character");

			_tile = tile;
			_character = character;
		}

		protected override CommandResult OnExecute(CommandContext context)
		{
			_tile.Character = _character;

			return CommandResult.Succeeded;
		}
	}
}