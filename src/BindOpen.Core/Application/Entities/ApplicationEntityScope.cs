using System.Xml.Serialization;

namespace BindOpen.Application.Entities
{
    /// <summary>
    /// This enumeration represents the possible application entity scopes.
    /// </summary>
    [XmlType("ApplicationEntityScope", Namespace = "https://storage.bindopen.org/pgrkhpym/docs/code/xsd/bindopen")]
    public enum ApplicationEntityScope
    {
        /// <summary>
        /// Any.
        /// </summary>
        Any,

        /// <summary>
        /// System.
        /// </summary>
        System,

        /// <summary>
        /// Platform.
        /// </summary>
        Platform,

        /// <summary>
        /// Platform module.
        /// </summary>
        PlatformModule,

        /// <summary>
        /// Business.
        /// </summary>
        Business
    };

}
