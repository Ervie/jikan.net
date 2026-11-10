using Tenrai.Consts;
using Tenrai.Extensions;
using Tenrai.Helpers;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Tenrai;

public partial class TenraiClient
{
	/// <inheritdoc />
	public Task<PaginatedTenraiResponse<ICollection<Anime>>> GetSeasonAsync(int year, Season season, CancellationToken cancellationToken = default)
		=> GetSeasonAsync(year, season, new SeasonSearchConfig(), cancellationToken);

	/// <inheritdoc />
	public Task<PaginatedTenraiResponse<ICollection<Anime>>> GetSeasonAsync(int year, Season season, int page, CancellationToken cancellationToken = default)
	{
		Guard.IsGreaterThanZero(page, nameof(page));
		return GetSeasonAsync(year, season, new SeasonSearchConfig { Page = page }, cancellationToken);
	}

	/// <inheritdoc />
	public async Task<PaginatedTenraiResponse<ICollection<Anime>>> GetSeasonAsync(int year, Season season, SeasonSearchConfig searchConfig, CancellationToken cancellationToken = default)
	{
		Guard.IsValid(x => x is >= 1000 and < 10000, year, nameof(year));
		Guard.IsValidEnum(season, nameof(season));
		Guard.IsNotNull(searchConfig, nameof(searchConfig));
		var endpointParts = new[] { TenraiEndpointConsts.Seasons, year.ToString(), season.GetDescription() + QueryFor(searchConfig) };
		return await ExecuteGetRequestAsync<PaginatedTenraiResponse<ICollection<Anime>>>(endpointParts, cancellationToken);
	}

	/// <inheritdoc />
	public Task<PaginatedTenraiResponse<ICollection<Anime>>> GetCurrentSeasonAsync(SeasonSearchConfig searchConfig, CancellationToken cancellationToken = default)
		=> GetSeasonListAsync(TenraiEndpointConsts.Now, searchConfig, cancellationToken);

	/// <inheritdoc />
	public Task<PaginatedTenraiResponse<ICollection<Anime>>> GetUpcomingSeasonAsync(SeasonSearchConfig searchConfig, CancellationToken cancellationToken = default)
		=> GetSeasonListAsync(TenraiEndpointConsts.Upcoming, searchConfig, cancellationToken);

	/// <inheritdoc />
	public Task<PaginatedTenraiResponse<ICollection<Anime>>> GetPreviousSeasonAsync(SeasonSearchConfig searchConfig, CancellationToken cancellationToken = default)
		=> GetSeasonListAsync(TenraiEndpointConsts.Previous, searchConfig, cancellationToken);

	/// <inheritdoc />
	public async Task<PaginatedTenraiResponse<ICollection<SeasonArchive>>> GetSeasonArchiveAsync(CancellationToken cancellationToken = default)
	{
		var endpointParts = new[] { TenraiEndpointConsts.Seasons };
		return await ExecuteGetRequestAsync<PaginatedTenraiResponse<ICollection<SeasonArchive>>>(endpointParts, cancellationToken);
	}

	/// <inheritdoc />
	public Task<PaginatedTenraiResponse<ICollection<Anime>>> GetCurrentSeasonAsync(CancellationToken cancellationToken = default)
		=> GetCurrentSeasonAsync(new SeasonSearchConfig(), cancellationToken);

	/// <inheritdoc />
	public Task<PaginatedTenraiResponse<ICollection<Anime>>> GetCurrentSeasonAsync(int page, CancellationToken cancellationToken = default)
	{
		Guard.IsGreaterThanZero(page, nameof(page));
		return GetCurrentSeasonAsync(new SeasonSearchConfig { Page = page }, cancellationToken);
	}

	/// <inheritdoc />
	public Task<PaginatedTenraiResponse<ICollection<Anime>>> GetUpcomingSeasonAsync(CancellationToken cancellationToken = default)
		=> GetUpcomingSeasonAsync(new SeasonSearchConfig(), cancellationToken);

	/// <inheritdoc />
	public Task<PaginatedTenraiResponse<ICollection<Anime>>> GetUpcomingSeasonAsync(int page, CancellationToken cancellationToken = default)
	{
		Guard.IsGreaterThanZero(page, nameof(page));
		return GetUpcomingSeasonAsync(new SeasonSearchConfig { Page = page }, cancellationToken);
	}

	/// <inheritdoc />
	public Task<PaginatedTenraiResponse<ICollection<Anime>>> GetPreviousSeasonAsync(CancellationToken cancellationToken = default)
		=> GetPreviousSeasonAsync(new SeasonSearchConfig(), cancellationToken);

	/// <inheritdoc />
	public Task<PaginatedTenraiResponse<ICollection<Anime>>> GetPreviousSeasonAsync(int page, CancellationToken cancellationToken = default)
	{
		Guard.IsGreaterThanZero(page, nameof(page));
		return GetPreviousSeasonAsync(new SeasonSearchConfig { Page = page }, cancellationToken);
	}
}
