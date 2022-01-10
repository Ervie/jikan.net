using FluentAssertions;
using FluentAssertions.Execution;
using JikanDotNet.Exceptions;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace JikanDotNet.Tests.AnimeTests
{
	public class GetAnimePicturesAsyncTests
	{
		private readonly IJikan _jikan;

		public GetAnimePicturesAsyncTests()
		{
			_jikan = new Jikan();
		}

		[Theory]
		[InlineData(long.MinValue)]
		[InlineData(-1)]
		[InlineData(0)]
		public async Task GetAnimePicturesAsync_InvalidId_ShouldThrowValidationException(long malId)
		{
			// When
			var func = _jikan.Awaiting(x => x.GetAnimePicturesAsync(malId));

			// Then
			await func.Should().ThrowExactlyAsync<JikanValidationException>();
		}

		[Fact]
		public async Task GetAnimePicturesAsync_BebopId_ShouldParseCowboyBebopImages()
		{
			// When
			var bebop = await _jikan.GetAnimePicturesAsync(1);

			// Then
			using var _ = new AssertionScope();
			bebop.Data.Should().HaveCount(13);
			bebop.Data.First().JPG.ImageUrl.Should().NotBeNullOrWhiteSpace();
			bebop.Data.First().JPG.SmallImageUrl.Should().NotBeNullOrWhiteSpace();
			bebop.Data.First().JPG.LargeImageUrl.Should().NotBeNullOrWhiteSpace();
			bebop.Data.First().WebP.ImageUrl.Should().NotBeNullOrWhiteSpace();
			bebop.Data.First().WebP.SmallImageUrl.Should().NotBeNullOrWhiteSpace();
			bebop.Data.First().WebP.LargeImageUrl.Should().NotBeNullOrWhiteSpace();
		}
	}
}