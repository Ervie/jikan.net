using Tenrai.Consts;
using Tenrai.Helpers;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Tenrai;

public partial class TenraiClient
{
	/// <inheritdoc />
	public Task<PaginatedTenraiResponse<ICollection<Anime>>> GetScheduleAsync(CancellationToken cancellationToken = default)
		=> GetScheduleAsync(new ScheduleSearchConfig(), cancellationToken);

	/// <inheritdoc />
	public Task<PaginatedTenraiResponse<ICollection<Anime>>> GetScheduleAsync(int page, CancellationToken cancellationToken = default)
	{
		Guard.IsGreaterThanZero(page, nameof(page));
		return GetScheduleAsync(new ScheduleSearchConfig { Page = page }, cancellationToken);
	}

	/// <inheritdoc />
	public Task<PaginatedTenraiResponse<ICollection<Anime>>> GetScheduleAsync(ScheduledDay scheduledDay, CancellationToken cancellationToken = default)
	{
		Guard.IsValidEnum(scheduledDay, nameof(scheduledDay));
		return GetScheduleAsync(new ScheduleSearchConfig { Day = scheduledDay }, cancellationToken);
	}

	/// <inheritdoc />
	public async Task<PaginatedTenraiResponse<ICollection<Anime>>> GetScheduleAsync(ScheduleSearchConfig searchConfig, CancellationToken cancellationToken = default)
	{
		Guard.IsNotNull(searchConfig, nameof(searchConfig));
		var endpointParts = new[] { TenraiEndpointConsts.Schedules + QueryFor(searchConfig) };
		return await ExecuteGetRequestAsync<PaginatedTenraiResponse<ICollection<Anime>>>(endpointParts, cancellationToken);
	}
}
