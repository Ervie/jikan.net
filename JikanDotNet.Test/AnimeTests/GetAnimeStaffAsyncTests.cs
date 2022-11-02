using FluentAssertions;
using FluentAssertions.Execution;
using JikanDotNet.Exceptions;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace JikanDotNet.Tests.AnimeTests
{
	public class GetAnimeStaffAsyncTests
	{
		private readonly IJikan _jikan;

		public GetAnimeStaffAsyncTests()
		{
			_jikan = new Jikan();
		}

		[Theory]
		[InlineData(long.MinValue)]
		[InlineData(-1)]
		[InlineData(0)]
		public async Task GetAnimeStaffAsync_InvalidId_ShouldThrowValidationException(long malId)
		{
			// When
			var func = _jikan.Awaiting(x => x.GetAnimeStaffAsync(malId));

			// Then
			await func.Should().ThrowExactlyAsync<JikanValidationException>();
		}

		[Fact]
		public async Task GetAnimeStaffAsync_BebopId_ShouldParseCowboyBebopStaff()
		{
			// When
			var bebop = await _jikan.GetAnimeStaffAsync(1);

			// Then
			bebop.Data.Should().Contain(x => x.Person.Name.Equals("Watanabe, Shinichiro"));
		}

		[Fact]
		public async Task GetAnimeStaffAsync_BebopId_ShouldParseShinichiroWatanabeDetails()
		{
			// When
			var bebop = await _jikan.GetAnimeStaffAsync(1);

			// Then
			var shinichiroWatanabe = bebop.Data.First(x => x.Person.Name.Equals("Watanabe, Shinichiro"));
			using (new AssertionScope())
			{
				shinichiroWatanabe.Position.Should().HaveCount(4);
				shinichiroWatanabe.Position.Should().Contain("Director");
				shinichiroWatanabe.Position.Should().Contain("Script");
				shinichiroWatanabe.Person.Name.Should().Be("Watanabe, Shinichiro");
				shinichiroWatanabe.Person.MalId.Should().Be(2009);
			}
		}

		[Fact]
		public async Task GetAnimeStaffAsync_BebopId_ShouldParseShinichiroWatanabePictures()
		{
			// When
			var bebop = await _jikan.GetAnimeStaffAsync(1);

			// Then
			var shinichiroWatanabe = bebop.Data.First(x => x.Person.Name.Equals("Watanabe, Shinichiro"));
			shinichiroWatanabe.Person.Images.JPG.ImageUrl.Should().NotBeNullOrEmpty();
		}
	}
}