using System;

using TextAdventure.Engine.Common;
using TextAdventure.Engine.Objects;

namespace TextAdventure.Samples.Ctxna.Actors
{
	public class MessagesActor : Actor
	{
		public static readonly Guid ActorId = Guid.Parse("8eeee2c9-03f8-447d-baf1-db4e27e50cd5");

		public MessagesActor()
			: base(ActorId, "Messages", "", new Character(Symbol.FilledSmiley, Color.MultiplyAlpha(Color.White, 0.5f), Color.TransparentBlack))
		{
		}
	}
}