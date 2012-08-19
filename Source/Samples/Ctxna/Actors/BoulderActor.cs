using System;

using TextAdventure.Engine.Common;
using TextAdventure.Engine.Objects;

namespace TextAdventure.Samples.Ctxna.Actors
{
	public class BoulderActor : Actor
	{
		public static readonly Guid ActorId = Guid.Parse("bce1ea8b-d657-476b-a186-8f59e61844b1");

		public BoulderActor()
			: base(ActorId, "Boulder", "", new Character(Symbol.Square, new Color(162, 255, 141), Color.TransparentBlack))
		{
		}
	}
}