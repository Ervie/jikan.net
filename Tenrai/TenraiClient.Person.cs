using Tenrai.Consts;
using Tenrai.Helpers;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Tenrai;

public partial class TenraiClient
{
	/// <inheritdoc />
	public async Task<BaseTenraiResponse<Person>> GetPersonAsync(long id, CancellationToken cancellationToken = default)
	{
		Guard.IsGreaterThanZero(id, nameof(id));
		var endpointParts = new[] { TenraiEndpointConsts.People, id.ToString() };
		return await ExecuteGetRequestAsync<BaseTenraiResponse<Person>>(endpointParts, cancellationToken);
	}

	/// <inheritdoc />
	public Task<BaseTenraiResponse<ICollection<PersonAnimeographyEntry>>> GetPersonAnimeAsync(long id, CancellationToken cancellationToken = default)
		=> GetPersonAnimeAsync(id, new SfwConfig(), cancellationToken);

	/// <inheritdoc />
	public async Task<BaseTenraiResponse<ICollection<PersonAnimeographyEntry>>> GetPersonAnimeAsync(long id, SfwConfig searchConfig, CancellationToken cancellationToken = default)
	{
		Guard.IsGreaterThanZero(id, nameof(id));
		Guard.IsNotNull(searchConfig, nameof(searchConfig));
		var endpointParts = new[] { TenraiEndpointConsts.People, id.ToString(), TenraiEndpointConsts.Anime + QueryFor(searchConfig) };
		return await ExecuteGetRequestAsync<BaseTenraiResponse<ICollection<PersonAnimeographyEntry>>>(endpointParts, cancellationToken);
	}

	/// <inheritdoc />
	public Task<BaseTenraiResponse<ICollection<PersonMangaographyEntry>>> GetPersonMangaAsync(long id, CancellationToken cancellationToken = default)
		=> GetPersonMangaAsync(id, new SfwConfig(), cancellationToken);

	/// <inheritdoc />
	public async Task<BaseTenraiResponse<ICollection<PersonMangaographyEntry>>> GetPersonMangaAsync(long id, SfwConfig searchConfig, CancellationToken cancellationToken = default)
	{
		Guard.IsGreaterThanZero(id, nameof(id));
		Guard.IsNotNull(searchConfig, nameof(searchConfig));
		var endpointParts = new[] { TenraiEndpointConsts.People, id.ToString(), TenraiEndpointConsts.Manga + QueryFor(searchConfig) };
		return await ExecuteGetRequestAsync<BaseTenraiResponse<ICollection<PersonMangaographyEntry>>>(endpointParts, cancellationToken);
	}

	/// <inheritdoc />
	public Task<BaseTenraiResponse<ICollection<VoiceActingRole>>> GetPersonVoiceActingRolesAsync(long id, CancellationToken cancellationToken = default)
		=> GetPersonVoiceActingRolesAsync(id, new SfwConfig(), cancellationToken);

	/// <inheritdoc />
	public async Task<BaseTenraiResponse<ICollection<VoiceActingRole>>> GetPersonVoiceActingRolesAsync(long id, SfwConfig searchConfig, CancellationToken cancellationToken = default)
	{
		Guard.IsGreaterThanZero(id, nameof(id));
		Guard.IsNotNull(searchConfig, nameof(searchConfig));
		var endpointParts = new[] { TenraiEndpointConsts.People, id.ToString(), TenraiEndpointConsts.Voices + QueryFor(searchConfig) };
		return await ExecuteGetRequestAsync<BaseTenraiResponse<ICollection<VoiceActingRole>>>(endpointParts, cancellationToken);
	}

	/// <inheritdoc />
	public async Task<BaseTenraiResponse<ICollection<ImagesSet>>> GetPersonPicturesAsync(long id, CancellationToken cancellationToken = default)
	{
		Guard.IsGreaterThanZero(id, nameof(id));
		var endpointParts = new[] { TenraiEndpointConsts.People, id.ToString(), TenraiEndpointConsts.Pictures };
		return await ExecuteGetRequestAsync<BaseTenraiResponse<ICollection<ImagesSet>>>(endpointParts, cancellationToken);
	}

	/// <inheritdoc />
	public Task<PaginatedTenraiResponse<ICollection<Article>>> GetPersonArticlesAsync(long id, CancellationToken cancellationToken = default)
		=> GetEntityArticlesAsync(TenraiEndpointConsts.People, id, new ArticleFilterConfig(), cancellationToken);

	/// <inheritdoc />
	public Task<PaginatedTenraiResponse<ICollection<Article>>> GetPersonArticlesAsync(long id, ArticleFilterConfig filterConfig, CancellationToken cancellationToken = default)
		=> GetEntityArticlesAsync(TenraiEndpointConsts.People, id, filterConfig, cancellationToken);

	/// <inheritdoc />
	public Task<BaseTenraiResponse<PersonFull>> GetPersonFullDataAsync(long id, CancellationToken cancellationToken = default)
		=> GetPersonFullDataAsync(id, new SfwConfig(), cancellationToken);

	/// <inheritdoc />
	public async Task<BaseTenraiResponse<PersonFull>> GetPersonFullDataAsync(long id, SfwConfig searchConfig, CancellationToken cancellationToken = default)
	{
		Guard.IsGreaterThanZero(id, nameof(id));
		Guard.IsNotNull(searchConfig, nameof(searchConfig));
		var endpointParts = new[] { TenraiEndpointConsts.People, id.ToString(), TenraiEndpointConsts.Full + QueryFor(searchConfig) };
		return await ExecuteGetRequestAsync<BaseTenraiResponse<PersonFull>>(endpointParts, cancellationToken);
	}

	/// <inheritdoc />
	public async Task<IdsTenraiResponse> GetPersonIdsAsync(CancellationToken cancellationToken = default)
	{
		var endpointParts = new[] { TenraiEndpointConsts.People, TenraiEndpointConsts.Ids };
		return await ExecuteGetRequestAsync<IdsTenraiResponse>(endpointParts, cancellationToken);
	}

	/// <inheritdoc />
	public Task<PaginatedTenraiResponse<ICollection<Person>>> SearchPersonAsync(string query, CancellationToken cancellationToken = default)
		=> SearchPersonAsync(new PersonSearchConfig {Query = query}, cancellationToken);

	/// <inheritdoc />
	public async Task<PaginatedTenraiResponse<ICollection<Person>>> SearchPersonAsync(PersonSearchConfig searchConfig, CancellationToken cancellationToken = default)
	{
		Guard.IsNotNull(searchConfig, nameof(searchConfig));
		var endpointParts = new[] { TenraiEndpointConsts.People + QueryFor(searchConfig)};
		return await ExecuteGetRequestAsync<PaginatedTenraiResponse<ICollection<Person>>>(endpointParts, cancellationToken);
	}
}
