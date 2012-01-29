using System;

using TextAdventure.Engine.Common;
using TextAdventure.Engine.Objects;

namespace TextAdventure.Samples.Ctxna.Actors
{
	public class ActorsActor : Actor
	{
		public static readonly Guid ActorId = Guid.Parse("5685496a-896e-45b8-baf4-8b60dc425dc8");

		public ActorsActor()
			: base(ActorId, "Actors", "", new Character(Symbol.LowercaseDelta, Color.Yellow, Color.TransparentBlack))
		{
		}
	}
}