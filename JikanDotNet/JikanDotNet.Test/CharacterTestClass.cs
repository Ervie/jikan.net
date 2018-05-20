using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace JikanDotNet.Tests
{
	public class CharacterTestClass
	{
		private readonly IJikan jikan;

		public CharacterTestClass()
		{
			jikan = new Jikan(true);
		}

		[Theory]
		[InlineData(1)]
		[InlineData(2)]
		[InlineData(3)]
		public void ShouldReturnNotNullCharacter(long malId)
		{
			Character returnedCharacter = Task.Run(() => jikan.GetCharacter(malId)).Result;

			Assert.NotNull(returnedCharacter);
		}

		[Theory]
		[InlineData(8)]
		[InlineData(9)]
		[InlineData(10)]
		public void ShouldReturnNullNameCharacter(long malId)
		{
			Character returnedCharacter = Task.Run(() => jikan.GetCharacter(malId)).Result;

			Assert.Null(returnedCharacter.Name);
		}

		[Fact]
		public void ShouldParseIchigoKurosaki()
		{
			Character ichigo = Task.Run(() => jikan.GetCharacter(5)).Result;

			Assert.Equal("Ichigo Kurosaki", ichigo.Name);
		}

		[Fact]
		public void ShouldParseIchigoKurosakiBleach()
		{
			Character ichigo = Task.Run(() => jikan.GetCharacter(5)).Result;

			Assert.Contains("Bleach", ichigo.Animeography.Select(x => x.Name));
			Assert.Contains("Bleach", ichigo.Mangaography.Select(x => x.Name));
		}

		[Fact]
		public void ShouldParseEin()
		{
			Character ein = Task.Run(() => jikan.GetCharacter(4)).Result;

			Assert.Equal("Ein", ein.Name);

			Assert.Equal("Supporting", ein.Animeography.First().Role);
			Assert.Equal("Supporting", ein.Mangaography.First().Role);
			Assert.Equal("Main", ein.Animeography.Last().Role);
		}
	}
}