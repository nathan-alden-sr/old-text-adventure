using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;

using TextAdventure.Engine.Game.Commands;

namespace TextAdventure.WindowsGame.Configuration
{
	public class VolumeTypeConverter : TypeConverter
	{
		private readonly Dictionary<Type, Func<object, Volume>> _convertFromConversions =
			new Dictionary<Type, Func<object, Volume>>
				{
					{ typeof(string), value => new Volume(Single.Parse((string)value)) }
				};

		private readonly Dictionary<Type, Func<Volume, object>> _convertToConversions =
			new Dictionary<Type, Func<Volume, object>>
				{
					{ typeof(string), value => value.Value.ToString(CultureInfo.InvariantCulture) }
				};

		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return _convertFromConversions.ContainsKey(sourceType) || base.CanConvertFrom(context, sourceType);
		}

		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return _convertToConversions.ContainsKey(destinationType) || base.CanConvertTo(context, destinationType);
		}

		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			Func<object, Volume> @delegate;

			if (value != null && _convertFromConversions.TryGetValue(value.GetType(), out @delegate))
			{
				return @delegate(value);
			}

			return base.ConvertFrom(context, culture, value);
		}

		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			Func<Volume, object> @delegate;

			if (value is Volume && _convertToConversions.TryGetValue(value.GetType(), out @delegate))
			{
				return @delegate((Volume)value);
			}

			return base.ConvertTo(context, culture, value, destinationType);
		}
	}
}