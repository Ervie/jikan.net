using FluentAssertions;
using FluentAssertions.Execution;
using JikanDotNet.Exceptions;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace JikanDotNet.Tests.MangaTests
{
	public class GetMangaForumTopicsAsyncTests
	{
		private readonly IJikan _jikan;

		public GetMangaForumTopicsAsyncTests()
		{
			_jikan = new Jikan();
		}

		[Theory]
		[InlineData(long.MinValue)]
		[InlineData(-1)]
		[InlineData(0)]
		public async Task GetMangaForumTopicsAsync_InvalidId_ShouldThrowValidationException(long malId)
		{
			// When
			var func = _jikan.Awaiting(x => x.GetMangaForumTopicsAsync(malId));

			// Then
			await func.Should().ThrowExactlyAsync<JikanValidationException>();
		}

		[Fact]
		public async Task GetMangaForumTopicsAsync_MonsterId_ShouldParseMonsterTopics()
		{
			// When
			var monster = await _jikan.GetMangaForumTopicsAsync(1);

			// Then
			using var _ = new AssertionScope();
			monster.Data.Should().Contain(x => x.Title.StartsWith("Monster Chapter"));
			monster.Data.Should().HaveCount(15);
		}

		[Fact]
		public async Task GetMangaForumTopics_MonsterId_ShouldParseMonsterTopicsIds()
		{
			// When
			var monster = await _jikan.GetMangaForumTopicsAsync(1);

			// Then
			var topics = monster.Data.Select(x => x.MalId);
			using (new AssertionScope())
			{
				topics.Should().Contain(395611);
				topics.Should().Contain(57668);
			}
		}
	}
}