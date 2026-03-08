namespace JikanDotNet.Tests;

public class JikanFixture
{
	public IJikan Jikan { get; }

	public JikanFixture()
	{
		Jikan = new Jikan();
	}
}
