using FluentAssertions;
using FluentAssertions.Execution;
using JikanDotNet.Exceptions;
using System.Threading.Tasks;
using Xunit;

namespace JikanDotNet.Tests.MangaTests
{
	public class GetMangaRelationsAsyncTests
	{
		private readonly IJikan _jikan;

		public GetMangaRelationsAsyncTests()
		{
			_jikan = new Jikan();
		}

		[Theory]
		[InlineData(long.MinValue)]
		[InlineData(-1)]
		[InlineData(0)]
		public async Task GetMangaRelationsAsync_InvalidId_ShouldThrowValidationException(long id)
		{
			// When
			var func = _jikan.Awaiting(x => x.GetMangaRelationsAsync(id));

			// Then
			await func.Should().ThrowExactlyAsync<JikanValidationException>();
		}

		[Fact]
		public async Task GetMangaRelationsAsync_MonsterId_ShouldParseMonsterRelations()
		{
			// When
			var monster = await _jikan.GetMangaRelationsAsync(1);

			// Then
			using var _ = new AssertionScope();
			monster.Data.Should().HaveCount(2);
			monster.Data.Should().ContainSingle(x => x.Relation.Equals("Adaptation") && x.Entry.Count == 1);
			monster.Data.Should().ContainSingle(x => x.Relation.Equals("Side story") && x.Entry.Count == 1);
		}
	}
}