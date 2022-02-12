using FluentAssertions;
using FluentAssertions.Execution;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace JikanDotNet.Tests.MangaTests
{
	public class GetMangaUserUpdatesAsyncTests
	{
		private readonly IJikan _jikan;

		public GetMangaUserUpdatesAsyncTests()
		{
			_jikan = new Jikan();
		}

		[Fact]
		public async Task GetMangaUserUpdatesAsync_MonsterId_ShouldParseMonsterUserUpdates()
		{
			// When
			var monster = await _jikan.GetMangaUserUpdatesAsync(1);

			// Then
			var firstUpdate = monster.Data.First();
			using (new AssertionScope())
			{
				monster.Data.Should().HaveCount(75);
				firstUpdate.Date.Value.Should().BeBefore(DateTime.Now);
				firstUpdate.ChaptersTotal.Should().Be(162);
			}
		}

		[Fact]
		public async Task GetMangaUserUpdatesAsync_MonsterIdSecondPage_ShouldParseMonsterUserUpdatesPaged()
		{
			// When
			var monster = await _jikan.GetMangaUserUpdatesAsync(1, 2);

			// Then
			var firstUpdate = monster.Data.First();
			using (new AssertionScope())
			{
				monster.Data.Should().HaveCount(75);
				firstUpdate.Date.Value.Should().BeBefore(DateTime.Now);
				firstUpdate.ChaptersTotal.Should().Be(162);
			}
		}
	}
}