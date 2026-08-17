# Getting Started

## Installation

Add JikanDotNet to your project via NuGet.

### Package Manager

```
PM> Install-Package JikanDotNet
```

### .NET CLI

```
dotnet add package JikanDotNet
```

Supported target frameworks: `netstandard2.0`, `net6.0`, `net8.0`, and `net10.0`.

## Initialization

Initialize a `Jikan` instance to make requests:

```csharp
using JikanDotNet;

IJikan jikan = new Jikan();
```

For custom configuration (rate limiting, exception handling, etc.), use `JikanClientConfiguration`:

```csharp
using JikanDotNet;
using JikanDotNet.Config;

var config = new JikanClientConfiguration
{
    SuppressException = false,
    LimiterConfigurations = TaskLimiterConfiguration.Default
};
var jikan = new Jikan(config);
```

See [Rate Limiting](RateLimiting.md) for limiter options.

To customize the HTTP client (base address, timeout, headers, etc.), pass an `HttpClient` to the constructor:

```csharp
using JikanDotNet;
using JikanDotNet.Config;
using System.Net.Http;

var httpClient = new HttpClient
{
    BaseAddress = new Uri("https://api.jikan.moe/v4/"),
    Timeout = TimeSpan.FromSeconds(10)
};
var jikan = new Jikan(new JikanClientConfiguration(), httpClient);
```

Use a trailing slash on `BaseAddress` so relative request paths resolve correctly.

## Dependency Injection

Register `Jikan` for dependency injection:

### Autofac

```csharp
public class YourModule : Module
{
    public override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<Jikan>().As<IJikan>();
    }
}
```

### Ninject

```csharp
public class YourModule : NinjectModule
{
    public override void Load()
    {
        Bind<IJikan>().To<Jikan>();
    }
}
```

### Microsoft.Extensions.DependencyInjection

```csharp
var services = new ServiceCollection()
    .AddSingleton<IJikan, Jikan>()
    .BuildServiceProvider();
```

The parameterless `Jikan()` constructor is used, which applies default configuration and the public Jikan API endpoint.

### HttpClientFactory

`Jikan` accepts an injected `HttpClient`, but it does not expose an `HttpClient`-only constructor, so `AddHttpClient<IJikan, Jikan>()` is not supported. Register a named client and construct `Jikan` manually instead:

```csharp
services.AddHttpClient("Jikan", client =>
{
    client.BaseAddress = new Uri("https://api.jikan.moe/v4/");
    client.Timeout = TimeSpan.FromSeconds(10);
});

services.AddSingleton<IJikan>(sp =>
{
    var httpClientFactory = sp.GetRequiredService<IHttpClientFactory>();
    var httpClient = httpClientFactory.CreateClient("Jikan");
    return new Jikan(new JikanClientConfiguration(), httpClient);
});
```

## Using Own Instance of Jikan API

The Jikan public API will be discontinued on October 1, 2026. Pointing this client at a self-hosted instance is the supported way to keep using JikanDotNet after shutdown. For new work, the direct successor is [Tenrai.net](https://github.com/Kareadita/tenrai.net). See the [README announcement](../README.md#important-notice-jikan-public-api-shutdown-and-library-dormancy).

To use a self-hosted instance of the Jikan REST API instead of the public one, set `HttpClient.BaseAddress` when constructing `Jikan`:

```csharp
using JikanDotNet;
using JikanDotNet.Config;
using System.Net.Http;

var httpClient = new HttpClient
{
    BaseAddress = new Uri("https://your-jikan-instance.example.com/v4/")
};
var jikan = new Jikan(new JikanClientConfiguration(), httpClient);
```
