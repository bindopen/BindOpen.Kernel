using System;
using System.Xml.Serialization;

namespace BindOpen.Framework.Core.Application.Entities
{

    /// <summary>
    /// This enumeration represents the possible application entity scopes.
    /// </summary>
    [Serializable()]
    [XmlType("ApplicationEntityScope", Namespace = "http://meltingsoft.com/bindopen/xsd")]
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
