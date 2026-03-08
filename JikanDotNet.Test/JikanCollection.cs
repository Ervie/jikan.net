using Xunit;

namespace JikanDotNet.Tests;

[CollectionDefinition("JikanTests")]
public class JikanCollection : ICollectionFixture<JikanFixture>
{
}
