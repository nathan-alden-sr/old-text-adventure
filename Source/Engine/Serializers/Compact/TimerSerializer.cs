using System;
using System.Text;

using Junior.Common;

using TextAdventure.Engine.Game.Events;
using TextAdventure.Engine.Objects;

namespace TextAdventure.Engine.Serializers.Compact
{
	public class TimerSerializer
	{
		public static readonly TimerSerializer Instance = new TimerSerializer();

		private TimerSerializer()
		{
		}

		public byte[] Serialize(Timer timer)
		{
			timer.ThrowIfNull("timer");

			var serializer = new CompactSerializer();

			serializer[0] = timer.Id.ToByteArray();
			serializer[1] = Encoding.UTF8.GetBytes(timer.Name);
			serializer[2] = Encoding.UTF8.GetBytes(timer.Description);
			serializer[3] = BitConverter.GetBytes(timer.Interval.Ticks);
			serializer[4] = Encoding.UTF8.GetBytes(timer.State.ToString());
			serializer[5] = BitConverter.GetBytes(timer.ElapsedTime.Ticks);
			serializer[6] = EventHandlerSerializer.Instance.Serialize(timer.TimerElapsedEventHandler);

			return serializer.Serialize();
		}

		public Timer Deserialize(byte[] serializedData)
		{
			serializedData.ThrowIfNull("serializedData");

			var serializer = new CompactSerializer(serializedData);
			var id = new Guid(serializer[0]);
			string name = Encoding.UTF8.GetString(serializer[1]);
			string description = Encoding.UTF8.GetString(serializer[2]);
			TimeSpan interval = TimeSpan.FromTicks(BitConverter.ToInt64(serializer[3], 0));
			TimerState state = Enum<TimerState>.Parse(Encoding.UTF8.GetString(serializer[4]));
			TimeSpan elapsedTime = TimeSpan.FromTicks(BitConverter.ToInt64(serializer[5], 0));
			IEventHandler<TimerElapsedEvent> timerElapsedEventHandler = EventHandlerSerializer.Instance.Deserialize<TimerElapsedEvent>(serializer[6]);

			return new Timer(id, name, description, interval, state, elapsedTime, timerElapsedEventHandler);
		}
	}
}