using FluentAssertions;
using FluentAssertions.Execution;
using JikanDotNet.Exceptions;
using System.Threading.Tasks;
using Xunit;

namespace JikanDotNet.Tests.ClubTests
{
	public class GetClubAsyncTests
	{
		private readonly IJikan _jikan;

		public GetClubAsyncTests()
		{
			_jikan = new Jikan();
		}

		[Theory]
		[InlineData(long.MinValue)]
		[InlineData(-1)]
		[InlineData(0)]
		public async Task GetClubAsync_InvalidId_ShouldThrowValidationException(long malId)
		{
			// When
			var func = _jikan.Awaiting(x => x.GetClubAsync(malId));

			// Then
			await func.Should().ThrowExactlyAsync<JikanValidationException>();
		}

		[Fact]
		public async Task GetClubAsync_BebopId_ShouldParseCowboyBebopClub()
		{
			// When
			var club = await _jikan.GetClubAsync(1);

			// Then
			using (new AssertionScope())
			{
				club.Should().NotBeNull();
				club.Data.Category.Should().Be("anime");
				club.Data.Name.Should().Be("Cowboy Bebop");
				club.Data.Access.Should().Be("public");
				club.Data.Created.Should().BeSameDateAs(System.DateTime.Parse("2007-03-29"));
			}
		}

		[Fact]
		public async Task GetClubAsync_AnimeCafeId_ShouldParseAnimeCafeClub()
		{
			// When
			var club = await _jikan.GetClubAsync(73113);

			// Then
			using (new AssertionScope())
			{
				club.Should().NotBeNull();
				club.Data.Category.Should().Be("anime");
				club.Data.Access.Should().Be("public");
			}
		}
	}
}