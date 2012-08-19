using System.Collections.Generic;
using System.Linq;

using Junior.Common;

using TextAdventure.Engine.Common;
using TextAdventure.Engine.Objects;

namespace TextAdventure.Samples.Factories
{
	public class SpriteFactory
	{
		public static readonly SpriteFactory Instance = new SpriteFactory();

		private SpriteFactory()
		{
		}

		public IEnumerable<Sprite> CreateArea(Coordinate originCoordinate, Size size, Character character)
		{
			character.ThrowIfNull("character");

			for (int x = 0; x < size.Width; x++)
			{
				for (int y = 0; y < size.Height; y++)
				{
					yield return new Sprite(new Coordinate(originCoordinate.X + x, originCoordinate.Y + y), character);
				}
			}
		}

		public IEnumerable<Sprite> CreateBorder(Coordinate originCoordinate, Size size, Character character)
		{
			character.ThrowIfNull("character");

			for (int x = 0; x < size.Width; x++)
			{
				yield return new Sprite(new Coordinate(originCoordinate.X + x, originCoordinate.Y), character);
				yield return new Sprite(new Coordinate(originCoordinate.X + x, originCoordinate.Y + size.Height - 1), character);
			}
			for (int y = 1; y < size.Height - 1; y++)
			{
				yield return new Sprite(new Coordinate(originCoordinate.X, originCoordinate.Y + y), character);
				yield return new Sprite(new Coordinate(originCoordinate.X + size.Width - 1, originCoordinate.Y + y), character);
			}
		}

		public IEnumerable<Sprite> CreateText(string text, Coordinate originCoordinate, Color foregroundColor, Color backgroundColor)
		{
			int x = originCoordinate.X;

			return text.Select(arg => new Sprite(new Coordinate(x++, originCoordinate.Y), new Character((byte)arg, foregroundColor, backgroundColor)));
		}

		public IEnumerable<Sprite> CreateCenteredText(string text, int originY, int width, Color foregroundColor, Color backgroundColor)
		{
			return CreateText(text, new Coordinate((width / 2) - (text.Length / 2), originY), foregroundColor, backgroundColor);
		}
	}
}