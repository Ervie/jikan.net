using System;
using System.Collections.Generic;
using System.Text;

namespace JikanDotNet
{
	/// <summary>
	/// Interface for MAL entities with their own Id.
	/// </summary>
	public interface IMalEntity
	{
		/// <summary>
		/// ID associated with MyAnimeList.
		/// </summary>
		long MalId { get; set; }
	}
}
