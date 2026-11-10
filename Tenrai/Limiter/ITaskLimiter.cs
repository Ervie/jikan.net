using System;
using System.Threading.Tasks;

namespace Tenrai.Limiter;

/// <summary>
/// Base task limiter implementation
/// </summary>
internal interface ITaskLimiter
{
	Task LimitAsync(Func<Task> taskFactory);

	Task<T> LimitAsync<T>(Func<Task<T>> taskFactory);
}