using FluentAssertions;
using FluentAssertions.Execution;
using JikanDotNet.Exceptions;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace JikanDotNet.Tests.ClubTests
{
	public class GetClubStaffAsyncTests
	{
		private readonly IJikan _jikan;

		public GetClubStaffAsyncTests()
		{
			_jikan = new Jikan();
		}

		[Theory]
		[InlineData(long.MinValue)]
		[InlineData(-1)]
		[InlineData(0)]
		public async Task GetClubStaffAsync_InvalidId_ShouldThrowValidationException(long malId)
		{
			// When
			var func = _jikan.Awaiting(x => x.GetClubStaffAsync(malId));

			// Then
			await func.Should().ThrowExactlyAsync<JikanValidationException>();
		}

		[Fact]
		public async Task GetClubStaffAsync_BebopId_ShouldParseCowboyBebopClubStaffList()
		{
			// When
			var club = await _jikan.GetClubStaffAsync(1);

			// Then
			using (new AssertionScope())
			{
				club.Data.Should().NotBeEmpty().And.HaveCount(2);
				club.Data.First().Username.Should().Be("daya");
				club.Data.Last().Username.Should().Be("Xinil");
			}
		}
	}
}