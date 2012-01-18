using Junior.Common;

using TextAdventure.Engine.Objects;

namespace TextAdventure.Engine.Game.Commands
{
	public class PlaySoundEffectCommand : Command
	{
		private readonly SoundEffect _soundEffect;

		public PlaySoundEffectCommand(SoundEffect soundEffect)
		{
			soundEffect.ThrowIfNull("soundEffect");

			_soundEffect = soundEffect;
		}

		protected override CommandResult OnExecute(CommandContext context)
		{
			context.ThrowIfNull("context");

			context.MultimediaPlayer.PlaySoundEffect(_soundEffect.Id, _soundEffect.Data);

			return CommandResult.Succeeded;
		}
	}
}