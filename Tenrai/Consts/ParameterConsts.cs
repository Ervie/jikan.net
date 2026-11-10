namespace Tenrai.Consts;

/// <summary>
/// Constants values related to limit of api.
/// </summary>
public static class ParameterConsts
{
	/// <summary>
	/// Maximum page size for general requests
	/// </summary>
	public const int MaximumPageSize = 100;

	/// <summary>
	/// Page size the API uses when no limit is sent.
	/// </summary>
	public const int DefaultPageSize = 25;

	/// <summary>
	/// Most unique ids one full-data request may name, the path id included.
	/// </summary>
	public const int MaximumFullDataIds = 25;
}