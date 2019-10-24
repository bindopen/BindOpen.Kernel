using System;
using System.Xml.Serialization;

namespace BindOpen.Framework.Core.Data.Items.Source
{

    // --------------------------------------------------
    // ENUMERATIONS
    // --------------------------------------------------

    #region Enumerations

    /// <summary>
    /// This enumeration represents the data source kinds.
    /// </summary>
    [Serializable()]
    [XmlType("DataSourceKind", Namespace = "https://bindopen.org/xsd")]
    public enum DataSourceKind
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
        /// Database.
        /// </summary>
        Database,

        /// <summary>
        /// Repository.
        /// </summary>
        Repository,

        /// <summary>
        /// Rest API.
        /// </summary>
        RestApi,

        /// <summary>
        /// SOAP API.
        /// </summary>
        SoapAPI,

        /// <summary>
        /// Email server.
        /// </summary>
        EmailServer,

        /// <summary>
        /// Memory.
        /// </summary>
        Memory,

        /// <summary>
        /// Console.
        /// </summary>
        Console
    }

    #endregion

}
