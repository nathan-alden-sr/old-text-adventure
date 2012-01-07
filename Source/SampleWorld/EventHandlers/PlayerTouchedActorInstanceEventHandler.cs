using System;

using TextAdventure.Engine.Common;
using TextAdventure.Engine.Game.Commands;
using TextAdventure.Engine.Game.Events;
using TextAdventure.Engine.Objects;

namespace TextAdventure.SampleWorld.EventHandlers
{
	public class PlayerTouchedActorInstanceEventHandler : Engine.Game.Events.EventHandler<PlayerTouchedActorInstanceEvent>
	{
		public override void HandleEvent(IEventContext context, PlayerTouchedActorInstanceEvent @event)
		{
			Message message = Message.Build(new Color(0f, 0f, 0.5f))
				.Color(Color.White)
				.Text("Hello, ")
				.Color(Color.Yellow)
				.Text("Nathan")
				.Color(Color.White)
				.Text("! I have an interesting story to tell you, if you can spare some time. However, I must warn you that this tale could take a long time to tell. You should make yourself comfortable.")
				.LineBreak()
				.LineBreak()
				.Text("Lorem ipsum")
				.LineBreak()
				.Text("Lorem ipsum")
				.LineBreak()
				.Text("Lorem ipsum")
				.LineBreak()
				.Text("Lorem ipsum")
				.LineBreak()
				.Text("Lorem ipsum")
				.LineBreak()
				.Text("Lorem ipsum")
				.LineBreak()
				.Text("Lorem ipsum")
				.LineBreak()
				.Text("Lorem ipsum")
				.LineBreak()
				.Text("Lorem ipsum")
				.LineBreak()
				.Text("Lorem ipsum")
				.LineBreak()
				.Text("Lorem ipsum")
				.LineBreak()
				.Text("Lorem ipsum")
				.LineBreak()
				.Text("Lorem ipsum")
				.LineBreak()
				.Text("Lorem ipsum")
				.LineBreak()
				.Text("Lorem ipsum")
				.LineBreak()
				.Question(
					"Listen to the traveler's story?",
					Color.Cyan,
					Color.White,
					Color.TransparentBlack,
					Color.White,
					new Color(Color.Green, 0.75f),
					MessageAnswer.Build(Guid.Parse("5248e964-2d33-4f88-a743-a490baa91cb3"), "Yes"),
					MessageAnswer.Build(Guid.Parse("ab09f937-80f2-4b63-b379-b72ba670464b"), "No"));
			var command = new MessageCommand(message);

			context.EnqueueCommand(command);
		}
	}
}