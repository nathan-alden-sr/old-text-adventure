using System.Drawing;
using System.Windows.Forms;

using TextAdventure.WindowsGame.Properties;
using TextAdventure.Xna;

namespace TextAdventure.WindowsGame.Xna
{
	public class TextAdventureXnaControl : XnaControl
	{
		public TextAdventureXnaControl()
		{
			DrawBackground = true;
		}

		public bool DrawBackground
		{
			get;
			set;
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			if (DrawBackground)
			{
				e.Graphics.FillRectangle(Brushes.Black, ClientRectangle);

				var logoRectangle = new Rectangle(
					new Point((ClientSize.Width / 2) - (Resources.Game_Thumbnail.Width / 2), (ClientSize.Height / 2) - (Resources.Game_Thumbnail.Height / 2)),
					Resources.Game_Thumbnail.Size);

				e.Graphics.DrawImage(Resources.Game_Thumbnail, logoRectangle);
			}

			base.OnPaint(e);
		}
	}
}