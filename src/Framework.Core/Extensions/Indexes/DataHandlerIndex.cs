using BindOpen.Framework.Core.Extensions.Definition.Handlers;
using BindOpen.Framework.Core.Extensions.Runtime.Handlers;
using System;
using System.Xml.Serialization;

namespace BindOpen.Framework.Core.Extensions.Indexes
{
    /// <summary>
    /// This class represents a handler index.
    /// </summary>
    [Serializable()]
    [XmlType("DataHandlerIndex", Namespace = "http://meltingsoft.com/bindopen/xsd")]
    [XmlRoot(ElementName = "handlers.index", Namespace = "http://meltingsoft.com/bindopen/xsd", IsNullable = false)]
    public class DataHandlerIndex : TAppExtensionItemIndex<HandlerDefinition>
    {
        
        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the DataHandlerIndex class.
        /// </summary>
        public DataHandlerIndex()
        {
        }

        #endregion
        
    }
}
