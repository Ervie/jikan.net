using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace JikanDotNet.Tests
{
	public class MetadataTestClass
	{
		private readonly IJikan jikan;

		public MetadataTestClass()
		{
			jikan = new Jikan(true);
		}

		[Fact]
		public async Task GetStatusMetadata_NoParameter_ShouldParseStatusMetadata()
		{
			StatusMetadata statusMetadata = await jikan.GetStatusMetadata();

			Assert.NotNull(statusMetadata);
			Assert.True(Int32.Parse(statusMetadata.TotalConnectionsReceived) > 10000000);
		}
	}
}