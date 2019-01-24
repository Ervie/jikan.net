using JikanDotNet.Resources;
using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace JikanDotNet.Extensions
{
	internal static class EnumExtensions
	{
		public static string GetDescription<T>(this Enum source)
		{
			return
				source
					.GetType()
					.GetMember(source.ToString())
					.FirstOrDefault()
					?.GetCustomAttribute<DescriptionAttribute>()
					?.Description;
		}
	}
}
