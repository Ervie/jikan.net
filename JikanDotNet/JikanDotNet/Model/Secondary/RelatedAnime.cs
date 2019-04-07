using Newtonsoft.Json;
using System.Collections.Generic;

namespace JikanDotNet
{
	/// <summary>
	/// Model class representing collection of related anime entries.
	/// </summary>
	public class RelatedAnime
	{
		/// <summary>
		/// Collection of alternative versions.
		/// </summary>
		[JsonProperty(PropertyName = "Alternative Version")]
		public ICollection<MALSubItem> AlternativeVersions { get; set; }

		/// <summary>
		/// Collection of alternative settings.
		/// </summary>
		[JsonProperty(PropertyName = "Alternative Setting")]
		public ICollection<MALSubItem> AlternativeSettings { get; set; }

		/// <summary>
		/// Collection of adaptations.
		/// </summary>
		[JsonProperty(PropertyName = "Adaptation")]
		public ICollection<MALSubItem> Adaptations { get; set; }

		/// <summary>
		/// Collection of character related entries.
		/// </summary>
		[JsonProperty(PropertyName = "Character")]
		public ICollection<MALSubItem> Characters { get; set; }

		/// <summary>
		/// Collection of full stories.
		/// </summary>
		[JsonProperty(PropertyName = "Full story")]
		public ICollection<MALSubItem> FullStories { get; set; }

		/// <summary>
		/// Collection of parent stories.
		/// </summary>
		[JsonProperty(PropertyName = "Parent story")]
		public ICollection<MALSubItem> ParentStories { get; set; }

		/// <summary>
		/// Collection of prequels.
		/// </summary>
		[JsonProperty(PropertyName = "Prequel")]
		public ICollection<MALSubItem> Prequels { get; set; }

		/// <summary>
		/// Collection of other entries.
		/// </summary>
		[JsonProperty(PropertyName = "Other")]
		public ICollection<MALSubItem> Others { get; set; }

		/// <summary>
		/// Collection of sequels.
		/// </summary>
		[JsonProperty(PropertyName = "Sequel")]
		public ICollection<MALSubItem> Sequels { get; set; }

		/// <summary>
		/// Collection of side stories.
		/// </summary>
		[JsonProperty(PropertyName = "Side Story")]
		public ICollection<MALSubItem> SideStories { get; set; }

		/// <summary>
		/// Collection of spin-off.
		/// </summary>
		[JsonProperty(PropertyName = "Spin-off")]
		public ICollection<MALSubItem> SpinOffs { get; set; }

		/// <summary>
		/// Collection of summaries.
		/// </summary>
		[JsonProperty(PropertyName = "Summary")]
		public ICollection<MALSubItem> Summaries { get; set; }
	}
}