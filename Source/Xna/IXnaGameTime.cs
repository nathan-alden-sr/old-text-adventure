using System;

namespace TextAdventure.Xna
{
	public interface IXnaGameTime
	{
		TimeSpan TotalGameTime
		{
			get;
		}
		TimeSpan ElapsedGameTime
		{
			get;
		}
	}
}