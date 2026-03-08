using FluentAssertions;
using FluentAssertions.Execution;
using JikanDotNet.Exceptions;
using System.Threading.Tasks;
using Xunit;

namespace JikanDotNet.Tests.PersonTests
{
	[Collection("JikanTests")]
	public class GetPersonPicturesAsyncTests
	{
		private readonly IJikan _jikan;

		public GetPersonPicturesAsyncTests(JikanFixture jikanFixture)
		{
			_jikan = jikanFixture.Jikan;
		}

		[Theory]
		[InlineData(long.MinValue)]
		[InlineData(-1)]
		[InlineData(0)]
		public async Task GetPersonPicturesAsync_InvalidId_ShouldThrowValidationException(long malId)
		{
			// When
			var func = _jikan.Awaiting(x => x.GetPersonPicturesAsync(malId));

			// Then
			await func.Should().ThrowExactlyAsync<JikanValidationException>();
		}

		[Fact]
		public async Task GetPersonPicturesAsync_WakamotoId_ShouldParseNorioWakamotoImages()
		{
			// Given
			var norioWakamoto = await _jikan.GetPersonPicturesAsync(84);

			// Then
			norioWakamoto.Data.Should().HaveCountGreaterOrEqualTo(4);
		}

		[Fact]
		public async Task GetPersonPicturesAsync_SugitaId_ShouldParseSugitaTomokazuImages()
		{
			// Given
			var norioWakamoto = await _jikan.GetPersonPicturesAsync(2);

			// Then
			norioWakamoto.Data.Should().HaveCountGreaterOrEqualTo(8);
		}
	}
}
