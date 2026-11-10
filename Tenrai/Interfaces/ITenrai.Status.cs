using System.Threading;
using System.Threading.Tasks;

namespace Tenrai;

public partial interface ITenrai
{
	/// <summary>
	/// Returns the current public status snapshot of the Tenrai services.
	/// </summary>
	/// <remarks>
	/// This call targets the status host (<c>https://tenrai.org</c>) rather than the API host, so it ignores any
	/// custom base address and is not subject to the client's rate limiter. Poll no more than once per minute;
	/// the server caches the snapshot for 30 seconds. If a Server Key is configured it is sent on this request too,
	/// because the underlying <c>HttpClient</c> is shared.
	/// </remarks>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>Current public status snapshot.</returns>
	Task<TenraiStatus> GetStatusAsync(CancellationToken cancellationToken = default);
}
