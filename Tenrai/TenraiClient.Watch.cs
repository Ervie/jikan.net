using Tenrai.Consts;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Tenrai;

public partial class TenraiClient
{
	/// <inheritdoc />
	[Obsolete(ErrorMessagesConst.UnsupportedWatch)]
	public Task<PaginatedTenraiResponse<ICollection<WatchEpisode>>> GetWatchRecentEpisodesAsync(CancellationToken cancellationToken = default)
		=> throw new NotSupportedException(ErrorMessagesConst.UnsupportedWatch);

	/// <inheritdoc />
	[Obsolete(ErrorMessagesConst.UnsupportedWatch)]
	public Task<PaginatedTenraiResponse<ICollection<WatchEpisode>>> GetWatchPopularEpisodesAsync(CancellationToken cancellationToken = default)
		=> throw new NotSupportedException(ErrorMessagesConst.UnsupportedWatch);

	/// <inheritdoc />
	[Obsolete(ErrorMessagesConst.UnsupportedWatch)]
	public Task<PaginatedTenraiResponse<ICollection<WatchPromoVideo>>> GetWatchRecentPromosAsync(CancellationToken cancellationToken = default)
		=> throw new NotSupportedException(ErrorMessagesConst.UnsupportedWatch);

	/// <inheritdoc />
	[Obsolete(ErrorMessagesConst.UnsupportedWatch)]
	public Task<PaginatedTenraiResponse<ICollection<WatchPromoVideo>>> GetWatchRecentPromosAsync(int page, CancellationToken cancellationToken = default)
		=> throw new NotSupportedException(ErrorMessagesConst.UnsupportedWatch);

	/// <inheritdoc />
	[Obsolete(ErrorMessagesConst.UnsupportedWatch)]
	public Task<PaginatedTenraiResponse<ICollection<WatchPromoVideo>>> GetWatchPopularPromosAsync(CancellationToken cancellationToken = default)
		=> throw new NotSupportedException(ErrorMessagesConst.UnsupportedWatch);
}
