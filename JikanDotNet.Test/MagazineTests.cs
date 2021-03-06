﻿using FluentAssertions;
using FluentAssertions.Execution;
using JikanDotNet.Exceptions;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace JikanDotNet.Tests
{
	public class MagazineTests
	{
		private readonly IJikan _jikan;

		public MagazineTests()
		{
			_jikan = new Jikan(true);
		}

		[Theory]
		[InlineData(long.MinValue)]
		[InlineData(-1)]
		[InlineData(0)]
		public async Task GetMagazine_InvalidId_ShouldThrowValidationException(long id)
		{
			// When
			Func<Task<Magazine>> func = _jikan.Awaiting(x => x.GetMagazine(id));

			// Then
			await func.Should().ThrowExactlyAsync<JikanValidationException>();
		}

		[Fact]
		public async Task GetMagazine_BigComicOriginalId_ShouldParseBigComicOriginal()
		{
			// When
			var magazine = await _jikan.GetMagazine(1);

			// Then
			using (new AssertionScope())
			{
				magazine.Should().NotBeNull();
				magazine.Metadata.Name.Should().Be("Big Comic Original");
				magazine.Metadata.MalId.Should().Be(1);
				magazine.MalId.Should().Be(1);
				magazine.Manga.First().Title.Should().Be("Monster");
				magazine.Manga.Select(x => x.Title).Should().Contain("Pluto");
			}
		}

		[Fact]
		public async Task GetMagazine_YoungAnimalId_ShouldParseYoungAnimal()
		{
			// When
			var magazine = await _jikan.GetMagazine(2);

			// Then
			using (new AssertionScope())
			{
				magazine.Should().NotBeNull();
				magazine.Metadata.Name.Should().Be("Young Animal");
				magazine.Manga.First().Title.Should().Be("Berserk");
				magazine.Manga.First().Members.Should().BeGreaterThan(200000);
				magazine.Manga.Select(x => x.Title).Should().Contain("3-gatsu no Lion");
			}
		}

		[Fact]
		public async Task GetMagazine_YoungAnimalIdSecondPage_ShouldParseYoungAnimalSecondPage()
		{
			// When
			var magazine = await _jikan.GetMagazine(2, 2);

			// Then
			using (new AssertionScope())
			{
				magazine.Should().NotBeNull();
				magazine.Manga.Count.Should().BeGreaterThan(10);
			}
		}

		[Fact]
		public async Task GetMagazine_ShonenJumpId_ShouldParseShonenJump()
		{
			// When
			var magazine = await _jikan.GetMagazine(83);

			// Then
			using (new AssertionScope())
			{
				magazine.Should().NotBeNull();
				magazine.Metadata.Name.Should().Be("Shounen Jump (Weekly)");
				magazine.Metadata.MalId.Should().Be(83);
				magazine.Manga.First().Title.Should().Be("One Piece");
				magazine.Manga.First().Members.Should().BeGreaterThan(200000);
				magazine.Manga.Skip(1).First().Title.Should().Be("Naruto");
				magazine.Manga.Select(x => x.Title).Should().Contain("Shaman King");
			}
		}
	}
}