using FluentAssertions;
using FluentAssertions.Execution;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace JikanDotNet.Tests
{
	public class ClubTestClass
	{
		private readonly IJikan _jikan;

		public ClubTestClass()
		{
			_jikan = new Jikan(true);
		}

		[Fact]
		public async Task GetClub_BebopId_ShouldParseCowboyBebopClub()
		{
			// When
			var club = await _jikan.GetClub(1);

			// Then
			using (new AssertionScope())
			{
				club.Should().NotBeNull();
				club.Category.Should().Be("Anime");
				club.Type.Should().Be("public");
				club.PicturesCount.Should().Be(25);
				club.MangaRelations.Should().HaveCount(2);
				club.AnimeRelations.First().Name.Should().Be("Cowboy Bebop");
			}
		}

		[Fact]
		public async Task GetClub_AnimeCafeId_ShouldParseAnimeCafeClub()
		{
			// When
			var club = await _jikan.GetClub(73113);

			// Then
			using (new AssertionScope())
			{
				club.Should().NotBeNull();
				club.Category.Should().Be("Anime");
				club.Type.Should().Be("public");
				club.CharacterRelations.Should().BeEmpty();
				club.Staff.First().Name.Should().Be("Fehr");
			}
		}

		[Fact]
		public async Task GetClubMembers_BebopId_ShouldParseCowboyBebopClubMemberList()
		{
			// When
			var club = await _jikan.GetClubMembers(1);

			// Then
			using (new AssertionScope())
			{
				club.Members.Should().NotBeEmpty();
				club.Members.First().Username.Should().Be("-alquimista-");
			}
		}

		[Fact]
		public async Task GetClubMembers_BebopIdSecondPage_ShouldParseCowboyBebopClubMemberListPaged()
		{
			// When
			ClubMembers club = await _jikan.GetClubMembers(1, 2);

			// Then
			using (new AssertionScope())
			{
				club.Members.Should().NotBeEmpty();
				club.Members.Should().HaveCount(36);
			}
		}
	}
}