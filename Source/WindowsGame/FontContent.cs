using Junior.Common;

using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace TextAdventure.WindowsGame
{
	public class FontContent
	{
		private readonly SpriteFont _calibri;
		private readonly SpriteFont _calibriBold;
		private readonly SpriteFont _lucidaConsole;

		public FontContent(ContentManager contentManager)
		{
			contentManager.ThrowIfNull("contentManager");

			_calibri = contentManager.Load<SpriteFont>(@"Fonts\Calibri");
			_calibriBold = contentManager.Load<SpriteFont>(@"Fonts\Calibri Bold");
			_lucidaConsole = contentManager.Load<SpriteFont>(@"Fonts\Lucida Console");
		}

		public SpriteFont Calibri
		{
			get
			{
				return _calibri;
			}
		}

		public SpriteFont CalibriBold
		{
			get
			{
				return _calibriBold;
			}
		}

		public SpriteFont LucidaConsole
		{
			get
			{
				return _lucidaConsole;
			}
		}
	}
}