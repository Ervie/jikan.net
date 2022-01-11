using FluentAssertions;
using JikanDotNet.Exceptions;
using System.Threading.Tasks;
using Xunit;

namespace JikanDotNet.Tests.AnimeTests
{
	public class GetAnimeRelationsAsyncTests
	{
		private readonly IJikan _jikan;

		public GetAnimeRelationsAsyncTests()
		{
			_jikan = new Jikan();
		}

		[Theory]
		[InlineData(long.MinValue)]
		[InlineData(-1)]
		[InlineData(0)]
		public async Task GetAnimeRelationsAsync_InvalidId_ShouldThrowValidationException(long malId)
		{
			// When
			var func = _jikan.Awaiting(x => x.GetAnimeRelationsAsync(malId));

			// Then
			await func.Should().ThrowExactlyAsync<JikanValidationException>();
		}


		[Fact]
		public async Task GetAnimeRelationsAsync_BebopId_ShouldParseCowboyBebopRelations()
		{
			// When
			var bebop = await _jikan.GetAnimeRelationsAsync(1);

			// Then
			bebop.Data.Should().HaveCount(3);
			bebop.Data.Should().ContainSingle(x => x.Relation.Equals("Adaptation") && x.Entry.Count == 2);
			bebop.Data.Should().ContainSingle(x => x.Relation.Equals("Side story") && x.Entry.Count == 2);
			bebop.Data.Should().ContainSingle(x => x.Relation.Equals("Summary") && x.Entry.Count == 1);
		}
	}
}