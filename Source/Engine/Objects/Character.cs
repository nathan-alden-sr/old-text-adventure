using TextAdventure.Engine.Common;

namespace TextAdventure.Engine.Objects
{
	public class Character
	{
		private readonly Color _backgroundColor;
		private readonly Color _foregroundColor;
		private readonly byte _symbol;

		public Character(
			byte symbol,
			Color foregroundColor,
			Color backgroundColor)
		{
			_symbol = symbol;
			_foregroundColor = foregroundColor;
			_backgroundColor = backgroundColor;
		}

		public byte Symbol
		{
			get
			{
				return _symbol;
			}
		}

		public Color ForegroundColor
		{
			get
			{
				return _foregroundColor;
			}
		}

		public Color BackgroundColor
		{
			get
			{
				return _backgroundColor;
			}
		}
	}
}