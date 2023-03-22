using JikanDotNet.Limiter;
using System;
using System.Collections.Generic;

namespace JikanDotNet.Config;

/// <summary> <see cref="TaskLimiter"/> configuration class </summary>
/// <seealso cref="CompositeTaskLimiter"/>
public class TaskLimiterConfiguration : IEquatable<TaskLimiterConfiguration>, IComparable<TaskLimiterConfiguration>, IComparable
{
    /// <summary> Maximum number of task executions per unit of time </summary>
    public int Count { get; }

    /// <summary> Unit of time </summary>
    public TimeSpan TimeSpan { get; }

    /// <summary> Maximum execution rate of the configuration </summary>
    /// <remarks> Rate is expressed in times per second </remarks>
    public double MaximumRate => Count / TimeSpan.TotalSeconds;

    /// <summary> Disable task limiting </summary>
    public static List<TaskLimiterConfiguration> None { get; } = new();

    /// <summary> Default task limiting configuration tuned for public endpoint (https://api.jikan.moe/v4/)</summary>
    public static List<TaskLimiterConfiguration> Default { get; } = new()
    {
        new TaskLimiterConfiguration(1, TimeSpan.FromMilliseconds(300)),    // Space every request by at least 300ms
        new TaskLimiterConfiguration(3, TimeSpan.FromMilliseconds(1000)),   // Rate limit for request bursts (3/s)
        new TaskLimiterConfiguration(4, TimeSpan.FromMilliseconds(4000))    // Baseline limit (60/min)
    };

    public TaskLimiterConfiguration(int count, TimeSpan timeSpan)
    {
        Count = count;
        TimeSpan = timeSpan;
    }

    #region Equality methods

    public bool Equals(TaskLimiterConfiguration other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Count == other.Count && TimeSpan.Equals(other.TimeSpan);
    }

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((TaskLimiterConfiguration)obj);
    }

    public override int GetHashCode()
    {
        unchecked
        {
            return (Count * 397) ^ TimeSpan.GetHashCode();
        }
    }

    public static bool operator ==(TaskLimiterConfiguration left, TaskLimiterConfiguration right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(TaskLimiterConfiguration left, TaskLimiterConfiguration right)
    {
        return !Equals(left, right);
    }

    #endregion

    #region Comparator methods

    public int CompareTo(TaskLimiterConfiguration other)
    {
        if (ReferenceEquals(this, other)) return 0;
        if (ReferenceEquals(null, other)) return 1;
        return MaximumRate.CompareTo(other.MaximumRate);
    }

    public int CompareTo(object obj)
    {
        if (ReferenceEquals(null, obj)) return 1;
        if (ReferenceEquals(this, obj)) return 0;
        return obj is TaskLimiterConfiguration other ? CompareTo(other) : throw new ArgumentException($"Object must be of type {nameof(TaskLimiterConfiguration)}");
    }

    public static bool operator <(TaskLimiterConfiguration left, TaskLimiterConfiguration right)
    {
        return Comparer<TaskLimiterConfiguration>.Default.Compare(left, right) < 0;
    }

    public static bool operator >(TaskLimiterConfiguration left, TaskLimiterConfiguration right)
    {
        return Comparer<TaskLimiterConfiguration>.Default.Compare(left, right) > 0;
    }

    public static bool operator <=(TaskLimiterConfiguration left, TaskLimiterConfiguration right)
    {
        return Comparer<TaskLimiterConfiguration>.Default.Compare(left, right) <= 0;
    }

    public static bool operator >=(TaskLimiterConfiguration left, TaskLimiterConfiguration right)
    {
        return Comparer<TaskLimiterConfiguration>.Default.Compare(left, right) >= 0;
    }

    #endregion
}