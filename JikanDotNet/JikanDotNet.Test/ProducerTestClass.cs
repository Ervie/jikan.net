using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace JikanDotNet.Tests
{
	public class ProducerTestClass
	{
		private readonly IJikan jikan;

		public ProducerTestClass()
		{
			jikan = new Jikan(true);
		}

		[Fact]
		public void ShouldParseStudioPierrot()
		{
			Producer producer = Task.Run(() => jikan.GetProducer(1)).Result;

			Assert.NotNull(producer);
			Assert.Equal("Studio Pierrot", producer.Metadata.Name);
			Assert.Equal("Tokyo Ghoul", producer.Anime.First().Title);
			Assert.Contains("Black Clover", producer.Anime.Select(x => x.Title));
		}

		[Fact]
		public void ShouldParseStudioPierrotSecondPage()
		{
			Producer producer = Task.Run(() => jikan.GetProducer(1, 2)).Result;

			Assert.NotNull(producer);
			Assert.Contains("Ura Tegamibachi", producer.Anime.Select(x => x.Title));
		}

		[Fact]
		public void ShouldParseKyotoAnimation()
		{
			Producer producer = Task.Run(() => jikan.GetProducer(2)).Result;

			Assert.NotNull(producer);
			Assert.Equal("Kyoto Animation", producer.Metadata.Name);
			Assert.Equal(2, producer.Metadata.MalId);
			Assert.Equal("Clannad", producer.Anime.First().Title);
			Assert.Contains("Violet Evergarden", producer.Anime.Select(x => x.Title));
		}

		[Fact]
		public void ShouldParseBones()
		{
			Producer producer = Task.Run(() => jikan.GetProducer(4)).Result;

			Assert.NotNull(producer);
			Assert.Equal("Bones", producer.Metadata.Name);
			Assert.Equal(4, producer.Metadata.MalId);
			Assert.Equal("Fullmetal Alchemist: Brotherhood", producer.Anime.First().Title);
			Assert.Contains("Soul Eater", producer.Anime.Select(x => x.Title));
		}
	}
}