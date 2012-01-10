using System;
using System.Collections.Generic;

namespace TextAdventure.Engine.Game.Events
{
	public class AnswerSelectedEvent : Event
	{
		private readonly Guid _answerId;

		public AnswerSelectedEvent(Guid answerId)
		{
			_answerId = answerId;
		}

		public override IEnumerable<string> Details
		{
			get
			{
				yield return "ID: " + _answerId;
			}
		}

		public Guid AnswerId
		{
			get
			{
				return _answerId;
			}
		}
	}
}