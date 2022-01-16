using FluentAssertions;
using FluentAssertions.Execution;
using JikanDotNet.Exceptions;
using System.Threading.Tasks;
using Xunit;

namespace JikanDotNet.Tests.PersonTests
{
	public class GetPersonAsyncTests
	{
		private readonly IJikan _jikan;

		public GetPersonAsyncTests()
		{
			_jikan = new Jikan();
		}

		[Theory]
		[InlineData(long.MinValue)]
		[InlineData(-1)]
		[InlineData(0)]
		public async Task GetPersonAsync_InvalidId_ShouldThrowValidationException(long malId)
		{
			// When
			var func = _jikan.Awaiting(x => x.GetPersonAsync(malId));

			// Then
			await func.Should().ThrowExactlyAsync<JikanValidationException>();
		}

		[Theory]
		[InlineData(1)]
		[InlineData(2)]
		[InlineData(3)]
		public async Task GetPersonAsync_CorrectId_ShouldReturnNotNullPerson(long malId)
		{
			// Given
			var returnedPerson = await _jikan.GetPersonAsync(malId);

			// Then
			returnedPerson.Should().NotBeNull();
		}

		[Theory]
		[InlineData(13308)]
		[InlineData(13310)]
		[InlineData(13312)]
		public void GetPersonAsync_WrongId_ShouldReturnNullPerson(long malId)
		{
			// When
			var func = _jikan.Awaiting(x => x.GetPersonAsync(malId));

			// Then
			func.Should().ThrowExactlyAsync<JikanRequestException>();
		}

		[Fact]
		public async Task GetPersonAsync_WakamotoId_ShouldParseNorioWakamoto()
		{
			// Given
			var norioWakamoto = await _jikan.GetPersonAsync(84);

			// Then
			using (new AssertionScope())
			{
				norioWakamoto.Data.Name.Should().Be("Norio Wakamoto");
				norioWakamoto.Data.Birthday.Value.Year.Should().Be(1945);
			}
		}

		//[Fact]
		//public async Task GetPersonAsync_MinoriSuzukiId_ShouldParseMinoriSuzukiRoles()
		//{
		//	// Given
		//	var minoriSuzuki = await _jikan.GetPersonAsync(39460);

		//	// Then
		//	using (new AssertionScope())
		//	{
		//		minoriSuzuki.VoiceActingRoles.Select(x => x.Character.Name).Should().Contain("Wion, Freyja");
		//		minoriSuzuki.VoiceActingRoles.Select(x => x.Anime.Name).Should().Contain("Cardcaptor Sakura: Clear Card-hen");
		//	}
		//}
	}
}
