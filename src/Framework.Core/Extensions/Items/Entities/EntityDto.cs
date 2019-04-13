using System;
using System.Xml.Serialization;
using BindOpen.Framework.Core.Data.Elements;
using BindOpen.Framework.Core.Data.Items.Schema;
using BindOpen.Framework.Core.Extensions.Common;
using BindOpen.Framework.Core.Extensions.Definition.Entities;

namespace BindOpen.Framework.Core.Extensions.Items.Entities
{
    /// <summary>
    /// This class represents an entity configuration.
    /// </summary>
    [Serializable()]
    [XmlType("EntityDto", Namespace = "http://meltingsoft.com/bindopen/xsd")]
    [XmlRoot(ElementName = "entity", Namespace = "http://meltingsoft.com/bindopen/xsd", IsNullable = false)]
    public class EntityDto
        : TAppExtensionTitledItemDto<EntityDefinition>, IEntityDto
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// The schema of this instance. 
        /// </summary>
        [XmlIgnore()]
        public DataSchema Schema { get; set; } = null;

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the EntityDto class.
        /// </summary>
        public EntityDto() : base()
        {
        }

        /// <summary>
        /// Instantiates a new instance of the EntityDto class.
        /// </summary>
        /// <param name="definitionUniqueId">The definition unique ID to consider.</param>
        /// <param name="schema">The schema to consider.</param>
        /// <param name="items">The items to consider.</param>
        public EntityDto(
            string definitionUniqueId,
            IDataSchema schema = null,
            params IDataElement[] items)
            : base(AppExtensionItemKind.Entity, definitionUniqueId, items)
        {
            Schema = schema as DataSchema;
        }

        #endregion

        // ------------------------------------------
        // CLONING
        // ------------------------------------------

        #region Cloning

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>Returns the cloned metrics definition.</returns>
        public override object Clone()
        {
            EntityDto configuration = base.Clone() as EntityDto;
            if (this.Schema != null)
                configuration.Schema = this.Schema.Clone() as DataSchema;

            return configuration;
        }

        #endregion
    }
}