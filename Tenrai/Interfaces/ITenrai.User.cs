using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Tenrai.Consts;

namespace Tenrai;

public partial interface ITenrai
{
	/// <summary>
	/// Returns information about user's profile with given username.
	/// </summary>
	/// <param name="username">Username.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>Information about user's profile with given username.</returns>
	[Obsolete(ErrorMessagesConst.UnsupportedUserData)]
	Task<BaseTenraiResponse<UserProfile>> GetUserProfileAsync(string username, CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns user profile by numeric id.
	/// </summary>
	/// <param name="id">Numeric MAL user id.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>User profile by id.</returns>
	[Obsolete(ErrorMessagesConst.UnsupportedUserData)]
	Task<BaseTenraiResponse<UserProfile>> GetUserByIdAsync(long id, CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns information about user's anime and manga statistics
	/// </summary>
	/// <param name="username">Username.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>Information about user's anime and manga statistics.</returns>
	[Obsolete(ErrorMessagesConst.UnsupportedUserData)]
	Task<BaseTenraiResponse<UserStatistics>> GetUserStatisticsAsync(string username, CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns information about user's favorite section.
	/// </summary>
	/// <param name="username">Username.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>Information about user's favorite section..</returns>
	[Obsolete(ErrorMessagesConst.UnsupportedUserData)]
	Task<BaseTenraiResponse<UserFavorites>> GetUserFavoritesAsync(string username, CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns information about user's updates on anime/manga progress.
	/// </summary>
	/// <param name="username">Username.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>Information about user's updates on anime/manga progress.</returns>
	[Obsolete(ErrorMessagesConst.UnsupportedUserData)]
	Task<BaseTenraiResponse<UserUpdates>> GetUserUpdatesAsync(string username, CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns information about user's description on the profile.
	/// </summary>
	/// <param name="username">Username.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>Information about user's description on the profile.</returns>
	[Obsolete(ErrorMessagesConst.UnsupportedUserData)]
	Task<BaseTenraiResponse<UserAbout>> GetUserAboutAsync(string username, CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns information about user's history with given username.
	/// </summary>
	/// <param name="username">Username.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>Information about user's profile with given username.</returns>
	[Obsolete(ErrorMessagesConst.UnsupportedUserData)]
	Task<BaseTenraiResponse<ICollection<HistoryEntry>>> GetUserHistoryAsync(string username, CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns information about user's history with given username.
	/// </summary>
	/// <param name="username">Username.</param>
	/// <param name="filter">Option to filter history.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>Information about user's profile with given username.</returns>
	[Obsolete(ErrorMessagesConst.UnsupportedUserData)]
	Task<BaseTenraiResponse<ICollection<HistoryEntry>>> GetUserHistoryAsync(string username, UserHistoryExtension filter, CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns information about user's friends with given username.
	/// </summary>
	/// <param name="username">Username.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>Information about user's friends with given username.</returns>
	[Obsolete(ErrorMessagesConst.UnsupportedUserData)]
	Task<PaginatedTenraiResponse<ICollection<Friend>>> GetUserFriendsAsync(string username, CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns information about user's friends with given username.
	/// </summary>
	/// <param name="username">Username.</param>
	/// <param name="page">Index of the page.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>Information about user's friends with given username.</returns>
	[Obsolete(ErrorMessagesConst.UnsupportedUserData)]
	Task<PaginatedTenraiResponse<ICollection<Friend>>> GetUserFriendsAsync(string username, int page, CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns user's reviews.
	/// </summary>
	/// <param name="username">Username.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>User's reviews.</returns>
	[Obsolete(ErrorMessagesConst.UnsupportedUserData)]
	Task<PaginatedTenraiResponse<ICollection<Review>>> GetUserReviewsAsync(string username, CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns user's reviews.
	/// </summary>
	/// <param name="username">Username.</param>
	/// <param name="page">Index of page folding 10 records of top ranging (e.g. 1 will return first 10 records, 2 will return record from 11 to 21 etc.)</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>User's reviews.</returns>
	[Obsolete(ErrorMessagesConst.UnsupportedUserData)]
	Task<PaginatedTenraiResponse<ICollection<Review>>> GetUserReviewsAsync(string username, int page, CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns user's recommendations.
	/// </summary>
	/// <param name="username">Username.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>User's recommendations.</returns>
	[Obsolete(ErrorMessagesConst.UnsupportedUserData)]
	Task<PaginatedTenraiResponse<ICollection<UserRecommendation>>> GetUserRecommendationsAsync(string username, CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns user's recommendations.
	/// </summary>
	/// <param name="username">Username.</param>
	/// <param name="page">Index of page folding 10 records of top ranging (e.g. 1 will return first 10 records, 2 will return record from 11 to 21 etc.)</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>User's recommendations.</returns>
	[Obsolete(ErrorMessagesConst.UnsupportedUserData)]
	Task<PaginatedTenraiResponse<ICollection<UserRecommendation>>> GetUserRecommendationsAsync(string username, int page, CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns user's clubs.
	/// </summary>
	/// <param name="username">Username.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>User's clubs.</returns>
	[Obsolete(ErrorMessagesConst.UnsupportedUserData)]
	Task<PaginatedTenraiResponse<ICollection<MalUrl>>> GetUserClubsAsync(string username, CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns user's clubs.
	/// </summary>
	/// <param name="username">Username.</param>
	/// <param name="page">Index of page folding 10 records of top ranging (e.g. 1 will return first 10 records, 2 will return record from 11 to 21 etc.)</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>User's clubs.</returns>
	[Obsolete(ErrorMessagesConst.UnsupportedUserData)]
	Task<PaginatedTenraiResponse<ICollection<MalUrl>>> GetUserClubsAsync(string username, int page, CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns collection of external services links related to user.
	/// </summary>
	/// <param name="username">Username.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>Collection of external services links related to anime.</returns>
	[Obsolete(ErrorMessagesConst.UnsupportedUserData)]
	Task<BaseTenraiResponse<ICollection<ExternalLink>>> GetUserExternalLinksAsync(string username, CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns user with additional data.
	/// </summary>
	/// <param name="username">Username.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>User profile with additional data.</returns>
	[Obsolete(ErrorMessagesConst.UnsupportedUserData)]
	Task<BaseTenraiResponse<UserFull>> GetUserFullDataAsync(string username, CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns list of results related to search.
	/// </summary>
	/// <param name="query">Search query.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>List of result related to search query.</returns>
	[Obsolete(ErrorMessagesConst.UnsupportedUserData)]
	Task<PaginatedTenraiResponse<ICollection<UserMetadata>>> SearchUserAsync(string query, CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns list of results related to search.
	/// </summary>
	/// <param name="searchConfig">Additional configuration for advanced search.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>List of result related to search query.</returns>
	[Obsolete(ErrorMessagesConst.UnsupportedUserData)]
	Task<PaginatedTenraiResponse<ICollection<UserMetadata>>> SearchUserAsync(UserSearchConfig searchConfig, CancellationToken cancellationToken = default);
}
