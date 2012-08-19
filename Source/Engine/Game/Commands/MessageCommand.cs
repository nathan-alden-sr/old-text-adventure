using System.Collections.Generic;

using Junior.Common;

using TextAdventure.Engine.Game.Messages;
using TextAdventure.Engine.Objects;

namespace TextAdventure.Engine.Game.Commands
{
	public class MessageCommand : Command
	{
		private readonly Message _message;

		public MessageCommand(Message message)
		{
			message.ThrowIfNull("message");

			_message = message;
		}

		public override IEnumerable<string> Details
		{
			get
			{
				yield return FormatNamedObjectDetailText("Message", _message);
			}
		}

		protected override CommandResult OnExecute(CommandContext context)
		{
			context.ThrowIfNull("context");

			context.EnqueueMessage(_message, MessageQueuePosition.Last);

			return CommandResult.Succeeded;
		}
	}
}