﻿using System;
using FluentAssertions;
using FluentAssertions.Execution;
using JikanDotNet.Exceptions;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace JikanDotNet.Tests.UserTests
{
	public class GetUserMangaListAsyncTests
	{
		private readonly IJikan _jikan;

		public GetUserMangaListAsyncTests()
		{
			_jikan = new Jikan();
		}

		[Fact]
		public async Task GetUserMangaListAsync_OnDefaultEndpoint_ShouldThrowNotSupportedException()
		{
			// When
			var func = _jikan.Awaiting(x => x.GetUserMangaListAsync("Ervelan"));

			// Then
			await func.Should().ThrowExactlyAsync<NotSupportedException>();
		}

		[Theory]
		[InlineData(null)]
		[InlineData("")]
		[InlineData("\n\n\t    \t")]
		public async Task GetUserMangaListAsync_InvalidUsername_ShouldThrowValidationException(string username)
		{
			// When
			var func = _jikan.Awaiting(x => x.GetUserMangaListAsync(username));

			// Then
			await func.Should().ThrowExactlyAsync<JikanValidationException>();
		}

		[Fact]
		public async Task GetUserMangaListAsync_Ervelan_ShouldParseErvelanMangaList()
		{
			// When
			var mangaList = await _jikan.GetUserMangaListAsync("Ervelan");

			// Then
			using (new AssertionScope())
			{
				mangaList.Should().NotBeNull();
				mangaList.Data.Count.Should().BeGreaterThan(90);
				mangaList.Data.Select(x => x.Manga.Title).Should().Contain("Dr. Stone");
			}
		}
		[Fact]
		public async Task GetUserMangaListAsync_ErvelanSecondPage_ShouldParseErvelanMangaList()
		{
			// When
			var mangaList = await _jikan.GetUserMangaListAsync("Ervelan", 2);

			// Then
			using (new AssertionScope())
			{
				mangaList.Should().NotBeNull();
				mangaList.Data.Should().BeEmpty();
			}
		}


		[Fact]
		public void GetUserMangaListAsync_onrix_ShouldParseOnrixMangaList()
		{
			// When
			var action = _jikan.Awaiting(x => x.GetUserMangaListAsync("onrix"));

			// Then
			action.Should().ThrowExactlyAsync<JikanRequestException>();
		}

		[Fact]
		public async Task GetUserMangaListAsync_SonMati_ShouldParseSonMatiMangaList()
		{
			// When
			var mangaList = await _jikan.GetUserMangaListAsync("SonMati");

			// Then
			using (new AssertionScope())
			{
				mangaList.Should().NotBeNull();
				mangaList.Data.Should().HaveCount(300);
			}
		}

		[Fact]
		public async Task GetUserMangaListAsync_MithogawaSecondPage_ShouldParseMithogawaMangaList()
		{
			// When
			var mangaList = await _jikan.GetUserMangaListAsync("Mithogawa", 2);

			// Then
			using (new AssertionScope())
			{
				mangaList.Should().NotBeNull();
				mangaList.Data.Should().HaveCount(300);
				mangaList.Data.Should().NotContain(x => x.Manga.Title.Equals("Baki-dou"));
			}
		}
	}
}
