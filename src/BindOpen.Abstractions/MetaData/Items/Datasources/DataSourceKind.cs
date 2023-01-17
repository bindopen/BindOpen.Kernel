using System.Xml.Serialization;

namespace BindOpen.MetaData.Items
{
    // --------------------------------------------------
    // ENUMERATIONS
    // --------------------------------------------------

    #region Enumerations

    /// <summary>
    /// This enumeration represents the data source kinds.
    /// </summary>
    [XmlType("DatasourceKind", Namespace = "https://xsd.bindopen.org")]
    public enum DatasourceKind
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
