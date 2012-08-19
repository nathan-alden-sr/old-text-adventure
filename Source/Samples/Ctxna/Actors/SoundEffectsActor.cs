using System;

using TextAdventure.Engine.Common;
using TextAdventure.Engine.Objects;

namespace TextAdventure.Samples.Ctxna.Actors
{
	public class SoundEffectsActor : Actor
	{
		public static readonly Guid ActorId = Guid.Parse("77119d6f-af98-4698-952a-2a5ba8bd5c63");

		public SoundEffectsActor()
			: base(ActorId, "SoundEffects", "", new Character(Symbol.EighthNote, Color.LightRed, Color.TransparentBlack))
		{
		}
	}
}