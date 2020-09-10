using System.Text.Json.Serialization;

namespace JikanDotNet
{
	/// <summary>
	/// Single manga user update model class.
	/// </summary>
	public class MangaUserUpdate : UserUpdate
	{
		/// <summary>
		/// Amount of volumes read by the user.
		/// </summary>
		[JsonPropertyName("volumes_read")]
		public int? VolumesRead { get; set; }

		/// <summary>
		/// Total amount of the volumes.
		/// </summary>
		[JsonPropertyName("volumes_total")]
		public int? VolumesTotal { get; set; }

		/// <summary>
		/// Amount of chapters read by the user.
		/// </summary>
		[JsonPropertyName("chapters_read")]
		public int? ChaptersRead { get; set; }

		/// <summary>
		/// Total amount of the chapters.
		/// </summary>
		[JsonPropertyName("chapters_total")]
		public int? ChaptersTotal { get; set; }
	}
}