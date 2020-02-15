using BindOpen.Data.Helpers.Strings;

namespace BindOpen.Databases.Extensions
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
