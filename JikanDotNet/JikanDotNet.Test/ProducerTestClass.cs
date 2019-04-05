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
		public async Task GetProducer_PierrotId_ShouldParseStudioPierrot()
		{
			Producer producer = await jikan.GetProducer(1);

			Assert.NotNull(producer);
			Assert.Equal("Studio Pierrot", producer.Metadata.Name);
			Assert.Equal(1, producer.Metadata.MalId);
			Assert.Equal(1, producer.MalId);
			Assert.Equal("Tokyo Ghoul", producer.Anime.First().Title);
			Assert.Contains("Black Clover", producer.Anime.Select(x => x.Title));
		}

		[Fact]
		public async Task GetProducer_PierrotIdSecondPage_ShouldParseStudioPierrotSecondPage()
		{
			Producer producer = await jikan.GetProducer(1, 2);

			Assert.NotNull(producer);
			Assert.Contains("Super GALS! Kotobuki Ran", producer.Anime.Select(x => x.Title));
		}

		[Fact]
		public async Task GetProducer_KyoAniId_ShouldParseKyotoAnimation()
		{
			Producer producer = await jikan.GetProducer(2);

			Assert.NotNull(producer);
			Assert.Equal("Kyoto Animation", producer.Metadata.Name);
			Assert.Equal(2, producer.Metadata.MalId);
			Assert.Equal("Clannad", producer.Anime.First().Title);
			Assert.Contains("Violet Evergarden", producer.Anime.Select(x => x.Title));
		}

		[Fact]
		public async Task GetProducer_BonesId_ShouldParseBones()
		{
			Producer producer = await jikan.GetProducer(4);

			Assert.NotNull(producer);
			Assert.Equal("Bones", producer.Metadata.Name);
			Assert.Equal(4, producer.Metadata.MalId);
			Assert.Equal("Fullmetal Alchemist: Brotherhood", producer.Anime.First().Title);
			Assert.Contains("Soul Eater", producer.Anime.Select(x => x.Title));
		}
	}
}