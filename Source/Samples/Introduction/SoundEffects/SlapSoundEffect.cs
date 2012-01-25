using System;

using TextAdventure.Engine.Objects;

namespace TextAdventure.Samples.Introduction.SoundEffects
{
	public class SlapSoundEffect : SoundEffect
	{
		public static readonly Guid SoundEffectId = Guid.Parse("61b53764-2a86-467d-b067-d49254b0b75f");

		public SlapSoundEffect()
			: base(SoundEffectId, "Slap", "", SoundEffects.Slap)
		{
		}
	}
}