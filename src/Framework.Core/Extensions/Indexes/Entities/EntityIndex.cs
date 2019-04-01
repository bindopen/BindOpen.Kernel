using System;
using System.Xml.Serialization;
using BindOpen.Framework.Core.Extensions.Definition.Entities;

namespace BindOpen.Framework.Core.Extensions.Indexes.Entities
{
    /// <summary>
    /// This class represents an entity index.
    /// </summary>
    [Serializable()]
    [XmlType("EntityIndex", Namespace = "http://meltingsoft.com/bindopen/xsd")]
    [XmlRoot(ElementName = "entities.index", Namespace = "http://meltingsoft.com/bindopen/xsd", IsNullable = false)]
    public class EntityIndex : TAppExtensionItemIndex<IEntityDefinition>
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
