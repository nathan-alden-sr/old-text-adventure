using System;

using Junior.Common;

using Microsoft.Xna.Framework.Graphics;

namespace TextAdventure.Xna
{
	public class XnaServiceProvider : IServiceProvider, IGraphicsDeviceService
	{
		private readonly GraphicsDevice _graphicsDevice;

		public XnaServiceProvider(GraphicsDevice graphicsDevice)
		{
			graphicsDevice.ThrowIfNull("graphicsDevice");

			_graphicsDevice = graphicsDevice;
		}

		public event EventHandler<EventArgs> DeviceReset
		{
			add
			{
				_graphicsDevice.DeviceReset += value;
			}
			remove
			{
				_graphicsDevice.DeviceReset -= value;
			}
		}

		public event EventHandler<EventArgs> DeviceResetting
		{
			add
			{
				_graphicsDevice.DeviceResetting += value;
			}
			remove
			{
				_graphicsDevice.DeviceResetting -= value;
			}
		}

		public event EventHandler<EventArgs> DeviceCreated
		{
			add
			{
			}
			remove
			{
			}
		}

		public event EventHandler<EventArgs> DeviceDisposing
		{
			add
			{
				_graphicsDevice.Disposing += value;
			}
			remove
			{
				_graphicsDevice.Disposing -= value;
			}
		}

		public GraphicsDevice GraphicsDevice
		{
			get
			{
				return _graphicsDevice;
			}
		}

		public object GetService(Type serviceType)
		{
			return this;
		}
	}
}