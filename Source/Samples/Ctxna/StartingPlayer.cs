using System;

using TextAdventure.Engine.Common;
using TextAdventure.Engine.Objects;
using TextAdventure.Samples.Ctxna.Boards;

namespace TextAdventure.Samples.Ctxna
{
	public class StartingPlayer : Player
	{
		public static readonly Guid PlayerId = Guid.Parse("65747269-dc21-4c0b-acb4-bb66f3855be3");

		public StartingPlayer()
			: base(PlayerId, BoardsBoard.BoardId, new Coordinate(9, 9), new Character(Symbol.FilledSmiley, Color.White, Color.DarkBlue))
		{
		}
	}
}