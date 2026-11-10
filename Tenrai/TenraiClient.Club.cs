using Tenrai.Consts;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Tenrai;

public partial class TenraiClient
{
	/// <inheritdoc />
	[Obsolete(ErrorMessagesConst.UnsupportedClub)]
	public Task<BaseTenraiResponse<Club>> GetClubAsync(long id, CancellationToken cancellationToken = default)
		=> throw new NotSupportedException(ErrorMessagesConst.UnsupportedClub);

	/// <inheritdoc />
	[Obsolete(ErrorMessagesConst.UnsupportedClub)]
	public Task<PaginatedTenraiResponse<ICollection<ClubMember>>> GetClubMembersAsync(long id, CancellationToken cancellationToken = default)
		=> throw new NotSupportedException(ErrorMessagesConst.UnsupportedClub);

	/// <inheritdoc />
	[Obsolete(ErrorMessagesConst.UnsupportedClub)]
	public Task<PaginatedTenraiResponse<ICollection<ClubMember>>> GetClubMembersAsync(long id, int page, CancellationToken cancellationToken = default)
		=> throw new NotSupportedException(ErrorMessagesConst.UnsupportedClub);

	/// <inheritdoc />
	[Obsolete(ErrorMessagesConst.UnsupportedClub)]
	public Task<BaseTenraiResponse<ICollection<ClubStaff>>> GetClubStaffAsync(long id, CancellationToken cancellationToken = default)
		=> throw new NotSupportedException(ErrorMessagesConst.UnsupportedClub);

	/// <inheritdoc />
	[Obsolete(ErrorMessagesConst.UnsupportedClub)]
	public Task<BaseTenraiResponse<ClubRelations>> GetClubRelationsAsync(long id, CancellationToken cancellationToken = default)
		=> throw new NotSupportedException(ErrorMessagesConst.UnsupportedClub);

	/// <inheritdoc />
	[Obsolete(ErrorMessagesConst.UnsupportedClub)]
	public Task<PaginatedTenraiResponse<ICollection<Club>>> SearchClubAsync(string query, CancellationToken cancellationToken = default)
		=> throw new NotSupportedException(ErrorMessagesConst.UnsupportedClub);

	/// <inheritdoc />
	[Obsolete(ErrorMessagesConst.UnsupportedClub)]
	public Task<PaginatedTenraiResponse<ICollection<Club>>> SearchClubAsync(ClubSearchConfig searchConfig, CancellationToken cancellationToken = default)
		=> throw new NotSupportedException(ErrorMessagesConst.UnsupportedClub);
}
