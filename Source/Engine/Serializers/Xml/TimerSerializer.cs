using System;
using System.Globalization;
using System.Xml.Linq;

using Junior.Common;

using TextAdventure.Engine.Game.Events;
using TextAdventure.Engine.Objects;

namespace TextAdventure.Engine.Serializers.Xml
{
	public class TimerSerializer
	{
		public static readonly TimerSerializer Instance = new TimerSerializer();

		private TimerSerializer()
		{
		}

		public XElement Serialize(Timer timer, string elementName = "timer")
		{
			timer.ThrowIfNull("timer");
			elementName.ThrowIfNull("elementName");

			return new XElement(
				elementName,
				EventHandlerSerializer.Instance.Serialize(timer.TimerElapsedEventHandler, "timerElapsedEventHandler"),
				new XAttribute("id", timer.Id),
				new XAttribute("name", timer.Name),
				new XAttribute("description", timer.Description),
				new XAttribute("interval", timer.Interval.ToString("c")),
				new XAttribute("state", timer.State),
				new XAttribute("elapsed", timer.ElapsedTime.ToString("c")));
		}

		public Timer Deserialize(XElement timerElement)
		{
			timerElement.ThrowIfNull("timerElement");

			return new Timer(
				(Guid)timerElement.Attribute("id"),
				(string)timerElement.Attribute("name"),
				(string)timerElement.Attribute("description"),
				TimeSpan.ParseExact((string)timerElement.Attribute("interval"), "c", CultureInfo.InvariantCulture),
				Enum<TimerState>.Parse((string)timerElement.Attribute("state")),
				TimeSpan.ParseExact((string)timerElement.Attribute("elapsed"), "c", CultureInfo.InvariantCulture),
				timerElement.Element("timerElapsedEventHandler").IfNotNull(EventHandlerSerializer.Instance.Deserialize<TimerElapsedEvent>));
		}
	}
}