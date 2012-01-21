using System.Drawing;
using System.Windows.Forms;

using Microsoft.Xna.Framework.Graphics;

using TextAdventure.WindowsGame.Properties;

namespace TextAdventure.WindowsGame.Xna
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
				return new Size(Constants.GameWindow.Width, Constants.GameWindow.Height);
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
			var presentationParameters = new PresentationParameters
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

			GraphicsDevice = new GraphicsDevice(GraphicsAdapter.DefaultAdapter, GraphicsProfile.HiDef, presentationParameters);
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