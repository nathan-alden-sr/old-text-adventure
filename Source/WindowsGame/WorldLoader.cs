using System;
using System.IO;
using System.Linq;
using System.Reflection;

using Junior.Common;

namespace TextAdventure.WindowsGame
{
	public class WorldLoader
	{
		public static readonly WorldLoader Instance = new WorldLoader();

		private WorldLoader()
		{
		}

		public Engine.Objects.World FromAssembly(string assemblyPath, string worldTypeName)
		{
			assemblyPath.ThrowIfNull("assemblyPath");
			worldTypeName.ThrowIfNull("worldTypeName");

			ValidateAssemblyPath(assemblyPath);

			Assembly assembly = LoadAssembly(assemblyPath);
			Type worldType = assembly.GetType(worldTypeName);

			if (worldType == null)
			{
				throw new TypeLoadException(String.Format("World type '{0}' not found in assembly '{1}'.", worldTypeName, assemblyPath));
			}

			return GetWorld(assemblyPath, assembly, worldType);
		}

		public Engine.Objects.World FromAssembly(string assemblyPath)
		{
			assemblyPath.ThrowIfNull("assemblyPath");

			ValidateAssemblyPath(assemblyPath);

			Assembly assembly = LoadAssembly(assemblyPath);
			Type[] worldTypes = assembly.GetTypes()
				.Where(arg => arg.IsSubclassOf(typeof(Engine.Objects.World)))
				.ToArray();

			if (worldTypes.Length > 1)
			{
				throw new ArgumentException("Assembly '{0}' contains more than one world.", assemblyPath);
			}
			if (worldTypes.Length == 0)
			{
				throw new ArgumentException("Assembly '{0}' does not contain any worlds.", assemblyPath);
			}

			return GetWorld(assemblyPath, assembly, worldTypes.Single());
		}

		private static void ValidateAssemblyPath(string assemblyPath)
		{
			if (!File.Exists(assemblyPath))
			{
				throw new FileNotFoundException("World file not found.", assemblyPath);
			}
		}

		private static Engine.Objects.World GetWorld(string assemblyPath, Assembly assembly, Type worldType)
		{
			if (!worldType.IsPublic || worldType.IsAbstract)
			{
				throw new TypeLoadException(String.Format("World type '{0}' in assembly '{1}' must be declared as a public instance class.", worldType.Name, assemblyPath));
			}
			if (worldType.GetConstructors(BindingFlags.Public | BindingFlags.Instance).All(arg => arg.GetParameters().Any()))
			{
				throw new TypeLoadException(String.Format("World type '{0}' in assembly '{1}' must have a public instance constructor that takes no parameters.", worldType.Name, assemblyPath));
			}

			return (Engine.Objects.World)Activator.CreateInstance(worldType);
		}

		private static Assembly LoadAssembly(string assemblyPath)
		{
			try
			{
				return Assembly.LoadFrom(assemblyPath);
			}
			catch (Exception exception)
			{
				throw new Exception("World assembly is invalid.", exception);
			}
		}
	}
}