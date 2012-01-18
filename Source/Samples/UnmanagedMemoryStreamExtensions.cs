using System.IO;

namespace TextAdventure.Samples
{
	public static class UnmanagedMemoryStreamExtensions
	{
		public static byte[] GetBuffer(this UnmanagedMemoryStream unmanagedMemoryStream)
		{
			var buffer = new byte[unmanagedMemoryStream.Length];

			unmanagedMemoryStream.Read(buffer, 0, buffer.Length);

			return buffer;
		}
	}
}