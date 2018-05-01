using Newtonsoft.Json;
using System.Collections.Generic;

namespace JikanDotNet
{
	/// <summary>
	/// Model class representing collection of related manga entries.
	/// </summary>
	public class RelatedManga
	{
		/// <summary>
		/// Collection of alternative versions.
		/// </summary>
		[JsonProperty(PropertyName = "Alternative Version")]
		public ICollection<MangaSubItem> AlternativeVersions { get; set; }

		/// <summary>
		/// Collection of adaptations.
		/// </summary>
		[JsonProperty(PropertyName = "Adaptation")]
		public ICollection<MangaSubItem> Adaptations { get; set; }

		/// <summary>
		/// Collection of character related entries.
		/// </summary>
		[JsonProperty(PropertyName = "Character")]
		public ICollection<MangaSubItem> Characters { get; set; }

		/// <summary>
		/// Collection of prequels.
		/// </summary>
		[JsonProperty(PropertyName = "Prequel")]
		public ICollection<MangaSubItem> Prequels { get; set; }

		/// <summary>
		/// Collection of other entries.
		/// </summary>
		[JsonProperty(PropertyName = "Other")]
		public ICollection<MangaSubItem> Others { get; set; }

		/// <summary>
		/// Collection of sequels.
		/// </summary>
		[JsonProperty(PropertyName = "Sequel")]
		public ICollection<MangaSubItem> Sequels { get; set; }

		/// <summary>
		/// Collection of side stories.
		/// </summary>
		[JsonProperty(PropertyName = "Side Story")]
		public ICollection<MangaSubItem> SideStories { get; set; }

		/// <summary>
		/// Collection of spin-off.
		/// </summary>
		[JsonProperty(PropertyName = "Spin-off")]
		public ICollection<MangaSubItem> SpinOffs { get; set; }

		/// <summary>
		/// Collection of summaries.
		/// </summary>
		[JsonProperty(PropertyName = "Summary")]
		public ICollection<MangaSubItem> Summaries { get; set; }
	}
}