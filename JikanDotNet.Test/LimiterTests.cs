using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using JikanDotNet.Config;
using Xunit;

namespace JikanDotNet.Tests;

public class LimiterTests
{
    [Fact]
    public async Task GetAnimeAsync_DefaultLimiterWithTwoCalls_ShouldWait()
    {
        // Given
        var jikan = new Jikan();
        
        // When
        var watch = System.Diagnostics.Stopwatch.StartNew();
        await jikan.GetAnimeAsync(1);
        await jikan.GetAnimeAsync(1);
        watch.Stop();

        // Then
        // Usually calls takes around 200-300 ms each, so adding 300 ms wait should go over 700
        watch.ElapsedMilliseconds.Should().BeGreaterThan(700);
    }
    
    [Fact]
    public async Task GetAnimeAsync_NoneLimiter_ShouldNotWait()
    {
        // Given
        var config = new JikanClientConfiguration()
        {
            LimiterConfigurations = TaskLimiterConfiguration.None
        };
        var jikan = new Jikan(config);
        
        // When
        var watch = System.Diagnostics.Stopwatch.StartNew();
        await jikan.GetAnimeAsync(1);
        await jikan.GetAnimeAsync(1);
        watch.Stop();

        // Then
        // Usually calls takes around 200-300 ms each, so adding 300 ms wait should go over 700
        watch.ElapsedMilliseconds.Should().BeLessThan(700);
    }
    
    [Fact]
    public async Task GetAnimeAsync_OneCallPerSecond_ShouldWait()
    {
        // Given
        var config = new JikanClientConfiguration()
        {
            LimiterConfigurations = new List<TaskLimiterConfiguration>()
            {
                new(1, TimeSpan.FromMilliseconds(1000))
            }
        };
        var jikan = new Jikan(config);
        
        // When
        var watch = System.Diagnostics.Stopwatch.StartNew();
        await jikan.GetAnimeAsync(1);
        await jikan.GetAnimeAsync(1);
        watch.Stop();

        // Then
        watch.ElapsedMilliseconds.Should().BeGreaterThan(1000);
    }
    
    [Fact]
    public async Task GetAnimeAsync_OneCallThreeSeconds_ShouldWait()
    {
        // Given
        var config = new JikanClientConfiguration()
        {
            LimiterConfigurations = new List<TaskLimiterConfiguration>()
            {
                new(1, TimeSpan.FromMilliseconds(3000))
            }
        };
        var jikan = new Jikan(config);
        
        // When
        var watch = System.Diagnostics.Stopwatch.StartNew();
        await jikan.GetAnimeAsync(1);
        await jikan.GetAnimeAsync(1);
        watch.Stop();

        // Then
        watch.ElapsedMilliseconds.Should().BeGreaterThan(3000);
    }
    
    [Fact]
    public async Task GetAnimeAsync_OneCallPerSecondAndTwoPerFiveSeconds_ShouldWaitForLonger()
    {
        // Given
        var config = new JikanClientConfiguration()
        {
            LimiterConfigurations = new List<TaskLimiterConfiguration>()
            {
                new(1, TimeSpan.FromMilliseconds(1000)),
                new(2, TimeSpan.FromMilliseconds(5000))
            }
        };
        var jikan = new Jikan(config);
        
        // When
        var watch = System.Diagnostics.Stopwatch.StartNew();
        await jikan.GetAnimeAsync(1);
        await jikan.GetAnimeAsync(1);
        await jikan.GetAnimeAsync(1);
        watch.Stop();

        // Then
        watch.ElapsedMilliseconds.Should().BeGreaterThan(5);
    }
}