using System;

using TextAdventure.Engine.Common;
using TextAdventure.Engine.Objects;

namespace TextAdventure.Samples.Ctxna.Actors
{
	public class BoardsActor : Actor
	{
		public static readonly Guid ActorId = Guid.Parse("5e5c2503-90f9-4233-a392-3c76ff39a68e");

		public BoardsActor()
			: base(ActorId, "Boards", "", new Character(Symbol.Intersection, Color.LightBrown, Color.TransparentBlack))
		{
		}
	}
}