using TextAdventure.Engine.Helpers;

namespace TextAdventure.Engine.Game.Commands
{
	public struct Volume
	{
		public static readonly Volume Full = new Volume(1f);
		public static readonly Volume Silent = new Volume(0f);
		private readonly float _value;

		public Volume(float value)
		{
			_value = MathHelper.Instance.Clamp(value, 0f, 1f);
		}

		public float Value
		{
			get
			{
				return _value;
			}
		}

		public static implicit operator float(Volume volume)
		{
			return volume.Value;
		}

		public static implicit operator Volume(float volume)
		{
			return new Volume(volume);
		}
	}
}