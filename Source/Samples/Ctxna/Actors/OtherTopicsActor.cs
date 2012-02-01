using System;

using TextAdventure.Engine.Common;
using TextAdventure.Engine.Objects;

namespace TextAdventure.Samples.Ctxna.Actors
{
	public class OtherTopicsActor : Actor
	{
		public static readonly Guid ActorId = Guid.Parse("2c121ca1-cce1-4abb-8834-b71f60294633");

		public OtherTopicsActor()
			: base(ActorId, "OtherTopics", "", new Character(Symbol.OutlinedSmiley, Color.White, Color.TransparentBlack))
		{
		}
	}
}