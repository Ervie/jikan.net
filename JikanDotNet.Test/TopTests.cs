using FluentAssertions;
using FluentAssertions.Execution;
using JikanDotNet.Exceptions;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace JikanDotNet.Tests
{
	public class TopTestss
	{
		private readonly IJikan _jikan;

		public TopTestss()
		{
			_jikan = new Jikan();
		}

		[Fact]
		public async Task GetPeopleTop_NoParameters_ShouldParseKanaHanazawa()
		{
			// When
			var top = await _jikan.GetPeopleTop();

			// Then
			var kana = top.Top.First();
			using (new AssertionScope())
			{
				kana.Name.Should().Be("Hanazawa, Kana");
				kana.NameKanji.Should().Be("花澤 香菜");
				kana.MalId.Should().Be(185);
				kana.Birthday.Value.Year.Should().Be(1989);
				kana.Favorites.Should().BeGreaterThan(60000);
			}
		}

		[Fact]
		public async Task GetPeopleTop_NoParameters_ShouldParseHiroshiKamiya()
		{
			// When
			var top = await _jikan.GetPeopleTop();

			// Then
			var kamiya = top.Top.Skip(1).First();
			using (new AssertionScope())
			{
				kamiya.Name.Should().Be("Kamiya, Hiroshi");
				kamiya.NameKanji.Should().Be("神谷 浩史");
				kamiya.MalId.Should().Be(118);
				kamiya.Birthday.Value.Year.Should().Be(1975);
				kamiya.Favorites.Should().BeGreaterThan(50000);
			}
		}

		[Theory]
		[InlineData(int.MinValue)]
		[InlineData(-1)]
		[InlineData(0)]
		public async Task GetPeopleTop_InvalidPage_ShouldThrowValidationException(int page)
		{
			// When
			Func<Task<PeopleTop>> func = _jikan.Awaiting(x => x.GetPeopleTop(page));

			// Then
			await func.Should().ThrowExactlyAsync<JikanValidationException>();
		}

		[Fact]
		public async Task GetPeopleTop_SecondPage_ShouldFindKentarouMiura()
		{
			// When
			var top = await _jikan.GetPeopleTop(2);

			// Then
			top.Top.Select(x => x.Name).Should().Contain("Miura, Kentarou");
		}

		[Fact]
		public async Task GetCharactersTop_NoParameters_ShouldParseLelouchLamperouge()
		{
			// When
			var top = await _jikan.GetCharactersTop();

			// Then
			var lelouch = top.Top.First();
			using (new AssertionScope())
			{
				lelouch.Name.Should().Be("Lamperouge, Lelouch");
				lelouch.NameKanji.Should().Be("ルルーシュ・ランペルージ");
				lelouch.MalId.Should().Be(417);
				lelouch.Favorites.Should().BeGreaterThan(85000);
			}
		}

		[Fact]
		public async Task GetCharactersTop_NoParameters_ShouldParseLLawliet()
		{
			// When
			var top = await _jikan.GetCharactersTop();

			// Then
			var l = top.Top.Skip(1).First();
			using (new AssertionScope())
			{
				l.Name.Should().Be("Lawliet, L");
				l.NameKanji.Should().Be("エル ローライト");
				l.MalId.Should().Be(71);
				l.Favorites.Should().BeGreaterThan(65000);
			}
		}

		[Fact]
		public async Task GetCharactersTop_NoParameters_ShouldParseLuffyAnimeography()
		{
			// When
			var top = await _jikan.GetCharactersTop();

			// Then
			using (new AssertionScope())
			{
				top.Top.Skip(2).First().Name.Should().Be("Monkey D., Luffy");
				top.Top.Skip(2).First().Animeography.Select(x => x.Name).Should().Contain("One Piece");
			}
		}

		[Theory]
		[InlineData(int.MinValue)]
		[InlineData(-1)]
		[InlineData(0)]
		public async Task GetCharactersTop_InvalidPage_ShouldThrowValidationException(int page)
		{
			// When
			Func<Task<CharactersTop>> func = _jikan.Awaiting(x => x.GetCharactersTop(page));

			// Then
			await func.Should().ThrowExactlyAsync<JikanValidationException>();
		}

		[Fact]
		public async Task GetCharactersTop_SecondPage_ShouldFindTachibanaKanade()
		{
			// When
			var top = await _jikan.GetCharactersTop(2);

			// Then
			top.Top.Select(x => x.Name).Should().Contain("Tachibana, Kanade");
		}
	}
}