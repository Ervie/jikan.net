# API Reference (2.x)

All methods are asynchronous and return `Task<T>`. Response models wrap data in a `Data` property. Paginated endpoints return `PaginatedJikanResponse<T>` with pagination metadata. See [Models](Models.md) for response model details.

## Anime

### GetAnimeAsync

Returns anime with given MAL id.

| Parameter | Type | Description |
|-----------|------|-------------|
| id | long | MAL id of anime |

**Returns:** `BaseJikanResponse<Anime>`

**Example:**

```csharp
var anime = await jikan.GetAnimeAsync(1);
Console.WriteLine(anime.Data.Title);
Console.WriteLine(anime.Data.Type);
```

**Model:** [Anime](Models.md#anime)

### GetAnimeCharactersAsync

Returns collection of characters of anime.

| Parameter | Type | Description |
|-----------|------|-------------|
| id | long | MAL id of anime |

**Returns:** `BaseJikanResponse<ICollection<AnimeCharacter>>`

**Example:**

```csharp
var characters = await jikan.GetAnimeCharactersAsync(1);
foreach (var c in characters.Data)
    Console.WriteLine($"{c.Character.Name} - {c.Role}");
```

**Model:** [AnimeCharacter](Models.md#animecharacter)

### GetAnimeStaffAsync

Returns collection of staff of anime.

| Parameter | Type | Description |
|-----------|------|-------------|
| id | long | MAL id of anime |

**Returns:** `BaseJikanResponse<ICollection<AnimeStaffPosition>>`

**Example:**

```csharp
var staff = await jikan.GetAnimeStaffAsync(1);
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

**Returns:** `PaginatedJikanResponse<ICollection<AnimeEpisode>>`

**Example:**

```csharp
var episodes = await jikan.GetAnimeEpisodesAsync(1);
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

**Returns:** `BaseJikanResponse<AnimeEpisode>`

**Example:**

```csharp
var episode = await jikan.GetAnimeEpisodeAsync(1, 1);
Console.WriteLine(episode.Data.Synopsis);
```

**Model:** [AnimeEpisode](Models.md#animeepisode)

### GetAnimeNewsAsync

Returns news related to anime. Overload: `(long id, int page)`.

| Parameter | Type | Description |
|-----------|------|-------------|
| id | long | MAL id of anime |
| page | int | (optional) Page index |

**Returns:** `PaginatedJikanResponse<ICollection<News>>`

**Example:**

```csharp
var news = await jikan.GetAnimeNewsAsync(1);
foreach (var n in news.Data)
    Console.WriteLine(n.Title);
```

### GetAnimeForumTopicsAsync

Returns forum topics. Overload: `(long id, ForumTopicType type)`.

| Parameter | Type | Description |
|-----------|------|-------------|
| id | long | MAL id of anime |
| type | ForumTopicType | (optional) Filter by topic type |

**Returns:** `BaseJikanResponse<ICollection<ForumTopic>>`

**Example:**

```csharp
var topics = await jikan.GetAnimeForumTopicsAsync(1);
foreach (var t in topics.Data)
    Console.WriteLine($"{t.Title} ({t.Comments} comments)");
```

**Model:** [ForumTopic](Models.md#forumtopic)

### GetAnimeVideosAsync

Returns videos (PV, episodes) related to anime.

| Parameter | Type | Description |
|-----------|------|-------------|
| id | long | MAL id of anime |

**Returns:** `BaseJikanResponse<AnimeVideos>`

**Example:**

```csharp
var videos = await jikan.GetAnimeVideosAsync(1);
// videos.Data contains Promo, Episodes, MusicVideos
```

### GetAnimeVideosEpisodesAsync

Returns video episodes. Overload: `(long id, int page)`.

| Parameter | Type | Description |
|-----------|------|-------------|
| id | long | MAL id of anime |
| page | int | (optional) Page index |

**Returns:** `PaginatedJikanResponse<ICollection<EpisodeVideo>>`

**Example:**

```csharp
var episodes = await jikan.GetAnimeVideosEpisodesAsync(1);
foreach (var ep in episodes.Data)
    Console.WriteLine(ep.Title);
```

### GetAnimePicturesAsync

Returns links to pictures.

| Parameter | Type | Description |
|-----------|------|-------------|
| id | long | MAL id of anime |

**Returns:** `BaseJikanResponse<ICollection<ImagesSet>>`

**Example:**

```csharp
var pictures = await jikan.GetAnimePicturesAsync(1);
foreach (var img in pictures.Data)
    Console.WriteLine(img.Jpg?.ImageUrl);
```

### GetAnimeStatisticsAsync

Returns statistics.

| Parameter | Type | Description |
|-----------|------|-------------|
| id | long | MAL id of anime |

**Returns:** `BaseJikanResponse<AnimeStatistics>`

**Example:**

```csharp
var stats = await jikan.GetAnimeStatisticsAsync(1);
Console.WriteLine($"Score: {stats.Data.Score?.Mean}");
```

### GetAnimeMoreInfoAsync

Returns additional information.

| Parameter | Type | Description |
|-----------|------|-------------|
| id | long | MAL id of anime |

**Returns:** `BaseJikanResponse<MoreInfo>`

**Example:**

```csharp
var info = await jikan.GetAnimeMoreInfoAsync(1);
Console.WriteLine(info.Data.MoreInfo);
```

### GetAnimeRecommendationsAsync

Returns anime recommendations.

| Parameter | Type | Description |
|-----------|------|-------------|
| id | long | MAL id of anime |

**Returns:** `BaseJikanResponse<ICollection<Recommendation>>`

**Example:**

```csharp
var recs = await jikan.GetAnimeRecommendationsAsync(1);
foreach (var r in recs.Data)
    Console.WriteLine(r.Content);
```

**Model:** [Recommendation](Models.md#recommendation)

### GetAnimeUserUpdatesAsync

Returns user updates. Overload: `(long id, int page)`.

| Parameter | Type | Description |
|-----------|------|-------------|
| id | long | MAL id of anime |
| page | int | (optional) Page index |

**Returns:** `PaginatedJikanResponse<ICollection<AnimeUserUpdate>>`

**Example:**

```csharp
var updates = await jikan.GetAnimeUserUpdatesAsync(1);
foreach (var u in updates.Data)
    Console.WriteLine($"{u.User?.Username}: {u.Score}");
```

### GetAnimeReviewsAsync

Returns reviews. Overloads with `page`, `includePreliminary`, `includeSpoiler`.

| Parameter | Type | Description |
|-----------|------|-------------|
| id | long | MAL id of anime |
| page | int | (optional) Page index |
| includePreliminary | bool | (optional, default true) Include preliminary reviews |
| includeSpoiler | bool | (optional, default false) Include spoiler reviews |

**Returns:** `PaginatedJikanResponse<ICollection<Review>>`

**Example:**

```csharp
var reviews = await jikan.GetAnimeReviewsAsync(1);
foreach (var r in reviews.Data)
    Console.WriteLine($"{r.User?.Username}: {r.Score}");
```

**Model:** [Review](Models.md#review)

### GetAnimeRelationsAsync

Returns related entries.

| Parameter | Type | Description |
|-----------|------|-------------|
| id | long | MAL id of anime |

**Returns:** `PaginatedJikanResponse<ICollection<RelatedEntry>>`

**Example:**

```csharp
var relations = await jikan.GetAnimeRelationsAsync(1);
foreach (var r in relations.Data)
    Console.WriteLine($"{r.Relation}: {r.Entry?.FirstOrDefault()?.Name}");
```

**Model:** [RelatedEntry](Models.md#relatedentry)

### GetAnimeThemesAsync

Returns openings and endings.

| Parameter | Type | Description |
|-----------|------|-------------|
| id | long | MAL id of anime |

**Returns:** `BaseJikanResponse<AnimeThemes>`

**Example:**

```csharp
var themes = await jikan.GetAnimeThemesAsync(1);
// themes.Data contains Openings, Endings
```

### GetAnimeExternalLinksAsync

Returns external services links.

| Parameter | Type | Description |
|-----------|------|-------------|
| id | long | MAL id of anime |

**Returns:** `BaseJikanResponse<ICollection<ExternalLink>>`

**Example:**

```csharp
var links = await jikan.GetAnimeExternalLinksAsync(1);
foreach (var l in links.Data)
    Console.WriteLine($"{l.Name}: {l.Url}");
```

**Model:** [ExternalLink](Models.md#externallink)

### GetAnimeStreamingLinksAsync

Returns streaming services links.

| Parameter | Type | Description |
|-----------|------|-------------|
| id | long | MAL id of anime |

**Returns:** `BaseJikanResponse<ICollection<ExternalLink>>`

**Example:**

```csharp
var links = await jikan.GetAnimeStreamingLinksAsync(1);
foreach (var l in links.Data)
    Console.WriteLine($"{l.Name}: {l.Url}");
```

### GetAnimeFullDataAsync

Returns anime with all additional data.

| Parameter | Type | Description |
|-----------|------|-------------|
| id | long | MAL id of anime |

**Returns:** `BaseJikanResponse<AnimeFull>`

**Example:**

```csharp
var full = await jikan.GetAnimeFullDataAsync(1);
// full.Data contains anime plus relations, news, etc.
```

---

## Manga

### GetMangaAsync

Returns manga with given MAL id.

| Parameter | Type | Description |
|-----------|------|-------------|
| id | long | MAL id of manga |

**Returns:** `BaseJikanResponse<Manga>`

**Example:**

```csharp
var manga = await jikan.GetMangaAsync(2);
Console.WriteLine(manga.Data.Title);
Console.WriteLine(manga.Data.Status);
```

**Model:** [Manga](Models.md#manga)

### GetMangaCharactersAsync

Returns characters appearing in manga.

| Parameter | Type | Description |
|-----------|------|-------------|
| id | long | MAL id of manga |

**Returns:** `BaseJikanResponse<ICollection<MangaCharacter>>`

**Example:**

```csharp
var characters = await jikan.GetMangaCharactersAsync(2);
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

**Returns:** `PaginatedJikanResponse<ICollection<News>>`

**Example:**

```csharp
var news = await jikan.GetMangaNewsAsync(2);
foreach (var n in news.Data)
    Console.WriteLine(n.Title);
```

### GetMangaForumTopicsAsync

Returns forum topics. Overload: `(long id, ForumTopicType type)`.

| Parameter | Type | Description |
|-----------|------|-------------|
| id | long | MAL id of manga |
| type | ForumTopicType | (optional) Filter by topic type |

**Returns:** `BaseJikanResponse<ICollection<ForumTopic>>`

**Example:**

```csharp
var topics = await jikan.GetMangaForumTopicsAsync(2);
foreach (var t in topics.Data)
    Console.WriteLine(t.Title);
```

### GetMangaPicturesAsync

Returns links to pictures.

| Parameter | Type | Description |
|-----------|------|-------------|
| id | long | MAL id of manga |

**Returns:** `BaseJikanResponse<ICollection<ImagesSet>>`

**Example:**

```csharp
var pictures = await jikan.GetMangaPicturesAsync(2);
```

### GetMangaStatisticsAsync

Returns statistics.

| Parameter | Type | Description |
|-----------|------|-------------|
| id | long | MAL id of manga |

**Returns:** `BaseJikanResponse<MangaStatistics>`

**Example:**

```csharp
var stats = await jikan.GetMangaStatisticsAsync(2);
Console.WriteLine($"Score: {stats.Data.Score?.Mean}");
```

### GetMangaMoreInfoAsync

Returns additional information.

| Parameter | Type | Description |
|-----------|------|-------------|
| id | long | MAL id of manga |

**Returns:** `BaseJikanResponse<MoreInfo>`

**Example:**

```csharp
var info = await jikan.GetMangaMoreInfoAsync(2);
```

### GetMangaUserUpdatesAsync

Returns user updates. Overload: `(long id, int page)`.

| Parameter | Type | Description |
|-----------|------|-------------|
| id | long | MAL id of manga |
| page | int | (optional) Page index |

**Returns:** `PaginatedJikanResponse<ICollection<MangaUserUpdate>>`

**Example:**

```csharp
var updates = await jikan.GetMangaUserUpdatesAsync(2);
```

### GetMangaRecommendationsAsync

Returns manga recommendations.

| Parameter | Type | Description |
|-----------|------|-------------|
| id | long | MAL id of manga |

**Returns:** `BaseJikanResponse<ICollection<Recommendation>>`

**Example:**

```csharp
var recs = await jikan.GetMangaRecommendationsAsync(2);
```

### GetMangaReviewsAsync

Returns reviews. Overloads with `page`, `includePreliminary`, `includeSpoiler`.

| Parameter | Type | Description |
|-----------|------|-------------|
| id | long | MAL id of manga |
| page | int | (optional) Page index |
| includePreliminary | bool | (optional, default true) |
| includeSpoiler | bool | (optional, default false) |

**Returns:** `PaginatedJikanResponse<ICollection<Review>>`

**Example:**

```csharp
var reviews = await jikan.GetMangaReviewsAsync(2);
```

### GetMangaRelationsAsync

Returns related entries.

| Parameter | Type | Description |
|-----------|------|-------------|
| id | long | MAL id of manga |

**Returns:** `PaginatedJikanResponse<ICollection<RelatedEntry>>`

**Example:**

```csharp
var relations = await jikan.GetMangaRelationsAsync(2);
```

### GetMangaExternalLinksAsync

Returns external services links.

| Parameter | Type | Description |
|-----------|------|-------------|
| id | long | MAL id of manga |

**Returns:** `BaseJikanResponse<ICollection<ExternalLink>>`

**Example:**

```csharp
var links = await jikan.GetMangaExternalLinksAsync(2);
```

### GetMangaFullDataAsync

Returns manga with all additional data.

| Parameter | Type | Description |
|-----------|------|-------------|
| id | long | MAL id of manga |

**Returns:** `BaseJikanResponse<MangaFull>`

**Example:**

```csharp
var full = await jikan.GetMangaFullDataAsync(2);
```

---

## Character

### GetCharacterAsync

Returns character with given MAL id.

| Parameter | Type | Description |
|-----------|------|-------------|
| id | long | MAL id of character |

**Returns:** `BaseJikanResponse<Character>`

**Example:**

```csharp
var character = await jikan.GetCharacterAsync(2219);
Console.WriteLine(character.Data.Name);
Console.WriteLine(character.Data.About);
```

**Model:** [Character](Models.md#character)

### GetCharacterAnimeAsync

Returns animeography (anime where character appeared).

| Parameter | Type | Description |
|-----------|------|-------------|
| id | long | MAL id of character |

**Returns:** `BaseJikanResponse<ICollection<CharacterAnimeographyEntry>>`

**Example:**

```csharp
var anime = await jikan.GetCharacterAnimeAsync(2219);
foreach (var entry in anime.Data)
    Console.WriteLine($"{entry.Anime?.Title} - {entry.Role}");
```

### GetCharacterMangaAsync

Returns mangaography (manga where character appeared).

| Parameter | Type | Description |
|-----------|------|-------------|
| id | long | MAL id of character |

**Returns:** `BaseJikanResponse<ICollection<CharacterMangaographyEntry>>`

**Example:**

```csharp
var manga = await jikan.GetCharacterMangaAsync(2219);
foreach (var entry in manga.Data)
    Console.WriteLine(entry.Manga?.Title);
```

### GetCharacterVoiceActorsAsync

Returns voice actors.

| Parameter | Type | Description |
|-----------|------|-------------|
| id | long | MAL id of character |

**Returns:** `BaseJikanResponse<ICollection<VoiceActorEntry>>`

**Example:**

```csharp
var va = await jikan.GetCharacterVoiceActorsAsync(2219);
foreach (var entry in va.Data)
    Console.WriteLine($"{entry.Person.Name} ({entry.Language})");
```

### GetCharacterPicturesAsync

Returns links to pictures.

| Parameter | Type | Description |
|-----------|------|-------------|
| id | long | MAL id of character |

**Returns:** `BaseJikanResponse<ICollection<ImagesSet>>`

**Example:**

```csharp
var pictures = await jikan.GetCharacterPicturesAsync(2219);
```

### GetCharacterFullDataAsync

Returns character with all additional data.

| Parameter | Type | Description |
|-----------|------|-------------|
| id | long | MAL id of character |

**Returns:** `BaseJikanResponse<CharacterFull>`

**Example:**

```csharp
var full = await jikan.GetCharacterFullDataAsync(2219);
```

---

## Person

### GetPersonAsync

Returns person with given MAL id.

| Parameter | Type | Description |
|-----------|------|-------------|
| id | long | MAL id of person |

**Returns:** `BaseJikanResponse<Person>`

**Example:**

```csharp
var person = await jikan.GetPersonAsync(1870);
Console.WriteLine(person.Data.FamilyName);
Console.WriteLine(person.Data.GivenName);
```

**Model:** [Person](Models.md#person)

### GetPersonAnimeAsync

Returns animeography (anime the person collaborated on).

| Parameter | Type | Description |
|-----------|------|-------------|
| id | long | MAL id of person |

**Returns:** `BaseJikanResponse<ICollection<PersonAnimeographyEntry>>`

**Example:**

```csharp
var anime = await jikan.GetPersonAnimeAsync(1870);
foreach (var entry in anime.Data)
    Console.WriteLine(entry.Anime?.Title);
```

### GetPersonMangaAsync

Returns mangaography (manga the person worked on).

| Parameter | Type | Description |
|-----------|------|-------------|
| id | long | MAL id of person |

**Returns:** `BaseJikanResponse<ICollection<PersonMangaographyEntry>>`

**Example:**

```csharp
var manga = await jikan.GetPersonMangaAsync(1870);
```

### GetPersonVoiceActingRolesAsync

Returns voice acting roles.

| Parameter | Type | Description |
|-----------|------|-------------|
| id | long | MAL id of person |

**Returns:** `BaseJikanResponse<ICollection<VoiceActingRole>>`

**Example:**

```csharp
var roles = await jikan.GetPersonVoiceActingRolesAsync(1870);
foreach (var r in roles.Data)
    Console.WriteLine($"{r.Character?.Name} in {r.Anime?.Title}");
```

### GetPersonPicturesAsync

Returns links to pictures.

| Parameter | Type | Description |
|-----------|------|-------------|
| id | long | MAL id of person |

**Returns:** `BaseJikanResponse<ICollection<ImagesSet>>`

**Example:**

```csharp
var pictures = await jikan.GetPersonPicturesAsync(1870);
```

### GetPersonFullDataAsync

Returns person with all additional data.

| Parameter | Type | Description |
|-----------|------|-------------|
| id | long | MAL id of person |

**Returns:** `BaseJikanResponse<PersonFull>`

**Example:**

```csharp
var full = await jikan.GetPersonFullDataAsync(1870);
```

---

## Season

### GetSeasonAsync

Returns season preview. Overload: `(int year, Season season, int page)`.

| Parameter | Type | Description |
|-----------|------|-------------|
| year | int | Year of season |
| season | Season | Season (Winter, Spring, Summer, Fall) |
| page | int | (optional) Page index |

**Returns:** `PaginatedJikanResponse<ICollection<Anime>>`

**Example:**

```csharp
var season = await jikan.GetSeasonAsync(2024, Season.Spring);
foreach (var anime in season.Data)
    Console.WriteLine(anime.Title);
```

### GetSeasonArchiveAsync

Returns list of available seasons.

**Returns:** `PaginatedJikanResponse<ICollection<SeasonArchive>>`

**Example:**

```csharp
var archive = await jikan.GetSeasonArchiveAsync();
foreach (var s in archive.Data)
    Console.WriteLine($"{s.Year} {s.Season}");
```

### GetCurrentSeasonAsync

Returns current airing season. Overload: `(int page)`.

| Parameter | Type | Description |
|-----------|------|-------------|
| page | int | (optional) Page index |

**Returns:** `PaginatedJikanResponse<ICollection<Anime>>`

**Example:**

```csharp
var current = await jikan.GetCurrentSeasonAsync();
```

### GetUpcomingSeasonAsync

Returns anime with undefined airing (marked "Later" on MAL). Overload: `(int page)`.

| Parameter | Type | Description |
|-----------|------|-------------|
| page | int | (optional) Page index |

**Returns:** `PaginatedJikanResponse<ICollection<Anime>>`

**Example:**

```csharp
var upcoming = await jikan.GetUpcomingSeasonAsync();
```

---

## Schedule

### GetScheduleAsync

Returns current season schedule. Overloads: `(int page)`, `(ScheduledDay scheduledDay)`.

| Parameter | Type | Description |
|-----------|------|-------------|
| page | int | (optional) Page index |
| scheduledDay | ScheduledDay | (optional) Filter by day |

**Returns:** `PaginatedJikanResponse<ICollection<Anime>>`

**Example:**

```csharp
var schedule = await jikan.GetScheduleAsync(ScheduledDay.Monday);
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

**Returns:** `PaginatedJikanResponse<ICollection<Anime>>`

**Example:**

```csharp
var top = await jikan.GetTopAnimeAsync();
foreach (var anime in top.Data)
    Console.WriteLine($"{anime.Rank}. {anime.Title}");
```

### GetTopMangaAsync

Returns top manga. Overloads: `(int page)`, `(MangaTopSearchConfig searchConfig)`.

| Parameter | Type | Description |
|-----------|------|-------------|
| page | int | (optional) Page index |
| searchConfig | MangaTopSearchConfig | (optional) Advanced filters |

**Returns:** `PaginatedJikanResponse<ICollection<Manga>>`

**Example:**

```csharp
var top = await jikan.GetTopMangaAsync();
```

### GetTopPeopleAsync

Returns most popular people. Overload: `(int page)`.

| Parameter | Type | Description |
|-----------|------|-------------|
| page | int | (optional) Page index |

**Returns:** `PaginatedJikanResponse<ICollection<Person>>`

**Example:**

```csharp
var top = await jikan.GetTopPeopleAsync();
```

### GetTopCharactersAsync

Returns most popular characters. Overload: `(int page)`.

| Parameter | Type | Description |
|-----------|------|-------------|
| page | int | (optional) Page index |

**Returns:** `PaginatedJikanResponse<ICollection<Character>>`

**Example:**

```csharp
var top = await jikan.GetTopCharactersAsync();
```

### GetTopReviewsAsync

Returns most popular reviews. Overload: `(int page)`.

| Parameter | Type | Description |
|-----------|------|-------------|
| page | int | (optional) Page index |

**Returns:** `PaginatedJikanResponse<ICollection<Review>>`

**Example:**

```csharp
var top = await jikan.GetTopReviewsAsync();
```

---

## Genre

### GetAnimeGenresAsync

Returns anime genres. Overload: `(GenresFilter filter)`.

| Parameter | Type | Description |
|-----------|------|-------------|
| filter | GenresFilter | (optional) Filter for genre types |

**Returns:** `BaseJikanResponse<ICollection<Genre>>`

**Example:**

```csharp
var genres = await jikan.GetAnimeGenresAsync();
foreach (var g in genres.Data)
    Console.WriteLine(g.Name);
```

### GetMangaGenresAsync

Returns manga genres. Overload: `(GenresFilter filter)`.

| Parameter | Type | Description |
|-----------|------|-------------|
| filter | GenresFilter | (optional) Filter for genre types |

**Returns:** `BaseJikanResponse<ICollection<Genre>>`

**Example:**

```csharp
var genres = await jikan.GetMangaGenresAsync();
```

---

## Producer

### GetProducersAsync

Returns producers list. Overload: `(int page)`.

| Parameter | Type | Description |
|-----------|------|-------------|
| page | int | (optional) Page index |

**Returns:** `PaginatedJikanResponse<ICollection<Producer>>`

**Example:**

```csharp
var producers = await jikan.GetProducersAsync();
```

### GetProducerAsync

Returns producer by id.

| Parameter | Type | Description |
|-----------|------|-------------|
| id | long | MAL id of producer |

**Returns:** `BaseJikanResponse<Producer>`

**Example:**

```csharp
var producer = await jikan.GetProducerAsync(1);
```

### GetProducerExternalLinksAsync

Returns external links related to producer.

| Parameter | Type | Description |
|-----------|------|-------------|
| id | long | MAL id of producer |

**Returns:** `BaseJikanResponse<ICollection<ExternalLink>>`

**Example:**

```csharp
var links = await jikan.GetProducerExternalLinksAsync(1);
```

### GetProducerFullDataAsync

Returns full producer data.

| Parameter | Type | Description |
|-----------|------|-------------|
| id | long | MAL id of producer |

**Returns:** `BaseJikanResponse<ProducerFull>`

**Example:**

```csharp
var full = await jikan.GetProducerFullDataAsync(1);
```

---

## Magazine

### GetMagazinesAsync

Returns magazines list. Overload: `(int page)`.

| Parameter | Type | Description |
|-----------|------|-------------|
| page | int | (optional) Page index |

**Returns:** `PaginatedJikanResponse<ICollection<Magazine>>`

**Example:**

```csharp
var magazines = await jikan.GetMagazinesAsync();
```

---

## Club

### GetClubAsync

Returns club profile.

| Parameter | Type | Description |
|-----------|------|-------------|
| id | long | MAL id of club |

**Returns:** `BaseJikanResponse<Club>`

**Example:**

```csharp
var club = await jikan.GetClubAsync(1);
Console.WriteLine(club.Data.Name);
```

### GetClubMembersAsync

Returns member list. Overload: `(long id, int page)`.

| Parameter | Type | Description |
|-----------|------|-------------|
| id | long | MAL id of club |
| page | int | (optional) Page index |

**Returns:** `PaginatedJikanResponse<ICollection<ClubMember>>`

**Example:**

```csharp
var members = await jikan.GetClubMembersAsync(1);
```

### GetClubStaffAsync

Returns staff list.

| Parameter | Type | Description |
|-----------|------|-------------|
| id | long | MAL id of club |

**Returns:** `BaseJikanResponse<ICollection<ClubStaff>>`

**Example:**

```csharp
var staff = await jikan.GetClubStaffAsync(1);
```

### GetClubRelationsAsync

Returns related entities.

| Parameter | Type | Description |
|-----------|------|-------------|
| id | long | MAL id of club |

**Returns:** `BaseJikanResponse<ClubRelations>`

**Example:**

```csharp
var relations = await jikan.GetClubRelationsAsync(1);
```

---

## User

### GetUserProfileAsync

Returns user profile.

| Parameter | Type | Description |
|-----------|------|-------------|
| username | string | Username |

**Returns:** `BaseJikanResponse<UserProfile>`

**Example:**

```csharp
var profile = await jikan.GetUserProfileAsync("username");
Console.WriteLine(profile.Data.Username);
Console.WriteLine(profile.Data.LastOnline);
```

**Model:** [UserProfile](Models.md#userprofile)

### GetUserByIdAsync

Returns user profile by numeric id.

| Parameter | Type | Description |
|-----------|------|-------------|
| id | long | Numeric MAL user id |

**Returns:** `BaseJikanResponse<UserProfile>`

**Example:**

```csharp
var profile = await jikan.GetUserByIdAsync(12345);
```

### GetUserStatisticsAsync

Returns anime and manga statistics.

| Parameter | Type | Description |
|-----------|------|-------------|
| username | string | Username |

**Returns:** `BaseJikanResponse<UserStatistics>`

**Example:**

```csharp
var stats = await jikan.GetUserStatisticsAsync("username");
```

### GetUserFavoritesAsync

Returns favorite section.

| Parameter | Type | Description |
|-----------|------|-------------|
| username | string | Username |

**Returns:** `BaseJikanResponse<UserFavorites>`

**Example:**

```csharp
var favorites = await jikan.GetUserFavoritesAsync("username");
```

### GetUserUpdatesAsync

Returns updates on anime/manga progress.

| Parameter | Type | Description |
|-----------|------|-------------|
| username | string | Username |

**Returns:** `BaseJikanResponse<UserUpdates>`

**Example:**

```csharp
var updates = await jikan.GetUserUpdatesAsync("username");
```

### GetUserAboutAsync

Returns profile description.

| Parameter | Type | Description |
|-----------|------|-------------|
| username | string | Username |

**Returns:** `BaseJikanResponse<UserAbout>`

**Example:**

```csharp
var about = await jikan.GetUserAboutAsync("username");
```

### GetUserHistoryAsync

Returns history. Overload: `(string username, UserHistoryExtension filter)`.

| Parameter | Type | Description |
|-----------|------|-------------|
| username | string | Username |
| filter | UserHistoryExtension | (optional) Filter by Anime/Manga |

**Returns:** `BaseJikanResponse<ICollection<HistoryEntry>>`

**Example:**

```csharp
var history = await jikan.GetUserHistoryAsync("username");
```

### GetUserFriendsAsync

Returns friends. Overload: `(string username, int page)`.

| Parameter | Type | Description |
|-----------|------|-------------|
| username | string | Username |
| page | int | (optional) Page index |

**Returns:** `PaginatedJikanResponse<ICollection<Friend>>`

**Example:**

```csharp
var friends = await jikan.GetUserFriendsAsync("username");
```

### GetUserReviewsAsync

Returns user's reviews. Overload: `(string username, int page)`.

| Parameter | Type | Description |
|-----------|------|-------------|
| username | string | Username |
| page | int | (optional) Page index |

**Returns:** `PaginatedJikanResponse<ICollection<Review>>`

**Example:**

```csharp
var reviews = await jikan.GetUserReviewsAsync("username");
```

### GetUserRecommendationsAsync

Returns user's recommendations. Overload: `(string username, int page)`.

| Parameter | Type | Description |
|-----------|------|-------------|
| username | string | Username |
| page | int | (optional) Page index |

**Returns:** `PaginatedJikanResponse<ICollection<UserRecommendation>>`

**Example:**

```csharp
var recs = await jikan.GetUserRecommendationsAsync("username");
```

### GetUserClubsAsync

Returns user's clubs. Overload: `(string username, int page)`.

| Parameter | Type | Description |
|-----------|------|-------------|
| username | string | Username |
| page | int | (optional) Page index |

**Returns:** `PaginatedJikanResponse<ICollection<MalUrl>>`

**Example:**

```csharp
var clubs = await jikan.GetUserClubsAsync("username");
```

### GetUserExternalLinksAsync

Returns external links.

| Parameter | Type | Description |
|-----------|------|-------------|
| username | string | Username |

**Returns:** `BaseJikanResponse<ICollection<ExternalLink>>`

**Example:**

```csharp
var links = await jikan.GetUserExternalLinksAsync("username");
```

### GetUserFullDataAsync

Returns user with all additional data.

| Parameter | Type | Description |
|-----------|------|-------------|
| username | string | Username |

**Returns:** `BaseJikanResponse<UserFull>`

**Example:**

```csharp
var full = await jikan.GetUserFullDataAsync("username");
```

---

## Random

### GetRandomAnimeAsync

Gets random anime.

**Returns:** `BaseJikanResponse<Anime>`

**Example:**

```csharp
var anime = await jikan.GetRandomAnimeAsync();
Console.WriteLine(anime.Data.Title);
```

### GetRandomMangaAsync

Gets random manga.

**Returns:** `BaseJikanResponse<Manga>`

**Example:**

```csharp
var manga = await jikan.GetRandomMangaAsync();
```

### GetRandomCharacterAsync

Gets random character.

**Returns:** `BaseJikanResponse<Character>`

**Example:**

```csharp
var character = await jikan.GetRandomCharacterAsync();
```

### GetRandomPersonAsync

Gets random person.

**Returns:** `BaseJikanResponse<Person>`

**Example:**

```csharp
var person = await jikan.GetRandomPersonAsync();
```

### GetRandomUserAsync

Gets random user.

**Returns:** `BaseJikanResponse<UserProfile>`

**Example:**

```csharp
var user = await jikan.GetRandomUserAsync();
```

---

## Recommendations

### GetRecentAnimeRecommendationsAsync

Returns recently added anime recommendations. Overload: `(int page)`.

| Parameter | Type | Description |
|-----------|------|-------------|
| page | int | (optional) Page index |

**Returns:** `PaginatedJikanResponse<ICollection<UserRecommendation>>`

**Example:**

```csharp
var recs = await jikan.GetRecentAnimeRecommendationsAsync();
```

### GetRecentMangaRecommendationsAsync

Returns recently added manga recommendations. Overload: `(int page)`.

| Parameter | Type | Description |
|-----------|------|-------------|
| page | int | (optional) Page index |

**Returns:** `PaginatedJikanResponse<ICollection<UserRecommendation>>`

**Example:**

```csharp
var recs = await jikan.GetRecentMangaRecommendationsAsync();
```

---

## Reviews

### GetRecentAnimeReviewsAsync

Returns recently added anime reviews. Overload: `(int page)`.

| Parameter | Type | Description |
|-----------|------|-------------|
| page | int | (optional) Page index |

**Returns:** `PaginatedJikanResponse<ICollection<Review>>`

**Example:**

```csharp
var reviews = await jikan.GetRecentAnimeReviewsAsync();
```

### GetRecentMangaReviewsAsync

Returns recently added manga reviews. Overload: `(int page)`.

| Parameter | Type | Description |
|-----------|------|-------------|
| page | int | (optional) Page index |

**Returns:** `PaginatedJikanResponse<ICollection<Review>>`

**Example:**

```csharp
var reviews = await jikan.GetRecentMangaReviewsAsync();
```

---

## Watch

### GetWatchRecentEpisodesAsync

Returns recently released episodes.

**Returns:** `PaginatedJikanResponse<ICollection<WatchEpisode>>`

**Example:**

```csharp
var episodes = await jikan.GetWatchRecentEpisodesAsync();
foreach (var ep in episodes.Data)
    Console.WriteLine(ep.Title);
```

### GetWatchPopularEpisodesAsync

Returns popular episodes.

**Returns:** `PaginatedJikanResponse<ICollection<WatchEpisode>>`

**Example:**

```csharp
var episodes = await jikan.GetWatchPopularEpisodesAsync();
```

### GetWatchRecentPromosAsync

Returns recently released promos. Overload: `(int page)`.

| Parameter | Type | Description |
|-----------|------|-------------|
| page | int | (optional) Page index |

**Returns:** `PaginatedJikanResponse<ICollection<WatchPromoVideo>>`

**Example:**

```csharp
var promos = await jikan.GetWatchRecentPromosAsync();
```

### GetWatchPopularPromosAsync

Returns popular promos.

**Returns:** `PaginatedJikanResponse<ICollection<WatchPromoVideo>>`

**Example:**

```csharp
var promos = await jikan.GetWatchPopularPromosAsync();
```

---

## Search

### SearchAnimeAsync

Returns anime search results. Overload: `(AnimeSearchConfig searchConfig)`.

| Parameter | Type | Description |
|-----------|------|-------------|
| query | string | Search query |
| searchConfig | AnimeSearchConfig | (optional) Advanced filters |

**Returns:** `PaginatedJikanResponse<ICollection<Anime>>`

**Example:**

```csharp
var results = await jikan.SearchAnimeAsync("cowboy bebop");
foreach (var anime in results.Data)
    Console.WriteLine(anime.Title);
```

### SearchMangaAsync

Returns manga search results. Overload: `(MangaSearchConfig searchConfig)`.

| Parameter | Type | Description |
|-----------|------|-------------|
| query | string | Search query |
| searchConfig | MangaSearchConfig | (optional) Advanced filters |

**Returns:** `PaginatedJikanResponse<ICollection<Manga>>`

**Example:**

```csharp
var results = await jikan.SearchMangaAsync("berserk");
```

### SearchPersonAsync

Returns person search results. Overload: `(PersonSearchConfig searchConfig)`.

| Parameter | Type | Description |
|-----------|------|-------------|
| query | string | Search query |
| searchConfig | PersonSearchConfig | (optional) Advanced filters |

**Returns:** `PaginatedJikanResponse<ICollection<Person>>`

**Example:**

```csharp
var results = await jikan.SearchPersonAsync("miyazaki");
```

### SearchCharacterAsync

Returns character search results. Overload: `(CharacterSearchConfig searchConfig)`.

| Parameter | Type | Description |
|-----------|------|-------------|
| query | string | Search query |
| searchConfig | CharacterSearchConfig | (optional) Advanced filters |

**Returns:** `PaginatedJikanResponse<ICollection<Character>>`

**Example:**

```csharp
var results = await jikan.SearchCharacterAsync("lain");
```

### SearchUserAsync

Returns user search results. Overload: `(UserSearchConfig searchConfig)`.

| Parameter | Type | Description |
|-----------|------|-------------|
| query | string | Search query |
| searchConfig | UserSearchConfig | (optional) Advanced filters |

**Returns:** `PaginatedJikanResponse<ICollection<UserMetadata>>`

**Example:**

```csharp
var results = await jikan.SearchUserAsync("username");
```

### SearchClubAsync

Returns club search results. Overload: `(ClubSearchConfig searchConfig)`.

| Parameter | Type | Description |
|-----------|------|-------------|
| query | string | Search query |
| searchConfig | ClubSearchConfig | (optional) Advanced filters |

**Returns:** `PaginatedJikanResponse<ICollection<Club>>`

**Example:**

```csharp
var results = await jikan.SearchClubAsync("anime");
```
