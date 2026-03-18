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

Then restore dependencies:

```
dotnet restore
```

## Initialization

Initialize a `Jikan` instance to make requests:

```csharp
IJikan jikan = new Jikan();
```

For custom configuration (endpoint, rate limiting, etc.), use `JikanClientConfiguration`:

```csharp
var config = new JikanClientConfiguration
{
    Endpoint = "https://api.jikan.moe/v4"
};
var jikan = new Jikan(config);
```

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

### HttpClientFactory

For custom HttpClient (timeout, base address, etc.):

```csharp
services.AddHttpClient<IJikan, Jikan>(client =>
{
    client.Timeout = TimeSpan.FromSeconds(10);
    client.BaseAddress = new Uri("https://api.jikan.moe/v4");
});
```

## Using Own Instance of Jikan API

To use a self-hosted instance of the Jikan REST API instead of the public one, set the `Endpoint` in `JikanClientConfiguration`:

```csharp
var config = new JikanClientConfiguration
{
    Endpoint = "https://your-jikan-instance.example.com/v4"
};
var jikan = new Jikan(config);
```
