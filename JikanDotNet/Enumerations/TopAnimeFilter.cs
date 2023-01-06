using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace JikanDotNet.Enumerations
{
    /// <summary>
	/// Defines filter used in top anime request
	/// </summary>
    public enum TopAnimeFilter
    {
        /// <summary>
		/// Filter by airing
		/// </summary>
		[Description("airing")]
        Airing,

        /// <summary>
        /// Filter by upcoming
        /// </summary>
        [Description("upcoming")]
        Upcoming,

        /// <summary>
        /// Filter by popularity
        /// </summary>
        [Description("popularity")]
        ByPopularity,

        /// <summary>
        /// Filter by favorites
        /// </summary>
        [Description("favorites")]
        Favorite
    }
}
