using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace JikanDotNet.Tests
{
	public class PersonTestClass
	{
		private readonly IJikan jikan;

		public PersonTestClass()
		{
			jikan = new Jikan(true);
		}

		[Theory]
		[InlineData(1)]
		[InlineData(2)]
		[InlineData(3)]
		public async Task GetPerson_CorrectId_ShouldReturnNotNullPerson(long malId)
		{
			Person returnedPerson = await jikan.GetPerson(malId);

			Assert.NotNull(returnedPerson);
		}

		[Theory]
		[InlineData(13308)]
		[InlineData(13310)]
		[InlineData(13312)]
		public async Task GetPerson_WrongId_ShouldReturnNullPerson(long malId)
		{
			Person returnedPerson = await jikan.GetPerson(malId);

			Assert.Null(returnedPerson);
		}

		[Fact]
		public async Task GetPerson_WakamotoId_ShouldParseNorioWakamoto()
		{
			Person norioWakamoto = await jikan.GetPerson(84);

			Assert.Equal("Norio Wakamoto", norioWakamoto.Name);
			Assert.Equal(1945, norioWakamoto.Birthday.Value.Year);
		}

		[Fact]
		public async Task GetPerson_MinoriSuzukiId_ShouldParseMinoriSuzukiRoles()
		{
			Person minoriSuzuki = await jikan.GetPerson(39460);

			Assert.Contains("Wion, Freyja", minoriSuzuki.VoiceActingRoles.Select(x => x.Character.Name));
			Assert.Contains("Cardcaptor Sakura: Clear Card-hen", minoriSuzuki.VoiceActingRoles.Select(x => x.Anime.Name));
		}
	}
}