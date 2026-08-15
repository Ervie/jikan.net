# Tenrai.Net Documentation

Tenrai.Net is a .NET wrapper for the [Tenrai](https://tenrai.org) REST API (`https://api.tenrai.org/v1`), fetching data from [MyAnimeList](https://myanimelist.com). Tenrai is a drop-in successor to the [Jikan](https://jikan.moe) v4 API. This library is based on [@Ervie](https://github.com/Ervie)'s [Jikan.NET](https://github.com/Ervie/jikan.net).

## Contents

- [Quick Start](README.md#quick-start)
- [Getting Started](GettingStarted.md) - Installation, initialization, Server Key, dependency injection
- [API Reference](API.md) - All available endpoints with parameters, examples, and model links
- [Models](Models.md) - Response model properties and types
- [Rate Limiting](RateLimiting.md) - Configuring rate limits and Server Keys
- [Migrating from Jikan.NET](Migrating-from-JikanDotNet.md) - Renames and breaking changes when moving from `JikanDotNet`
- [Migrating from 1.x to 2.0](Migrating-from-1.x-to-2.0.md) - Older Jikan.NET migration guide

## Quick Start

```csharp
// Initialize Tenrai client
var tenrai = new TenraiClient();

// Get anime by MAL id (e.g. Cowboy Bebop)
var bebop = await tenrai.GetAnimeAsync(1);
Console.WriteLine(bebop.Data.Title);      // "Cowboy Bebop"
Console.WriteLine(bebop.Data.Type);       // "TV"
Console.WriteLine(bebop.Data.Rating);     // "R - 17+ (violence & profanity)"

// Get manga by MAL id (e.g. Berserk)
var berserk = await tenrai.GetMangaAsync(2);
Console.WriteLine(berserk.Data.Title);     // "Berserk"
Console.WriteLine(berserk.Data.Status);    // "Publishing"

// Get manga reviews sorted by newest, recommended sentiment only
var reviews = await tenrai.GetMangaReviewsAsync(2, new ReviewsSearchConfig
{
    Sort = ReviewSortOrder.Newest,
    Sentiment = ReviewSentiment.Recommended
});
Console.WriteLine(reviews.Data.First().Tags.First());  // "Recommended"
```

All response models contain a `Data` property where the actual response data resides. Paginated endpoints also include pagination metadata. The one exception is `GetStatusAsync`, which returns `TenraiStatus` directly because the status service does not wrap its response.

---

## Documentation conventions (for contributors and agents)

- **API reference:** [docs/API.md](API.md) - One section per region, each method has: description, parameters table, return type, example, and model link when relevant.
- **Models:** [docs/Models.md](Models.md) - Response model classes with property tables.
- **To add a new endpoint:** Add a subsection under the correct region in `docs/API.md` with: method name, description, parameters table, return type, code example, and link to model in `Models.md` if applicable.
- **To add a new model:** Add a section in `docs/Models.md` with class name and properties table.
- **Conceptual pages:** `docs/GettingStarted.md`, `docs/RateLimiting.md`, `docs/Migrating-from-JikanDotNet.md` - Edit these directly when updating setup, migration, or configuration docs.
