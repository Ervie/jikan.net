# Response Models

Response data is wrapped in `BaseJikanResponse<T>` or `PaginatedJikanResponse<T>`. Access the actual data via the `Data` property.

## Response wrappers

### BaseJikanResponse&lt;T&gt;

| Property | Type | Description |
|----------|------|-------------|
| Data | T | The response data |

### PaginatedJikanResponse&lt;T&gt;

Extends `BaseJikanResponse<T>` with pagination metadata.

| Property | Type | Description |
|----------|------|-------------|
| Data | T | The response data |
| Pagination | Pagination | Page info (current page, has next, items count, etc.) |

---

## Anime

### Anime

| Property | Type | Description |
|----------|------|-------------|
| MalId | long? | MAL id |
| Url | string | Canonical link |
| Images | ImagesSet | Images in various formats |
| Trailer | AnimeTrailer | Trailer info |
| Title | string | Title (obsolete; use Titles) |
| Titles | ICollection&lt;TitleEntry&gt; | Multiple titles |
| Type | string | e.g. "TV", "Movie" |
| Source | string | e.g. "Manga", "Original" |
| Episodes | int? | Episode count |
| Status | string | Airing status |
| Airing | bool | Currently airing |
| Aired | TimePeriod | Airing period |
| Duration | string | Duration per episode |
| Rating | string | Age rating |
| Score | double? | MAL score |
| ScoredBy | int? | Number of scorers |
| Rank | int? | Score rank |
| Popularity | int? | Popularity rank |
| Members | int? | Members count |
| Favorites | int? | Favorites count |
| Synopsis | string | Synopsis |
| Background | string | Background info |
| Season | Season? | Premier season |
| Year | int? | Premier year |
| Broadcast | AnimeBroadcast | Broadcast day/time |
| Producers | ICollection&lt;MalUrl&gt; | Producers |
| Licensors | ICollection&lt;MalUrl&gt; | Licensors |
| Studios | ICollection&lt;MalUrl&gt; | Studios |
| Genres | ICollection&lt;MalUrl&gt; | Genres |
| Themes | ICollection&lt;MalUrl&gt; | Themes |
| Demographics | ICollection&lt;MalUrl&gt; | Demographics |
| Approved | bool | Entry approved on MAL |

### AnimeCharacter

| Property | Type | Description |
|----------|------|-------------|
| Character | CharacterEntry | Character details |
| Role | string | e.g. "main", "supporting" |
| Favorites | int? | Favorites count |
| VoiceActors | ICollection&lt;VoiceActorEntry&gt; | Voice actors (anime only) |

### AnimeStaffPosition

| Property | Type | Description |
|----------|------|-------------|
| Person | MalImageSubItem | Person details |
| Position | ICollection&lt;string&gt; | Staff positions/roles |

### AnimeEpisode

| Property | Type | Description |
|----------|------|-------------|
| MalId | long | MAL id |
| Url | string | Episode URL |
| Title | string | Episode title |
| TitleJapanese | string | Japanese title |
| Duration | int? | Duration in seconds |
| Aired | DateTime? | Air date |
| Filler | bool? | Is filler |
| Recap | bool? | Is recap |
| Synopsis | string | Synopsis |
| ForumUrl | string | Forum discussion URL |
| Score | double? | Average score |

---

## Manga

### Manga

| Property | Type | Description |
|----------|------|-------------|
| MalId | long | MAL id |
| Url | string | Canonical link |
| Title | string | Title (obsolete; use Titles) |
| Titles | ICollection&lt;TitleEntry&gt; | Multiple titles |
| Images | ImagesSet | Images |
| Status | string | e.g. "Finished" |
| Type | string | e.g. "Manga", "Light Novel" |
| Volumes | int? | Volume count |
| Chapters | int? | Chapter count |
| Publishing | bool | Currently publishing |
| Published | TimePeriod | Publication period |
| Score | decimal? | MAL score |
| ScoredBy | int? | Number of scorers |
| Rank | int? | Score rank |
| Popularity | int? | Popularity rank |
| Members | int? | Members count |
| Favorites | int? | Favorites count |
| Synopsis | string | Synopsis |
| Background | string | Background info |
| Genres | ICollection&lt;MalUrl&gt; | Genres |
| Authors | ICollection&lt;MalUrl&gt; | Authors |
| Serializations | ICollection&lt;MalUrl&gt; | Serializations |
| Themes | ICollection&lt;MalUrl&gt; | Themes |
| Demographics | ICollection&lt;MalUrl&gt; | Demographics |
| Approved | bool | Entry approved on MAL |

