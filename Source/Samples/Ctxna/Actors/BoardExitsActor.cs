using System;

using TextAdventure.Engine.Common;
using TextAdventure.Engine.Objects;

namespace TextAdventure.Samples.Ctxna.Actors
{
	public class BoardExitsActor : Actor
	{
		public static readonly Guid ActorId = Guid.Parse("470c59ee-82e5-4e6e-ad52-2ca63c0dcbfa");

		public BoardExitsActor()
			: base(ActorId, "BoardExits", "", new Character(Symbol.Yen, Color.Yellow, Color.TransparentBlack))
		{
		}
	}
}