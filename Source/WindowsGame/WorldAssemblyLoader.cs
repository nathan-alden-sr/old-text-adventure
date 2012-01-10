using System;
using System.IO;
using System.Linq;
using System.Reflection;

using Junior.Common;

namespace TextAdventure.WindowsGame
{
	public static class WorldLoader
	{
		public static Engine.Objects.World FromAssembly(string assemblyFileName, string worldTypeName)
		{
			assemblyFileName.ThrowIfNull("assemblyFileName");
			worldTypeName.ThrowIfNull("worldTypeName");

			string directory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
			string assemblyPath = Path.Combine(directory, assemblyFileName);

			if (!File.Exists(assemblyPath))
			{
				throw new FileNotFoundException("World file not found.", assemblyPath);
			}

			Assembly assembly;

			try
			{
				assembly = Assembly.LoadFrom(assemblyPath);
			}
			catch (Exception exception)
			{
				throw new Exception("World file is invalid.", exception);
			}

			Type worldType = assembly.GetType(worldTypeName);

			if (worldType == null)
			{
				throw new TypeLoadException(String.Format("World type '{0}' not found in file '{1}'.", worldTypeName, assemblyPath));
			}
			if (!worldType.IsPublic || worldType.IsAbstract)
			{
				throw new TypeLoadException(String.Format("World type '{0}' in file '{1}' must be declared as a public instance class.", worldTypeName, assemblyPath));
			}
			if (worldType.GetConstructors(BindingFlags.Public | BindingFlags.Instance).All(arg => arg.GetParameters().Any()))
			{
				throw new TypeLoadException(String.Format("World type '{0}' in file '{1}' must have a public instance constructor that takes no parameters.", worldTypeName, assemblyPath));
			}

			return (Engine.Objects.World)Activator.CreateInstance(worldType);
		}
	}
}