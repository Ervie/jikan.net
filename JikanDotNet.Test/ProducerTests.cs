﻿using FluentAssertions;
using FluentAssertions.Execution;
using JikanDotNet.Exceptions;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace JikanDotNet.Tests
{
	public class ProducerTests
	{
		private readonly IJikan _jikan;

		public ProducerTests()
		{
			_jikan = new Jikan();
		}

		[Theory]
		[InlineData(long.MinValue)]
		[InlineData(-1)]
		[InlineData(0)]
		public async Task GetProducer_InvalidId_ShouldThrowValidationException(long id)
		{
			// When
			Func<Task<Producer>> func = _jikan.Awaiting(x => x.GetProducer(id));

			// Then
			await func.Should().ThrowExactlyAsync<JikanValidationException>();
		}

		[Fact]
		public async Task GetProducer_PierrotId_ShouldParseStudioPierrot()
		{
			// When
			var producer = await _jikan.GetProducer(1);

			// Then
			using (new AssertionScope())
			{
				producer.Should().NotBeNull();
				producer.Metadata.Name.Should().Be("Studio Pierrot");
				producer.MalId.Should().Be(1);
				producer.Metadata.MalId.Should().Be(1);
				producer.Anime.First().Title.Should().Be("Tokyo Ghoul");
				producer.Anime.Select(x => x.Title).Should().Contain("Black Clover");
			}
		}

		[Fact]
		public async Task GetProducer_PierrotIdSecondPage_ShouldParseStudioPierrotSecondPage()
		{
			// When
			var producer = await _jikan.GetProducer(1, 2);

			// Then
			using (new AssertionScope())
			{
				producer.Should().NotBeNull();
				producer.Anime.Select(x => x.Title).Should().Contain("Yuu☆Yuu☆Hakusho: Eizou Hakusho II");
			}
		}

		[Fact]
		public async Task GetProducer_KyoAniId_ShouldParseKyotoAnimation()
		{
			// When
			var producer = await _jikan.GetProducer(2);

			// Then
			using (new AssertionScope())
			{
				producer.Should().NotBeNull();
				producer.Metadata.Name.Should().Be("Kyoto Animation");
				producer.Metadata.MalId.Should().Be(2);
				producer.Anime.Select(x => x.Title).Should().Contain("Clannad");
				producer.Anime.Select(x => x.Title).Should().Contain("Violet Evergarden");
			}
		}

		[Fact]
		public async Task GetProducer_BonesId_ShouldParseBones()
		{
			// When
			var producer = await _jikan.GetProducer(4);

			// Then
			using (new AssertionScope())
			{
				producer.Should().NotBeNull();
				producer.Metadata.Name.Should().Be("Bones");
				producer.Metadata.MalId.Should().Be(4);
				producer.Anime.First().Title.Should().Be("Fullmetal Alchemist: Brotherhood");
				producer.Anime.Select(x => x.Title).Should().Contain("Soul Eater");
			}
		}
	}
}