using BindOpen.Framework.Runtime.Extensions.Common;
using System;

namespace BindOpen.Framework.Runtime.Extensions.Handlers
{

    /// <summary>
    /// This enumeration lists all the possible kinds of the 'Standard' handlers.
    /// </summary>
    public enum HandlerKind_standard
    {
        /// <summary>
        /// None.
        /// </summary>
        None,

        /// <summary>
        /// Any.
        /// </summary>
        Any,

        /// <summary>
        /// BdO Script.
        /// </summary>
        BdOS,

        /// <summary>
        /// Dynamic data context.
        /// </summary>
        DynamicDataContext,

        /// <summary>
        /// Xml file.
        /// </summary>
        XmlFile,

        /// <summary>
        /// Xml file reference in database.
        /// </summary>
        XmlFileReferenceInDatabase,

        /// <summary>
        /// Text from file.
        /// </summary>
        TextFromFile,

        /// <summary>
        /// Text file reference in database.
        /// </summary>
        TextFileReferenceInDatabase,

        /// <summary>
        /// Database.
        /// </summary>
        Database,

        /// <summary>
        /// Xml String.
        /// </summary>
        TextFromXmlString,

        /// <summary>
        /// Text String.
        /// </summary>
        TextString,

        /// <summary>
        /// Embed resource
        /// </summary>
        EmbedResource,

        /// <summary>
        /// Web service
        /// </summary>
        WebService,

        /// <summary>
        /// Reference source ID
        /// </summary>
        ReferenceSourceId,

        /// <summary>
        /// Source ID
        /// </summary>
        SourceId,
    }


    // --------------------------------------------------
    // EXTENSION
    // --------------------------------------------------

    #region Extension

    /// <summary>
    /// This class represents an extension of the HandlerUniqueName_standard enumeration.
    /// </summary>
    public static class HandlerUniqueName_standardExtension
    {
        /// <summary>
        /// Gets the unique name corresponding to the specified handler kind.
        /// </summary>
        /// <param name="kind">The handler kind to consider.</param>
        /// <returns>The result object.</returns>
        public static string GetUniqueName(this HandlerKind_standard kind)
        {
            return kind.ToString().ToLower().GetUniqueName_standard();
        }
    }

    #endregion

}
