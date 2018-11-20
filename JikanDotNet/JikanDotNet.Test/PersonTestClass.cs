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
		public void ShouldReturnNotNullPerson(long malId)
		{
			Person returnedPerson = Task.Run(() => jikan.GetPerson(malId)).Result;

			Assert.NotNull(returnedPerson);
		}

		[Theory]
		[InlineData(13308)]
		[InlineData(13310)]
		[InlineData(13312)]
		public void ShouldReturnNullPerson(long malId)
		{
			Person returnedPerson = Task.Run(() => jikan.GetPerson(malId)).Result;

			Assert.Null(returnedPerson);
		}

		[Fact]
		public void ShouldParseNorioWakamoto()
		{
			Person norioWakamoto = Task.Run(() => jikan.GetPerson(84)).Result;

			Assert.Equal("Norio Wakamoto", norioWakamoto.Name);
			Assert.Equal(1945, norioWakamoto.Birthday.Value.Year);
		}

		[Fact]
		public void ShouldParseMinoriSuzukiRoles()
		{
			Person minoriSuzuki = Task.Run(() => jikan.GetPerson(39460)).Result;

			Assert.Contains("Wion, Freyja", minoriSuzuki.VoiceActingRoles.Select(x => x.Character.Name));
			Assert.Contains("Cardcaptor Sakura: Clear Card-hen", minoriSuzuki.VoiceActingRoles.Select(x => x.Anime.Name));
		}
	}
}