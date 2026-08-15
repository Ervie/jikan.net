# Response Models

Response data is wrapped in `BaseTenraiResponse<T>` or `PaginatedTenraiResponse<T>`. Access the actual data via the `Data` property.

## Response wrappers

### BaseTenraiResponse&lt;T&gt;

| Property | Type | Description |
|----------|------|-------------|
| Data | T | The response data |

### PaginatedTenraiResponse&lt;T&gt;

Extends `BaseTenraiResponse<T>` with pagination metadata.

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
| IsPreliminary | bool | Written before the entry finished airing/publishing |
| Tags | ICollection&lt;string&gt; | Sentiment tags (e.g. "Recommended", "Mixed Feelings", "Not Recommended") |

---

## Interest Stacks

### InterestStack

| Property | Type | Description |
|----------|------|-------------|
| MalId | long | MAL id |
| Url | string | Canonical link |
| StackType | string | Type of entries the stack holds: "anime" or "manga" |
| Title | string | Stack title |
| Description | string | Author's description. Empty string when they did not write one |
| AuthorUsername | string | MAL username of the author |
| AuthorUrl | string | Link to the author's profile |
| IsOfficial | bool | Curated by MyAnimeList staff |
| IsChallenge | bool | Marked as a challenge |
| IsSpoiler | bool | Marked as containing spoilers |
| RestackCount | int | Number of users who restacked it |
| EntryCount | int | Number of entries in the stack |
| CreatedAt | DateTime? | Creation date |

### InterestStackDetails

Extends `InterestStack`. Returned by `GetInterestStackAsync`.

| Property | Type | Description |
|----------|------|-------------|
| Entries | ICollection&lt;InterestStackEntry&gt; | Entries in the order the author arranged them |

### InterestStackEntry

The entry shape depends on the parent stack's `StackType`, and the entry itself carries no discriminator. `Episodes` and `AiredFromYear` are populated only for `"anime"` stacks; `Volumes` and `PublishedFromYear` only for `"manga"` stacks. The unused pair is always null.

| Property | Type | Description |
|----------|------|-------------|
| Position | int | Position within the stack, starting at 1 |
| MalId | long | MAL id of the anime or manga |
| Url | string | Canonical link |
| Images | ImagesSet | Images in various formats |
| Title | string | Entry title |
| TitleEnglish | string | English title, null when MAL has none |
| Type | string | e.g. "TV", "Movie", "Manga" |
| AuthorScore | int? | Score the stack's author gave, null when unscored |
| Note | string | Note the author attached, null when none |
| Episodes | int? | Episode count (anime stacks only) |
| AiredFromYear | int? | Year it started airing (anime stacks only) |
| Volumes | int? | Volume count (manga stacks only). 0 is a real value, not a sentinel |
| PublishedFromYear | int? | Year it started publishing (manga stacks only) |

---

## Status

### TenraiStatus

Returned bare by `GetStatusAsync`, not wrapped in `BaseTenraiResponse<T>`.

| Property | Type | Description |
|----------|------|-------------|
| ApiVersion | int | Version of the status API contract |
| Page | StatusPage | Status page this snapshot belongs to |
| Checker | StatusChecker | Health of the monitoring process itself |
| GeneratedAt | DateTime? | When the snapshot was generated |
| Services | ICollection&lt;StatusService&gt; | Monitored services |
| ScheduledMaintenances | ICollection&lt;StatusMaintenance&gt; | Scheduled or running maintenance windows |

### StatusPage

| Property | Type | Description |
|----------|------|-------------|
| Name | string | Name of the status page |
| Url | string | Link to the human readable status page |

### StatusChecker

| Property | Type | Description |
|----------|------|-------------|
| Healthy | bool | Is the checker running normally. When false, service statuses may be out of date |
| LastCheckAt | DateTime? | Date of the last completed check |
| StaleAfterSeconds | int | Seconds after LastCheckAt at which the snapshot is considered stale |

### StatusService

| Property | Type | Description |
|----------|------|-------------|
| Id | string | Service identifier, e.g. "tenrai" |
| Name | string | Display name |
| Status | string | "operational", "degraded", "down", "unknown", or "maintenance" |
| HomepageUrl | string | Link to the service homepage |
| LogoUrl | string | Link to the service logo |
| LastCheckAt | DateTime? | When this service was last probed |
| OutageMinutes90d | int | Total minutes down over the last 90 days |
| DegradedMinutes90d | int | Total minutes degraded over the last 90 days |
| DailyOutageMinutes90d | IDictionary&lt;string, int&gt; | Minutes down per day, keyed "yyyy-MM-dd". Clean days are omitted |
| DailyDegradedMinutes90d | IDictionary&lt;string, int&gt; | Minutes degraded per day, keyed "yyyy-MM-dd". Clean days are omitted |
| ActiveMaintenance | StatusMaintenance | Maintenance window currently affecting this service, null when none |

### StatusMaintenance

| Property | Type | Description |
|----------|------|-------------|
| Id | string | Identifier of the maintenance window |
| ServiceId | string | Affected service id, or the literal "all" |
| Title | string | Title of the window |
| StartsAt | DateTime? | Start date |
| EndsAt | DateTime? | End date |
| Status | string | Status the affected service reports while the window runs |
| State | string | "scheduled" or "active" |
| CreatedAt | DateTime? | Creation date |
| UpdatedAt | DateTime? | Last update date |

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
| From | DateTimeOffset? | Start (preserves UTC offset returned by API) |
| To | DateTimeOffset? | End (preserves UTC offset returned by API) |

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
