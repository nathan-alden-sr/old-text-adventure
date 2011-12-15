using System;

namespace TextAdventure.Engine.Game.Commands
{
	[Flags]
	public enum RandomMoveDirection
	{
		Up = 1,
		Down = 2,
		Left = 4,
		Right = 8,
		All = Up | Down | Left | Right
	}
}