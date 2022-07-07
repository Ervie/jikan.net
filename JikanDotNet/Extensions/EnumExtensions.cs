using System;
using System.ComponentModel;
using System.Reflection;

namespace JikanDotNet.Extensions
{
	internal static class EnumExtensions
	{
		public static string GetDescription<T>(this T source) where T : Enum => typeof(T)
					.GetField(source.ToString())
					?.GetCustomAttribute<DescriptionAttribute>()
					?.Description;
	}
}
