using System;

using TextAdventure.Engine.Objects;

namespace TextAdventure.Samples.Ctxna.SoundEffects
{
	public class WindowsUserAccountControlSoundEffect : SoundEffect
	{
		public static readonly Guid SoundEffectId = Guid.Parse("dff9338d-ef90-444e-aeea-367d65b46876");

		public WindowsUserAccountControlSoundEffect()
			: base(SoundEffectId, "WindowsUserAccountControl", "", SoundEffects.Windows_User_Account_Control.GetBuffer())
		{
		}
	}
}