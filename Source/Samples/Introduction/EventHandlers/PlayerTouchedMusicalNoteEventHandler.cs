using System;

using TextAdventure.Engine.Game.Commands;
using TextAdventure.Engine.Game.Events;
using TextAdventure.Engine.Objects;

namespace TextAdventure.Samples.Introduction.EventHandlers
{
	public class PlayerTouchedMusicalNoteEventHandler : Engine.Game.Events.EventHandler<PlayerTouchedActorInstanceEvent>
	{
		private bool _playing;

		public override void HandleEvent(EventContext context, PlayerTouchedActorInstanceEvent @event)
		{
			if (!_playing)
			{
				Song song = context.GetSongById(Guid.Parse("9c01844c-4786-4a93-8092-66036666bf2f"));

				context.EnqueueCommand(Commands.PlaySong(song, 0.3f));
			}
			else
			{
				context.EnqueueCommand(Commands.StopSong());
			}

			_playing = !_playing;
		}
	}
}