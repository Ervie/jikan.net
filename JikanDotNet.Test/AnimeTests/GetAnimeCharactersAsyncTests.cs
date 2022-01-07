using FluentAssertions;
using FluentAssertions.Execution;
using JikanDotNet.Exceptions;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace JikanDotNet.Tests.AnimeTests
{
	public class GetAnimeCharactersAsyncTests
	{
		private readonly IJikan _jikan;

		public GetAnimeCharactersAsyncTests()
		{
			_jikan = new Jikan();
		}

		[Theory]
		[InlineData(long.MinValue)]
		[InlineData(-1)]
		[InlineData(0)]
		public async Task GetAnimeCharactersStaffAsync_InvalidId_ShouldThrowValidationException(long malId)
		{
			// When
			Func<Task<AnimeCharactersStaff>> func = _jikan.Awaiting(x => x.GetAnimeCharactersStaffAsync(malId));

			// Then
			await func.Should().ThrowExactlyAsync<JikanValidationException>();
		}

		[Fact]
		public async Task GetAnimeCharactersStaffAsync_BebopId_ShouldParseCowboyBebopCharactersAndStaff()
		{
			// When
			var bebop = await _jikan.GetAnimeCharactersStaffAsync(1);

			// Then
			using (new AssertionScope())
			{
				bebop.Characters.Should().Contain(x => x.Name.Equals("Black, Jet"));
				bebop.Staff.Where(x => x.Role.Contains("Director") && x.Role.Contains("Script")).Select(x => x.Name).Should().Contain("Watanabe, Shinichiro");
			}
		}

		[Fact]
		public async Task GetAnimeCharactersStaffAsync_BebopId_ShouldParseJetBlackPictures()
		{
			// When
			var bebop = await _jikan.GetAnimeCharactersStaffAsync(1);

			// Then
			var jetBlack = bebop.Characters.First(x => x.Name.Equals("Black, Jet"));
			using (new AssertionScope())
			{
				jetBlack.ImageURL.SmallImageUrl.Should().NotBeNullOrEmpty();
				jetBlack.ImageURL.ImageUrl.Should().NotBeNullOrEmpty();
			}
		}

		[Fact]
		public async Task GetAnimeCharactersStaffAsync_BebopId_ShouldParseShinichiroWatanabePictures()
		{
			// When
			var bebop = await _jikan.GetAnimeCharactersStaffAsync(1);

			// Then
			var shinichiroWatanabe = bebop.Staff.First(x => x.Name.Equals("Watanabe, Shinichiro"));
			using (new AssertionScope())
			{
				shinichiroWatanabe.ImageURL.SmallImageUrl.Should().NotBeNullOrEmpty();
				shinichiroWatanabe.ImageURL.ImageUrl.Should().NotBeNullOrEmpty();
			}
		}
	}
}