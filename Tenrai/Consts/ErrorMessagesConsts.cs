namespace Tenrai.Consts;

internal static class ErrorMessagesConst
{
	public const string FailedRequest = "GET request failed. Status code: {0} Inner message: {1}";
	public const string SerializationFailed = "Serialization failed.";

	/// <summary>Shared guidance appended to unsupported-endpoint messages.</summary>
	private const string OfficialApiPointer = " If you need user-specific data, use the official MyAnimeList API (https://myanimelist.net/apiconfig/references/api/v2).";

	public const string UnsupportedUserData = "Tenrai does not expose user data endpoints." + OfficialApiPointer;
	public const string UnsupportedClub = "Tenrai does not expose club endpoints." + OfficialApiPointer;
	public const string UnsupportedWatch = "Tenrai does not expose watch (popular/recent episodes and promos) endpoints.";
	public const string UnsupportedUserUpdates = "Tenrai does not expose user update endpoints." + OfficialApiPointer;
}