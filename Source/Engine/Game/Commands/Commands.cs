using System;

using Junior.Common;

using TextAdventure.Engine.Common;
using TextAdventure.Engine.Objects;

namespace TextAdventure.Engine.Game.Commands
{
	public static class Commands
	{
		public static ActorInstanceCreateCommand ActorInstanceCreate(Board board, ActorInstance actorInstance)
		{
			return new ActorInstanceCreateCommand(board, actorInstance);
		}

		public static BoardChangeCommand BoardChange(Board board, Coordinate newPlayerCoordinate)
		{
			return new BoardChangeCommand(board, newPlayerCoordinate);
		}

		public static ActorInstanceDestroyCommand ActorInstanceDestroy(ActorInstance actorInstance)
		{
			return new ActorInstanceDestroyCommand(actorInstance);
		}

		public static ActorInstanceMoveCommand ActorInstanceMove(ActorInstance actorInstance, MoveDirection direction)
		{
			return new ActorInstanceMoveCommand(actorInstance, direction);
		}

		public static ActorInstanceRandomMoveCommand ActorInstanceRandomMove(ActorInstance actorInstance, RandomMoveDirection directions = RandomMoveDirection.AnyUnoccupied)
		{
			return new ActorInstanceRandomMoveCommand(actorInstance, directions);
		}

		public static ActorInstanceTransportCommand ActorInstanceTransport(ActorInstance actorInstance, Coordinate coordinate)
		{
			return new ActorInstanceTransportCommand(actorInstance, coordinate);
		}

		public static PlayerMoveCommand PlayerMove(MoveDirection direction)
		{
			return new PlayerMoveCommand(direction);
		}

		public static PlayerTransportCommand PlayerTransport(Coordinate coordinate)
		{
			return new PlayerTransportCommand(coordinate);
		}

		public static PlayerSuspendInputCommand PlayerSuspendInput()
		{
			return new PlayerSuspendInputCommand();
		}

		public static PlayerResumeInputCommand PlayerResumeInput()
		{
			return new PlayerResumeInputCommand();
		}

		public static LogCommand Log(string message)
		{
			return new LogCommand(message);
		}

		public static MessageCommand Message(Message message)
		{
			return new MessageCommand(message);
		}

		public static PerformTimerActionCommand PerformTimerAction(Timer timer, TimerAction action)
		{
			return new PerformTimerActionCommand(timer, action);
		}

		public static PlaySoundEffectCommand PlaySoundEffect(SoundEffect soundEffect)
		{
			return new PlaySoundEffectCommand(soundEffect);
		}

		public static PlaySoundEffectCommand PlaySoundEffect(SoundEffect soundEffect, Volume volume)
		{
			return new PlaySoundEffectCommand(soundEffect, volume);
		}

		public static PlaySongCommand PlaySong(Song song)
		{
			return new PlaySongCommand(song);
		}

		public static PlaySongCommand PlaySong(Song song, Volume volume)
		{
			return new PlaySongCommand(song, volume);
		}

		public static StopSongCommand StopSong()
		{
			return new StopSongCommand();
		}

		public static SetSpriteCommand SetSprite(SpriteLayer spriteLayer, Sprite sprite)
		{
			return new SetSpriteCommand(spriteLayer, sprite);
		}

		public static SetTileCharacterCommand SetTileCharacter(Tile tile, Character character)
		{
			return new SetTileCharacterCommand(tile, character);
		}

		public static RemoveSpriteCommand RemoveSprite(SpriteLayer spriteLayer, Coordinate coordinate)
		{
			return new RemoveSpriteCommand(spriteLayer, coordinate);
		}

		public static DelayCommand Delay(TimeSpan delay)
		{
			return new DelayCommand(delay);
		}

		public static RandomDelayCommand RandomDelay(TimeSpan minimumDelay, TimeSpan maximumDelay)
		{
			return new RandomDelayCommand(minimumDelay, maximumDelay);
		}

		public static ChainedCommand Chain(Command command)
		{
			return new ChainedCommand(command);
		}

		public static RetryCommand Retry(Command command, int maximumAttempts = MaximumAttempts.Infinite)
		{
			return new RetryCommand(command, maximumAttempts);
		}

		public static RetryCommand Retry(Command command, double retryDelayInSeconds, int maximumAttempts = MaximumAttempts.Infinite)
		{
			return new RetryCommand(command, retryDelayInSeconds, maximumAttempts);
		}

		public static RetryCommand Retry(Command command, TimeSpan retryDelay, int maximumAttempts = MaximumAttempts.Infinite)
		{
			return new RetryCommand(command, retryDelay, maximumAttempts);
		}

		public static RepeatCommand Repeat(Command command, int totalRepeats = TotalRepeats.Infinite)
		{
			return new RepeatCommand(command, totalRepeats);
		}

		public static RepeatCommand Repeat(Command command, double repeatDelayInSeconds, int totalRepeats = TotalRepeats.Infinite)
		{
			return new RepeatCommand(command, repeatDelayInSeconds, totalRepeats);
		}

		public static RepeatCommand Repeat(Command command, TimeSpan repeatDelay, int totalRepeats = TotalRepeats.Infinite)
		{
			return new RepeatCommand(command, repeatDelay, totalRepeats);
		}

		public static Command Tagged(Command command, IUnique tag)
		{
			command.ThrowIfNull("command");

			return command.WithTag(tag);
		}

		public static Command Tagged(Command command, Guid tag)
		{
			command.ThrowIfNull("command");

			return command.WithTag(tag);
		}

		public static ContextCommand Contextual(Func<CommandContext, Command> commandDelegate)
		{
			return new ContextCommand(commandDelegate);
		}
	}
}