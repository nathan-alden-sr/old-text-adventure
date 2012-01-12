using System;
using System.Diagnostics;

using TextAdventure.Engine.Helpers;

namespace TextAdventure.Engine.Common
{
	[DebuggerDisplay("R = {R}, G = {G}, B = {B}, A = {A}")]
	public struct Color : IEquatable<Color>
	{
		public static readonly Color Black = new Color(0f, 0f, 0f);
		public static readonly Color Blue = new Color(0f, 0f, 1f);
		public static readonly Color Cyan = new Color(0f, 1f, 1f);
		public static readonly Color Green = new Color(0f, 1f, 0f);
		public static readonly Color Magenta = new Color(1f, 0f, 1f);
		public static readonly Color Red = new Color(1f, 0f, 0f);
		public static readonly Color TransparentBlack = new Color(0f, 0f, 0f, 0f);
		public static readonly Color TransparentWhite = new Color(1f, 1f, 1f, 0f);
		public static readonly Color White = new Color(1f, 1f, 1f);
		public static readonly Color Yellow = new Color(1f, 1f, 0f);

		public Color(float r, float g, float b, float a = 1f)
			: this()
		{
			r = MathHelper.Instance.Clamp(r, 0f, 1f);
			g = MathHelper.Instance.Clamp(g, 0f, 1f);
			b = MathHelper.Instance.Clamp(b, 0f, 1f);
			a = MathHelper.Instance.Clamp(a, 0f, 1f);

			R = r;
			G = g;
			B = b;
			A = a;
		}

		public Color(float a)
			: this(0, 0, 0, a)
		{
		}

		public Color(byte r, byte g, byte b, byte a = (byte)255)
			: this(r / 255f, g / 255f, b / 255f, a / 255f)
		{
		}

		public Color(byte a)
			: this(0, 0, 0, a)
		{
		}

		public Color(Color color, float a)
			: this(color.R, color.G, color.B, a)
		{
		}

		public Color(Color color, byte a)
			: this(color.R, color.G, color.B, a / 255f)
		{
		}

		public float R
		{
			get;
			set;
		}

		public float G
		{
			get;
			set;
		}

		public float B
		{
			get;
			set;
		}

		public float A
		{
			get;
			set;
		}

		public bool Equals(Color other)
		{
			return other.R.Equals(R) && other.G.Equals(G) && other.B.Equals(B) && other.A.Equals(A);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj))
			{
				return false;
			}
			if (obj.GetType() != typeof(Color))
			{
				return false;
			}

			return Equals((Color)obj);
		}

		public override int GetHashCode()
		{
			unchecked
			{
				int result = R.GetHashCode();

				result = (result * 397) ^ G.GetHashCode();
				result = (result * 397) ^ B.GetHashCode();
				result = (result * 397) ^ A.GetHashCode();

				return result;
			}
		}

		public override string ToString()
		{
			return String.Format("R = {0}, G = {1}, B = {2}, A = {3}", R, G, B, A);
		}

		public static bool operator ==(Color a, Color b)
		{
			return a.Equals(b);
		}

		public static bool operator !=(Color a, Color b)
		{
			return !a.Equals(b);
		}
	}
}