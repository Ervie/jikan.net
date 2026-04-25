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

		/// <summary>
		/// Initializes a new instance of <see cref="JikanClientConfiguration"/> with default settings:
		/// exceptions are not suppressed and the default task limiter configuration is applied.
		/// </summary>
		public JikanClientConfiguration()
		{
			SuppressException = false;
			LimiterConfigurations = TaskLimiterConfiguration.Default;
		}
	}
}