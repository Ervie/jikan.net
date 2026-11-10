# API Reference

All methods are asynchronous and return `Task<T>`. Response models wrap data in a `Data` property. Paginated endpoints return `PaginatedTenraiResponse<T>` with pagination metadata. See [Models](Models.md) for response model details.

> **Unsupported endpoints:** Tenrai does not implement user, club, watch, or anime/manga user-update endpoints (plus random user and user/club search). Those methods are retained on `ITenrai` for source compatibility but are marked `[Obsolete]` and throw `NotSupportedException`; they are not listed below. For user-specific data, use the [official MyAnimeList API](https://myanimelist.net/apiconfig/references/api/v2).

> **Server Key:** The bulk `Get*IdsAsync` methods require a Tenrai Server Key (set `TenraiClientConfiguration.ServerKey`); without one the API returns 401. See [Rate Limiting](RateLimiting.md).

## Anime

### GetAnimeAsync

Returns anime with given MAL id.

| Parameter | Type | Description |
|-----------|------|-------------|
| id | long | MAL id of anime |

**Returns:** `BaseTenraiResponse<Anime>`

**Example:**

```csharp
var anime = await tenrai.GetAnimeAsync(1);
Console.WriteLine(anime.Data.Title);
Console.WriteLine(anime.Data.Type);
```

**Model:** [Anime](Models.md#anime)

### GetAnimeCharactersAsync

Returns collection of characters of anime.

| Parameter | Type | Description |
|-----------|------|-------------|
| id | long | MAL id of anime |

**Returns:** `BaseTenraiResponse<ICollection<AnimeCharacter>>`

**Example:**

```csharp
var characters = await tenrai.GetAnimeCharactersAsync(1);
foreach (var c in characters.Data)
    Console.WriteLine($"{c.Character.Name} - {c.Role}");
```

**Model:** [AnimeCharacter](Models.md#animecharacter)

### GetAnimeStaffAsync

Returns collection of staff of anime.

| Parameter | Type | Description |
|-----------|------|-------------|
| id | long | MAL id of anime |

**Returns:** `BaseTenraiResponse<ICollection<AnimeStaffPosition>>`

**Example:**

```csharp
var staff = await tenrai.GetAnimeStaffAsync(1);
foreach (var member in staff.Data)
    Console.WriteLine($"{member.Person.Name} ({string.Join(", ", member.Position)})");
```

**Model:** [AnimeStaffPosition](Models.md#animestaffposition)

### GetAnimeEpisodesAsync

Returns list of episodes. Overload: `(long id, int page)` for pagination.

| Parameter | Type | Description |
|-----------|------|-------------|
| id | long | MAL id of anime |
| page | int | (optional) Page index |

**Returns:** `PaginatedTenraiResponse<ICollection<AnimeEpisode>>`

**Example:**

```csharp
var episodes = await tenrai.GetAnimeEpisodesAsync(1);
foreach (var ep in episodes.Data)
    Console.WriteLine($"Ep {ep.MalId}: {ep.Title}");
```

**Model:** [AnimeEpisode](Models.md#animeepisode)

### GetAnimeEpisodeAsync

Returns details about specific episode.

| Parameter | Type | Description |
|-----------|------|-------------|
| animeId | long | MAL id of anime |
| episodeId | int | Id of episode |

**Returns:** `BaseTenraiResponse<AnimeEpisode>`

**Example:**

```csharp
var episode = await tenrai.GetAnimeEpisodeAsync(1, 1);
Console.WriteLine(episode.Data.Synopsis);
```

**Model:** [AnimeEpisode](Models.md#animeepisode)

### GetAnimeNewsAsync

Returns news related to anime. Overloads: `(long id, int page)`, `(long id, NewsFilterConfig filterConfig)`. `GetAnimeArticlesAsync` has the same shape with an [`ArticleFilterConfig`](#articlefilterconfig) and returns articles.

| Parameter | Type | Description |
|-----------|------|-------------|
| id | long | MAL id of anime |
| page | int | (optional) Page index |
| filterConfig | [NewsFilterConfig](#newsfilterconfig) | (optional) Author, publish dates, sort, paging, sfw |

**Returns:** `PaginatedTenraiResponse<ICollection<News>>`

**Example:**

```csharp
var news = await tenrai.GetAnimeNewsAsync(1);
foreach (var n in news.Data)
    Console.WriteLine(n.Title);
```

### GetAnimeForumTopicsAsync

Returns episode discussion threads, 30 per page. Other forum topics are not available. Overload: `(long id, AnimeForumSearchConfig searchConfig)`.

| Parameter | Type | Description |
|-----------|------|-------------|
| id | long | MAL id of anime |
| searchConfig | AnimeForumSearchConfig | (optional) `Page`, `OrderBy` (`EpisodeNumber`, `CreatedOn`, `CommentCount`, `LastPost`), `SortDirection` |

**Returns:** `PaginatedTenraiResponse<ICollection<ForumTopic>>`

**Example:**

```csharp
var threads = await tenrai.GetAnimeForumTopicsAsync(21, new AnimeForumSearchConfig
{
    OrderBy = AnimeForumOrderBy.CommentCount,
    SortDirection = SortDirection.Descending
});
```

**Model:** [ForumTopic](Models.md#forumtopic)

### GetAnimeVideosAsync

Returns videos (PV, episodes) related to anime.

| Parameter | Type | Description |
|-----------|------|-------------|
| id | long | MAL id of anime |

**Returns:** `BaseTenraiResponse<AnimeVideos>`

**Example:**

```csharp
var videos = await tenrai.GetAnimeVideosAsync(1);
// videos.Data contains Promo, Episodes, MusicVideos
```

### GetAnimeVideosEpisodesAsync

Returns video episodes. Overload: `(long id, int page)`.

| Parameter | Type | Description |
|-----------|------|-------------|
| id | long | MAL id of anime |
| page | int | (optional) Page index |

**Returns:** `PaginatedTenraiResponse<ICollection<EpisodeVideo>>`

**Example:**

```csharp
var episodes = await tenrai.GetAnimeVideosEpisodesAsync(1);
foreach (var ep in episodes.Data)
    Console.WriteLine(ep.Title);
```

### GetAnimePicturesAsync

Returns links to pictures.

| Parameter | Type | Description |
|-----------|------|-------------|
| id | long | MAL id of anime |

**Returns:** `BaseTenraiResponse<ICollection<ImagesSet>>`

**Example:**

```csharp
var pictures = await tenrai.GetAnimePicturesAsync(1);
foreach (var img in pictures.Data)
    Console.WriteLine(img.Jpg?.ImageUrl);
```

### GetAnimeStatisticsAsync

Returns statistics.

| Parameter | Type | Description |
|-----------|------|-------------|
| id | long | MAL id of anime |

**Returns:** `BaseTenraiResponse<AnimeStatistics>`

**Example:**

```csharp
var stats = await tenrai.GetAnimeStatisticsAsync(1);
Console.WriteLine($"Score: {stats.Data.Score?.Mean}");
```

### GetAnimeStatisticsHistoryAsync

Returns one entry per day with that day's score, rank, popularity, members and favorites, newest first. Overload: `(long id, StatisticsHistoryConfig config)`.

| Parameter | Type | Description |
|-----------|------|-------------|
| id | long | MAL id of anime |
| config | [StatisticsHistoryConfig](#statisticshistoryconfig) | (optional) Date range, order, paging |

**Returns:** `PaginatedTenraiResponse<ICollection<StatisticsHistoryEntry<AnimeStatistics>>>`

**Example:**

```csharp
var history = await tenrai.GetAnimeStatisticsHistoryAsync(1, new StatisticsHistoryConfig
{
    StartDate = new DateTime(2026, 4, 1),
    SortDirection = SortDirection.Ascending
});
foreach (var day in history.Data)
    Console.WriteLine($"{day.Date:yyyy-MM-dd}: {day.Members} members, {day.Statistics?.Completed} completed");
```

`Statistics` (the watching/completed/score breakdown) is null on days before 2026-04-04 and for titles under 1000 members.

**Model:** [StatisticsHistoryEntry&lt;T&gt;](Models.md#statisticshistoryentryt)

#### StatisticsHistoryConfig

| Property | Type | Description |
|----------|------|-------------|
| StartDate | DateTime? | First day to include |
| EndDate | DateTime? | Last day to include |
| SortDirection | SortDirection? | Not sent when null; the API returns newest first |
| Page | int? | 1-based page index |
| Limit | int? | Page size, 100 max |

### GetAnimeMoreInfoAsync

Returns additional information.

| Parameter | Type | Description |
|-----------|------|-------------|
| id | long | MAL id of anime |

**Returns:** `BaseTenraiResponse<MoreInfo>`

**Example:**

```csharp
var info = await tenrai.GetAnimeMoreInfoAsync(1);
Console.WriteLine(info.Data.MoreInfo);
```

### GetAnimeRecommendationsAsync

Returns anime recommendations. Overload: `(long id, RecommendationsConfig config)` with `Username` (only recommendations by that MyAnimeList user), `Sfw` and `SfwStrict`.

| Parameter | Type | Description |
|-----------|------|-------------|
| id | long | MAL id of anime |

**Returns:** `BaseTenraiResponse<ICollection<Recommendation>>`

**Example:**

```csharp
var recs = await tenrai.GetAnimeRecommendationsAsync(1);
foreach (var r in recs.Data)
    Console.WriteLine(r.Content);
```

**Model:** [Recommendation](Models.md#recommendation)

### GetAnimeReviewsAsync

Returns reviews. A simple `long id` overload returns the first page; a second overload takes a [`ReviewsSearchConfig`](#reviewssearchconfig) for paging, sorting, and filtering.

| Parameter | Type | Description |
|-----------|------|-------------|
| id | long | MAL id of anime |
| searchConfig | ReviewsSearchConfig | (overload) Page, sort, preliminary/spoiler filters, sentiment |

**Returns:** `PaginatedTenraiResponse<ICollection<Review>>`

**Example:**

```csharp
// Simple: first page
var reviews = await tenrai.GetAnimeReviewsAsync(1);

// Filtered: newest first, recommended sentiment, spoilers excluded
var filtered = await tenrai.GetAnimeReviewsAsync(1, new ReviewsSearchConfig
{
    Sort = ReviewSortOrder.Newest,
    Sentiment = ReviewSentiment.Recommended,
    Spoilers = InclusionFilter.Exclude
});
foreach (var r in filtered.Data)
    Console.WriteLine($"{r.User?.Username}: {r.Score} [{string.Join(", ", r.Tags)}]");
```

**Model:** [Review](Models.md#review) · **Config:** [ReviewsSearchConfig](#reviewssearchconfig)

### GetAnimeRelationsAsync

Returns related entries. Overload: `(long id, SfwConfig config)`, to override the [client SFW setting](GettingStarted.md#safe-for-work-filtering) for this call.

| Parameter | Type | Description |
|-----------|------|-------------|
| id | long | MAL id of anime |

**Returns:** `PaginatedTenraiResponse<ICollection<RelatedEntry>>`

**Example:**

```csharp
var relations = await tenrai.GetAnimeRelationsAsync(1);
foreach (var r in relations.Data)
    Console.WriteLine($"{r.Relation}: {r.Entry?.FirstOrDefault()?.Name}");
```

**Model:** [RelatedEntry](Models.md#relatedentry)

### GetAnimeThemesAsync

Returns openings and endings.

| Parameter | Type | Description |
|-----------|------|-------------|
| id | long | MAL id of anime |

**Returns:** `BaseTenraiResponse<AnimeThemes>`

**Example:**

```csharp
var themes = await tenrai.GetAnimeThemesAsync(1);
// themes.Data contains Openings, Endings
```

### GetAnimeExternalLinksAsync

Returns external services links.

| Parameter | Type | Description |
|-----------|------|-------------|
| id | long | MAL id of anime |

**Returns:** `BaseTenraiResponse<ICollection<ExternalLink>>`

**Example:**

```csharp
var links = await tenrai.GetAnimeExternalLinksAsync(1);
foreach (var l in links.Data)
    Console.WriteLine($"{l.Name}: {l.Url}");
```

**Model:** [ExternalLink](Models.md#externallink)

### GetAnimeStreamingLinksAsync

Returns streaming services links.

| Parameter | Type | Description |
|-----------|------|-------------|
| id | long | MAL id of anime |

**Returns:** `BaseTenraiResponse<ICollection<ExternalLink>>`

**Example:**

```csharp
var links = await tenrai.GetAnimeStreamingLinksAsync(1);
foreach (var l in links.Data)
    Console.WriteLine($"{l.Name}: {l.Url}");
```

### GetAnimeFullDataAsync

Returns anime with all additional data. Overload: `(long id, FullDataConfig config)` fetches up to 24 more anime in the same request, or searches the denied/deleted archive.

| Parameter | Type | Description |
|-----------|------|-------------|
| id | long | MAL id of anime |
| config | [FullDataConfig](#fulldataconfig) | (overload) Extra ids, archive mode, sfw filtering of relations |

**Returns:** `FullTenraiResponse<AnimeFull>` (`Included` and `MissingIds` are only filled by the overload)

**Example:**

```csharp
var full = await tenrai.GetAnimeFullDataAsync(1);
// full.Data contains anime plus relations, news, etc.

var batch = await tenrai.GetAnimeFullDataAsync(1, new FullDataConfig { Ids = new List<long> { 5, 6, 99999999 } });
// batch.Data is anime 1, batch.Included holds 5 and 6, batch.MissingIds is [99999999]

var deleted = await tenrai.GetAnimeFullDataAsync(2632, new FullDataConfig { Mode = FullDataMode.Deleted });
```

#### FullDataConfig

| Property | Type | Description |
|----------|------|-------------|
| Ids | ICollection&lt;long&gt; | Extra MAL ids, returned in `Included`. At most 24: the API allows 25 including the path id, and the client throws `TenraiValidationException` past that. Duplicates and the path id are dropped by the API |
| Mode | FullDataMode? | `Denied` or `Deleted` searches that archive only; an id not in it returns 404 (path id) or lands in `MissingIds` (extra ids). Null searches the regular catalogue |
| Sfw | bool? | Filter out adult entries. `null` (default) uses the [client setting](GettingStarted.md#safe-for-work-filtering) |
| SfwStrict | bool? | Also filter out ecchi entries. `null` (default) uses the [client setting](GettingStarted.md#safe-for-work-filtering) |

### GetUnapprovedAnimeAsync, GetDeniedAnimeAsync, GetDeletedAnimeAsync

Return anime still awaiting approval on MyAnimeList (`Approved` is `false`), submissions MyAnimeList denied, and anime deleted from it. Each has an overload taking a search config: [`CatalogueSearchConfig`](#cataloguesearchconfig) for unapproved and denied, [`DeletedSearchConfig`](#deletedsearchconfig) for deleted.

**Returns:** `PaginatedTenraiResponse<ICollection<Anime>>`

**Example:**

```csharp
var deleted = await tenrai.GetDeletedAnimeAsync(new DeletedSearchConfig
{
    OrderBy = DeletedOrderBy.Members,
    SortDirection = SortDirection.Descending
});
```

#### CatalogueSearchConfig

| Property | Type | Description |
|----------|------|-------------|
| Query | string | Search text matched against titles |
| OrderBy | CatalogueOrderBy | `MalId` or `Title`. These lists cannot be sorted by score or members |
| SortDirection | SortDirection | Direction for `OrderBy` |
| Page | int? | 1-based page index |
| Limit | int? | Page size, 100 max |
| Sfw | bool? | Filter out adult entries. `null` (default) uses the [client setting](GettingStarted.md#safe-for-work-filtering) |
| SfwStrict | bool? | Also filter out ecchi entries. `null` (default) uses the [client setting](GettingStarted.md#safe-for-work-filtering) |

#### DeletedSearchConfig

The same as `CatalogueSearchConfig`, with `OrderBy` a `DeletedOrderBy`: `MalId`, `Title`, `Score` or `Members`.

---

## Manga

### GetMangaAsync

Returns manga with given MAL id.

| Parameter | Type | Description |
|-----------|------|-------------|
| id | long | MAL id of manga |

**Returns:** `BaseTenraiResponse<Manga>`

**Example:**

```csharp
var manga = await tenrai.GetMangaAsync(2);
Console.WriteLine(manga.Data.Title);
Console.WriteLine(manga.Data.Status);
```

**Model:** [Manga](Models.md#manga)

### GetMangaCharactersAsync

Returns characters appearing in manga.

| Parameter | Type | Description |
|-----------|------|-------------|
| id | long | MAL id of manga |

**Returns:** `BaseTenraiResponse<ICollection<MangaCharacter>>`

**Example:**

```csharp
var characters = await tenrai.GetMangaCharactersAsync(2);
foreach (var c in characters.Data)
    Console.WriteLine($"{c.Character.Name} - {c.Role}");
```

**Model:** [MangaCharacter](Models.md#mangacharacter)

### GetMangaNewsAsync

Returns news. Overload: `(long id, int page)`.

| Parameter | Type | Description |
|-----------|------|-------------|
| id | long | MAL id of manga |
| page | int | (optional) Page index |

**Returns:** `PaginatedTenraiResponse<ICollection<News>>`

**Example:**

```csharp
var news = await tenrai.GetMangaNewsAsync(2);
foreach (var n in news.Data)
    Console.WriteLine(n.Title);
```

### GetMangaForumTopicsAsync

Returns chapter discussion threads, 30 per page. Other forum topics are not available. Overload: `(long id, MangaForumSearchConfig searchConfig)`.

| Parameter | Type | Description |
|-----------|------|-------------|
| id | long | MAL id of manga |
| searchConfig | MangaForumSearchConfig | (optional) `Page`, `OrderBy` (`ChapterNumber`, `CreatedOn`, `CommentCount`, `LastPost`), `SortDirection` |

**Returns:** `PaginatedTenraiResponse<ICollection<ForumTopic>>`

**Example:**

```csharp
var latest = await tenrai.GetMangaForumTopicsAsync(2, new MangaForumSearchConfig
{
    OrderBy = MangaForumOrderBy.ChapterNumber,
    SortDirection = SortDirection.Descending
});
```

**Model:** [ForumTopic](Models.md#forumtopic)

### GetMangaPicturesAsync

Returns links to pictures.

| Parameter | Type | Description |
|-----------|------|-------------|
| id | long | MAL id of manga |

**Returns:** `BaseTenraiResponse<ICollection<ImagesSet>>`

**Example:**

```csharp
var pictures = await tenrai.GetMangaPicturesAsync(2);
```

### GetMangaStatisticsAsync

Returns statistics.

| Parameter | Type | Description |
|-----------|------|-------------|
| id | long | MAL id of manga |

**Returns:** `BaseTenraiResponse<MangaStatistics>`

**Example:**

```csharp
var stats = await tenrai.GetMangaStatisticsAsync(2);
Console.WriteLine($"Score: {stats.Data.Score?.Mean}");
```

### GetMangaStatisticsHistoryAsync

Returns one entry per day for a manga, same as [GetAnimeStatisticsHistoryAsync](#getanimestatisticshistoryasync), with `MangaStatistics` (reading, plan to read, ...) in `Statistics`. Overload: `(long id, StatisticsHistoryConfig config)`.

**Returns:** `PaginatedTenraiResponse<ICollection<StatisticsHistoryEntry<MangaStatistics>>>`

**Example:**

```csharp
var history = await tenrai.GetMangaStatisticsHistoryAsync(2);
Console.WriteLine(history.Data.First().Statistics?.Reading);
```

### GetMangaMoreInfoAsync

Returns additional information.

| Parameter | Type | Description |
|-----------|------|-------------|
| id | long | MAL id of manga |

**Returns:** `BaseTenraiResponse<MoreInfo>`

**Example:**

```csharp
var info = await tenrai.GetMangaMoreInfoAsync(2);
```

### GetMangaRecommendationsAsync

Returns manga recommendations. Overload: `(long id, RecommendationsConfig config)` with `Username` (only recommendations by that MyAnimeList user), `Sfw` and `SfwStrict`.

| Parameter | Type | Description |
|-----------|------|-------------|
| id | long | MAL id of manga |

**Returns:** `BaseTenraiResponse<ICollection<Recommendation>>`

**Example:**

```csharp
var recs = await tenrai.GetMangaRecommendationsAsync(2);
```

### GetMangaReviewsAsync

Returns reviews. A simple `long id` overload returns the first page; a second overload takes a [`ReviewsSearchConfig`](#reviewssearchconfig) for paging, sorting, and filtering.

| Parameter | Type | Description |
|-----------|------|-------------|
| id | long | MAL id of manga |
| searchConfig | ReviewsSearchConfig | (overload) Page, sort, preliminary/spoiler filters, sentiment |

**Returns:** `PaginatedTenraiResponse<ICollection<Review>>`

**Example:**

```csharp
var reviews = await tenrai.GetMangaReviewsAsync(2);

// Only spoiler reviews, oldest first
var spoilers = await tenrai.GetMangaReviewsAsync(2, new ReviewsSearchConfig
{
    Spoilers = InclusionFilter.Only,
    Sort = ReviewSortOrder.Oldest
});
```

**Model:** [Review](Models.md#review) · **Config:** [ReviewsSearchConfig](#reviewssearchconfig)

### GetMangaRelationsAsync

Returns related entries. Overload: `(long id, SfwConfig config)`, to override the [client SFW setting](GettingStarted.md#safe-for-work-filtering) for this call.

| Parameter | Type | Description |
|-----------|------|-------------|
| id | long | MAL id of manga |

**Returns:** `PaginatedTenraiResponse<ICollection<RelatedEntry>>`

**Example:**

```csharp
var relations = await tenrai.GetMangaRelationsAsync(2);
```

### GetMangaExternalLinksAsync

Returns external services links.

| Parameter | Type | Description |
|-----------|------|-------------|
| id | long | MAL id of manga |

**Returns:** `BaseTenraiResponse<ICollection<ExternalLink>>`

**Example:**

```csharp
var links = await tenrai.GetMangaExternalLinksAsync(2);
```

### GetMangaFullDataAsync

Returns manga with all additional data. Overload: `(long id, FullDataConfig config)`, same as [GetAnimeFullDataAsync](#getanimefulldataasync).

| Parameter | Type | Description |
|-----------|------|-------------|
| id | long | MAL id of manga |
| config | [FullDataConfig](#fulldataconfig) | (overload) Extra ids, archive mode, sfw filtering of relations |

**Returns:** `FullTenraiResponse<MangaFull>` (`Included` and `MissingIds` are only filled by the overload)

**Example:**

```csharp
var full = await tenrai.GetMangaFullDataAsync(2);
```

### GetUnapprovedMangaAsync, GetDeniedMangaAsync, GetDeletedMangaAsync

Same as the [anime lists](#getunapprovedanimeasync-getdeniedanimeasync-getdeletedanimeasync), returning `PaginatedTenraiResponse<ICollection<Manga>>`. Overloads take a [`CatalogueSearchConfig`](#cataloguesearchconfig) or [`DeletedSearchConfig`](#deletedsearchconfig).

### GetAdaptedMangaAsync

Returns manga that have an anime adaptation. Overload: `(AdaptedMangaSearchConfig searchConfig)`.

**Returns:** `PaginatedTenraiResponse<ICollection<Manga>>`

**Example:**

```csharp
var airing = await tenrai.GetAdaptedMangaAsync(new AdaptedMangaSearchConfig
{
    Filter = AdaptedMangaFilter.Airing,
    Types = new List<MangaType> { MangaType.Manga, MangaType.Manhwa },
    MinimumScore = 8
});
```

#### AdaptedMangaSearchConfig

| Property | Type | Description |
|----------|------|-------------|
| Filter | AdaptedMangaFilter? | `All`, `Airing` (adaptation airing now) or `Upcoming`. Not sent when null, which the API treats as `All` |
| Types | ICollection&lt;MangaType&gt; | Manga types to include. `MangaType.Novel` and `MangaType.LightNovel` are rejected by the API here |
| Status | PublishingStatus? | Publishing status of the manga |
| Genres | ICollection&lt;MangaGenreSearch&gt; | Genres the manga must have |
| Score | double? | Exact score. The API rejects it combined with `MinimumScore` or `MaximumScore` |
| MinimumScore, MaximumScore | double? | Score range |
| StartDate, EndDate | DateTime? | Publishing date range |
| OrderBy | AdaptedMangaOrderBy | `MalId`, `Title`, `StartDate`, `EndDate`, `Chapters`, `Volumes`, `Score`, `ScoredBy`, `Rank`, `Popularity`, `Members`, `Favorites` |
| SortDirection | SortDirection | Direction for `OrderBy` |
| Page | int? | 1-based page index |
| Limit | int? | Page size, 100 max |
| Sfw | bool? | Filter out adult entries. `null` (default) uses the [client setting](GettingStarted.md#safe-for-work-filtering) |
| SfwStrict | bool? | Also filter out ecchi entries. `null` (default) uses the [client setting](GettingStarted.md#safe-for-work-filtering) |

---

## Character

### GetCharacterAsync

Returns character with given MAL id.

| Parameter | Type | Description |
|-----------|------|-------------|
| id | long | MAL id of character |

**Returns:** `BaseTenraiResponse<Character>`

**Example:**

```csharp
var character = await tenrai.GetCharacterAsync(2219);
Console.WriteLine(character.Data.Name);
Console.WriteLine(character.Data.About);
```

**Model:** [Character](Models.md#character)

### GetCharacterAnimeAsync

Returns animeography (anime where character appeared). Overload: `(long id, SfwConfig config)`, to override the [client SFW setting](GettingStarted.md#safe-for-work-filtering) for this call.

| Parameter | Type | Description |
|-----------|------|-------------|
| id | long | MAL id of character |

**Returns:** `BaseTenraiResponse<ICollection<CharacterAnimeographyEntry>>`

**Example:**

```csharp
var anime = await tenrai.GetCharacterAnimeAsync(2219);
foreach (var entry in anime.Data)
    Console.WriteLine($"{entry.Anime?.Title} - {entry.Role}");
```

### GetCharacterMangaAsync

Returns mangaography (manga where character appeared). Overload: `(long id, SfwConfig config)`, to override the [client SFW setting](GettingStarted.md#safe-for-work-filtering) for this call.

| Parameter | Type | Description |
|-----------|------|-------------|
| id | long | MAL id of character |

**Returns:** `BaseTenraiResponse<ICollection<CharacterMangaographyEntry>>`

**Example:**

```csharp
var manga = await tenrai.GetCharacterMangaAsync(2219);
foreach (var entry in manga.Data)
    Console.WriteLine(entry.Manga?.Title);
```

### GetCharacterVoiceActorsAsync

Returns voice actors.

| Parameter | Type | Description |
|-----------|------|-------------|
| id | long | MAL id of character |

**Returns:** `BaseTenraiResponse<ICollection<VoiceActorEntry>>`

**Example:**

```csharp
var va = await tenrai.GetCharacterVoiceActorsAsync(2219);
foreach (var entry in va.Data)
    Console.WriteLine($"{entry.Person.Name} ({entry.Language})");
```

### GetCharacterPicturesAsync

Returns links to pictures.

| Parameter | Type | Description |
|-----------|------|-------------|
| id | long | MAL id of character |

**Returns:** `BaseTenraiResponse<ICollection<ImagesSet>>`

**Example:**

```csharp
var pictures = await tenrai.GetCharacterPicturesAsync(2219);
```

### GetCharacterFullDataAsync

Returns character with all additional data. Overload: `(long id, SfwConfig config)`, to override the [client SFW setting](GettingStarted.md#safe-for-work-filtering) for this call.

| Parameter | Type | Description |
|-----------|------|-------------|
| id | long | MAL id of character |

**Returns:** `BaseTenraiResponse<CharacterFull>`

**Example:**

```csharp
var full = await tenrai.GetCharacterFullDataAsync(2219);
```

---

## Person

### GetPersonAsync

Returns person with given MAL id.

| Parameter | Type | Description |
|-----------|------|-------------|
| id | long | MAL id of person |

**Returns:** `BaseTenraiResponse<Person>`

**Example:**

```csharp
var person = await tenrai.GetPersonAsync(1870);
Console.WriteLine(person.Data.FamilyName);
Console.WriteLine(person.Data.GivenName);
```

**Model:** [Person](Models.md#person)

### GetPersonAnimeAsync

Returns animeography (anime the person collaborated on). Overload: `(long id, SfwConfig config)`, to override the [client SFW setting](GettingStarted.md#safe-for-work-filtering) for this call.

| Parameter | Type | Description |
|-----------|------|-------------|
| id | long | MAL id of person |

**Returns:** `BaseTenraiResponse<ICollection<PersonAnimeographyEntry>>`

**Example:**

```csharp
var anime = await tenrai.GetPersonAnimeAsync(1870);
foreach (var entry in anime.Data)
    Console.WriteLine(entry.Anime?.Title);
```

### GetPersonMangaAsync

Returns mangaography (manga the person worked on). Overload: `(long id, SfwConfig config)`, to override the [client SFW setting](GettingStarted.md#safe-for-work-filtering) for this call.

| Parameter | Type | Description |
|-----------|------|-------------|
| id | long | MAL id of person |

**Returns:** `BaseTenraiResponse<ICollection<PersonMangaographyEntry>>`

**Example:**

```csharp
var manga = await tenrai.GetPersonMangaAsync(1870);
```

### GetPersonVoiceActingRolesAsync

Returns voice acting roles. Overload: `(long id, SfwConfig config)`, to override the [client SFW setting](GettingStarted.md#safe-for-work-filtering) for this call.

| Parameter | Type | Description |
|-----------|------|-------------|
| id | long | MAL id of person |

**Returns:** `BaseTenraiResponse<ICollection<VoiceActingRole>>`

**Example:**

```csharp
var roles = await tenrai.GetPersonVoiceActingRolesAsync(1870);
foreach (var r in roles.Data)
    Console.WriteLine($"{r.Character?.Name} in {r.Anime?.Title}");
```

### GetPersonPicturesAsync

Returns links to pictures.

| Parameter | Type | Description |
|-----------|------|-------------|
| id | long | MAL id of person |

**Returns:** `BaseTenraiResponse<ICollection<ImagesSet>>`

**Example:**

```csharp
var pictures = await tenrai.GetPersonPicturesAsync(1870);
```

### GetPersonFullDataAsync

Returns person with all additional data. Overload: `(long id, SfwConfig config)`, to override the [client SFW setting](GettingStarted.md#safe-for-work-filtering) for this call.

| Parameter | Type | Description |
|-----------|------|-------------|
| id | long | MAL id of person |

**Returns:** `BaseTenraiResponse<PersonFull>`

**Example:**

```csharp
var full = await tenrai.GetPersonFullDataAsync(1870);
```

---

## Season

### GetSeasonAsync

Returns season preview. Overloads: `(int year, Season season, int page)`, `(int year, Season season, SeasonSearchConfig searchConfig)`. `GetCurrentSeasonAsync`, `GetPreviousSeasonAsync` and `GetUpcomingSeasonAsync` take the same config.

| Parameter | Type | Description |
|-----------|------|-------------|
| year | int | Year of season |
| season | Season | Season (Winter, Spring, Summer, Fall) |
| page | int | (optional) Page index |

**Returns:** `PaginatedTenraiResponse<ICollection<Anime>>`

**Example:**

```csharp
var season = await tenrai.GetSeasonAsync(2024, Season.Spring);
foreach (var anime in season.Data)
    Console.WriteLine(anime.Title);

// TV and ONA only, with shows continuing from earlier seasons
var current = await tenrai.GetCurrentSeasonAsync(new SeasonSearchConfig
{
    Types = { AnimeType.TV, AnimeType.ONA },
    IncludeContinuing = true
});
```

#### SeasonSearchConfig

| Property | Type | Description |
|----------|------|-------------|
| Types | ICollection&lt;AnimeType&gt; | Anime types to include |
| Status | AiringStatus? | Airing status. The upcoming list ignores it |
| Ratings | ICollection&lt;AnimeAgeRating&gt; | Age ratings to include |
| Genres, ExcludedGenres | ICollection&lt;AnimeGenreSearch&gt; | Genres to require or leave out |
| ExcludedIds | ICollection&lt;long&gt; | MAL ids to leave out |
| MinimumMembers ... MaximumEpisodes | int? | Member and episode count ranges |
| ExcludeKids | bool | Leave out kids titles |
| IncludeContinuing | bool | Also return TV series continuing from an earlier season (current season: 140 becomes 184) |
| Unapproved | InclusionFilter? | `Include` adds unapproved entries, `Only` returns only them |
| OrderBy, SortDirection | SeasonOrderBy, SortDirection | Members descending when not set |
| Page, Limit | int? | Paging, 100 max |
| Sfw, SfwStrict | bool? | Filter out adult and ecchi entries. `null` (default) uses the [client setting](GettingStarted.md#safe-for-work-filtering) |

Every overload, plain or config, applies the [client setting](GettingStarted.md#safe-for-work-filtering) unless the config sets `Sfw` itself.

### GetSeasonArchiveAsync

Returns list of available seasons.

**Returns:** `PaginatedTenraiResponse<ICollection<SeasonArchive>>`

**Example:**

```csharp
var archive = await tenrai.GetSeasonArchiveAsync();
foreach (var s in archive.Data)
    Console.WriteLine($"{s.Year} {s.Season}");
```

### GetCurrentSeasonAsync

Returns current airing season. Overload: `(int page)`.

| Parameter | Type | Description |
|-----------|------|-------------|
| page | int | (optional) Page index |

**Returns:** `PaginatedTenraiResponse<ICollection<Anime>>`

**Example:**

```csharp
var current = await tenrai.GetCurrentSeasonAsync();
```

### GetUpcomingSeasonAsync

Returns anime with undefined airing (marked "Later" on MAL). Overload: `(int page)`.

| Parameter | Type | Description |
|-----------|------|-------------|
| page | int | (optional) Page index |

**Returns:** `PaginatedTenraiResponse<ICollection<Anime>>`

**Example:**

```csharp
var upcoming = await tenrai.GetUpcomingSeasonAsync();
```

### GetPreviousSeasonAsync

Returns the season before the current one. The list also carries shows still airing from earlier seasons. Overload: `(int page)`.

| Parameter | Type | Description |
|-----------|------|-------------|
| page | int | (optional) Page index |

**Returns:** `PaginatedTenraiResponse<ICollection<Anime>>`

**Example:**

```csharp
var previous = await tenrai.GetPreviousSeasonAsync();
```

---

## Schedule

### GetScheduleAsync

Returns current season schedule. Overloads: `(int page)`, `(ScheduledDay scheduledDay)`, `(ScheduleSearchConfig searchConfig)`.

| Parameter | Type | Description |
|-----------|------|-------------|
| page | int | (optional) Page index |
| scheduledDay | ScheduledDay | (optional) Filter by day |
| searchConfig | ScheduleSearchConfig | (optional) `Day`, `Types`, `Ratings`, `Genres`, `ExcludedGenres`, `ExcludeKids`, `Unapproved`, `Page`, `Limit`, `Sfw`, `SfwStrict` |

**Returns:** `PaginatedTenraiResponse<ICollection<Anime>>`

**Example:**

```csharp
var schedule = await tenrai.GetScheduleAsync(ScheduledDay.Monday);
foreach (var anime in schedule.Data)
    Console.WriteLine(anime.Title);
```

---

## Top

### GetTopAnimeAsync

Returns top anime. Overloads: `(int page)`, `(TopAnimeFilter filter)`, `(TopAnimeFilter filter, int page)`, `(AnimeTopSearchConfig searchConfig)`.

| Parameter | Type | Description |
|-----------|------|-------------|
| page | int | (optional) Page index |
| filter | TopAnimeFilter | (optional) Airing, Upcoming, Popularity, Favorite |
| searchConfig | AnimeTopSearchConfig | (optional) Advanced filters |

**Returns:** `PaginatedTenraiResponse<ICollection<Anime>>`

**Example:**

```csharp
var top = await tenrai.GetTopAnimeAsync();
foreach (var anime in top.Data)
    Console.WriteLine($"{anime.Rank}. {anime.Title}");

var topMovies = await tenrai.GetTopAnimeAsync(new AnimeTopSearchConfig { Types = { AnimeType.Movie } });
```

#### AnimeTopSearchConfig and MangaTopSearchConfig

| Property | Type | Description |
|----------|------|-------------|
| Filter | TopAnimeFilter? / TopMangaFilter? | Airing or publishing, upcoming, by popularity, by favorites. `null` (default) for the overall ranking |
| Types | ICollection&lt;AnimeType&gt; / &lt;MangaType&gt; | Types to include |
| Ratings | ICollection&lt;AnimeAgeRating&gt; | (anime) Age ratings to include |
| Genres, ExcludedGenres | ICollection of the genre enum | Genres to require or leave out |
| ExcludedIds | ICollection&lt;long&gt; | MAL ids to leave out |
| MinimumMembers, MaximumMembers | int? | Member range |
| MinimumEpisodes, MaximumEpisodes | int? | (anime) Episode range |
| MinimumChapters ... MaximumVolumes | int? | (manga) Chapter and volume ranges |
| SortDirection | SortDirection? | `Descending` reverses the ranking |
| Page, Limit | int? | Paging, 100 max |
| Sfw, SfwStrict | bool? | Filter out adult and ecchi entries. `null` (default) uses the [client setting](GettingStarted.md#safe-for-work-filtering) |

### GetTopMangaAsync

Returns top manga. Overloads: `(int page)`, `(MangaTopSearchConfig searchConfig)`.

| Parameter | Type | Description |
|-----------|------|-------------|
| page | int | (optional) Page index |
| searchConfig | MangaTopSearchConfig | (optional) Advanced filters |

**Returns:** `PaginatedTenraiResponse<ICollection<Manga>>`

**Example:**

```csharp
var top = await tenrai.GetTopMangaAsync();
```

### GetTopPeopleAsync

Returns most popular people. Overloads: `(int page)` and `(PagingConfig config)`, which takes `Page` and `Limit` (100 max).

| Parameter | Type | Description |
|-----------|------|-------------|
| page | int | (optional) Page index |

**Returns:** `PaginatedTenraiResponse<ICollection<Person>>`

**Example:**

```csharp
var top = await tenrai.GetTopPeopleAsync();
```

### GetTopCharactersAsync

Returns most popular characters. Overloads: `(int page)` and `(PagingConfig config)`, which takes `Page` and `Limit` (100 max).

| Parameter | Type | Description |
|-----------|------|-------------|
| page | int | (optional) Page index |

**Returns:** `PaginatedTenraiResponse<ICollection<Character>>`

**Example:**

```csharp
var top = await tenrai.GetTopCharactersAsync();
```

### GetTopReviewsAsync

Returns most popular reviews. A parameterless overload returns the first page; a second overload takes a [`TopReviewsConfig`](#topreviewsconfig) (use `Type` to restrict to anime or manga).

| Parameter | Type | Description |
|-----------|------|-------------|
| searchConfig | TopReviewsConfig | (overload) Type, username, page, limit, preliminary/spoiler, SFW |

**Returns:** `PaginatedTenraiResponse<ICollection<Review>>`

**Example:**

```csharp
var top = await tenrai.GetTopReviewsAsync();

// Top manga reviews only
var topManga = await tenrai.GetTopReviewsAsync(new TopReviewsConfig
{
    Type = ReviewTargetType.Manga
});
```

**Model:** [Review](Models.md#review) · **Config:** [TopReviewsConfig](#topreviewsconfig)

### GetTopAnimeRecommendationsAsync, GetTopMangaRecommendationsAsync

Return the user recommendations ("if you liked X, try Y") with the most votes. Each has an overload taking a `TopRecommendationsConfig` (`Page`, `Limit`, `Sfw`, `SfwStrict`).

**Returns:** `PaginatedTenraiResponse<ICollection<TopRecommendation>>`

**Example:**

```csharp
var top = await tenrai.GetTopAnimeRecommendationsAsync();
// top.Data.First(): Death Note and Code Geass, Votes 650
```

**Model:** [TopRecommendation](Models.md#toprecommendation)

### GetTopEpisodesAsync

Returns the top anime episodes, each with the anime it belongs to. Without `OrderBy` the API ranks by score weighed by votes, so a 4.9 with 4341 votes comes before a 4.97 with 260. An overload takes a `TopEpisodesConfig`.

#### TopEpisodesConfig

| Property | Type | Description |
|----------|------|-------------|
| OrderBy | TopEpisodesOrderBy | `Score`, `Votes` or `Comments`, always descending |
| Page | int? | 1-based page index |
| Limit | int? | Page size, 100 max |
| Sfw | bool? | Filter out adult entries. `null` (default) uses the [client setting](GettingStarted.md#safe-for-work-filtering) |
| SfwStrict | bool? | Also filter out ecchi entries. `null` (default) uses the [client setting](GettingStarted.md#safe-for-work-filtering) |

**Returns:** `PaginatedTenraiResponse<ICollection<TopEpisode>>`

**Example:**

```csharp
var mostVoted = await tenrai.GetTopEpisodesAsync(new TopEpisodesConfig { OrderBy = TopEpisodesOrderBy.Votes });
```

**Model:** [TopEpisode](Models.md#topepisode)

### GetTopVideosAsync

Returns anime promos and music videos, most viewed on YouTube first. An overload takes a `TopVideosConfig`.

#### TopVideosConfig

| Property | Type | Description |
|----------|------|-------------|
| Filter | TopVideosFilter? | `All`, `Airing`, `Upcoming` or `Finished`. Not sent when null |
| Type | TopVideoType? | `Promo` or `Music`. Both when null |
| Year | int? | Year published. Cannot be combined with `MinimumYear`/`MaximumYear` (the API returns 400) |
| MinimumYear, MaximumYear | int? | Year range |
| OrderBy | TopVideosOrderBy | `Views`, `Likes`, `Dislikes`, `Comments`, `Title` or `PublishedAt` |
| SortDirection | SortDirection | Direction for `OrderBy` |
| Page | int? | 1-based page index |
| Limit | int? | Page size, 100 max |
| Sfw | bool? | Filter out adult entries. `null` (default) uses the [client setting](GettingStarted.md#safe-for-work-filtering) |
| SfwStrict | bool? | Also filter out ecchi entries. `null` (default) uses the [client setting](GettingStarted.md#safe-for-work-filtering) |

**Returns:** `PaginatedTenraiResponse<ICollection<TopVideo>>`

**Example:**

```csharp
var music = await tenrai.GetTopVideosAsync(new TopVideosConfig { Type = TopVideoType.Music });
var song = music.Data.First().Entry.First().Meta; // Title and Author (the artist)
```

**Model:** [TopVideo](Models.md#topvideo)

---

## Genre

### GetAnimeGenresAsync

Returns anime genres. Overloads: `(GenresFilter filter)` and `(GenresConfig config)`, which takes `Filter`, `Sfw` and `SfwStrict` (`Sfw` leaves out Hentai and Erotica, `SfwStrict` also Ecchi).

| Parameter | Type | Description |
|-----------|------|-------------|
| filter | GenresFilter | (optional) Filter for genre types |

**Returns:** `BaseTenraiResponse<ICollection<Genre>>`

**Example:**

```csharp
var genres = await tenrai.GetAnimeGenresAsync();
foreach (var g in genres.Data)
    Console.WriteLine(g.Name);
```

### GetMangaGenresAsync

Returns manga genres. Overloads: `(GenresFilter filter)` and `(GenresConfig config)`, which takes `Filter`, `Sfw` and `SfwStrict` (`Sfw` leaves out Hentai and Erotica, `SfwStrict` also Ecchi).

| Parameter | Type | Description |
|-----------|------|-------------|
| filter | GenresFilter | (optional) Filter for genre types |

**Returns:** `BaseTenraiResponse<ICollection<Genre>>`

**Example:**

```csharp
var genres = await tenrai.GetMangaGenresAsync();
```

---

## Producer

### GetProducersAsync, SearchProducersAsync

Returns producers list. `GetProducersAsync` has overloads `()` and `(int page)`; `SearchProducersAsync(ProducerSearchConfig searchConfig)` searches, filters and sorts.

| Parameter | Type | Description |
|-----------|------|-------------|
| page | int | (optional) Page index |
| searchConfig | ProducerSearchConfig | (`SearchProducersAsync`) Search, filters and sorting |

**Returns:** `PaginatedTenraiResponse<ICollection<Producer>>`

**Example:**

```csharp
var producers = await tenrai.GetProducersAsync();

// Studios active since 2020, most credits first
var studios = await tenrai.SearchProducersAsync(new ProducerSearchConfig
{
    Roles = { ProducerRole.Studio },
    ActiveAfter = 2020,
    OrderBy = ProducerSearchOrderBy.Count,
    SortDirection = SortDirection.Descending
});
```

#### ProducerSearchConfig

| Property | Type | Description |
|----------|------|-------------|
| Query, Letter | string, char? | Name search |
| Roles | ICollection&lt;ProducerRole&gt; | `Studio`, `Producer`, `Licensor` |
| AnimeTypes, Sources | ICollection&lt;AnimeType&gt;, &lt;AnimeSource&gt; | Companies credited on anime of these types or sources |
| Genres, ExcludedGenres | ICollection&lt;AnimeGenreSearch&gt; | Genres of the credited anime |
| WorkedWithIds | ICollection&lt;long&gt; | Companies that worked on an anime with any of these |
| HasAbout, HasImage, HasEstablished, Dissolved, Airing, Upcoming | bool? | `true` or `false` filters |
| MinimumFavorites, MaximumFavorites, MinimumAnime, MaximumAnime | int? | Favorites and credited-anime ranges |
| Seasons, SeasonYears | ICollection&lt;Season&gt;, &lt;int&gt; | Debut seasons of the credited anime |
| ActiveAfter, ActiveBefore, DebutAfter, LastCreditBefore, EstablishedAfter, EstablishedBefore | int? | Years, e.g. `2020` |
| OrderBy, SortDirection | ProducerSearchOrderBy, SortDirection | `MalId`, `Name`, `Count`, `Favorites`, `Established`, `Debut`, `LastCredit` |
| Page, Limit | int? | Paging, 100 max |
| Sfw, SfwStrict | bool? | `null` (default) uses the [client setting](GettingStarted.md#safe-for-work-filtering) |

### GetProducerAsync

Returns producer by id.

| Parameter | Type | Description |
|-----------|------|-------------|
| id | long | MAL id of producer |

**Returns:** `BaseTenraiResponse<Producer>`

**Example:**

```csharp
var producer = await tenrai.GetProducerAsync(1);
```

### GetProducerExternalLinksAsync

Returns external links related to producer.

| Parameter | Type | Description |
|-----------|------|-------------|
| id | long | MAL id of producer |

**Returns:** `BaseTenraiResponse<ICollection<ExternalLink>>`

**Example:**

```csharp
var links = await tenrai.GetProducerExternalLinksAsync(1);
```

### GetProducerFullDataAsync

Returns full producer data.

| Parameter | Type | Description |
|-----------|------|-------------|
| id | long | MAL id of producer |

**Returns:** `BaseTenraiResponse<ProducerFull>`

**Example:**

```csharp
var full = await tenrai.GetProducerFullDataAsync(1);
```

---

## Magazine

### GetMagazinesAsync, SearchMagazinesAsync

Returns magazines list. `GetMagazinesAsync` has overloads `()` and `(int page)`; `SearchMagazinesAsync(MagazineSearchConfig searchConfig)` takes `Query`, `Letter`, `OrderBy` (`MalId`, `Name`, `Count`), `SortDirection`, `Page`, `Limit`.

| Parameter | Type | Description |
|-----------|------|-------------|
| page | int | (optional) Page index |
| searchConfig | MagazineSearchConfig | (`SearchMagazinesAsync`) Search and sorting |

**Returns:** `PaginatedTenraiResponse<ICollection<Magazine>>`

**Example:**

```csharp
var magazines = await tenrai.GetMagazinesAsync();
```

---

## Random

### GetRandomAnimeAsync

Gets random anime. Overload: `(RandomAnimeConfig config)` with `Types`, `Status`, `Ratings`, `Sources`, `HasTrailer`, `Genres`, `ExcludedGenres`, `ProducerIds`, `ExcludedIds`, `MinimumScore`/`MaximumScore`, `Year` or `MinimumYear`/`MaximumYear` (not both, the API returns 400), `Sfw`, `SfwStrict`.

**Returns:** `BaseTenraiResponse<Anime>`

**Example:**

```csharp
var anime = await tenrai.GetRandomAnimeAsync();
Console.WriteLine(anime.Data.Title);

var movie = await tenrai.GetRandomAnimeAsync(new RandomAnimeConfig { Types = { AnimeType.Movie }, MinimumScore = 8 });
```

### GetRandomMangaAsync

Gets random manga. Overload: `(RandomMangaConfig config)` with `Types`, `Status`, `Genres`, `ExcludedGenres`, `AuthorIds`, `ExcludedIds`, score and year filters as for anime, `Sfw`, `SfwStrict`.

**Returns:** `BaseTenraiResponse<Manga>`

**Example:**

```csharp
var manga = await tenrai.GetRandomMangaAsync();
```

### GetRandomCharacterAsync

Gets random character. Overload: `(RandomFavoritesConfig config)` with `MinimumFavorites`/`MaximumFavorites`.

**Returns:** `BaseTenraiResponse<Character>`

**Example:**

```csharp
var character = await tenrai.GetRandomCharacterAsync();
```

### GetRandomPersonAsync

Gets random person. Overload: `(RandomFavoritesConfig config)` with `MinimumFavorites`/`MaximumFavorites`.

**Returns:** `BaseTenraiResponse<Person>`

**Example:**

```csharp
var person = await tenrai.GetRandomPersonAsync();
```

## Recommendations

### GetRecentAnimeRecommendationsAsync

Returns recently added anime recommendations. Overloads: `(int page)` and `(RecentRecommendationsConfig config)`, which takes `Page`, `Limit` (100 max and default), `Username`, `Sfw` and `SfwStrict`.

| Parameter | Type | Description |
|-----------|------|-------------|
| page | int | (optional) Page index |

**Returns:** `PaginatedTenraiResponse<ICollection<UserRecommendation>>`

**Example:**

```csharp
var recs = await tenrai.GetRecentAnimeRecommendationsAsync();
```

### GetRecentMangaRecommendationsAsync

Returns recently added manga recommendations. Overloads: `(int page)` and `(RecentRecommendationsConfig config)`, which takes `Page`, `Limit` (100 max and default), `Username`, `Sfw` and `SfwStrict`.

| Parameter | Type | Description |
|-----------|------|-------------|
| page | int | (optional) Page index |

**Returns:** `PaginatedTenraiResponse<ICollection<UserRecommendation>>`

**Example:**

```csharp
var recs = await tenrai.GetRecentMangaRecommendationsAsync();
```

---

## Reviews

### GetRecentAnimeReviewsAsync

Returns recently added anime reviews. A parameterless overload returns the first page; a second overload takes a [`RecentReviewsConfig`](#recentreviewsconfig).

| Parameter | Type | Description |
|-----------|------|-------------|
| searchConfig | RecentReviewsConfig | (overload) Page, limit, username, sort, filters, sentiment, SFW |

**Returns:** `PaginatedTenraiResponse<ICollection<Review>>`

**Example:**

```csharp
var reviews = await tenrai.GetRecentAnimeReviewsAsync();
```

### GetRecentMangaReviewsAsync

Returns recently added manga reviews. A parameterless overload returns the first page; a second overload takes a [`RecentReviewsConfig`](#recentreviewsconfig).

| Parameter | Type | Description |
|-----------|------|-------------|
| searchConfig | RecentReviewsConfig | (overload) Page, limit, username, sort, filters, sentiment, SFW |

**Returns:** `PaginatedTenraiResponse<ICollection<Review>>`

**Example:**

```csharp
var reviews = await tenrai.GetRecentMangaReviewsAsync();
```

---

## Search

Every search config sends its values URL-encoded, so a query like `"C#"` or `"Tom & Jerry"` reaches the API as typed. Shared properties:

| Property | Type | Description |
|----------|------|-------------|
| Limit | int? | Entries per page, 100 max. The API returns 25 when unset |
| Sfw | bool? | Filter out adult entries. `null` (default) uses the [client setting](GettingStarted.md#safe-for-work-filtering) |
| SfwStrict | bool? | Also filter out ecchi entries. `null` (default) uses the [client setting](GettingStarted.md#safe-for-work-filtering) |
| MinimumScore, MaximumScore | double? | Score range, e.g. `7.5` (anime and manga search) |

### SearchAnimeAsync

Returns anime search results. Overload: `(AnimeSearchConfig searchConfig)`.

| Parameter | Type | Description |
|-----------|------|-------------|
| query | string | Search query |
| searchConfig | AnimeSearchConfig | (optional) Advanced filters |

**Returns:** `PaginatedTenraiResponse<ICollection<Anime>>`

**Example:**

```csharp
var results = await tenrai.SearchAnimeAsync("cowboy bebop");
foreach (var anime in results.Data)
    Console.WriteLine(anime.Title);
```

### SearchMangaAsync

Returns manga search results. Overload: `(MangaSearchConfig searchConfig)`.

| Parameter | Type | Description |
|-----------|------|-------------|
| query | string | Search query |
| searchConfig | MangaSearchConfig | (optional) Advanced filters |

**Returns:** `PaginatedTenraiResponse<ICollection<Manga>>`

**Example:**

```csharp
var results = await tenrai.SearchMangaAsync("berserk");
```

#### AnimeSearchConfig and MangaSearchConfig

List filters match any of their values (genres must all match). The API rejects a list over 25 values, 1000 for `Ids`/`ExcludedIds`, and these combinations: `Years` with `MinimumYear`/`MaximumYear`, `Score` with `MinimumScore`/`MaximumScore`, and `CharacterRole` without `CharacterIds`. Each comes back as a `TenraiRequestException`.

| Property | Type | Description |
|----------|------|-------------|
| Query, Letter | string, char? | Text search, or titles starting with a letter |
| Types, ExcludedTypes | ICollection&lt;AnimeType&gt; / &lt;MangaType&gt; | e.g. `{ AnimeType.TV, AnimeType.TvSpecial }`. `MangaType.Novel` is a novel and `MangaType.LightNovel` a light novel |
| Statuses, ExcludedStatuses | ICollection&lt;AiringStatus&gt; / &lt;PublishingStatus&gt; | Airing or publishing status |
| Genres, ExcludedGenres | ICollection&lt;AnimeGenreSearch&gt; / &lt;MangaGenreSearch&gt; | Genre, theme and demographic ids |
| CharacterIds, CharacterRole | ICollection&lt;long&gt;, CharacterRole? | Entries with these characters, optionally only as `Main` or `Supporting` |
| Ids, ExcludedIds | ICollection&lt;long&gt; | Search only within, or leave out, these MAL ids (e.g. a user's list) |
| MinimumId, MaximumId | long? | MAL id range |
| Score | double? | Exact score |
| MinimumScoredBy ... MaximumFavorites | int? | Ranges for scored-by, reviews, members and favorites |
| Years, MinimumYear, MaximumYear | ICollection&lt;int&gt;, int? | Calendar start year |
| StartDate, EndDate | DateTime? | Start and end date range |
| Unapproved | InclusionFilter? | `Include` adds unapproved entries, `Only` returns only them |
| OrderBy, SortDirection, Nulls | enum, SortDirection, NullsOrder? | Sort, and where entries without a value go (`First`, `Last`, `Exclude`) |

Anime only:

| Property | Type | Description |
|----------|------|-------------|
| Ratings, ExcludedRatings | ICollection&lt;AnimeAgeRating&gt; | Age ratings. Excluding keeps entries without a rating |
| Sources, ExcludedSources | ICollection&lt;AnimeSource&gt; | Source material, e.g. `LightNovel`, `Original` |
| HasTrailer | bool? | Whether a main trailer is listed |
| StreamingServices | ICollection&lt;string&gt; | e.g. `"Crunchyroll"`, `"Netflix"` |
| HasRelations | InclusionFilter? | `Exclude`: only entries without relations; `Only`: only entries with them |
| ProducerIds, ExcludedProducerIds | ICollection&lt;long&gt; | Producer, studio or licensor ids |
| StaffIds, VoiceActorIds | ICollection&lt;long&gt; | Credited people |
| VoiceLanguages | ICollection&lt;VoiceLanguage&gt; | e.g. `English` for anime with an English dub |
| MinimumEpisodes, MaximumEpisodes | int? | Episode count range |
| Seasons, SeasonYears | ICollection&lt;Season&gt;, ICollection&lt;int&gt; | MAL debut season, which can differ from the calendar year |
| BroadcastDays | ICollection&lt;BroadcastDay&gt; | Day it airs in Japan, including `Today` and `Tomorrow` |
| AddedAfter, AddedBefore | DateTime? | When it was added to MyAnimeList |

Manga only:

| Property | Type | Description |
|----------|------|-------------|
| MagazineIds, ExcludedMagazineIds | ICollection&lt;long&gt; | Magazine or serialization platform ids |
| AuthorIds, AuthorRole, ExcludedAuthorIds | ICollection&lt;long&gt;, AuthorRole? | Authors, optionally only as `Story` or `Art` |
| SoloAuthor | bool? | `true` for manga written and drawn by one person |
| MinimumChapters ... MaximumVolumes | int? | Chapter and volume count ranges |

```csharp
var dubbed = await tenrai.SearchAnimeAsync(new AnimeSearchConfig
{
    Types = { AnimeType.TV },
    VoiceLanguages = { VoiceLanguage.English },
    StreamingServices = { "Crunchyroll" },
    ExcludedIds = myListIds
});
```

### SearchPersonAsync

Returns person search results. Overload: `(PersonSearchConfig searchConfig)`.

| Parameter | Type | Description |
|-----------|------|-------------|
| query | string | Search query |
| searchConfig | PersonSearchConfig | (optional) Advanced filters |

**Returns:** `PaginatedTenraiResponse<ICollection<Person>>`

**Example:**

```csharp
var results = await tenrai.SearchPersonAsync("miyazaki");

// The most favorited people
var popular = await tenrai.SearchPersonAsync(new PersonSearchConfig { MinimumFavorites = 50000 });
```

`PersonSearchConfig` takes `Query`, `Letter`, `MinimumFavorites`/`MaximumFavorites`, `OrderBy` (`MalId`, `Name`, `Birthday`, `Favorites`), `SortDirection`, `Page` and `Limit`.

### SearchCharacterAsync

Returns character search results. Overload: `(CharacterSearchConfig searchConfig)`.

| Parameter | Type | Description |
|-----------|------|-------------|
| query | string | Search query |
| searchConfig | CharacterSearchConfig | (optional) Advanced filters |

**Returns:** `PaginatedTenraiResponse<ICollection<Character>>`

**Example:**

```csharp
var results = await tenrai.SearchCharacterAsync("lain");

// Cowboy Bebop's main cast
var bebop = await tenrai.SearchCharacterAsync(new CharacterSearchConfig { AnimeIds = { 1 }, Role = CharacterRole.Main });
```

#### CharacterSearchConfig

| Property | Type | Description |
|----------|------|-------------|
| Query, Letter, GivenName, FamilyName | string, char? | Name search |
| HasAbout, HasImage, HasNicknames, HasKanji, HasFamilyName, HasVoiceActor | bool? | `true` or `false` filters on whether the field is set |
| AnimeIds, MangaIds | ICollection&lt;long&gt; | Characters in any of these titles |
| Role | CharacterRole? | `Main` or `Supporting`, in `AnimeIds`/`MangaIds` when set, otherwise in any title |
| RoleOnly | CharacterRole? | Characters only ever listed in this role |
| AppearsIn | AppearsIn? | `Anime`, `Manga` or `Both` |
| Airing | bool? | `true` for characters in an anime airing now, `false` for characters in none |
| VoiceActorIds | ICollection&lt;long&gt; | Voice actor person ids |
| VoiceLanguages, ExcludedVoiceLanguages | ICollection&lt;VoiceLanguage&gt; | Voiced (or not) in these languages, e.g. English but not Japanese |
| MinimumFavorites ... MaximumVoiceActors | int? | Ranges for favorites, appearances (all, anime, manga), main and supporting roles, voice languages, voice actors |
| OrderBy, SortDirection | CharacterSearchOrderBy, SortDirection | Also by kanji name, appearances, roles, voice languages and voice actors |
| Page, Limit | int? | Paging, 100 max |
| Sfw, SfwStrict | bool? | Filter out adult and ecchi entries. `null` (default) uses the [client setting](GettingStarted.md#safe-for-work-filtering) |

---

## Interest Stacks

An interest stack is a user curated, ordered list of anime or manga. Every stack holds one kind of entry, given by its `StackType` (`"anime"` or `"manga"`).

### GetInterestStacksAsync

Returns a page of public interest stacks.

| Parameter | Type | Description |
|-----------|------|-------------|
| page | int | Page index (optional overload) |

**Returns:** `PaginatedTenraiResponse<ICollection<InterestStack>>`

**Example:**

```csharp
var stacks = await tenrai.GetInterestStacksAsync();
foreach (var stack in stacks.Data)
    Console.WriteLine($"{stack.Title} ({stack.StackType}, {stack.EntryCount} entries)");
```

**Model:** [InterestStack](Models.md#intereststack)

### GetInterestStackAsync

Returns a single interest stack including its entries, in the order the author arranged them. With SFW on (the [client setting](GettingStarted.md#safe-for-work-filtering), or the `SfwConfig` overload) adult entries are removed from the stack.

| Parameter | Type | Description |
|-----------|------|-------------|
| id | long | MAL id of the interest stack |
| config | SfwConfig | (overload) Safe-for-work filtering for this call |

**Returns:** `BaseTenraiResponse<InterestStackDetails>`

**Example:**

```csharp
var stack = await tenrai.GetInterestStackAsync(89452);
foreach (var entry in stack.Data.Entries)
    Console.WriteLine($"{entry.Position}. {entry.Title}");
```

**Model:** [InterestStackDetails](Models.md#intereststackdetails)

### GetAnimeInterestStacksAsync

Returns a page of public interest stacks containing the given anime. A third overload takes an `InterestStackListConfig`: the same properties as [`InterestStackSearchConfig`](#searchintereststacksasync) except `Type`.

| Parameter | Type | Description |
|-----------|------|-------------|
| id | long | MAL id of anime |
| page | int | Page index (optional overload) |
| config | InterestStackListConfig | (overload) Search text, spoiler/challenge/official filters, sorting, paging, SFW |

**Returns:** `PaginatedTenraiResponse<ICollection<InterestStack>>`

**Example:**

```csharp
var stacks = await tenrai.GetAnimeInterestStacksAsync(1);
Console.WriteLine($"Cowboy Bebop appears in {stacks.Pagination.Items.Total} stacks");
```

**Model:** [InterestStack](Models.md#intereststack)

### GetMangaInterestStacksAsync

Returns a page of public interest stacks containing the given manga. A third overload takes an `InterestStackListConfig`: the same properties as [`InterestStackSearchConfig`](#searchintereststacksasync) except `Type`.

| Parameter | Type | Description |
|-----------|------|-------------|
| id | long | MAL id of manga |
| page | int | Page index (optional overload) |
| config | InterestStackListConfig | (overload) Search text, spoiler/challenge/official filters, sorting, paging, SFW |

**Returns:** `PaginatedTenraiResponse<ICollection<InterestStack>>`

**Example:**

```csharp
var stacks = await tenrai.GetMangaInterestStacksAsync(2);
```

**Model:** [InterestStack](Models.md#intereststack)

### SearchInterestStacksAsync

Returns public interest stacks matching an `InterestStackSearchConfig`.

| Property | Type | Description |
|----------|------|-------------|
| Page | int? | Page index |
| Limit | int? | Entries per page (100 max, 25 when unset) |
| Query | string | Search query, matched against title and description |
| Type | InterestStackType? | `Anime` or `Manga`. `null` (default) for both |
| Spoilers | InclusionFilter? | `Include`, `Exclude`, or `Only` spoiler stacks |
| Challenges | InclusionFilter? | `Include`, `Exclude`, or `Only` challenge stacks |
| Official | InclusionFilter? | `Include`, `Exclude`, or `Only` official stacks |
| OrderBy | InterestStackSearchOrderBy | `CreatedAt` or `RestackCount`. `NoSorting` (default) leaves the order to the API |
| SortDirection | SortDirection | `Ascending` or `Descending`. Only applied when `OrderBy` is set |
| Sfw | bool? | Filter out adult entries. `null` (default) uses the [client setting](GettingStarted.md#safe-for-work-filtering) |
| SfwStrict | bool? | Also filter out ecchi entries. `null` (default) uses the [client setting](GettingStarted.md#safe-for-work-filtering) |

The API leaves out spoiler, challenge and official stacks unless asked: `Include` adds that kind, `Only` returns just that kind. Cowboy Bebop is in 687 stacks by default, 777 with `Spoilers = InclusionFilter.Include`, and 90 with `Spoilers = InclusionFilter.Only`.

`OrderBy` accepts only `CreatedAt` and `RestackCount` because those are the only two values the API sorts by. Other fields, including `EntryCount`, return 400.

**Returns:** `PaginatedTenraiResponse<ICollection<InterestStack>>`

**Example:**

```csharp
var config = new InterestStackSearchConfig
{
    Query = "witch",
    Type = InterestStackType.Manga,
    OrderBy = InterestStackSearchOrderBy.RestackCount,
    SortDirection = SortDirection.Descending
};
var stacks = await tenrai.SearchInterestStacksAsync(config);
```

**Model:** [InterestStack](Models.md#intereststack)

---

## News and Articles

MyAnimeList news and editorial articles. Per-entity lists: `GetAnimeNewsAsync`, `GetMangaNewsAsync` (news), and `GetAnimeArticlesAsync`, `GetMangaArticlesAsync`, `GetCharacterArticlesAsync`, `GetPersonArticlesAsync` (articles), each with an optional filter config.

| Method | Returns |
|--------|---------|
| `GetNewsAsync()` | First page of all news, newest first: `PaginatedTenraiResponse<ICollection<News>>` |
| `SearchNewsAsync(NewsSearchConfig)` | Matching news |
| `GetNewsItemAsync(long id)` | `BaseTenraiResponse<NewsDetails>`: the item plus `Content` (HTML and Markdown) and `Relations` (anime, manga) |
| `GetNewsTagsAsync()` | `BaseTenraiResponse<ICollection<ContentTag>>`: every tag with its count |
| `GetArticlesAsync()` | First page of all articles: `PaginatedTenraiResponse<ICollection<Article>>` |
| `SearchArticlesAsync(ArticleSearchConfig)` | Matching articles |
| `GetArticleAsync(long id)` | `BaseTenraiResponse<ArticleDetails>`: adds `Content` and `Relations` (anime, manga, people, characters) |
| `GetArticleTagsAsync()` | Every article tag with its count |

The single-item methods send no sfw flag: with one, the API answers 404 for an item that mentions adult entries.

**Example:**

```csharp
var previews = await tenrai.SearchNewsAsync(new NewsSearchConfig
{
    Tag = "Preview",
    PublishedAfter = new DateTime(2026, 1, 1),
    OrderBy = NewsOrderBy.Comments,
    SortDirection = SortDirection.Descending
});

var item = await tenrai.GetNewsItemAsync(56264086);
Console.WriteLine(item.Data.Content.Markdown);
```

### NewsFilterConfig

Used by `GetAnimeNewsAsync`/`GetMangaNewsAsync`. `NewsSearchConfig` extends it with `Query` and `Tag` for `SearchNewsAsync`.

| Property | Type | Description |
|----------|------|-------------|
| Page | int? | Page index |
| Limit | int? | Entries per page, 100 max |
| AuthorUsername | string | Only items by this MyAnimeList user |
| PublishedAfter, PublishedBefore | DateTime? | Publish date range, sent as `yyyy-MM-dd` |
| OrderBy | NewsOrderBy | `PublishedAt` or `Comments`. `NoSorting` (default) leaves newest first |
| SortDirection | SortDirection | Applied when `OrderBy` is set |
| Sfw | bool? | Filter out adult entries. `null` (default) uses the [client setting](GettingStarted.md#safe-for-work-filtering) |
| SfwStrict | bool? | Also filter out ecchi entries. `null` (default) uses the [client setting](GettingStarted.md#safe-for-work-filtering) |

### ArticleFilterConfig

Same properties as `NewsFilterConfig`, with `OrderBy` of type `ArticleOrderBy` (`PublishedAt` or `Views`). `ArticleSearchConfig` extends it with `Query` and `Tag`.

**Models:** [News](Models.md#news), [NewsDetails](Models.md#newsdetails), [Article](Models.md#article), [ArticleDetails](Models.md#articledetails), [ContentTag](Models.md#contenttag)

---

## Status

### GetStatusAsync

Returns the current public status snapshot of the Tenrai services.

This call targets the status host (`https://tenrai.org/status/api/status`) rather than the API host. Two consequences: it ignores a custom `HttpClient` base address, and it is **not** subject to the client's rate limiter, so a status check is not throttled while the API is saturated. Poll no more than once per minute; the server caches the snapshot for 30 seconds. If a Server Key is configured it is sent on this request too, because the `HttpClient` is shared.

Unlike every other endpoint, the response is **not** wrapped in a `Data` property.

**Returns:** `TenraiStatus`

**Example:**

```csharp
var status = await tenrai.GetStatusAsync();
foreach (var service in status.Services)
    Console.WriteLine($"{service.Name}: {service.Status}");
```

**Model:** [TenraiStatus](Models.md#tenraistatus)

---

## Bulk IDs

> These endpoints require a Tenrai Server Key (set `TenraiClientConfiguration.ServerKey`); without one the API returns 401.

### GetAnimeIdsAsync / GetMangaIdsAsync / GetCharacterIdsAsync / GetPersonIdsAsync / GetProducerIdsAsync

Returns the complete collection of MAL IDs for the given resource. Anime and manga ids follow the [client SFW setting](GettingStarted.md#safe-for-work-filtering) and have an overload taking a filter config:

| Property | Type | Description |
|----------|------|-------------|
| Types | ICollection&lt;AnimeType&gt; / ICollection&lt;MangaType&gt; | Only entries of any of these types |
| Ratings | ICollection&lt;AnimeAgeRating&gt; | (anime only) Only entries with any of these ratings |
| Status | AiringStatus? / PublishingStatus? | Only entries with this status |
| UpdatedAfter | DateTime? | Only entries updated on or after this date (sent as `yyyy-MM-dd`) |
| Sfw | bool? | Leave out adult ids. `null` (default) uses the client setting |
| SfwStrict | bool? | Also leave out ecchi ids. `null` (default) uses the client setting |

`GetAnimeIdEntriesAsync` and `GetMangaIdEntriesAsync` take the same configs and return each id with its title and type (`IdEntriesTenraiResponse`, `Data` items are `IdEntry` with `MalId`, `Title`, `Type`).

**Returns:** `IdsTenraiResponse` (`Data` is the ids, `Total` their count)

**Example:**

```csharp
var tenrai = new TenraiClient(new TenraiClientConfiguration { ServerKey = "your-server-key" });
var animeIds = await tenrai.GetAnimeIdsAsync();
Console.WriteLine(animeIds.Data.Count);

// Movies changed since November 1, with their titles
var movies = await tenrai.GetAnimeIdEntriesAsync(new AnimeIdsConfig
{
    Types = { AnimeType.Movie },
    UpdatedAfter = new DateTime(2026, 11, 1)
});
foreach (var entry in movies.Data)
    Console.WriteLine($"{entry.MalId}: {entry.Title}");
```

---

## Reviews Configuration

Each review endpoint takes different filters, so each has its own config.

### ReviewsSearchConfig

Passed to `GetAnimeReviewsAsync` and `GetMangaReviewsAsync`. These reviews take no `Sfw`.

| Property | Type | Description |
|----------|------|-------------|
| Page | int? | Page index |
| Username | string | Only reviews by this MyAnimeList user (full username) |
| Sort | ReviewSortOrder? | `MostHelpful` (API default), `Newest`, `Oldest` |
| Preliminary | InclusionFilter? | `Include`, `Exclude`, or `Only` preliminary reviews. Left out unless set |
| Spoilers | InclusionFilter? | `Include`, `Exclude`, or `Only` spoiler reviews. Left out unless set |
| Sentiment | ReviewSentiment? | `Recommended`, `MixedFeelings`, `NotRecommended` |

### RecentReviewsConfig

Passed to `GetRecentAnimeReviewsAsync` and `GetRecentMangaReviewsAsync`. The same properties as `ReviewsSearchConfig` (`Sort` defaults to `Newest` here), plus:

| Property | Type | Description |
|----------|------|-------------|
| Limit | int? | Entries per page (100 max) |
| Sfw | bool? | Leave out reviews of adult entries. `null` (default) uses the [client setting](GettingStarted.md#safe-for-work-filtering) |
| SfwStrict | bool? | Also leave out reviews of ecchi entries. `null` (default) uses the client setting |

### TopReviewsConfig

Passed to `GetTopReviewsAsync`.

| Property | Type | Description |
|----------|------|-------------|
| Page | int? | Page index |
| Limit | int? | Entries per page (100 max) |
| Type | ReviewTargetType? | `Anime` or `Manga` |
| Username | string | Only reviews by this MyAnimeList user (full username) |
| Preliminary | bool? | Include preliminary reviews. The API includes them when unset |
| Spoilers | bool? | Include reviews with spoilers. The API includes them when unset |
| Sfw | bool? | Leave out reviews of adult entries. `null` (default) uses the client setting |
| SfwStrict | bool? | Also leave out reviews of ecchi entries. `null` (default) uses the client setting |

`Preliminary` and `Spoilers` are `bool?` here because `/top/reviews` answers 400 to `only`.

**Enums:** `ReviewSortOrder`, `InclusionFilter`, `ReviewSentiment`, `ReviewTargetType`.

**Example:**

```csharp
var config = new ReviewsSearchConfig
{
    Page = 1,
    Sort = ReviewSortOrder.Newest,
    Preliminary = InclusionFilter.Exclude,
    Spoilers = InclusionFilter.Only,
    Sentiment = ReviewSentiment.NotRecommended
};
var reviews = await tenrai.GetMangaReviewsAsync(2, config);
```

