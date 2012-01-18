using System.Collections.Generic;

using Junior.Common;

using TextAdventure.Engine.Objects;

namespace TextAdventure.Engine.Game.Commands
{
	public class PlaySoundEffectCommand : Command
	{
		private readonly SoundParameters _parameters;
		private readonly SoundEffect _soundEffect;

		public PlaySoundEffectCommand(SoundEffect soundEffect, SoundParameters parameters)
		{
			soundEffect.ThrowIfNull("soundEffect");
			parameters.ThrowIfNull("parameters");

			_soundEffect = soundEffect;
			_parameters = parameters;
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

			context.MultimediaPlayer.PlaySoundEffect(_soundEffect.Id, _soundEffect.Data, _parameters);

			return CommandResult.Succeeded;
		}
	}
}