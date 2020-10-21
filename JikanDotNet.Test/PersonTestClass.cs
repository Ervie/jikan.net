using FluentAssertions;
using FluentAssertions.Execution;
using JikanDotNet.Exceptions;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace JikanDotNet.Tests
{
	public class PersonTestClass
	{
		private readonly IJikan _jikan;

		public PersonTestClass()
		{
			_jikan = new Jikan(true);
		}

		[Theory]
		[InlineData(1)]
		[InlineData(2)]
		[InlineData(3)]
		public async Task GetPerson_CorrectId_ShouldReturnNotNullPerson(long malId)
		{
			// Given
			var returnedPerson = await _jikan.GetPerson(malId);

			// Then
			returnedPerson.Should().NotBeNull();
		}

		[Theory]
		[InlineData(13308)]
		[InlineData(13310)]
		[InlineData(13312)]
		public void GetPerson_WrongId_ShouldReturnNullPerson(long malId)
		{
			// When
			Func<Task<Person>> func = _jikan.Awaiting(x => x.GetPerson(malId));

			// Then
			func.Should().ThrowExactlyAsync<JikanRequestException>();
		}

		[Fact]
		public async Task GetPerson_WakamotoId_ShouldParseNorioWakamoto()
		{
			// Given
			var norioWakamoto = await _jikan.GetPerson(84);

			// Then
			using (new AssertionScope())
			{
				norioWakamoto.Name.Should().Be("Norio Wakamoto");
				norioWakamoto.Birthday.Value.Year.Should().Be(1945);
			}
		}

		[Fact]
		public async Task GetPerson_MinoriSuzukiId_ShouldParseMinoriSuzukiRoles()
		{
			// Given
			var minoriSuzuki = await _jikan.GetPerson(39460);

			// Then
			using (new AssertionScope())
			{
				minoriSuzuki.VoiceActingRoles.Select(x => x.Character.Name).Should().Contain("Wion, Freyja");
				minoriSuzuki.VoiceActingRoles.Select(x => x.Anime.Name).Should().Contain("Cardcaptor Sakura: Clear Card-hen");
			}
		}
	}
}