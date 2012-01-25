using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Globalization;
using System.IO;
using System.Windows.Forms;

using NUnit.Framework;

namespace TextAdventure.IntegrationTests.FontGenerator
{
	[TestFixture]
	[Explicit]
	public class SymbolImageGenerator
	{
		private const int RegularHeight = 24;
		private static readonly char[] _characters = new[] { '\u0020', '\u263a', '\u263b', '\u2665', '\u2666', '\u2663', '\u2660', '\u2022', '\u25d8', '\u25bc', '\u25d9', '\u2642', '\u2640', '\u266a', '\u266b', '\u263c', '\u25ba', '\u25c4', '\u2195', '\u203c', '\u00b6', '\u00a7', '\u25ac', '\u21a8', '\u2191', '\u2193', '\u2192', '\u2190', '\u221f', '\u2194', '\u25b2', '\u25bc', '\u0020', '\u0021', '\u0022', '\u0023', '\u0024', '\u0025', '\u0026', '\u0027', '\u0028', '\u0029', '\u002a', '\u002b', '\u002c', '\u002d', '\u002e', '\u002f', '\u0030', '\u0031', '\u0032', '\u0033', '\u0034', '\u0035', '\u0036', '\u0037', '\u0038', '\u0039', '\u003a', '\u003b', '\u003c', '\u003d', '\u003e', '\u003f', '\u0040', '\u0041', '\u0042', '\u0043', '\u0044', '\u0045', '\u0046', '\u0047', '\u0048', '\u0049', '\u004a', '\u004b', '\u004c', '\u004d', '\u004e', '\u004f', '\u0050', '\u0051', '\u0052', '\u0053', '\u0054', '\u0055', '\u0056', '\u0057', '\u0058', '\u0059', '\u005a', '\u005b', '\u005c', '\u005d', '\u005e', '\u005f', '\u0060', '\u0061', '\u0062', '\u0063', '\u0064', '\u0065', '\u0066', '\u0067', '\u0068', '\u0069', '\u006a', '\u006b', '\u006c', '\u006d', '\u006e', '\u006f', '\u0070', '\u0071', '\u0072', '\u0073', '\u0074', '\u0075', '\u0076', '\u0077', '\u0078', '\u0079', '\u007a', '\u007b', '\u007c', '\u007d', '\u007e', '\u2302', '\u00c7', '\u00fc', '\u00e9', '\u00e2', '\u00e4', '\u00e0', '\u00e5', '\u00e7', '\u00ea', '\u00eb', '\u00e8', '\u00ef', '\u00ee', '\u00ec', '\u00c4', '\u00c5', '\u00c9', '\u00e6', '\u00c6', '\u00f4', '\u00f6', '\u00f2', '\u00fb', '\u00f9', '\u00ff', '\u00d6', '\u00dc', '\u00a2', '\u00a3', '\u00a5', '\u20a7', '\u0192', '\u00e1', '\u00ed', '\u00f3', '\u00fa', '\u00f1', '\u00d1', '\u00aa', '\u00ba', '\u00bf', '\u2310', '\u00ac', '\u00db', '\u00dc', '\u00a1', '\u00ab', '\u00bb', '\u2591', '\u2592', '\u2593', '\u2502', '\u2524', '\u2561', '\u2562', '\u2556', '\u2555', '\u2563', '\u2551', '\u2557', '\u255d', '\u255c', '\u255b', '\u2510', '\u2514', '\u2534', '\u252c', '\u251c', '\u2500', '\u253c', '\u255e', '\u255f', '\u255a', '\u2554', '\u2569', '\u2566', '\u2560', '\u2550', '\u256c', '\u2567', '\u2568', '\u2564', '\u2565', '\u2559', '\u2558', '\u2552', '\u2553', '\u256b', '\u256a', '\u2518', '\u250c', '\u2588', '\u2584', '\u258c', '\u2590', '\u2580', '\u03ba', '\u00df', '\u0393', '\u03c0', '\u03a3', '\u03c3', '\u00b5', '\u03c4', '\u03a6', '\u0398', '\u03a9', '\u03b4', '\u221e', '\u03c6', '\u03b5', '\u2229', '\u2261', '\u00b1', '\u2265', '\u2264', '\u2320', '\u2321', '\u00f7', '\u2248', '\u00b0', '\u2219', '\u00b7', '\u221a', '\u207f', '\u00b2', '\u25a0', '\u00a0' };

		private static void GenerateImage(int fontHeight, int symbolsWide, Color backgroundColor, int imagePadding, int symbolPadding)
		{
			const TextFormatFlags textFormatFlags = TextFormatFlags.NoPadding | TextFormatFlags.NoPrefix;
			var font = new Font("Lucida Console", fontHeight, FontStyle.Regular, GraphicsUnit.Pixel);
			Image measureImage = new Bitmap(1, 1);
			Size characterSize;

			using (Graphics graphics = Graphics.FromImage(measureImage))
			{
				characterSize = TextRenderer.MeasureText(graphics, '\u2588'.ToString(CultureInfo.InvariantCulture), font, Size.Empty, textFormatFlags);
			}

			measureImage.Dispose();

			var symbolsHigh = (int)Math.Ceiling(256.0 / symbolsWide);
			Image fontImage = new Bitmap(
				(imagePadding * 2) + (characterSize.Width * symbolsWide) + (symbolPadding * 2 * (symbolsWide - 1)),
				(imagePadding * 2) + (characterSize.Height * symbolsHigh) + (symbolPadding * 2 * (symbolsHigh - 1)),
				PixelFormat.Format32bppArgb);

			using (Graphics graphics = Graphics.FromImage(fontImage))
			{
				using (Brush brush = new SolidBrush(backgroundColor))
				{
					graphics.FillRectangle(brush, 0, 0, fontImage.Width, fontImage.Height);
				}

				int index = 0;

				for (int y = 0; index < _characters.Length; y++)
				{
					for (int x = 0; x < symbolsWide && index < _characters.Length; x++, index++)
					{
						graphics.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;

						string character = _characters[index].ToString(CultureInfo.InvariantCulture);
						Size size = TextRenderer.MeasureText(graphics, character, font, characterSize, textFormatFlags);

						if (size.Width == 0 || size.Height == 0)
						{
							continue;
						}

						var rectangle = new Rectangle(
							imagePadding + (x * characterSize.Width) + (x * symbolPadding * 2),
							imagePadding + (y * characterSize.Height) + (y * symbolPadding * 2),
							characterSize.Width,
							characterSize.Height);

						TextRenderer.DrawText(
							graphics,
							character,
							font,
							rectangle,
							Color.White,
							backgroundColor,
							textFormatFlags);
					}
				}
			}

			font.Dispose();

			string path = Path.GetTempFileName() + ".png";

			fontImage.Save(path, ImageFormat.Png);
			fontImage.Dispose();

			var processStartInfo = new ProcessStartInfo(path)
			                       	{
			                       		UseShellExecute = true
			                       	};

			Process.Start(processStartInfo);

			Console.WriteLine("Character size is {0} x {1}", characterSize.Width, characterSize.Height);
		}

		[Test]
		public void GenerateCharactersImage()
		{
			GenerateImage(RegularHeight, 16, Color.Transparent, 0, 0);
		}

		[Test]
		public void GenerateSymbolSelectionImage()
		{
			GenerateImage(RegularHeight * 2, 24, Color.Black, 4, 4);
		}
	}
}