using System;
using System.Xml.Serialization;
using BindOpen.Framework.Core.Extensions.Definition.Handlers;

namespace BindOpen.Framework.Core.Extensions.Indexes.Handlers
{
    /// <summary>
    /// This class represents a handler index.
    /// </summary>
    [Serializable()]
    [XmlType("HandlerIndex", Namespace = "http://meltingsoft.com/bindopen/xsd")]
    [XmlRoot(ElementName = "handlers.index", Namespace = "http://meltingsoft.com/bindopen/xsd", IsNullable = false)]
    public class HandlerIndex : TAppExtensionItemIndex<IHandlerDefinition>
    {
        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the HandlerIndex class.
        /// </summary>
        public HandlerIndex()
        {
        }

        #endregion
    }
}
