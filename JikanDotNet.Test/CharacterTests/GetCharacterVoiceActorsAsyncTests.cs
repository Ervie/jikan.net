using FluentAssertions;
using FluentAssertions.Execution;
using JikanDotNet.Exceptions;
using System.Threading.Tasks;
using Xunit;

namespace JikanDotNet.Tests.CharacterTests
{
	public class GetCharacterVoiceActorsAsyncTests
	{
		private readonly IJikan _jikan;

		public GetCharacterVoiceActorsAsyncTests()
		{
			_jikan = new Jikan();
		}

		[Theory]
		[InlineData(long.MinValue)]
		[InlineData(-1)]
		[InlineData(0)]
		public async Task GetCharacterVoiceActorsAsync_InvalidId_ShouldThrowValidationException(long malId)
		{
			// When
			var func = _jikan.Awaiting(x => x.GetCharacterVoiceActorsAsync(malId));

			// Then
			await func.Should().ThrowExactlyAsync<JikanValidationException>();
		}

		[Fact]
		public async Task GetCharacterVoiceActorsAsync_SpikeSpiegelId_ShouldParseSpikeSpiegelVoiceActors()
		{
			// When
			var spike = await _jikan.GetCharacterVoiceActorsAsync(1);

			// Then
			using var _ = new AssertionScope();
			spike.Data.Should().HaveCount(13);
			spike.Data.Should().Contain(x => x.Language.Equals("Japanese") && x.Person.Name.Equals("Yamadera, Kouichi"));
			spike.Data.Should().Contain(x => x.Language.Equals("English") && x.Person.Name.Equals("Blum, Steven"));
			spike.Data.Should().Contain(x => x.Language.Equals("German") && x.Person.Name.Equals("Neumann, Viktor"));
		}

		[Fact]
		public async Task GetCharacterVoiceActorsAsync_FayeValentinelId_ShouldParseFayeValentineVoiceActors()
		{
			// When
			var faye = await _jikan.GetCharacterVoiceActorsAsync(2);

			// Then
			using (new AssertionScope())
			{
				faye.Data.Should().HaveCount(12);
				faye.Data.Should().Contain(x => x.Language.Equals("Japanese") && x.Person.Name.Equals("Hayashibara, Megumi"));
				faye.Data.Should().Contain(x => x.Language.Equals("English") && x.Person.Name.Equals("Lee, Wendee"));
			}
		}
	}
}
