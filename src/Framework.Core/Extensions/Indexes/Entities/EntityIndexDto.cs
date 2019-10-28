using System;
using System.Xml.Serialization;
using BindOpen.Framework.Core.Extensions.Items.Entities;
using BindOpen.Framework.Core.Extensions.Items.Entities.Definition;
using BindOpen.Framework.Core.Extensions.Items.Entities.Definition.Dto;

namespace BindOpen.Framework.Core.Extensions.Indexes.Entities
{
    /// <summary>
    /// This class represents a DTO entity index.
    /// </summary>
    [Serializable()]
    [XmlType("EntityIndex", Namespace = "https://bindopen.org/xsd")]
    [XmlRoot(ElementName = "entities.index", Namespace = "https://bindopen.org/xsd", IsNullable = false)]
    public class EntityIndexDto : TAppExtensionItemIndexDto<EntityDefinitionDto>
    {
        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the EntityIndex class.
        /// </summary>
        public EntityIndexDto()
        {
        }

        #endregion        
    }
}
