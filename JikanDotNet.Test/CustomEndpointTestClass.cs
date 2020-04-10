using JikanDotNet.Exceptions;
using System;
using System.Threading.Tasks;
using Xunit;

namespace JikanDotNet.Tests
{
	public class CustomEndpointTestClass
	{
		public CustomEndpointTestClass()
		{
		}

		[Fact]
		public async Task JikanConstructor_DefaultUrl_ShouldParseCorrectly()
		{
			IJikan jikan = new Jikan("https://api.jikan.moe/v3/");

			Anime bebop = await jikan.GetAnime(1);

			Assert.Equal(1, bebop.MalId);
		}

		[Fact]
		public async Task JikanConstructor_CustomUrl_ShouldParseCorrectly()
		{
			// TODO: fix endpoint
			//IJikan jikan = new Jikan("https://seiyuu.moe:8000/v3/");

			//Anime bebop = await jikan.GetAnime(1);

			//Assert.Equal(1, bebop.MalId);
		}

		[Fact]
		public async Task JikanConstructor_WrongUrl_ShouldNotParseCorrectly()
		{
			IJikan jikan = new Jikan("http://google.com");

			Anime bebop = await jikan.GetAnime(1);

			Assert.Null(bebop);
		}

		[Fact]
		public async Task JikanConstructor_WrongUrlNoSurpress_ShouldThrowJikanException()
		{
			IJikan jikan = new Jikan("http://google.com", false);

			await Assert.ThrowsAsync<JikanRequestException>(() => jikan.GetAnime(1));
		}

		[Fact]
		public void JikanConstructor_NotAnUrl_ShouldNotParseCorrectly()
		{
			IJikan jikan;

			Assert.Throws<UriFormatException>(() => jikan = new Jikan("Simple String"));
		}

		[Fact]
		public void JikanConstructor_Empty_ShouldNotParseCorrectly()
		{
			IJikan jikan;

			Assert.Throws<UriFormatException>(() => jikan = new Jikan(string.Empty));
		}

		[Fact]
		public async Task JikanConstructorUri_WrongUrlNoSurpress_ShouldThrowJikanException()
		{
			IJikan jikan = new Jikan(new Uri("http://google.com"), false);

			await Assert.ThrowsAsync<JikanRequestException>(() => jikan.GetAnime(1));
		}

		[Fact]
		public void JikanConstructorUri_NotAnUrl_ShouldNotParseCorrectly()
		{
			IJikan jikan;

			Assert.Throws<UriFormatException>(() => jikan = new Jikan(new Uri("Simple String")));
		}

		[Fact]
		public void JikanConstructorUri_Empty_ShouldNotParseCorrectly()
		{
			IJikan jikan;

			Assert.Throws<UriFormatException>(() => jikan = new Jikan(new Uri(string.Empty)));
		}
	}
}
