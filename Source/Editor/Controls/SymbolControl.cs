using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Windows.Forms;

using TextAdventure.Editor.Properties;

namespace TextAdventure.Editor.Controls
{
	[DefaultEvent("SymbolChanged")]
	public class SymbolControl : Control
	{
		private byte _selectedSymbol;
		private Color _symbolBackgroundColor;
		private Color _symbolForegroundColor;

		[Category("Property Changed")]
		public event EventHandler SymbolChanged;

		public SymbolControl()
		{
			SetStyle(ControlStyles.DoubleBuffer, true);
			SetStyle(ControlStyles.ResizeRedraw, true);
			SetStyle(ControlStyles.UserPaint, true);

			TabStop = false;
			ResetSymbolForegroundColor();
			ResetSymbolBackgroundColor();
		}

		[Category("Appearance")]
		[DefaultValue((byte)0)]
		public byte SelectedSymbol
		{
			get
			{
				return _selectedSymbol;
			}
			set
			{
				_selectedSymbol = value;
				Refresh();
				RaiseSymbolChanged(EventArgs.Empty);
			}
		}

		[Category("Appearance")]
		public Color SymbolForegroundColor
		{
			get
			{
				return _symbolForegroundColor;
			}
			set
			{
				_symbolForegroundColor = value;
				Refresh();
			}
		}

		[Category("Appearance")]
		public Color SymbolBackgroundColor
		{
			get
			{
				return _symbolBackgroundColor;
			}
			set
			{
				_symbolBackgroundColor = value;
				Refresh();
			}
		}

		protected override Size DefaultSize
		{
			get
			{
				return new Size(
					(TextAdventure.Xna.Constants.Tile.TileWidth * 2) + 6,
					(TextAdventure.Xna.Constants.Tile.TileHeight * 2) + 6);
			}
		}

		public void ResetSymbolForegroundColor()
		{
			_symbolForegroundColor = Color.White;
		}

		public void ResetSymbolBackgroundColor()
		{
			_symbolBackgroundColor = Color.Transparent;
		}

		public bool ShouldSerializeSymbolForegroundColor()
		{
			return _symbolForegroundColor != Color.White;
		}

		public bool ShouldSerializeSymbolBackgroundColor()
		{
			return _symbolForegroundColor != Color.Transparent;
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			const int tileWidth = TextAdventure.Xna.Constants.Tile.TileWidth;
			const int tileHeight = TextAdventure.Xna.Constants.Tile.TileHeight;
			var sourceRectangle = new Rectangle((_selectedSymbol % 16) * tileWidth, (_selectedSymbol / 16) * tileHeight, tileWidth, tileHeight);

			if (_symbolBackgroundColor.A < 255)
			{
				using (Brush brush = new TextureBrush(Resources.Transparent_Background, WrapMode.Tile))
				{
					e.Graphics.FillRectangle(brush, ClientRectangle);
				}
			}
			using (Brush brush = new SolidBrush(_symbolBackgroundColor))
			{
				e.Graphics.FillRectangle(brush, ClientRectangle);
			}

			e.Graphics.DrawRectangle(Pens.Black, 0, 0, Width - 1, Height - 1);

			var imageAttributes = new ImageAttributes();

			imageAttributes.SetColorMatrix(GetColorMatrix(_symbolForegroundColor), ColorMatrixFlag.Default, ColorAdjustType.Bitmap);

			e.Graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
			e.Graphics.PixelOffsetMode = PixelOffsetMode.Half;
			e.Graphics.DrawImage(
				Resources.Characters,
				Rectangle.Inflate(ClientRectangle, -2, -2),
				sourceRectangle.X,
				sourceRectangle.Y,
				sourceRectangle.Width,
				sourceRectangle.Height,
				GraphicsUnit.Pixel,
				imageAttributes);

			base.OnPaint(e);
		}

		private void RaiseSymbolChanged(EventArgs e)
		{
			EventHandler handler = SymbolChanged;

			if (handler != null)
			{
				handler(this, e);
			}
		}

		private static ColorMatrix GetColorMatrix(Color color)
		{
			return new ColorMatrix(new[]
			                       	{
			                       		new[] { color.R / 255f, 0f, 0f, 0f, 0f },
			                       		new[] { 0f, color.G / 255f, 0f, 0f, 0f },
			                       		new[] { 0f, 0f, color.B / 255f, 0f, 0f },
			                       		new[] { 0f, 0f, 0f, color.A / 255f, 0f },
			                       		new[] { 0f, 0f, 0f, 0f, 1f }
			                       	});
		}
	}
}