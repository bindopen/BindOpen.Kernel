using System;

namespace BindOpen.Framework.Extensions.Carriers
{

    /// <summary>
    /// This enumeration lists all the possible kinds of 'Standard' carriers.
    /// </summary>
    public enum CarrierKind_standard
    {
        /// <summary>
        /// Any.
        /// </summary>
        Any,

        /// <summary>
        /// File.
        /// </summary>
        File,

        /// <summary>
        /// Folder.
        /// </summary>
        Folder
    }


    // --------------------------------------------------
    // EXTENSION
    // --------------------------------------------------

    #region Extension

    /// <summary>
    /// This class represents an extension of the CarrierKind_standard enumeration.
    /// </summary>
    public static class CarrierKind_standardExtension
    {
        /// <summary>
        /// Gets the unique name corresponding to the specified carrier kind.
        /// </summary>
        /// <param name="aCarrierKind_standard">The carrier unique name to consider.</param>
        /// <returns>The result object.</returns>
        public static string GetUniqueName(this CarrierKind_standard aCarrierKind_standard)
        {
            return "runtime$" + aCarrierKind_standard.ToString().ToLower();
        }
    }

    #endregion

}
