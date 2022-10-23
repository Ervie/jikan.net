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
	}
}