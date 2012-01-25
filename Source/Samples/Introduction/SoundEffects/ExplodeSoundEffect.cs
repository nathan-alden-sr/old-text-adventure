using System;

using TextAdventure.Engine.Objects;

namespace TextAdventure.Samples.Introduction.SoundEffects
{
	public class ExplodeSoundEffect : SoundEffect
	{
		public static readonly Guid SoundEffectId = Guid.Parse("3f62e2bd-4218-48e4-9710-728203bf0da6");

		public ExplodeSoundEffect()
			: base(SoundEffectId, "Explode", "", SoundEffects.Explode)
		{
		}
	}
}