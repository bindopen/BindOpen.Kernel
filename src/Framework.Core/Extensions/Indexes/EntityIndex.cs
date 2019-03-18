using BindOpen.Framework.Core.Extensions.Definition.Entities;
using BindOpen.Framework.Core.Extensions.Configuration.Entities;
using System;
using System.Xml.Serialization;

namespace BindOpen.Framework.Core.Extensions.Indexes
{
    /// <summary>
    /// This class represents an entity index.
    /// </summary>
    [Serializable()]
    [XmlType("EntityIndex", Namespace = "http://meltingsoft.com/bindopen/xsd")]
    [XmlRoot(ElementName = "entities.index", Namespace = "http://meltingsoft.com/bindopen/xsd", IsNullable = false)]
    public class EntityIndex : TAppExtensionItemIndex<EntityDefinition>
    {

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the EntityIndex class.
        /// </summary>
        public EntityIndex()
        {
        }

        #endregion        

    }
}
