using System.Text.Json.Serialization;
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
		[JsonPropertyName("Alternative Version")]
		public ICollection<MalUrl> AlternativeVersions { get; set; }

		/// <summary>
		/// Collection of alternative settings.
		/// </summary>
		[JsonPropertyName("Alternative setting")]
		public ICollection<MalUrl> AlternativeSettings { get; set; }

		/// <summary>
		/// Collection of adaptations.
		/// </summary>
		[JsonPropertyName("Adaptation")]
		public ICollection<MalUrl> Adaptations { get; set; }

		/// <summary>
		/// Collection of character related entries.
		/// </summary>
		[JsonPropertyName("Character")]
		public ICollection<MalUrl> Characters { get; set; }

		/// <summary>
		/// Collection of full stories.
		/// </summary>
		[JsonPropertyName("Full story")]
		public ICollection<MalUrl> FullStories { get; set; }

		/// <summary>
		/// Collection of parent stories.
		/// </summary>
		[JsonPropertyName("Parent story")]
		public ICollection<MalUrl> ParentStories { get; set; }

		/// <summary>
		/// Collection of prequels.
		/// </summary>
		[JsonPropertyName("Prequel")]
		public ICollection<MalUrl> Prequels { get; set; }

		/// <summary>
		/// Collection of other entries.
		/// </summary>
		[JsonPropertyName("Other")]
		public ICollection<MalUrl> Others { get; set; }

		/// <summary>
		/// Collection of sequels.
		/// </summary>
		[JsonPropertyName("Sequel")]
		public ICollection<MalUrl> Sequels { get; set; }

		/// <summary>
		/// Collection of side stories.
		/// </summary>
		[JsonPropertyName("Side story")]
		public ICollection<MalUrl> SideStories { get; set; }

		/// <summary>
		/// Collection of spin-off.
		/// </summary>
		[JsonPropertyName("Spin-off")]
		public ICollection<MalUrl> SpinOffs { get; set; }

		/// <summary>
		/// Collection of summaries.
		/// </summary>
		[JsonPropertyName("Summary")]
		public ICollection<MalUrl> Summaries { get; set; }
	}
}