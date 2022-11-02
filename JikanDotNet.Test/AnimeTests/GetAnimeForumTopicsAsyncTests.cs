using FluentAssertions;
using FluentAssertions.Execution;
using JikanDotNet.Exceptions;
using System.Threading.Tasks;
using Xunit;

namespace JikanDotNet.Tests.AnimeTests
{
	public class GetAnimeForumTopicsAsyncTests
	{
		private readonly IJikan _jikan;

		public GetAnimeForumTopicsAsyncTests()
		{
			_jikan = new Jikan();
		}

		[Theory]
		[InlineData(long.MinValue)]
		[InlineData(-1)]
		[InlineData(0)]
		public async Task GetAnimeForumTopicsAsync_InvalidId_ShouldThrowValidationException(long malId)
		{
			// When
			var func = _jikan.Awaiting(x => x.GetAnimeForumTopicsAsync(malId));

			// Then
			await func.Should().ThrowExactlyAsync<JikanValidationException>();
		}

		[Theory]
		[InlineData((ForumTopicType)int.MaxValue)]
		[InlineData((ForumTopicType)int.MinValue)]
		public async Task GetAnimeForumTopicsAsync_InvalidEnum_ShouldThrowValidationException(ForumTopicType type)
		{
			// When
			var func = _jikan.Awaiting(x => x.GetAnimeForumTopicsAsync(1, type));

			// Then
			await func.Should().ThrowExactlyAsync<JikanValidationException>();
		}

		[Fact]
		public async Task GetAnimeForumTopicsAsync_BebopId_ShouldParseCowboyBebopTopics()
		{
			// When
			var bebop = await _jikan.GetAnimeForumTopicsAsync(1);

			// Then
			bebop.Data.Should().HaveCount(15);
		}

		[Fact]
		public async Task GetAnimeForumTopicsAsync_BebopIdAndTypeEpisode_ShouldParseCowboyBebopTopicsWithEpisodeDiscussionOnly()
		{
			// When
			var bebop = await _jikan.GetAnimeForumTopicsAsync(1, ForumTopicType.Episode);

			// Then
			using var _ = new AssertionScope();
			bebop.Data.Should().HaveCount(15);
			bebop.Data.Should().OnlyContain(topic => topic.Title.Contains("Cowboy Bebop Episode"));
		}

		[Fact]
		public async Task GetAnimeForumTopicsAsync_BebopIdAndTypeOther_ShouldParseCowboyBebopTopicsWithoutEpisodeDiscussion()
		{
			// When
			var bebop = await _jikan.GetAnimeForumTopicsAsync(1, ForumTopicType.Other);

			// Then
			using var _ = new AssertionScope();
			bebop.Data.Should().HaveCount(15);
			bebop.Data.Should().NotContain(topic => topic.Title.Contains("Cowboy Bebop Episode "));
		}
	}
}