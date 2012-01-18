using System;

using Junior.Common;

namespace TextAdventure.Engine.Objects
{
	public class SoundEffect : IUnique
	{
		private readonly byte[] _data;
		private readonly Guid _id;

		public SoundEffect(
			Guid id,
			byte[] data)
		{
			data.ThrowIfNull("data");

			_id = id;
			_data = data;
		}

		public byte[] Data
		{
			get
			{
				return _data;
			}
		}

		public Guid Id
		{
			get
			{
				return _id;
			}
		}
	}
}