### MangaCharacter

| Property | Type | Description |
|----------|------|-------------|
| Character | CharacterEntry | Character details |
| Role | string | Role in manga |
| Favorites | int? | Favorites count |

---

## Character

### Character

| Property | Type | Description |
|----------|------|-------------|
| MalId | long | MAL id |
| Url | string | Character page URL |
| Name | string | Character name |
| NameKanji | string | Name in kanji |
| Nicknames | ICollection&lt;string&gt; | Nicknames |
| About | string | About character |
| Favorites | int? | Favorites count |
| Images | ImagesSet | Images |

---

## Person

### Person

| Property | Type | Description |
|----------|------|-------------|
| MalId | long | MAL id |
| Url | string | Person URL |
| Name | string | Name |
| GivenName | string | Given name |
| FamilyName | string | Family name |
| AlternativeNames | ICollection&lt;string&gt; | Alternate names |
| Birthday | DateTime? | Birthday |
| WebsiteUrl | string | Website URL |
| MemberFavorites | int? | Favorites count |
| About | string | About |
| Images | ImagesSet | Images |

---

## User

### UserProfile

| Property | Type | Description |
|----------|------|-------------|
| MalId | long? | MAL user id |
| Username | string | Username |
| Url | string | Profile URL |
| Images | ImagesSet | Avatar images |
| Gender | string | Gender |
| Location | string | Location |
| LastOnline | DateTime? | Last activity |
| Birthday | DateTime? | Birthday |
| Joined | DateTime? | Account creation date |

---

## Forum

### ForumTopic

| Property | Type | Description |
|----------|------|-------------|
| MalId | long | Topic MAL id |
| Url | string | Topic URL |
| Title | string | Topic title |
| Date | DateTime? | Topic start date |
| AuthorUsername | string | Author username |
| AuthorUrl | string | Author profile URL |
| Comments | int? | Comment count |
| LastPost | ForumPostSnippet | Last comment info |

---

## Review

### Review

| Property | Type | Description |
|----------|------|-------------|
| MalId | long | MAL id |
| Url | string | Review URL |
| Type | string | Review type |
| Date | DateTime? | Creation date |
| Content | string | Review text |
| User | UserMetadata | Reviewer |
| Reactions | ReviewReactions | Reaction counts |
| EpisodesWatched | int? | Episodes watched (anime) |
| ChaptersRead | int? | Chapters read (manga) |
| Score | int | Review score |
| IsSpoiler | bool | Contains spoilers |

---

## Common types

### MalUrl

| Property | Type | Description |
|----------|------|-------------|
| MalId | long | MAL id |
| Name | string | Display name |
| Url | string | MAL URL |

### ImagesSet

Contains image URLs in various formats (e.g. Jpg, Webp) and sizes (e.g. ImageUrl, SmallImageUrl, LargeImageUrl).

### TimePeriod

| Property | Type | Description |
|----------|------|-------------|
| From | DateTime? | Start |
| To | DateTime? | End |

### ExternalLink

| Property | Type | Description |
|----------|------|-------------|
| Name | string | Service name |
| Url | string | Link URL |

### Recommendation

| Property | Type | Description |
|----------|------|-------------|
| Url | string | Recommendation URL |
| Votes | int | Number of users who recommended |
| Entry | RecommendationEntry | Recommended entry details |

### RelatedEntry

| Property | Type | Description |
|----------|------|-------------|
| Relation | string | Relation type |
| Entry | ICollection&lt;MalUrl&gt; | Related entries |
