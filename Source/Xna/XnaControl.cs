using System;
using System.Drawing;
using System.Windows.Forms;

using Microsoft.Xna.Framework.Graphics;

namespace TextAdventure.Xna
{
	public class XnaControl : Control
	{
		public XnaControl()
		{
			SetStyle(ControlStyles.DoubleBuffer, true);
			SetStyle(ControlStyles.ResizeRedraw, false);
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
				return new Size(100, 100);
			}
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

		private void DisposeGraphicsDevice()
		{
			if (GraphicsDevice != null)
			{
				GraphicsDevice.Dispose();
				GraphicsDevice = null;
			}
		}

		protected override void OnHandleCreated(EventArgs e)
		{
			if (!DesignMode)
			{
				GraphicsDevice = new GraphicsDevice(GraphicsAdapter.DefaultAdapter, GraphicsProfile.HiDef, GetPresentationParameters());
			}

			base.OnHandleCreated(e);
		}

		protected override void OnHandleDestroyed(EventArgs e)
		{
			DisposeGraphicsDevice();

			base.OnHandleDestroyed(e);
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
			DisposeGraphicsDevice();
			}

			base.Dispose(disposing);
		}
	}
}