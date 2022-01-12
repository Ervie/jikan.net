using FluentAssertions;
using FluentAssertions.Execution;
using JikanDotNet.Exceptions;
using System.Threading.Tasks;
using Xunit;

namespace JikanDotNet.Tests.CharacterTests
{
	public class GetCharacterPicturesAsyncTests
	{
		private readonly IJikan _jikan;

		public GetCharacterPicturesAsyncTests()
		{
			_jikan = new Jikan();
		}

		[Theory]
		[InlineData(long.MinValue)]
		[InlineData(-1)]
		[InlineData(0)]
		public async Task GetCharacterPicturesAsync_InvalidId_ShouldThrowValidationException(long id)
		{
			// When
			var func = _jikan.Awaiting(x => x.GetCharacterPicturesAsync(id));

			// Then
			await func.Should().ThrowExactlyAsync<JikanValidationException>();
		}

		[Fact]
		public async Task GetCharacterPicturesAsync_SpikeSpiegelId_ShouldParseSpikeSpiegelImages()
		{
			// When
			var spike = await _jikan.GetCharacterPicturesAsync(1);

			// Then
			using var _ = new AssertionScope();
			spike.Data.Should().HaveCount(15);
			spike.Data.Should().OnlyContain(x => !string.IsNullOrWhiteSpace(x.JPG.ImageUrl));
		}

		[Fact]
		public async Task GetCharacterPicturesAsync_SharoId_ShouldParseKirimaSharoImages()
		{
			// When
			var kirimaSharo = await _jikan.GetCharacterPicturesAsync(94947);

			// Then
			using var _ = new AssertionScope();
			kirimaSharo.Data.Should().HaveCount(9);
			kirimaSharo.Data.Should().OnlyContain(x => !string.IsNullOrWhiteSpace(x.JPG.ImageUrl));
		}
	}
}