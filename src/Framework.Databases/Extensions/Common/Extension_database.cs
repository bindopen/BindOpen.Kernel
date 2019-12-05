using BindOpen.Framework.Core.Data.Helpers.Strings;

namespace BindOpen.Framework.Databases.Extensions.Common
{
    // --------------------------------------------------
    // EXTENSION
    // --------------------------------------------------

    #region Extension

    /// <summary>
    /// This class represents the database extension.
    /// </summary>
    public static class Extension_database
    {
        /// <summary>
        /// Gets the database unique name.
        /// </summary>
        /// <param name="uniqueName">The unique name to consider.</param>
        /// <returns>The result object.</returns>
        public static string GetUniqueName_database(this string uniqueName)
        {
            return uniqueName.GetStartedString("database.") +"$client";
        }
    }

    #endregion
}
