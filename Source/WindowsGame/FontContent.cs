using Junior.Common;

using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace TextAdventure.WindowsGame
{
	public class FontContent
	{
		private readonly SpriteFont _calibri10Pt;
		private readonly SpriteFont _calibri10PtBold;
		private readonly SpriteFont _calibri12Pt;
		private readonly SpriteFont _lucidaConsole8Pt;

		public FontContent(ContentManager contentManager)
		{
			contentManager.ThrowIfNull("contentManager");

			_calibri10Pt = contentManager.Load<SpriteFont>(@"Fonts\Calibri 10pt");
			_calibri10PtBold = contentManager.Load<SpriteFont>(@"Fonts\Calibri 10pt Bold");
			_calibri12Pt = contentManager.Load<SpriteFont>(@"Fonts\Calibri 12pt");
			_lucidaConsole8Pt = contentManager.Load<SpriteFont>(@"Fonts\Lucida Console 8pt");
		}

		public SpriteFont Calibri10Pt
		{
			get
			{
				return _calibri10Pt;
			}
		}

		public SpriteFont Calibri10PtBold
		{
			get
			{
				return _calibri10PtBold;
			}
		}

		public SpriteFont Calibri12Pt
		{
			get
			{
				return _calibri12Pt;
			}
		}

		public SpriteFont LucidaConsole8Pt
		{
			get
			{
				return _lucidaConsole8Pt;
			}
		}
	}
}