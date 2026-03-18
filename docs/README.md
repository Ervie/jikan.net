# jikan.net Documentation

Jikan.net is a .NET wrapper for [Jikan](https://jikan.moe) REST API, fetching data from [MyAnimeList](https://myanimelist.com).

## Contents

- [Quick Start](README.md#quick-start)
- [Getting Started](GettingStarted.md) - Installation, initialization, dependency injection
- [API Reference](API.md) - All available endpoints (2.x) with parameters, examples, and model links
- [Models](Models.md) - Response model properties and types
- [Rate Limiting](RateLimiting.md) - Configuring rate limits
- [Migrating from 1.x to 2.0](Migrating-from-1.x-to-2.0.md) - Breaking changes and migration guide

## Quick Start

```csharp
// Initialize Jikan client
var jikan = new Jikan();

// Get anime by MAL id (e.g. Cowboy Bebop)
var bebop = await jikan.GetAnimeAsync(1);
Console.WriteLine(bebop.Data.Title);      // "Cowboy Bebop"
Console.WriteLine(bebop.Data.Type);       // "TV"
Console.WriteLine(bebop.Data.Rating);     // "R - 17+ (violence & profanity)"

// Get manga by MAL id (e.g. Berserk)
var berserk = await jikan.GetMangaAsync(2);
Console.WriteLine(berserk.Data.Title);     // "Berserk"
Console.WriteLine(berserk.Data.Status);    // "Publishing"

// Get person by MAL id (e.g. Hayao Miyazaki)
var miyazaki = await jikan.GetPersonAsync(1870);
Console.WriteLine(miyazaki.Data.FamilyName);  // "Miyazaki"

// Get character by MAL id (e.g. Lain Iwakura)
var lain = await jikan.GetCharacterAsync(2219);
Console.WriteLine(lain.Data.Url);  // "https://myanimelist.net/character/2219/Lain_Iwakura"
```

All response models contain a `Data` property where the actual response data resides. Paginated endpoints also include pagination metadata.

---

## Documentation conventions (for contributors and agents)

- **API reference:** [docs/API.md](API.md) - One section per region, each method has: description, parameters table, return type, example, and model link when relevant.
- **Models:** [docs/Models.md](Models.md) - Response model classes with property tables.
- **To add a new endpoint:** Add a subsection under the correct region in `docs/API.md` with: method name, description, parameters table, return type, code example, and link to model in `Models.md` if applicable.
- **To add a new model:** Add a section in `docs/Models.md` with class name and properties table.
- **Conceptual pages:** `docs/GettingStarted.md`, `docs/RateLimiting.md`, `docs/Migrating-from-1.x-to-2.0.md` - Edit these directly when updating setup, migration, or configuration docs.
