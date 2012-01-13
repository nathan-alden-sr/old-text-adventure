using System.Drawing;
using System.Windows.Forms;

using Microsoft.Xna.Framework.Graphics;

namespace TextAdventure.WindowsGame.Xna
{
	public class XnaControl : Control
	{
		public XnaControl()
		{
			SetStyle(ControlStyles.AllPaintingInWmPaint, true);
			SetStyle(ControlStyles.DoubleBuffer, false);
			SetStyle(ControlStyles.ResizeRedraw, true);
			SetStyle(ControlStyles.UserPaint, true);
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
				return new Size(Constants.GameWindow.PreferredBackBufferWidth, Constants.GameWindow.PreferredBackBufferHeight);
			}
		}

		public void CreateGraphicsDevice()
		{
			var presentationParameters = new PresentationParameters
			                             	{
			                             		BackBufferFormat = SurfaceFormat.Color,
			                             		BackBufferWidth = Constants.GameWindow.PreferredBackBufferWidth,
			                             		BackBufferHeight = Constants.GameWindow.PreferredBackBufferHeight,
			                             		DepthStencilFormat = DepthFormat.Depth24Stencil8,
			                             		DeviceWindowHandle = Handle,
			                             		IsFullScreen = false,
			                             		RenderTargetUsage = RenderTargetUsage.DiscardContents
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
			e.Graphics.FillRectangle(Brushes.Black, ClientRectangle);
		}
	}
}