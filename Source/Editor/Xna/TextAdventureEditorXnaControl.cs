using System.Drawing;
using System.Windows.Forms;

using TextAdventure.Editor.Properties;
using TextAdventure.Xna;

namespace TextAdventure.Editor.Xna
{
	public class TextAdventureEditorXnaControl : XnaControl
	{
		public TextAdventureEditorXnaControl()
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