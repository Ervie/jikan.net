# Rate Limiting

Jikan v4 API supports the following quota rules:

- Cached requests do not count towards the limit
- 2 requests per second
- 30 requests per minute
- Daily limit: Unlimited

Exceeding the limit returns HTTP 429 (Too Many Requests).

Jikan.net supports rate limiting by default (versions 2.6.0 or newer). Configure it via `JikanClientConfiguration` and `TaskLimiterConfiguration`. Each configuration defines a rule: max number of calls and the timespan in which they can be made.

Each Jikan instance can have its own rate limiter configuration, which does not count towards global calls made from your application.

## Examples

```csharp
// Allow one request per second
var config = new JikanClientConfiguration
{
    LimiterConfigurations = new List<TaskLimiterConfiguration>
    {
        new(1, TimeSpan.FromMilliseconds(1000))
    }
};
var jikan = new Jikan(config);

// Allow one request per 3 seconds
var config = new JikanClientConfiguration
{
    LimiterConfigurations = new List<TaskLimiterConfiguration>
    {
        new(1, TimeSpan.FromMilliseconds(3000))
    }
};
jikan = new Jikan(config);

// Allow one request per 3 seconds, and 2 per 5 seconds
var config = new JikanClientConfiguration
{
    LimiterConfigurations = new List<TaskLimiterConfiguration>
    {
        new(1, TimeSpan.FromMilliseconds(3000)),
        new(2, TimeSpan.FromMilliseconds(5000))
    }
};
jikan = new Jikan(config);

// Allow one request per 500 miliseconds, and max 100 per minute
var config = new JikanClientConfiguration
{
    LimiterConfigurations = new List<TaskLimiterConfiguration>
    {
        new(1, TimeSpan.FromMilliseconds(500)),
        new(100, TimeSpan.FromMinutes(1))
    }
};
jikan = new Jikan(config);
```

## Predefined Setups

| Configuration | Description |
|---------------|-------------|
| `TaskLimiterConfiguration.Default` | Default rules (recommended) |
| `TaskLimiterConfiguration.None` | No rate limiting |

```csharp
// Default rules (passed explicitly)
var config = new JikanClientConfiguration
{
    LimiterConfigurations = TaskLimiterConfiguration.Default
};
var jikan = new Jikan(config);

// No rate limiting
var config = new JikanClientConfiguration
{
    LimiterConfigurations = TaskLimiterConfiguration.None
};
var jikan = new Jikan(config);
```

Default rules:

- Space every request by at least 300ms
- Rate limit for bursts: 3 requests per second
- Baseline limit: 4 requests per 4 seconds (60/min)
