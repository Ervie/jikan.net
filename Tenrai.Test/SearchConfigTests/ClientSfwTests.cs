using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Tenrai.Config;
using Xunit;

namespace Tenrai.Tests.SearchConfigTests
{
	public class ClientSfwTests
	{
		[Fact]
		public async Task ClientSfwOn_PlainSeasonCall_SendsSfw()
		{
			var (client, handler) = CreateClient(sfw: true);

			await client.GetCurrentSeasonAsync();

			handler.LastQuery.Should().Be("?sfw=true");
		}

		[Fact]
		public async Task ClientSfwOn_ConfigTurnsItOff_SendsNoSfw()
		{
			var (client, handler) = CreateClient(sfw: true);

			await client.SearchAnimeAsync(new AnimeSearchConfig { Query = "berserk", Sfw = false });

			handler.LastQuery.Should().Be("?q=berserk");
		}

		[Fact]
		public async Task ClientSfwOff_ConfigTurnsItOn_SendsSfw()
		{
			var (client, handler) = CreateClient(sfw: false);

			await client.SearchAnimeAsync(new AnimeSearchConfig { Query = "berserk", Sfw = true });

			handler.LastQuery.Should().Be("?q=berserk&sfw=true");
		}

		[Fact]
		public async Task ClientSfwOff_PlainCall_SendsNoSfw()
		{
			var (client, handler) = CreateClient(sfw: false);

			await client.SearchAnimeAsync("berserk");

			handler.LastQuery.Should().Be("?q=berserk");
		}

		[Fact]
		public async Task ClientSfwOn_ConfigWithoutSfwParam_SendsNoSfw()
		{
			var (client, handler) = CreateClient(sfw: true);

			await client.GetAnimeForumTopicsAsync(1, new AnimeForumSearchConfig { Page = 2 });

			handler.LastQuery.Should().Be("?page=2");
		}

		[Fact]
		public async Task ClientSfwOn_PlainSubList_SendsSfw()
		{
			var (client, handler) = CreateClient(sfw: true);

			await client.GetPersonVoiceActingRolesAsync(1);

			handler.LastQuery.Should().Be("?sfw=true");
		}

		[Fact]
		public async Task ClientSfwOn_SubListConfigTurnsItOff_SendsNoSfw()
		{
			var (client, handler) = CreateClient(sfw: true);

			await client.GetPersonVoiceActingRolesAsync(1, new SfwConfig { Sfw = false });

			handler.LastQuery.Should().BeEmpty();
		}

		[Fact]
		public async Task ClientSfwOn_PlainInterestStack_SendsSfw()
		{
			var (client, handler) = CreateClient(sfw: true);
			handler.Body = "{\"data\":null}";

			await client.GetInterestStackAsync(1);

			handler.LastQuery.Should().Be("?sfw=true");
		}

		[Fact]
		public async Task ClientSfwOn_AnimeIdEntries_SendsSfwAndInclude()
		{
			var (client, handler) = CreateClient(sfw: true);

			await client.GetAnimeIdEntriesAsync(new AnimeIdsConfig { Types = { AnimeType.Movie } });

			handler.LastQuery.Should().Be("?type=Movie&sfw=true&include=title,type");
		}

		private static (TenraiClient Client, RecordingHandler Handler) CreateClient(bool sfw)
		{
			var handler = new RecordingHandler();
			var httpClient = new HttpClient(handler) { BaseAddress = new Uri("https://api.tenrai.org/v1/") };
			var client = new TenraiClient(new TenraiClientConfiguration { Sfw = sfw, LimiterConfigurations = TaskLimiterConfiguration.None }, httpClient);
			return (client, handler);
		}

		private sealed class RecordingHandler : HttpMessageHandler
		{
			public string LastQuery { get; private set; }

			public string Body { get; set; } = "{\"pagination\":{\"last_visible_page\":1,\"has_next_page\":false},\"data\":[]}";

			protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
			{
				LastQuery = request.RequestUri.Query;
				return Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(Body, Encoding.UTF8, "application/json") });
			}
		}
	}
}
