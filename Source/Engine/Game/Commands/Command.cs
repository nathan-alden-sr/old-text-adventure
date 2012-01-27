using System;
using System.Collections.Generic;

using Junior.Common;

using TextAdventure.Engine.Objects;

namespace TextAdventure.Engine.Game.Commands
{
	public abstract class Command : IUnique, ILoggable
	{
		private readonly Guid _id;
		private bool _paused;

		protected Command()
		{
			_id = Guid.NewGuid();
			Status = CommandStatus.Queued;
		}

		public CommandStatus Status
		{
			get;
			protected set;
		}

		public bool Paused
		{
			get
			{
				return _paused;
			}
			set
			{
				SetPaused(this, value);
			}
		}

		public Guid? Tag
		{
			get;
			private set;
		}

		public virtual IEnumerable<Command> NestedCommands
		{
			get
			{
				yield break;
			}
		}

		public virtual string Title
		{
			get
			{
				string typeName = GetType().Name;

				if (typeName.EndsWith("Command"))
				{
					typeName = typeName.Substring(0, typeName.Length - 7);
				}

				return typeName;
			}
		}

		public virtual IEnumerable<string> Details
		{
			get
			{
				yield break;
			}
		}

		public Guid Id
		{
			get
			{
				return _id;
			}
		}

		protected virtual void Reset()
		{
			Status = CommandStatus.Executing;
		}

		protected abstract CommandResult OnExecute(CommandContext context);

		public virtual ChainedCommand And(Command command)
		{
			var chainedCommand = new ChainedCommand(this);

			chainedCommand.And(this);

			return chainedCommand;
		}

		public CommandResult Execute(CommandContext context)
		{
			if (Paused)
			{
				return CommandResult.Deferred;
			}

			switch (Status)
			{
				case CommandStatus.Queued:
				case CommandStatus.Complete:
					Reset();
					break;
			}

			CommandResult result = OnExecute(context);

			if (result == CommandResult.Deferred)
			{
				return CommandResult.Deferred;
			}

			Status = CommandStatus.Complete;

			return result;
		}

		public void Pause()
		{
			SetPaused(this, true);
		}

		public void Resume()
		{
			SetPaused(this, false);
		}

		public void TogglePaused()
		{
			if (Paused)
			{
				Resume();
			}
			else
			{
				Pause();
			}
		}

		public RetryCommand Retry(int maximumAttempts = MaximumAttempts.Infinite)
		{
			return new RetryCommand(this, maximumAttempts);
		}

		public RetryCommand Retry(double retryDelayInSeconds, int maximumAttempts = MaximumAttempts.Infinite)
		{
			return new RetryCommand(this, retryDelayInSeconds, maximumAttempts);
		}

		public RetryCommand Retry(TimeSpan retryDelay, int maximumAttempts = MaximumAttempts.Infinite)
		{
			return new RetryCommand(this, retryDelay, maximumAttempts);
		}

		public RepeatCommand Repeat(int totalRepeats = TotalRepeats.Infinite)
		{
			return new RepeatCommand(this, totalRepeats);
		}

		public RepeatCommand Repeat(double retryDelayInSeconds, int totalRepeats = TotalRepeats.Infinite)
		{
			return new RepeatCommand(this, retryDelayInSeconds, totalRepeats);
		}

		public RepeatCommand Repeat(TimeSpan repeatDelay, int totalRepeats = TotalRepeats.Infinite)
		{
			return new RepeatCommand(this, repeatDelay, totalRepeats);
		}

		public Command WithTag(IUnique tag)
		{
			Tag = tag.IfNotNull(arg => (Guid?)arg.Id);

			return this;
		}

		public Command WithTag(Guid? tag)
		{
			Tag = tag;

			return this;
		}

		protected static string FormatIdDetailText(string prefix, Guid id)
		{
			prefix.ThrowIfNull("prefix");

			return DetailTextFormatter.Instance.FormatId(prefix, id);
		}

		protected static string FormatUniqueDetailText(string prefix, IUnique unique)
		{
			prefix.ThrowIfNull("prefix");
			unique.ThrowIfNull("unique");

			return DetailTextFormatter.Instance.FormatUnique(prefix, unique);
		}

		protected static string FormatNamedObjectDetailText(string prefix, INamedObject namedObject)
		{
			prefix.ThrowIfNull("prefix");
			namedObject.ThrowIfNull("namedObject");

			return namedObject.Name.Length == 0
			       	? DetailTextFormatter.Instance.FormatUnique(prefix, namedObject)
			       	: DetailTextFormatter.Instance.FormatNamedObject(prefix, namedObject);
		}

		private static void SetPaused(Command command, bool paused)
		{
			command._paused = paused;

			foreach (Command nestedCommand in command.NestedCommands)
			{
				SetPaused(nestedCommand, paused);
			}
		}
	}
}