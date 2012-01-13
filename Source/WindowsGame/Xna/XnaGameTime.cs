using System;

namespace TextAdventure.WindowsGame.Xna
{
	public class XnaGameTime
	{
		private TimeSpan _elapsedGameTime;
		private TimeSpan _totalGameTime;

		public TimeSpan TotalGameTime
		{
			get
			{
				return _totalGameTime;
			}
			set
			{
				if (value < TimeSpan.Zero)
				{
					throw new ArgumentOutOfRangeException("value");
				}

				_totalGameTime = value;
			}
		}

		public TimeSpan ElapsedGameTime
		{
			get
			{
				return _elapsedGameTime;
			}
			set
			{
				if (value < TimeSpan.Zero)
				{
					throw new ArgumentOutOfRangeException("value");
				}

				_elapsedGameTime = value;
			}
		}
	}
}