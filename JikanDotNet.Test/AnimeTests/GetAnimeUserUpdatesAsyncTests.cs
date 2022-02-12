using FluentAssertions;
using FluentAssertions.Execution;
using JikanDotNet.Exceptions;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace JikanDotNet.Tests.AnimeTests
{
	public class GetAnimeUserUpdatesAsyncTests
	{
		private readonly IJikan _jikan;

		public GetAnimeUserUpdatesAsyncTests()
		{
			_jikan = new Jikan();
		}


		[Theory]
		[InlineData(long.MinValue)]
		[InlineData(-1)]
		[InlineData(0)]
		public async Task GetAnimeUserUpdatesAsync_InvalidId_ShouldThrowValidationException(long malId)
		{
			// When
			var func = _jikan.Awaiting(x => x.GetAnimeUserUpdatesAsync(malId));

			// Then
			await func.Should().ThrowExactlyAsync<JikanValidationException>();
		}

		[Fact]
		public async Task GetAnimeUserUpdatesAsync_BebopId_ShouldParseCowboyBebopUserUpdates()
		{
			// When
			var bebop = await _jikan.GetAnimeUserUpdatesAsync(1);

			// Then
			var firstUpdate = bebop.Data.First();
			using (new AssertionScope())
			{
				bebop.Data.Should().HaveCount(75);
				firstUpdate.Date.Value.Should().BeBefore(DateTime.Now);
				firstUpdate.User.Should().NotBeNull();
				firstUpdate.User.Username.Should().NotBeNullOrWhiteSpace();
				firstUpdate.User.Url.Should().NotBeNullOrWhiteSpace();
			}
		}

		[Theory]
		[InlineData(long.MinValue)]
		[InlineData(-1)]
		[InlineData(0)]
		public async Task GetAnimeUserUpdatesAsync_SecondPageInvalidId_ShouldThrowValidationException(long malId)
		{
			// When
			var func = _jikan.Awaiting(x => x.GetAnimeUserUpdatesAsync(malId, 2));

			// Then
			await func.Should().ThrowExactlyAsync<JikanValidationException>();
		}

		[Theory]
		[InlineData(int.MinValue)]
		[InlineData(-1)]
		[InlineData(0)]
		public async Task GetAnimeUserUpdatesAsync_ValidIdInvalidpage_ShouldThrowValidationException(int page)
		{
			// When
			var func = _jikan.Awaiting(x => x.GetAnimeUserUpdatesAsync(1, page));

			// Then
			await func.Should().ThrowExactlyAsync<JikanValidationException>();
		}

		[Fact]
		public async Task GetAnimeUserUpdatesAsync_BebopIdSecondPage_ShouldParseCowboyBebopUserUpdatesPaged()
		{
			// When
			var bebop = await _jikan.GetAnimeUserUpdatesAsync(1, 2);

			// Then
			var firstUpdate = bebop.Data.First();
			using (new AssertionScope())
			{
				bebop.Data.Should().HaveCount(75);
				firstUpdate.EpisodesTotal.Should().HaveValue().And.Be(26);
				firstUpdate.User.Should().NotBeNull();
				firstUpdate.User.Username.Should().NotBeNullOrWhiteSpace();
				firstUpdate.User.Url.Should().NotBeNullOrWhiteSpace();
			}
		}
	}
}
