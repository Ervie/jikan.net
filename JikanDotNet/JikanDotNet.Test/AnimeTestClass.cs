using System;
using System.Threading.Tasks;
using Xunit;

namespace JikanDotNet.Test
{
    public class AnimeTestClass
    {
		private readonly IJikan jikan;

		public AnimeTestClass()
		{
			jikan = new Jikan(true);
		}

		[Theory]
		[InlineData(1)]
		[InlineData(5)]
		[InlineData(6)]
		public void ShouldReturnNotNullAnime(long malId)
        {
			Anime returnedAnime = Task.Run(() => jikan.GetAnime(malId)).Result;

			Assert.NotNull(returnedAnime);
        }

		[Theory]
		[InlineData(2)]
		[InlineData(3)]
		[InlineData(4)]
		public void ShouldReturnNullAnime(long malId)
		{
			Anime returnedAnime = Task.Run(() => jikan.GetAnime(malId)).Result;

			Assert.Null(returnedAnime);
		}
	}
}
