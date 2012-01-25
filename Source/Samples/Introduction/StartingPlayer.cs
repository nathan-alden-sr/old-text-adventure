using System;

using TextAdventure.Engine.Common;
using TextAdventure.Engine.Objects;
using TextAdventure.Samples.Introduction.Boards;

namespace TextAdventure.Samples.Introduction
{
	public class StartingPlayer : Player
	{
		public static readonly Guid PlayerId = Guid.Parse("65747269-dc21-4c0b-acb4-bb66f3855be3");

		public StartingPlayer()
			: base(PlayerId, WelcomeBoard.BoardId, new Coordinate(WelcomeBoard.BoardSize.Width / 2, 12), new Character(Symbol.FilledSmiley, Color.White, Color.DarkBlue))
		{
		}
	}
}