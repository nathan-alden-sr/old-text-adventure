using System;

using Junior.Common;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

using TextAdventure.Engine.Game.Commands;
using TextAdventure.Engine.Game.Events;
using TextAdventure.Engine.Game.World;
using TextAdventure.WindowsGame.Managers;

namespace TextAdventure.WindowsGame.Components
{
	public class WorldObserverComponent : TextAdventureGameComponent, IWorldObserver
	{
		private LogComponent _logComponent;
		private TimeSpan _totalGameTime;

		public WorldObserverComponent(GameManager gameManager)
			: base(gameManager)
		{
		}

		public void CommandExecuting(Command command)
		{
			command.ThrowIfNull("command");
		}

		public void CommandExecuted(Command command, CommandResult result)
		{
			command.ThrowIfNull("command");

			_logComponent.EnqueueCommandExecutedLogEntry(_totalGameTime, command, result);
		}

		public void EventRaising(Event @event)
		{
			@event.ThrowIfNull("event");

			_logComponent.EnqueueEventRaisingLogEntry(_totalGameTime, @event);
		}

		public void EventRaised(Event @event)
		{
			@event.ThrowIfNull("@event");
		}

		public void EventHandled<TEvent>(IEventHandler<TEvent> eventHandler, TEvent @event, EventResult result)
			where TEvent : Event
		{
			@event.ThrowIfNull("event");
			eventHandler.ThrowIfNull("eventHandler");

			_logComponent.EnqueueEventHandledLogEntry(_totalGameTime, eventHandler, @event, result);
		}

		protected override void AddGameComponents()
		{
			_logComponent = new LogComponent(GameManager);

			Game.Components.Add(_logComponent);

			base.AddGameComponents();
		}

		public override void Update(GameTime gameTime)
		{
			_totalGameTime = gameTime.TotalGameTime;

			KeyboardState keyboardState = Keyboard.GetState();

			if (keyboardState.IsKeyDown(Keys.Delete))
			{
				_logComponent.ClearLog();
			}

			base.Update(gameTime);
		}
	}
}