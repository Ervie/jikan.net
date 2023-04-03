using System;
using System.Threading.Tasks;

namespace JikanDotNet.Limiter;

/// <summary>
/// Base task limiter implementation
/// </summary>
internal interface ITaskLimiter
{
    Task LimitAsync(Func<Task> taskFactory);

    Task<T> LimitAsync<T>(Func<Task<T>> taskFactory);
}