using System;
using System.Collections.Generic;

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

		public override IEnumerable<string> Details
		{
			get
			{
				yield return "Coordinate: " + _tile.Coordinate;
				yield return String.Format("Symbol: 0x{0} ({1})", _character.Symbol.ToString("X2"), _character.Symbol);
				yield return "Foreground color: " + _character.ForegroundColor;
				yield return "Background color: " + _character.BackgroundColor;
			}
		}

		protected override CommandResult OnExecute(CommandContext context)
		{
			_tile.Character = _character;

			return CommandResult.Succeeded;
		}
	}
}