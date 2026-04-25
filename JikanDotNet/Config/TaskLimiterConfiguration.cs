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

    /// <summary> Initializes a new instance of <see cref="TaskLimiterConfiguration"/>. </summary>
    /// <param name="count"> Maximum number of task executions per <paramref name="timeSpan"/>. </param>
    /// <param name="timeSpan"> Unit of time over which <paramref name="count"/> executions are allowed. </param>
    public TaskLimiterConfiguration(int count, TimeSpan timeSpan)
    {
        Count = count;
        TimeSpan = timeSpan;
    }

    #region Equality methods

    /// <summary> Indicates whether the current configuration is equal to another configuration. </summary>
    /// <param name="other"> The configuration to compare with. </param>
    /// <returns> <c>true</c> if both configurations share the same <see cref="Count"/> and <see cref="TimeSpan"/>; otherwise <c>false</c>. </returns>
    public bool Equals(TaskLimiterConfiguration other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Count == other.Count && TimeSpan.Equals(other.TimeSpan);
    }

    /// <summary> Determines whether the specified object is equal to the current configuration. </summary>
    /// <param name="obj"> The object to compare with. </param>
    /// <returns> <c>true</c> if <paramref name="obj"/> is a <see cref="TaskLimiterConfiguration"/> with identical values; otherwise <c>false</c>. </returns>
    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((TaskLimiterConfiguration)obj);
    }

    /// <summary> Serves as the default hash function. </summary>
    /// <returns> A hash code derived from <see cref="Count"/> and <see cref="TimeSpan"/>. </returns>
    public override int GetHashCode()
    {
        unchecked
        {
            return (Count * 397) ^ TimeSpan.GetHashCode();
        }
    }

    /// <summary> Determines whether two <see cref="TaskLimiterConfiguration"/> instances are equal. </summary>
    public static bool operator ==(TaskLimiterConfiguration left, TaskLimiterConfiguration right)
    {
        return Equals(left, right);
    }

    /// <summary> Determines whether two <see cref="TaskLimiterConfiguration"/> instances are different. </summary>
    public static bool operator !=(TaskLimiterConfiguration left, TaskLimiterConfiguration right)
    {
        return !Equals(left, right);
    }

    #endregion

    #region Comparator methods

    /// <summary> Compares the current configuration with another configuration using <see cref="MaximumRate"/>. </summary>
    /// <param name="other"> The configuration to compare with. </param>
    /// <returns> A signed number indicating the relative rates of the configurations being compared. </returns>
    public int CompareTo(TaskLimiterConfiguration other)
    {
        if (ReferenceEquals(this, other)) return 0;
        if (ReferenceEquals(null, other)) return 1;
        return MaximumRate.CompareTo(other.MaximumRate);
    }

    /// <summary> Compares the current configuration with another object. </summary>
    /// <param name="obj"> The object to compare with. </param>
    /// <returns> A signed number indicating the relative rates of the configurations being compared. </returns>
    /// <exception cref="ArgumentException"> Thrown when <paramref name="obj"/> is not a <see cref="TaskLimiterConfiguration"/>. </exception>
    public int CompareTo(object obj)
    {
        if (ReferenceEquals(null, obj)) return 1;
        if (ReferenceEquals(this, obj)) return 0;
        return obj is TaskLimiterConfiguration other ? CompareTo(other) : throw new ArgumentException($"Object must be of type {nameof(TaskLimiterConfiguration)}");
    }

    /// <summary> Determines whether <paramref name="left"/> has a lower <see cref="MaximumRate"/> than <paramref name="right"/>. </summary>
    public static bool operator <(TaskLimiterConfiguration left, TaskLimiterConfiguration right)
    {
        return Comparer<TaskLimiterConfiguration>.Default.Compare(left, right) < 0;
    }

    /// <summary> Determines whether <paramref name="left"/> has a higher <see cref="MaximumRate"/> than <paramref name="right"/>. </summary>
    public static bool operator >(TaskLimiterConfiguration left, TaskLimiterConfiguration right)
    {
        return Comparer<TaskLimiterConfiguration>.Default.Compare(left, right) > 0;
    }

    /// <summary> Determines whether <paramref name="left"/> has a lower or equal <see cref="MaximumRate"/> compared to <paramref name="right"/>. </summary>
    public static bool operator <=(TaskLimiterConfiguration left, TaskLimiterConfiguration right)
    {
        return Comparer<TaskLimiterConfiguration>.Default.Compare(left, right) <= 0;
    }

    /// <summary> Determines whether <paramref name="left"/> has a higher or equal <see cref="MaximumRate"/> compared to <paramref name="right"/>. </summary>
    public static bool operator >=(TaskLimiterConfiguration left, TaskLimiterConfiguration right)
    {
        return Comparer<TaskLimiterConfiguration>.Default.Compare(left, right) >= 0;
    }

    #endregion
}