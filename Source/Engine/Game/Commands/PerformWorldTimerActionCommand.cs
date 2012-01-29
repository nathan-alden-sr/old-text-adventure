using System;
using System.Collections.Generic;
using System.Linq;

using Junior.Common;

using TextAdventure.Engine.Objects;

namespace TextAdventure.Engine.Game.Commands
{
	public class PerformWorldTimerActionCommand : Command
	{
		private readonly TimerAction _action;
		private readonly Timer _timer;

		public PerformWorldTimerActionCommand(Timer timer, TimerAction action)
		{
			timer.ThrowIfNull("timer");

			_timer = timer;
			_action = action;
		}

		public override IEnumerable<string> Details
		{
			get
			{
				yield return FormatNamedObjectDetailText("Timer", _timer);
				yield return "Action: " + _action;
			}
		}

		protected override CommandResult OnExecute(CommandContext context)
		{
			if (!context.Timers.Contains(_timer))
			{
				return CommandResult.Failed;
			}

			switch (_action)
			{
				case TimerAction.Start:
					_timer.Start();
					break;
				case TimerAction.Stop:
					_timer.Stop();
					break;
				case TimerAction.Reset:
					_timer.Reset();
					break;
				case TimerAction.Restart:
					_timer.Restart();
					break;
				default:
					throw new Exception(String.Format("Unexpected timer action '{0}'.", _action));
			}

			return CommandResult.Succeeded;
		}
	}
}