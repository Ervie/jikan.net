using FluentAssertions;
using JikanDotNet.Exceptions;
using System;
using System.Threading.Tasks;
using Xunit;

namespace JikanDotNet.Tests
{
	public class CustomEndpointTestClass
	{
		[Fact]
		public async Task JikanConstructor_DefaultUrl_ShouldParseCorrectly()
		{
			// Given
			IJikan jikan = new Jikan("https://api.jikan.moe/v3/");

			// When
			Anime bebop = await jikan.GetAnime(1);

			// Then
			bebop.MalId.Should().Be(1);
		}

		[Fact]
		public async Task JikanConstructor_CustomUrl_ShouldParseCorrectly()
		{
			// Given
			IJikan jikan = new Jikan("https://seiyuu.moe:8000/v3/");

			// When
			Anime bebop = await jikan.GetAnime(1);

			// Then
			bebop.MalId.Should().Be(1);
		}

		[Fact]
		public void JikanConstructor_WrongUrl_ShouldNotParseCorrectly()
		{
			// When
			IJikan jikan = new Jikan("http://google.com");

			// When
			Func<Task<Anime>> func = jikan.Awaiting(x => x.GetAnime(1));

			// Then
			func.Should().ThrowExactlyAsync<JikanRequestException>();
		}

		[Fact]
		public void JikanConstructor_WrongUrlNoSurpress_ShouldThrowJikanException()
		{
			// When
			IJikan jikan = new Jikan("http://google.com", false);

			// When
			Func<Task<Anime>> func = jikan.Awaiting(x => x.GetAnime(1));

			// Then
			func.Should().ThrowExactlyAsync<JikanRequestException>();
		}

		[Fact]
		public void JikanConstructor_NotAnUrl_ShouldNotParseCorrectly()
		{
			// When
			Func<Jikan> func = () => new Jikan("Simple String", false);

			// Then
			func.Should().ThrowExactly<UriFormatException>();
		}

		[Fact]
		public void JikanConstructor_Empty_ShouldNotParseCorrectly()
		{
			// When
			Func<Jikan> func = () => new Jikan(string.Empty, false);

			// Then
			func.Should().ThrowExactly<UriFormatException>();
		}

		[Fact]
		public void JikanConstructorUri_WrongUrlNoSurpress_ShouldThrowJikanException()
		{
			// Given
			var jikan = new Jikan(new Uri("http://google.com"), false);

			// When
			Func<Task<Anime>> func = jikan.Awaiting(x => x.GetAnime(1));

			// Then
			func.Should().ThrowExactlyAsync<JikanRequestException>();
		}

		[Fact]
		public void JikanConstructorUri_NotAnUrl_ShouldNotParseCorrectly()
		{
			// When
			Func<Jikan> func = () => new Jikan(new Uri("Simple String"), false);

			// Then
			func.Should().ThrowExactly<UriFormatException>();
		}

		[Fact]
		public void JikanConstructorUri_Empty_ShouldNotParseCorrectly()
		{
			// When
			Func<Jikan> func = () => new Jikan(new Uri(string.Empty), false);

			// Then
			func.Should().ThrowExactly<UriFormatException>();
		}
	}
}
