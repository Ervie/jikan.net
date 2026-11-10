using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Tenrai.Consts;

namespace Tenrai;

public partial interface ITenrai
{
	/// <summary>
	/// Return club's profile information.
	/// </summary>
	/// <param name="id">MAL id of the club.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>Club's profile information.</returns>
	[Obsolete(ErrorMessagesConst.UnsupportedClub)]
	Task<BaseTenraiResponse<Club>> GetClubAsync(long id, CancellationToken cancellationToken = default);

	/// <summary>
	/// Return club's member list.
	/// </summary>
	/// <param name="id">MAL id of the club.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>Club's member list.</returns>
	[Obsolete(ErrorMessagesConst.UnsupportedClub)]
	Task<PaginatedTenraiResponse<ICollection<ClubMember>>> GetClubMembersAsync(long id, CancellationToken cancellationToken = default);

	/// <summary>
	/// Return club's member list.
	/// </summary>
	/// <param name="id">MAL id of the club.</param>
	/// <param name="page">Index of page folding 36 records of top ranging (e.g. 1 will return first 36 records, 2 will return record from 37 to 72 etc.)</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>Club's member list.</returns>
	[Obsolete(ErrorMessagesConst.UnsupportedClub)]
	Task<PaginatedTenraiResponse<ICollection<ClubMember>>> GetClubMembersAsync(long id, int page, CancellationToken cancellationToken = default);

	/// <summary>
	/// Return club's staff list.
	/// </summary>
	/// <param name="id">MAL id of the club.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>Club's staff list.</returns>
	[Obsolete(ErrorMessagesConst.UnsupportedClub)]
	Task<BaseTenraiResponse<ICollection<ClubStaff>>> GetClubStaffAsync(long id, CancellationToken cancellationToken = default);

	/// <summary>
	/// Return club's related entities.
	/// </summary>
	/// <param name="id">MAL id of the club.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>Club's related entities collections..</returns>
	[Obsolete(ErrorMessagesConst.UnsupportedClub)]
	Task<BaseTenraiResponse<ClubRelations>> GetClubRelationsAsync(long id, CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns list of results related to search.
	/// </summary>
	/// <param name="query">Search query.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>List of result related to search query.</returns>
	[Obsolete(ErrorMessagesConst.UnsupportedClub)]
	Task<PaginatedTenraiResponse<ICollection<Club>>> SearchClubAsync(string query, CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns list of results related to search.
	/// </summary>
	/// <param name="searchConfig">Additional configuration for advanced search.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>List of result related to search query.</returns>
	[Obsolete(ErrorMessagesConst.UnsupportedClub)]
	Task<PaginatedTenraiResponse<ICollection<Club>>> SearchClubAsync(ClubSearchConfig searchConfig, CancellationToken cancellationToken = default);
}
