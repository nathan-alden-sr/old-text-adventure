using System.Collections.Generic;

using Junior.Common;

using TextAdventure.Engine.Objects;

namespace TextAdventure.Engine.Game.Commands
{
	public class PlaySoundEffectCommand : Command
	{
		private readonly SoundEffect _soundEffect;
		private readonly Volume _volume;

		public PlaySoundEffectCommand(SoundEffect soundEffect, Volume volume)
		{
			soundEffect.ThrowIfNull("soundEffect");

			_soundEffect = soundEffect;
			_volume = volume;
		}

		public override IEnumerable<string> Details
		{
			get
			{
				yield return FormatNamedObjectDetailText("Sound effect", _soundEffect);
			}
		}

		protected override CommandResult OnExecute(CommandContext context)
		{
			context.ThrowIfNull("context");

			context.MultimediaPlayer.PlaySoundEffect(_soundEffect.Id, _soundEffect.Data, _volume);

			return CommandResult.Succeeded;
		}
	}
}