using System.Collections.Generic;

namespace TextAdventure.Engine.Game
{
	public interface ILoggable
	{
		string Title
		{
			get;
		}
		IEnumerable<string> Details
		{
			get;
		}
	}
}