using Tenrai.Consts;
using Tenrai.Helpers;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Tenrai;

public partial class TenraiClient
{
	/// <inheritdoc />
	public async Task<BaseTenraiResponse<Character>> GetCharacterAsync(long id, CancellationToken cancellationToken = default)
	{
		Guard.IsGreaterThanZero(id, nameof(id));
		var endpointParts = new[] { TenraiEndpointConsts.Characters, id.ToString() };
		return await ExecuteGetRequestAsync<BaseTenraiResponse<Character>>(endpointParts, cancellationToken);
	}

	/// <inheritdoc />
	public Task<BaseTenraiResponse<ICollection<CharacterAnimeographyEntry>>> GetCharacterAnimeAsync(long id, CancellationToken cancellationToken = default)
		=> GetCharacterAnimeAsync(id, new SfwConfig(), cancellationToken);

	/// <inheritdoc />
	public async Task<BaseTenraiResponse<ICollection<CharacterAnimeographyEntry>>> GetCharacterAnimeAsync(long id, SfwConfig searchConfig, CancellationToken cancellationToken = default)
	{
		Guard.IsGreaterThanZero(id, nameof(id));
		Guard.IsNotNull(searchConfig, nameof(searchConfig));
		var endpointParts = new[] { TenraiEndpointConsts.Characters, id.ToString(), TenraiEndpointConsts.Anime + QueryFor(searchConfig) };
		return await ExecuteGetRequestAsync<BaseTenraiResponse<ICollection<CharacterAnimeographyEntry>>>(endpointParts, cancellationToken);
	}

	/// <inheritdoc />
	public Task<BaseTenraiResponse<ICollection<CharacterMangaographyEntry>>> GetCharacterMangaAsync(long id, CancellationToken cancellationToken = default)
		=> GetCharacterMangaAsync(id, new SfwConfig(), cancellationToken);

	/// <inheritdoc />
	public async Task<BaseTenraiResponse<ICollection<CharacterMangaographyEntry>>> GetCharacterMangaAsync(long id, SfwConfig searchConfig, CancellationToken cancellationToken = default)
	{
		Guard.IsGreaterThanZero(id, nameof(id));
		Guard.IsNotNull(searchConfig, nameof(searchConfig));
		var endpointParts = new[] { TenraiEndpointConsts.Characters, id.ToString(), TenraiEndpointConsts.Manga + QueryFor(searchConfig) };
		return await ExecuteGetRequestAsync<BaseTenraiResponse<ICollection<CharacterMangaographyEntry>>>(endpointParts, cancellationToken);
	}

	/// <inheritdoc />
	public async Task<BaseTenraiResponse<ICollection<VoiceActorEntry>>> GetCharacterVoiceActorsAsync(long id, CancellationToken cancellationToken = default)
	{
		Guard.IsGreaterThanZero(id, nameof(id));
		var endpointParts = new[] { TenraiEndpointConsts.Characters, id.ToString(), TenraiEndpointConsts.Voices };
		return await ExecuteGetRequestAsync<BaseTenraiResponse<ICollection<VoiceActorEntry>>>(endpointParts, cancellationToken);
	}

	/// <inheritdoc />
	public async Task<BaseTenraiResponse<ICollection<ImagesSet>>> GetCharacterPicturesAsync(long id, CancellationToken cancellationToken = default)
	{
		Guard.IsGreaterThanZero(id, nameof(id));
		var endpointParts = new[] { TenraiEndpointConsts.Characters, id.ToString(), TenraiEndpointConsts.Pictures };
		return await ExecuteGetRequestAsync<BaseTenraiResponse<ICollection<ImagesSet>>>(endpointParts, cancellationToken);
	}

	/// <inheritdoc />
	public Task<PaginatedTenraiResponse<ICollection<Article>>> GetCharacterArticlesAsync(long id, CancellationToken cancellationToken = default)
		=> GetEntityArticlesAsync(TenraiEndpointConsts.Characters, id, new ArticleFilterConfig(), cancellationToken);

	/// <inheritdoc />
	public Task<PaginatedTenraiResponse<ICollection<Article>>> GetCharacterArticlesAsync(long id, ArticleFilterConfig filterConfig, CancellationToken cancellationToken = default)
		=> GetEntityArticlesAsync(TenraiEndpointConsts.Characters, id, filterConfig, cancellationToken);

	/// <inheritdoc />
	public Task<BaseTenraiResponse<CharacterFull>> GetCharacterFullDataAsync(long id, CancellationToken cancellationToken = default)
		=> GetCharacterFullDataAsync(id, new SfwConfig(), cancellationToken);

	/// <inheritdoc />
	public async Task<BaseTenraiResponse<CharacterFull>> GetCharacterFullDataAsync(long id, SfwConfig searchConfig, CancellationToken cancellationToken = default)
	{
		Guard.IsGreaterThanZero(id, nameof(id));
		Guard.IsNotNull(searchConfig, nameof(searchConfig));
		var endpointParts = new[] { TenraiEndpointConsts.Characters, id.ToString(), TenraiEndpointConsts.Full + QueryFor(searchConfig) };
		return await ExecuteGetRequestAsync<BaseTenraiResponse<CharacterFull>>(endpointParts, cancellationToken);
	}

	/// <inheritdoc />
	public async Task<IdsTenraiResponse> GetCharacterIdsAsync(CancellationToken cancellationToken = default)
	{
		var endpointParts = new[] { TenraiEndpointConsts.Characters, TenraiEndpointConsts.Ids };
		return await ExecuteGetRequestAsync<IdsTenraiResponse>(endpointParts, cancellationToken);
	}

	/// <inheritdoc />
	public Task<PaginatedTenraiResponse<ICollection<Character>>> SearchCharacterAsync(string query, CancellationToken cancellationToken = default)
		=> SearchCharacterAsync(new CharacterSearchConfig {Query = query}, cancellationToken);

	/// <inheritdoc />
	public async Task<PaginatedTenraiResponse<ICollection<Character>>> SearchCharacterAsync(CharacterSearchConfig searchConfig, CancellationToken cancellationToken = default)
	{
		Guard.IsNotNull(searchConfig, nameof(searchConfig));
		var endpointParts = new[] { TenraiEndpointConsts.Characters + QueryFor(searchConfig)};
		return await ExecuteGetRequestAsync<PaginatedTenraiResponse<ICollection<Character>>>(endpointParts, cancellationToken);
	}
}
