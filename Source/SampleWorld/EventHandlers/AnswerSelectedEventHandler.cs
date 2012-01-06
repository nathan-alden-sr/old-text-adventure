using System;

using TextAdventure.Engine.Common;
using TextAdventure.Engine.Game.Commands;
using TextAdventure.Engine.Game.Events;
using TextAdventure.Engine.Objects;

namespace TextAdventure.SampleWorld.EventHandlers
{
	public class AnswerSelectedEventHandler : Engine.Game.Events.EventHandler<AnswerSelectedEvent>
	{
		public override void HandleEvent(IEventContext context, AnswerSelectedEvent @event)
		{
			if (@event.AnswerId == Guid.Parse("5248e964-2d33-4f88-a743-a490baa91cb3"))
			{
				MessageCommand command = Commands.Message(
					Message.Build(Color.Magenta)
						.Text("Yes!"));

				context.EnqueueCommand(command);
			}
			else if (@event.AnswerId == Guid.Parse("ab09f937-80f2-4b63-b379-b72ba670464b"))
			{
				MessageCommand command = Commands.Message(
					Message.Build(Color.White)
						.Color(Color.Black)
						.Text("No!"));

				context.EnqueueCommand(command);
			}
		}
	}
}