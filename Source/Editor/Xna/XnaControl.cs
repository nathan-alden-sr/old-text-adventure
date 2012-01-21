using System.Drawing;
using System.Windows.Forms;

using Microsoft.Xna.Framework.Graphics;

using TextAdventure.Editor.Properties;

namespace TextAdventure.Editor.Xna
{
	public class XnaControl : Control
	{
		public XnaControl()
		{
			SetStyle(ControlStyles.DoubleBuffer, true);
			SetStyle(ControlStyles.ResizeRedraw, false);
			SetStyle(ControlStyles.UserPaint, true);
			DrawBackground = true;
			TabStop = false;
		}

		public GraphicsDevice GraphicsDevice
		{
			get;
			private set;
		}

		protected override Size DefaultSize
		{
			get
			{
				return new Size(100, 100);
			}
		}

		public override string Text
		{
			get
			{
				return "";
			}
			set
			{
			}
		}

		public bool DrawBackground
		{
			get;
			set;
		}

		public void CreateGraphicsDevice()
		{
			GraphicsDevice = new GraphicsDevice(GraphicsAdapter.DefaultAdapter, GraphicsProfile.HiDef, GetPresentationParameters());
		}

		private PresentationParameters GetPresentationParameters()
		{
			return new PresentationParameters
			       	{
			       		BackBufferFormat = SurfaceFormat.Color,
						BackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width,
						BackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height,
						DepthStencilFormat = DepthFormat.Depth24Stencil8,
			       		DeviceWindowHandle = Handle,
			       		IsFullScreen = false,
			       		RenderTargetUsage = RenderTargetUsage.DiscardContents,
			       		PresentationInterval = PresentInterval.Immediate
			       	};
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (GraphicsDevice != null)
				{
					GraphicsDevice.Dispose();
				}
			}

			base.Dispose(disposing);
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