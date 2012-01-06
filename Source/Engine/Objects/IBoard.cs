using System;
using System.Collections.Generic;

using TextAdventure.Engine.Common;
using TextAdventure.Engine.Game.Events;

namespace TextAdventure.Engine.Objects
{
	public interface IBoard
	{
		Guid Id
		{
			get;
		}
		Size Size
		{
			get;
		}
		ISpriteLayer BackgroundLayer
		{
			get;
		}
		ISpriteLayer ForegroundLayer
		{
			get;
		}
		IActorInstanceLayer ActorInstanceLayer
		{
			get;
		}
		IEnumerable<IBoardExit> Exits
		{
			get;
		}
		IEventHandler<BoardEnteredEvent> BoardEnteredEventHandler
		{
			get;
		}
		IEventHandler<BoardExitedEvent> BoardExitedEventHandler
		{
			get;
		}

		bool CoordinateIntersects(Coordinate coordinate);
		Coordinate GetRandomEmptyActorInstanceLayerCoordinate();
	}
}