using JikanDotNet.Resources;
using System;
using System.ComponentModel;
using System.Reflection;

namespace JikanDotNet.Extensions
{
	internal static class EnumExtensions
	{
		public static string GetDescription<T>(this T enumerationValue)
	where T : struct
		{
			Type type = enumerationValue.GetType();
			if (!type.IsEnum)
			{
				throw new ArgumentException(Errors.NotEnum, "enumerationValue");
			}
			
			MemberInfo[] memberInfo = type.GetMember(enumerationValue.ToString());
			if (memberInfo != null && memberInfo.Length > 0)
			{
				object[] attrs = memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);

				if (attrs != null && attrs.Length > 0)
				{
					return ((DescriptionAttribute)attrs[0]).Description;
				}
			}
			return enumerationValue.ToString();
		}
	}
}