using System;

namespace JikanDotNet.Config
{
	/// <summary>
	/// Object containing information of client configuration.
	/// </summary>
	public class JikanClientConfiguration
	{
		/// <summary>
		/// Should exception be thrown in case of failed request.
		/// </summary>
		public bool SuppressException { get; set; }

		/// <summary>
		/// Endpoint of the REST API.
		/// </summary>
		[Obsolete("This config will be phased out in next version, please set custom endpoint in the passed HttpClient instead.")]
		public string Endpoint { get; set; }
	}
}