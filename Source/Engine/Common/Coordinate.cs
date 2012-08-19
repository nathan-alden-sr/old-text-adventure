using System;
using System.Diagnostics;

namespace TextAdventure.Engine.Common
{
	[DebuggerDisplay("X = {X}, Y = {Y}")]
	public struct Coordinate : IEquatable<Coordinate>
	{
		public static readonly Coordinate Zero = new Coordinate(0, 0);

		public Coordinate(int x, int y)
			: this()
		{
			X = x;
			Y = y;
		}

		public int X
		{
			get;
			set;
		}

		public int Y
		{
			get;
			set;
		}

		public bool Equals(Coordinate other)
		{
			return other.X == X && other.Y == Y;
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj))
			{
				return false;
			}
			if (obj.GetType() != typeof(Coordinate))
			{
				return false;
			}

			return Equals((Coordinate)obj);
		}

		public override int GetHashCode()
		{
			unchecked
			{
				return (X * 397) ^ Y;
			}
		}

		public override string ToString()
		{
			return String.Format("X = {0}, Y = {1}", X, Y);
		}

		public static bool operator ==(Coordinate a, Coordinate b)
		{
			return a.Equals(b);
		}

		public static bool operator !=(Coordinate a, Coordinate b)
		{
			return !a.Equals(b);
		}

		public static explicit operator Size(Coordinate coordinate)
		{
			return new Size(coordinate.X, coordinate.Y);
		}
	}
}