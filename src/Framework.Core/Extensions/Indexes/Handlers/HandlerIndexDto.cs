using System;
using System.Xml.Serialization;
using BindOpen.Framework.Core.Extensions.Definitions.Handlers;

namespace BindOpen.Framework.Core.Extensions.Indexes.Handlers
{
    /// <summary>
    /// This class represents a handler index.
    /// </summary>
    [Serializable()]
    [XmlType("HandlerIndex", Namespace = "https://bindopen.org/xsd")]
    [XmlRoot(ElementName = "handlers.index", Namespace = "https://bindopen.org/xsd", IsNullable = false)]
    public class HandlerIndexDto : TAppExtensionItemIndexDto<HandlerDefinitionDto>
    {
        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the HandlerIndex class.
        /// </summary>
        public HandlerIndexDto()
        {
        }

        #endregion
    }
}
