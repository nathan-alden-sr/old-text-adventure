using TextAdventure.Engine.Common;

namespace TextAdventure.Engine.Objects
{
	public class Character : ICharacter
	{
		public Character(
			byte symbol,
			Color foregroundColor,
			Color backgroundColor)
		{
			Symbol = symbol;
			ForegroundColor = foregroundColor;
			BackgroundColor = backgroundColor;
		}

		public byte Symbol
		{
			get;
			set;
		}

		public Color ForegroundColor
		{
			get;
			set;
		}

		public Color BackgroundColor
		{
			get;
			set;
		}
	}
}