# AGENTS.md

## Purpose

Tenrai.Net is a .NET wrapper for the Tenrai REST API (`https://api.tenrai.org/v1`), a drop-in successor to the Jikan v4 API. The public package is `Tenrai.Net` (assembly/namespace `Tenrai`), and the main goal is to expose strongly typed, asynchronous access to the MyAnimeList data returned by Tenrai.

## Start Here

- `README.md` explains the project, package installation, supported API areas, and changelog pointers.
- `docs/README.md` is the documentation index and contributor guide for public API docs.
- `docs/Tenrai-Conversion-Plan.md` tracks the Jikan.net → Tenrai.Net conversion (phases, decisions, differences).
- `Tenrai.slnx` contains the library project and the xUnit test project.
- `Tenrai/TenraiClient.cs` contains the endpoint implementations.
- `Tenrai/Interfaces/ITenrai.cs` defines the public client surface.
- `Tenrai/Model/Response`, `Tenrai/Model/Base`, `Tenrai/Model/Search`, and `Tenrai/Enumerations` hold public response models, wrappers, search configs, and enums.
- `Tenrai.Test` contains xUnit tests. Many tests call the live public Tenrai API and may be network-sensitive.

## Repository Rules

- Preserve public API compatibility unless the task explicitly asks for a breaking change.
- Keep `ITenrai` and `TenraiClient` aligned when adding or changing client methods.
- Endpoints Tenrai does not implement (users, clubs, watch, user updates) are kept but marked `[Obsolete]` and throw `NotSupportedException`; do not re-enable them against Tenrai.
- Validate public inputs with `Guard` before building endpoint routes.
- Prefer `TenraiEndpointConsts` for route segments instead of repeating string literals.
- Response models should use `System.Text.Json.Serialization.JsonPropertyName` for JSON field mapping.
- Keep XML documentation on public types and members meaningful because the package emits documentation files.
- In C# code, an `if` may skip braces only when its body is an immediate exit (`return`, `throw`, `continue`, `break`) on the same line: `if (arg == null) throw new TenraiValidationException(...);`. Every other `if` and `else` body gets braces, even a single statement.
- C# files use file-scoped namespaces (`namespace Tenrai;`) and tabs for indentation, as set in `.editorconfig`.

## Docs Rule

Canonical docs for this repo are `README.md`, `docs/README.md`, `docs/API.md`, `docs/Models.md`, `docs/GettingStarted.md`, `docs/RateLimiting.md`, and `docs/Migrating-from-1.x-to-2.0.md`.

If a change touches user-facing behavior, a public API, package setup, rate limiting, migration behavior, or anything already documented, update the relevant canonical doc in the same change.

When adding a new endpoint, update `docs/API.md` with the method description, parameters, return type, example, and model link. When adding or changing public response models, update `docs/Models.md`.

## Verification

Use the smallest verification that matches the change:

```bash
dotnet build Tenrai.slnx
```

```bash
dotnet test Tenrai.Test/Tenrai.Tests.csproj
```

For targeted test runs, prefer a filter, for example:

```bash
dotnet test Tenrai.Test/Tenrai.Tests.csproj --filter FullyQualifiedName~GetAnimeAsyncTests
```

Before relying on full test results, remember that many tests hit the live Tenrai API and can fail because of network, upstream data, or rate limiting rather than local code.
