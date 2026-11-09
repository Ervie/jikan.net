using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Tenrai.Consts;
using Tenrai.Extensions;

namespace Tenrai.Helpers
{
	internal sealed class QueryBuilder
	{
		private readonly List<string> _parts = new List<string>();

		internal QueryBuilder Page(int? page)
		{
			if (page.HasValue)
			{
				Guard.IsGreaterThanZero(page.Value, "Page");
				Append("page", page.Value.ToString(CultureInfo.InvariantCulture));
			}

			return this;
		}

		internal QueryBuilder Limit(int? limit)
		{
			if (limit.HasValue)
			{
				Guard.IsGreaterThanZero(limit.Value, "Limit");
				Guard.IsLesserOrEqualThan(limit.Value, ParameterConsts.MaximumPageSize, "Limit");
				Append("limit", limit.Value.ToString(CultureInfo.InvariantCulture));
			}

			return this;
		}

		internal QueryBuilder Add(string key, string value)
		{
			if (!string.IsNullOrWhiteSpace(value))
			{
				Append(key, Uri.EscapeDataString(value));
			}

			return this;
		}

		internal QueryBuilder Add(string key, long? value)
		{
			if (value.HasValue)
			{
				Append(key, value.Value.ToString(CultureInfo.InvariantCulture));
			}

			return this;
		}

		internal QueryBuilder Add(string key, double? value)
		{
			if (value.HasValue)
			{
				Append(key, value.Value.ToString(CultureInfo.InvariantCulture));
			}

			return this;
		}

		internal QueryBuilder Add(string key, DateTime? value)
		{
			if (value.HasValue)
			{
				Append(key, value.Value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture));
			}

			return this;
		}

		// A member with an empty [Description] ("EveryType", "NoSorting") means "do not filter"
		internal QueryBuilder Add<TEnum>(string key, TEnum value) where TEnum : struct, Enum
		{
			Guard.IsValidEnum(value, key);
			var description = value.GetDescription();
			if (!string.IsNullOrEmpty(description))
			{
				Append(key, Uri.EscapeDataString(description));
			}

			return this;
		}

		internal QueryBuilder Add<TEnum>(string key, TEnum? value) where TEnum : struct, Enum
		{
			return value.HasValue ? Add(key, value.Value) : this;
		}

		internal QueryBuilder AddList<TEnum>(string key, ICollection<TEnum> values) where TEnum : struct, Enum
		{
			if (values == null || values.Count == 0)
			{
				return this;
			}

			var descriptions = values.Select(value =>
			{
				Guard.IsValidEnum(value, key);
				return Uri.EscapeDataString(value.GetDescription());
			});
			Append(key, string.Join(",", descriptions));

			return this;
		}

		internal QueryBuilder AddList(string key, ICollection<long> values)
		{
			if (values == null || values.Count == 0)
			{
				return this;
			}

			Append(key, string.Join(",", values.Select(value => value.ToString(CultureInfo.InvariantCulture))));

			return this;
		}

		internal QueryBuilder Sfw(bool sfw, bool sfwStrict)
		{
			if (sfw)
			{
				Append("sfw", "true");
			}

			if (sfwStrict)
			{
				Append("sfw-strict", "true");
			}

			return this;
		}

		public override string ToString()
		{
			return _parts.Count == 0 ? string.Empty : "?" + string.Join("&", _parts);
		}

		private void Append(string key, string encodedValue)
		{
			_parts.Add(key + "=" + encodedValue);
		}
	}
}
