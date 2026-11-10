using Tenrai.Consts;
using Tenrai.Helpers;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Tenrai;

public partial class TenraiClient
{
	/// <inheritdoc/>
	public Task<BaseTenraiResponse<Anime>> GetRandomAnimeAsync(CancellationToken cancellationToken = default)
		=> GetRandomAnimeAsync(new RandomAnimeConfig(), cancellationToken);

	/// <inheritdoc/>
	public Task<BaseTenraiResponse<Manga>> GetRandomMangaAsync(CancellationToken cancellationToken = default)
		=> GetRandomMangaAsync(new RandomMangaConfig(), cancellationToken);

	/// <inheritdoc/>
	public async Task<BaseTenraiResponse<Anime>> GetRandomAnimeAsync(RandomAnimeConfig config, CancellationToken cancellationToken = default)
	{
		Guard.IsNotNull(config, nameof(config));
		var endpointParts = new[] { TenraiEndpointConsts.Random, TenraiEndpointConsts.Anime + QueryFor(config) };
		return await ExecuteGetRequestAsync<BaseTenraiResponse<Anime>>(endpointParts, cancellationToken);
	}

	/// <inheritdoc/>
	public async Task<BaseTenraiResponse<Manga>> GetRandomMangaAsync(RandomMangaConfig config, CancellationToken cancellationToken = default)
	{
		Guard.IsNotNull(config, nameof(config));
		var endpointParts = new[] { TenraiEndpointConsts.Random, TenraiEndpointConsts.Manga + QueryFor(config) };
		return await ExecuteGetRequestAsync<BaseTenraiResponse<Manga>>(endpointParts, cancellationToken);
	}

	/// <inheritdoc/>
	public async Task<BaseTenraiResponse<Character>> GetRandomCharacterAsync(CancellationToken cancellationToken = default)
	{
		var endpointParts = new[] { TenraiEndpointConsts.Random, TenraiEndpointConsts.Characters };
		return await ExecuteGetRequestAsync<BaseTenraiResponse<Character>>(endpointParts, cancellationToken);
	}

	/// <inheritdoc/>
	public async Task<BaseTenraiResponse<Person>> GetRandomPersonAsync(CancellationToken cancellationToken = default)
	{
		var endpointParts = new[] { TenraiEndpointConsts.Random, TenraiEndpointConsts.People };
		return await ExecuteGetRequestAsync<BaseTenraiResponse<Person>>(endpointParts, cancellationToken);
	}

	/// <inheritdoc/>
	public async Task<BaseTenraiResponse<Character>> GetRandomCharacterAsync(RandomFavoritesConfig config, CancellationToken cancellationToken = default)
	{
		Guard.IsNotNull(config, nameof(config));
		var endpointParts = new[] { TenraiEndpointConsts.Random, TenraiEndpointConsts.Characters + QueryFor(config) };
		return await ExecuteGetRequestAsync<BaseTenraiResponse<Character>>(endpointParts, cancellationToken);
	}

	/// <inheritdoc/>
	public async Task<BaseTenraiResponse<Person>> GetRandomPersonAsync(RandomFavoritesConfig config, CancellationToken cancellationToken = default)
	{
		Guard.IsNotNull(config, nameof(config));
		var endpointParts = new[] { TenraiEndpointConsts.Random, TenraiEndpointConsts.People + QueryFor(config) };
		return await ExecuteGetRequestAsync<BaseTenraiResponse<Person>>(endpointParts, cancellationToken);
	}

	/// <inheritdoc/>
	[Obsolete(ErrorMessagesConst.UnsupportedUserData)]
	public Task<BaseTenraiResponse<UserProfile>> GetRandomUserAsync(CancellationToken cancellationToken = default)
		=> throw new NotSupportedException(ErrorMessagesConst.UnsupportedUserData);
}
