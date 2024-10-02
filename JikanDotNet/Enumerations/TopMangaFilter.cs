using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace JikanDotNet
{
    /// <summary>
	/// Defines filter used in top manga request
	/// </summary>
    public enum TopMangaFilter
    {
        /// <summary>
		/// Filter by airing
		/// </summary>
		[Description("publishing")]
        Publishing,

        /// <summary>
        /// Filter by upcoming
        /// </summary>
        [Description("upcoming")]
        Upcoming,

        /// <summary>
        /// Filter by popularity
        /// </summary>
        [Description("bypopularity")]
        ByPopularity,

        /// <summary>
        /// Filter by favorites
        /// </summary>
        [Description("favorite")]
        Favorite,

        /// <summary>
        /// No filter
        /// </summary>
        [Description("")]
        NoFilter
    }
}
