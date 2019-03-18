using BindOpen.Framework.Core.Data.Helpers.Strings;
using System;

namespace BindOpen.Framework.Standard.Extensions.Common
{

    // --------------------------------------------------
    // EXTENSION
    // --------------------------------------------------

    #region Extension

    /// <summary>
    /// This class represents the standard extension.
    /// </summary>
    public static class Extension_standard
    {
        /// <summary>
        /// Gets the database unique name.
        /// </summary>
        /// <param name="uniqueName">The unique name to consider.</param>
        /// <returns>The result object.</returns>
        public static String GetUniqueName_standard(this String uniqueName)
        {
            return uniqueName.GetStartedString("standard$");
        }
    }

    #endregion


}
