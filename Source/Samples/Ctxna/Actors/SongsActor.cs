using System;

using TextAdventure.Engine.Common;
using TextAdventure.Engine.Objects;

namespace TextAdventure.Samples.Ctxna.Actors
{
	public class SongsActor : Actor
	{
		public static readonly Guid ActorId = Guid.Parse("652993f0-d1f9-4338-9a8a-e2371ba3f50f");

		public SongsActor()
			: base(ActorId, "Songs", "", new Character(Symbol.BarredEighthNote, Color.LightYellow, Color.TransparentBlack))
		{
		}
	}
}