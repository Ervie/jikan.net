using Tenrai.Consts;
using Tenrai.Helpers;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Tenrai;

public partial class TenraiClient
{
	/// <inheritdoc />
	public Task<BaseTenraiResponse<ICollection<Genre>>> GetAnimeGenresAsync(CancellationToken cancellationToken = default)
		=> GetAnimeGenresAsync(new GenresConfig(), cancellationToken);

	/// <inheritdoc />
	public async Task<BaseTenraiResponse<ICollection<Genre>>> GetAnimeGenresAsync(GenresConfig searchConfig, CancellationToken cancellationToken = default)
	{
		Guard.IsNotNull(searchConfig, nameof(searchConfig));
		var endpointParts = new[] { TenraiEndpointConsts.Genres, TenraiEndpointConsts.Anime + QueryFor(searchConfig) };
		return await ExecuteGetRequestAsync<BaseTenraiResponse<ICollection<Genre>>>(endpointParts, cancellationToken);
	}

	/// <inheritdoc />
	public Task<BaseTenraiResponse<ICollection<Genre>>> GetAnimeGenresAsync(GenresFilter filter, CancellationToken cancellationToken = default)
		=> GetAnimeGenresAsync(new GenresConfig { Filter = filter }, cancellationToken);

	/// <inheritdoc />
	public Task<BaseTenraiResponse<ICollection<Genre>>> GetMangaGenresAsync(CancellationToken cancellationToken = default)
		=> GetMangaGenresAsync(new GenresConfig(), cancellationToken);

	/// <inheritdoc />
	public async Task<BaseTenraiResponse<ICollection<Genre>>> GetMangaGenresAsync(GenresConfig searchConfig, CancellationToken cancellationToken = default)
	{
		Guard.IsNotNull(searchConfig, nameof(searchConfig));
		var endpointParts = new[] { TenraiEndpointConsts.Genres, TenraiEndpointConsts.Manga + QueryFor(searchConfig) };
		return await ExecuteGetRequestAsync<BaseTenraiResponse<ICollection<Genre>>>(endpointParts, cancellationToken);
	}

	/// <inheritdoc />
	public Task<BaseTenraiResponse<ICollection<Genre>>> GetMangaGenresAsync(GenresFilter filter, CancellationToken cancellationToken = default)
		=> GetMangaGenresAsync(new GenresConfig { Filter = filter }, cancellationToken);
}
