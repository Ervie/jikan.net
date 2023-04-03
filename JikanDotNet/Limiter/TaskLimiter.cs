using JikanDotNet.Config;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace JikanDotNet.Limiter;

/// <summary>
/// A task limiter that throttles task executions using <see cref="SemaphoreSlim"/>
/// </summary>
internal class TaskLimiter : ITaskLimiter, IEquatable<TaskLimiter>, IComparable<TaskLimiter>, IComparable
{
    private readonly SemaphoreSlim _semaphore;

    /// <summary> Configuration of the task limiter </summary>
    internal TaskLimiterConfiguration Configuration { get; }

    internal TaskLimiter(TaskLimiterConfiguration config)
    {
        Configuration = config;

        _semaphore = new SemaphoreSlim(Configuration.Count, Configuration.Count);
    }

    /// <summary>
    /// Throttle the passed task execution according to the limiter <see cref="Configuration"/>
    /// </summary>
    /// <param name="taskFactory">Delegate that represents a method that returns a <see cref="Task"/></param>
    public async Task LimitAsync(Func<Task> taskFactory)
    {
        await _semaphore.WaitAsync().ConfigureAwait(false);

        var task = taskFactory();

        // Fire and forget semaphore release
        _ = task.ContinueWith(async e =>
        {
            await Task.Delay(Configuration.TimeSpan);
            _semaphore.Release(1);
        });

        await task;
    }

    /// <summary>
    /// Throttle the passed task execution according to the limiter <see cref="Configuration"/>
    /// </summary>
    /// <param name="taskFactory">Delegate that represents a method that returns a <see cref="Task{T}"/></param>
    /// <returns> The awaited <see cref="Task{T}"/> result </returns>
    public async Task<T> LimitAsync<T>(Func<Task<T>> taskFactory)
    {
        await _semaphore.WaitAsync().ConfigureAwait(false);

        var task = taskFactory();

        // Fire and forget semaphore release
        _ = task.ContinueWith(async e =>
        {
            await Task.Delay(Configuration.TimeSpan);
            _semaphore.Release(1);
        });

        return await task;
    }

    #region Equality methods

    public bool Equals(TaskLimiter other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Configuration.Equals(other.Configuration);
    }

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((TaskLimiter)obj);
    }

    public override int GetHashCode()
    {
        return Configuration.GetHashCode();
    }

    public static bool operator ==(TaskLimiter left, TaskLimiter right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(TaskLimiter left, TaskLimiter right)
    {
        return !Equals(left, right);
    }

    #endregion

    #region Comparer methods

    public int CompareTo(TaskLimiter other)
    {
        if (ReferenceEquals(this, other)) return 0;
        if (ReferenceEquals(null, other)) return 1;
        return Configuration.CompareTo(other.Configuration);
    }

    public int CompareTo(object obj)
    {
        if (ReferenceEquals(null, obj)) return 1;
        if (ReferenceEquals(this, obj)) return 0;
        return obj is TaskLimiter other ? CompareTo(other) : throw new ArgumentException($"Object must be of type {nameof(TaskLimiter)}");
    }

    public static bool operator <(TaskLimiter left, TaskLimiter right)
    {
        return Comparer<TaskLimiter>.Default.Compare(left, right) < 0;
    }

    public static bool operator >(TaskLimiter left, TaskLimiter right)
    {
        return Comparer<TaskLimiter>.Default.Compare(left, right) > 0;
    }

    public static bool operator <=(TaskLimiter left, TaskLimiter right)
    {
        return Comparer<TaskLimiter>.Default.Compare(left, right) <= 0;
    }

    public static bool operator >=(TaskLimiter left, TaskLimiter right)
    {
        return Comparer<TaskLimiter>.Default.Compare(left, right) >= 0;
    }

    #endregion
}