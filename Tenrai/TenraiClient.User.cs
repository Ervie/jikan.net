using Tenrai.Consts;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Tenrai;

public partial class TenraiClient
{
	/// <inheritdoc />
	[Obsolete(ErrorMessagesConst.UnsupportedUserData)]
	public Task<BaseTenraiResponse<UserProfile>> GetUserProfileAsync(string username, CancellationToken cancellationToken = default)
		=> throw new NotSupportedException(ErrorMessagesConst.UnsupportedUserData);

	/// <inheritdoc />
	[Obsolete(ErrorMessagesConst.UnsupportedUserData)]
	public Task<BaseTenraiResponse<UserProfile>> GetUserByIdAsync(long id, CancellationToken cancellationToken = default)
		=> throw new NotSupportedException(ErrorMessagesConst.UnsupportedUserData);

	/// <inheritdoc />
	[Obsolete(ErrorMessagesConst.UnsupportedUserData)]
	public Task<BaseTenraiResponse<UserStatistics>> GetUserStatisticsAsync(string username, CancellationToken cancellationToken = default)
		=> throw new NotSupportedException(ErrorMessagesConst.UnsupportedUserData);

	/// <inheritdoc />
	[Obsolete(ErrorMessagesConst.UnsupportedUserData)]
	public Task<BaseTenraiResponse<UserFavorites>> GetUserFavoritesAsync(string username, CancellationToken cancellationToken = default)
		=> throw new NotSupportedException(ErrorMessagesConst.UnsupportedUserData);

	/// <inheritdoc />
	[Obsolete(ErrorMessagesConst.UnsupportedUserData)]
	public Task<BaseTenraiResponse<UserUpdates>> GetUserUpdatesAsync(string username, CancellationToken cancellationToken = default)
		=> throw new NotSupportedException(ErrorMessagesConst.UnsupportedUserData);

	/// <inheritdoc />
	[Obsolete(ErrorMessagesConst.UnsupportedUserData)]
	public Task<BaseTenraiResponse<UserAbout>> GetUserAboutAsync(string username, CancellationToken cancellationToken = default)
		=> throw new NotSupportedException(ErrorMessagesConst.UnsupportedUserData);

	/// <inheritdoc />
	[Obsolete(ErrorMessagesConst.UnsupportedUserData)]
	public Task<BaseTenraiResponse<ICollection<HistoryEntry>>> GetUserHistoryAsync(string username, CancellationToken cancellationToken = default)
		=> throw new NotSupportedException(ErrorMessagesConst.UnsupportedUserData);

	/// <inheritdoc />
	[Obsolete(ErrorMessagesConst.UnsupportedUserData)]
	public Task<BaseTenraiResponse<ICollection<HistoryEntry>>> GetUserHistoryAsync(string username, UserHistoryExtension historyExtension, CancellationToken cancellationToken = default)
		=> throw new NotSupportedException(ErrorMessagesConst.UnsupportedUserData);

	/// <inheritdoc />
	[Obsolete(ErrorMessagesConst.UnsupportedUserData)]
	public Task<PaginatedTenraiResponse<ICollection<Friend>>> GetUserFriendsAsync(string username, CancellationToken cancellationToken = default)
		=> throw new NotSupportedException(ErrorMessagesConst.UnsupportedUserData);

	/// <inheritdoc />
	[Obsolete(ErrorMessagesConst.UnsupportedUserData)]
	public Task<PaginatedTenraiResponse<ICollection<Friend>>> GetUserFriendsAsync(string username, int page, CancellationToken cancellationToken = default)
		=> throw new NotSupportedException(ErrorMessagesConst.UnsupportedUserData);

	/// <inheritdoc />
	[Obsolete(ErrorMessagesConst.UnsupportedUserData)]
	public Task<PaginatedTenraiResponse<ICollection<Review>>> GetUserReviewsAsync(string username, CancellationToken cancellationToken = default)
		=> throw new NotSupportedException(ErrorMessagesConst.UnsupportedUserData);

	/// <inheritdoc />
	[Obsolete(ErrorMessagesConst.UnsupportedUserData)]
	public Task<PaginatedTenraiResponse<ICollection<Review>>> GetUserReviewsAsync(string username, int page, CancellationToken cancellationToken = default)
		=> throw new NotSupportedException(ErrorMessagesConst.UnsupportedUserData);

	/// <inheritdoc />
	[Obsolete(ErrorMessagesConst.UnsupportedUserData)]
	public Task<PaginatedTenraiResponse<ICollection<UserRecommendation>>> GetUserRecommendationsAsync(string username, CancellationToken cancellationToken = default)
		=> throw new NotSupportedException(ErrorMessagesConst.UnsupportedUserData);

	/// <inheritdoc />
	[Obsolete(ErrorMessagesConst.UnsupportedUserData)]
	public Task<PaginatedTenraiResponse<ICollection<UserRecommendation>>> GetUserRecommendationsAsync(string username, int page, CancellationToken cancellationToken = default)
		=> throw new NotSupportedException(ErrorMessagesConst.UnsupportedUserData);

	/// <inheritdoc />
	[Obsolete(ErrorMessagesConst.UnsupportedUserData)]
	public Task<PaginatedTenraiResponse<ICollection<MalUrl>>> GetUserClubsAsync(string username, CancellationToken cancellationToken = default)
		=> throw new NotSupportedException(ErrorMessagesConst.UnsupportedUserData);

	/// <inheritdoc />
	[Obsolete(ErrorMessagesConst.UnsupportedUserData)]
	public Task<PaginatedTenraiResponse<ICollection<MalUrl>>> GetUserClubsAsync(string username, int page, CancellationToken cancellationToken = default)
		=> throw new NotSupportedException(ErrorMessagesConst.UnsupportedUserData);

	/// <inheritdoc />
	[Obsolete(ErrorMessagesConst.UnsupportedUserData)]
	public Task<BaseTenraiResponse<ICollection<ExternalLink>>> GetUserExternalLinksAsync(string username, CancellationToken cancellationToken = default)
		=> throw new NotSupportedException(ErrorMessagesConst.UnsupportedUserData);

	/// <inheritdoc />
	[Obsolete(ErrorMessagesConst.UnsupportedUserData)]
	public Task<BaseTenraiResponse<UserFull>> GetUserFullDataAsync(string username, CancellationToken cancellationToken = default)
		=> throw new NotSupportedException(ErrorMessagesConst.UnsupportedUserData);

	/// <inheritdoc />
	[Obsolete(ErrorMessagesConst.UnsupportedUserData)]
	public Task<PaginatedTenraiResponse<ICollection<UserMetadata>>> SearchUserAsync(string query, CancellationToken cancellationToken = default)
		=> throw new NotSupportedException(ErrorMessagesConst.UnsupportedUserData);

	/// <inheritdoc />
	[Obsolete(ErrorMessagesConst.UnsupportedUserData)]
	public Task<PaginatedTenraiResponse<ICollection<UserMetadata>>> SearchUserAsync(UserSearchConfig searchConfig, CancellationToken cancellationToken = default)
		=> throw new NotSupportedException(ErrorMessagesConst.UnsupportedUserData);
}
