using System;

using TextAdventure.Engine.Common;
using TextAdventure.Engine.Game.Events;

namespace TextAdventure.Engine.Objects
{
	public interface IPlayer : IUnique
	{
		Guid BoardId
		{
			get;
		}
		Character Character
		{
			get;
		}
		Coordinate Coordinate
		{
			get;
		}
		IEventHandler<ActorInstanceTouchedPlayerEvent> ActorInstanceTouchedPlayerEventHandler
		{
			get;
		}
	}
}