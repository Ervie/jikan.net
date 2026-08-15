# Rate Limiting

The Tenrai API enforces the following quotas:

| Access | Header | Requests/min | Requests/sec | Requests/day |
|--------|--------|-------------:|-------------:|-------------:|
| **Public** | *(none)* | 60 | 3 | 40,000 |
| **Server Key** | `X-Server-Key` | 300 | 5 | Unlimited |

- Cached requests do not count towards the limit.
- Exceeding the limit returns HTTP 429 (Too Many Requests); honor the `Retry-After` header before retrying.
- Public limits are enforced per IP; Server Key limits are enforced per key.

Tenrai.Net applies client-side rate limiting by default. Configure it via `TenraiClientConfiguration` and `TaskLimiterConfiguration`. Each configuration defines a rule: max number of calls and the timespan in which they can be made. Each `TenraiClient` instance has its own rate limiter, which does not count towards global calls made from your application.

`GetStatusAsync` is the one exception: it targets the status host rather than the API, so it deliberately bypasses the client limiter and does not consume your API budget. It would otherwise be throttled precisely when the API is saturated, which is the opposite of what a status check is for. Poll it no more than once per minute; the server caches the snapshot for 30 seconds.

## Server Keys

A Server Key ([Patreon supporters](https://www.patreon.com/TenraiAPI)) raises your limits. Set it on the configuration; it is sent as the `X-Server-Key` header, and — unless you have overridden `LimiterConfigurations` — the client automatically switches to the higher Server Key limiter tier.

```csharp
var config = new TenraiClientConfiguration
{
    ServerKey = "your-server-key"
};
var tenrai = new TenraiClient(config);
```

> Some endpoints (the bulk `Get*IdsAsync` methods) require a Server Key and return 401 without one.

## Examples

```csharp
// Allow one request per second
var config = new TenraiClientConfiguration
{
    LimiterConfigurations = new List<TaskLimiterConfiguration>
    {
        new(1, TimeSpan.FromMilliseconds(1000))
    }
};
var tenrai = new TenraiClient(config);

// Allow one request per 3 seconds, and 2 per 5 seconds
config = new TenraiClientConfiguration
{
    LimiterConfigurations = new List<TaskLimiterConfiguration>
    {
        new(1, TimeSpan.FromMilliseconds(3000)),
        new(2, TimeSpan.FromMilliseconds(5000))
    }
};
tenrai = new TenraiClient(config);
```

## Predefined Setups

| Configuration | Description |
|---------------|-------------|
| `TaskLimiterConfiguration.Default` | Public tier: 60/min, 3/sec (recommended) |
| `TaskLimiterConfiguration.ServerKey` | Server Key tier: 300/min, 5/sec |
| `TaskLimiterConfiguration.None` | No rate limiting |

```csharp
// Default rules (passed explicitly)
var config = new TenraiClientConfiguration
{
    LimiterConfigurations = TaskLimiterConfiguration.Default
};
var tenrai = new TenraiClient(config);

// No rate limiting
config = new TenraiClientConfiguration
{
    LimiterConfigurations = TaskLimiterConfiguration.None
};
tenrai = new TenraiClient(config);
```

Default (public tier) rules:

- Space every request by at least 300ms
- Rate limit for bursts: 3 requests per second
- Baseline limit: 4 requests per 4 seconds (60/min)

Server Key tier rules:

- Space every request by at least 200ms
- Rate limit / baseline: 5 requests per second (300/min)
