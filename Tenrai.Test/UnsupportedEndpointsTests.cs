using System;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

// The endpoints exercised here are intentionally obsolete: Tenrai v1 does not implement them
#pragma warning disable CS0618

namespace Tenrai.Tests;

/// <summary>
/// Verifies that endpoint families Tenrai does not implement (users, clubs, watch, forum topics,
/// user updates) are marked <see cref="ObsoleteAttribute"/> and throw <see cref="NotSupportedException"/>
/// at runtime instead of issuing a request that would 404. The calls throw synchronously, so they are
/// asserted as actions rather than awaited.
/// </summary>
public class UnsupportedEndpointsTests
{
	private readonly ITenrai _tenrai = new TenraiClient();

	[Fact]
	public void UserEndpoints_ShouldThrowNotSupported()
	{
		using (new AssertionScope())
		{
			FluentActions.Invoking(() => { _ = _tenrai.GetUserProfileAsync("Ervelan"); }).Should().Throw<NotSupportedException>();
			FluentActions.Invoking(() => { _ = _tenrai.GetUserByIdAsync(1); }).Should().Throw<NotSupportedException>();
			FluentActions.Invoking(() => { _ = _tenrai.GetUserReviewsAsync("Ervelan"); }).Should().Throw<NotSupportedException>();
			FluentActions.Invoking(() => { _ = _tenrai.GetUserFullDataAsync("Ervelan"); }).Should().Throw<NotSupportedException>();
			FluentActions.Invoking(() => { _ = _tenrai.SearchUserAsync("Ervelan"); }).Should().Throw<NotSupportedException>();
			FluentActions.Invoking(() => { _ = _tenrai.GetRandomUserAsync(); }).Should().Throw<NotSupportedException>();
		}
	}

	[Fact]
	public void ClubEndpoints_ShouldThrowNotSupported()
	{
		using (new AssertionScope())
		{
			FluentActions.Invoking(() => { _ = _tenrai.GetClubAsync(1); }).Should().Throw<NotSupportedException>();
			FluentActions.Invoking(() => { _ = _tenrai.GetClubMembersAsync(1); }).Should().Throw<NotSupportedException>();
			FluentActions.Invoking(() => { _ = _tenrai.SearchClubAsync("test"); }).Should().Throw<NotSupportedException>();
		}
	}

	[Fact]
	public void WatchEndpoints_ShouldThrowNotSupported()
	{
		using (new AssertionScope())
		{
			FluentActions.Invoking(() => { _ = _tenrai.GetWatchRecentEpisodesAsync(); }).Should().Throw<NotSupportedException>();
			FluentActions.Invoking(() => { _ = _tenrai.GetWatchPopularEpisodesAsync(); }).Should().Throw<NotSupportedException>();
			FluentActions.Invoking(() => { _ = _tenrai.GetWatchRecentPromosAsync(); }).Should().Throw<NotSupportedException>();
			FluentActions.Invoking(() => { _ = _tenrai.GetWatchPopularPromosAsync(); }).Should().Throw<NotSupportedException>();
		}
	}

	[Fact]
	public void UserUpdateEndpoints_ShouldThrowNotSupported()
	{
		using (new AssertionScope())
		{
			FluentActions.Invoking(() => { _ = _tenrai.GetAnimeUserUpdatesAsync(1); }).Should().Throw<NotSupportedException>();
			FluentActions.Invoking(() => { _ = _tenrai.GetMangaUserUpdatesAsync(1); }).Should().Throw<NotSupportedException>();
		}
	}
}

#pragma warning restore CS0618
