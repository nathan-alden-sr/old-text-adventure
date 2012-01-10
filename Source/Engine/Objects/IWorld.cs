using System;
using System.Collections.Generic;

using TextAdventure.Engine.Game.Events;

namespace TextAdventure.Engine.Objects
{
	public interface IWorld
	{
		Guid Id
		{
			get;
		}
		int Version
		{
			get;
		}
		IPlayer StartingPlayer
		{
			get;
		}
		IEnumerable<IBoard> Boards
		{
			get;
		}
		IEnumerable<IActor> Actors
		{
			get;
		}
		IEnumerable<IMessage> Messages
		{
			get;
		}
		IEnumerable<ITimer> Timers
		{
			get;
		}
		IEventHandler<AnswerSelectedEvent> AnswerSelectedEventHandler
		{
			get;
		}

		IBoard GetBoardById(Guid id);
		IActor GetActorById(Guid id);
		IMessage GetMessageById(Guid id);
		ITimer GetTimerById(Guid id);
	}
}