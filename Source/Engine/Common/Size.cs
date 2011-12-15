using System;
using System.Diagnostics;

namespace TextAdventure.Engine.Common
{
	[DebuggerDisplay("Width = {Width}, Height = {Height}")]
	public struct Size : ISize, IEquatable<Size>
	{
		public static readonly Size Zero = new Size(0, 0);

		private int _height;
		private int _width;

		public Size(int width, int height)
			: this()
		{
			Width = width;
			Height = height;
		}

		public bool Equals(Size other)
		{
			return other.Width == Width && other.Height == Height;
		}

		public int Width
		{
			get
			{
				return _width;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException("value", "Width must be at least 0.");
				}

				_width = value;
			}
		}

		public int Height
		{
			get
			{
				return _height;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException("value", "Height must be at least 0.");
				}

				_height = value;
			}
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj))
			{
				return false;
			}
			if (obj.GetType() != typeof(Size))
			{
				return false;
			}

			return Equals((Size)obj);
		}

		public override int GetHashCode()
		{
			unchecked
			{
				return (Width * 397) ^ Height;
			}
		}

		public override string ToString()
		{
			return String.Format("Width = {0}, Height = {1}", Width, Height);
		}

		public static bool operator ==(Size a, Size b)
		{
			return a.Equals(b);
		}

		public static bool operator !=(Size a, Size b)
		{
			return !a.Equals(b);
		}

		public static explicit operator Coordinate(Size size)
		{
			return new Coordinate(size.Width, size.Height);
		}
	}
}