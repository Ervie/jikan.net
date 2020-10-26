namespace JikanDotNet.Config
{
	public class JikanClientOptions
	{
		/// <summary>
		/// Should exception be thrown in case of failed request.
		/// </summary>
		public bool SuppressException { get; set; }

		/// <summary>
		/// Endpoint of the REST API.
		/// </summary>
		public string Endpoint { get; set; }
	}
}