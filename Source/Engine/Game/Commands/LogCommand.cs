using Junior.Common;

namespace TextAdventure.Engine.Game.Commands
{
	public class LogCommand : Command
	{
		private readonly string _message;

		public LogCommand(string message)
		{
			_message = message;
			message.ThrowIfNull("message");
		}

		public override string Title
		{
			get
			{
				return _message;
			}
		}

		protected override CommandResult OnExecute(CommandContext context)
		{
			context.ThrowIfNull("context");

			return CommandResult.Succeeded;
		}
	}
}