using Junior.Common;

using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace TextAdventure.WindowsGame
{
	public class FontContent
	{
		private readonly SpriteFont _calibri10pt;
		private readonly SpriteFont _calibri10ptBold;
		private readonly SpriteFont _calibri12pt;
		private readonly SpriteFont _lucidaConsole8pt;

		public FontContent(ContentManager contentManager)
		{
			contentManager.ThrowIfNull("contentManager");

			_calibri10pt = contentManager.Load<SpriteFont>(@"Fonts\Calibri 10pt");
			_calibri10ptBold = contentManager.Load<SpriteFont>(@"Fonts\Calibri 10pt Bold");
			_calibri12pt = contentManager.Load<SpriteFont>(@"Fonts\Calibri 12pt");
			_lucidaConsole8pt = contentManager.Load<SpriteFont>(@"Fonts\Lucida Console 8pt");
		}

		public SpriteFont Calibri10pt
		{
			get
			{
				return _calibri10pt;
			}
		}

		public SpriteFont Calibri10ptBold
		{
			get
			{
				return _calibri10ptBold;
			}
		}

		public SpriteFont Calibri12pt
		{
			get
			{
				return _calibri12pt;
			}
		}

		public SpriteFont LucidaConsole8pt
		{
			get
			{
				return _lucidaConsole8pt;
			}
		}
	}
}