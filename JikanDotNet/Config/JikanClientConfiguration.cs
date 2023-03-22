using System.Collections.Generic;

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
		/// Configuration of the API limiter
		/// </summary>
		public List<TaskLimiterConfiguration> LimiterConfigurations { get; set; }

		public JikanClientConfiguration()
		{
			SuppressException = false;
			LimiterConfigurations = TaskLimiterConfiguration.Default;
		}
	}
}