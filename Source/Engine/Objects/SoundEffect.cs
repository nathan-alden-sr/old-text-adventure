using System;

using Junior.Common;

namespace TextAdventure.Engine.Objects
{
	public class SoundEffect : INamedObject, IDescribedObject
	{
		private readonly byte[] _data;
		private readonly Guid _id;
		private string _description;
		private string _name;

		public SoundEffect(
			Guid id,
			string name,
			string description,
			byte[] data)
		{
			name.ThrowIfNull("name");
			description.ThrowIfNull("description");
			data.ThrowIfNull("data");

			_id = id;
			Name = name;
			Description = description;
			_data = data;
		}

		public byte[] Data
		{
			get
			{
				return _data;
			}
		}

		public string Description
		{
			get
			{
				return _description;
			}
			protected internal set
			{
				value.ThrowIfNull("value");

				_description = value;
			}
		}

		public string Name
		{
			get
			{
				return _name;
			}
			protected internal set
			{
				value.ThrowIfNull("value");

				_name = value;
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