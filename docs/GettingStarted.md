# Getting Started

## Installation

Add Tenrai.Net to your project via NuGet.

### Package Manager

```
PM> Install-Package Tenrai.Net
```

### .NET CLI

```
dotnet add package Tenrai.Net
```

Supported target frameworks: `netstandard2.0`, `net6.0`, `net8.0`, and `net10.0`.

## Initialization

Initialize a `TenraiClient` instance to make requests:

```csharp
using Tenrai;

ITenrai tenrai = new TenraiClient();
```

For custom configuration (rate limiting, exception handling, Server Key, etc.), use `TenraiClientConfiguration`:

```csharp
using Tenrai;
using Tenrai.Config;

var config = new TenraiClientConfiguration
{
    SuppressException = false,
    LimiterConfigurations = TaskLimiterConfiguration.Default
};
var tenrai = new TenraiClient(config);
```

See [Rate Limiting](RateLimiting.md) for limiter options.

### Server Key

Supply a Tenrai Server Key to raise your rate limits (and to access endpoints that require one, such as the bulk `Get*IdsAsync` methods). It is sent as the `X-Server-Key` header:

```csharp
var config = new TenraiClientConfiguration
{
    ServerKey = "your-server-key"
};
var tenrai = new TenraiClient(config);
```

### Custom HttpClient

To customize the HTTP client (base address, timeout, headers, etc.), pass an `HttpClient` to the constructor:

```csharp
using Tenrai;
using Tenrai.Config;
using System.Net.Http;

var httpClient = new HttpClient
{
    BaseAddress = new Uri("https://api.tenrai.org/v1/"),
    Timeout = TimeSpan.FromSeconds(10)
};
var tenrai = new TenraiClient(new TenraiClientConfiguration(), httpClient);
```

Use a trailing slash on `BaseAddress` so relative request paths resolve correctly. Note that when you supply your own `HttpClient`, you are responsible for adding the `X-Server-Key` header yourself.

`GetStatusAsync` is unaffected by `BaseAddress`. It requests the absolute URL `https://tenrai.org/status/api/status`, because the status service is hosted separately from the API, so pointing the client at a different base address does not redirect it.

## Dependency Injection

Register `TenraiClient` for dependency injection:

### Autofac

```csharp
public class YourModule : Module
{
    public override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<TenraiClient>().As<ITenrai>();
    }
}
```

### Ninject

```csharp
public class YourModule : NinjectModule
{
    public override void Load()
    {
        Bind<ITenrai>().To<TenraiClient>();
    }
}
```

### Microsoft.Extensions.DependencyInjection

```csharp
var services = new ServiceCollection()
    .AddSingleton<ITenrai, TenraiClient>()
    .BuildServiceProvider();
```

The parameterless `TenraiClient()` constructor is used, which applies default configuration and the public Tenrai API endpoint.

### HttpClientFactory

`TenraiClient` accepts an injected `HttpClient`, but it does not expose an `HttpClient`-only constructor, so `AddHttpClient<ITenrai, TenraiClient>()` is not supported. Register a named client and construct `TenraiClient` manually instead:

```csharp
services.AddHttpClient("Tenrai", client =>
{
    client.BaseAddress = new Uri("https://api.tenrai.org/v1/");
    client.Timeout = TimeSpan.FromSeconds(10);
});

services.AddSingleton<ITenrai>(sp =>
{
    var httpClientFactory = sp.GetRequiredService<IHttpClientFactory>();
    var httpClient = httpClientFactory.CreateClient("Tenrai");
    return new TenraiClient(new TenraiClientConfiguration(), httpClient);
});
```
