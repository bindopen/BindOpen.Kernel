using System;
using System.Xml.Serialization;
using BindOpen.Framework.Core.Data.Elements.Sets;
using BindOpen.Framework.Core.Data.Items.Schema;
using BindOpen.Framework.Core.Extensions.Definition.Entities;

namespace BindOpen.Framework.Core.Extensions.Configuration.Entities
{
    /// <summary>
    /// This class represents an entity configuration.
    /// </summary>
    [Serializable()]
    [XmlType("EntityConfiguration", Namespace = "http://meltingsoft.com/bindopen/xsd")]
    [XmlRoot(ElementName = "entity", Namespace = "http://meltingsoft.com/bindopen/xsd", IsNullable = false)]
    public class EntityConfiguration : TAppExtensionTitledItemConfiguration<IEntityDefinition>, IEntityConfiguration
    {

        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// The detail of this instance.
        /// </summary>
        [XmlElement("detail")]
        public IDataElementSet Detail { get; set; } = null;

        /// <summary>
        /// The schema of this instance. 
        /// </summary>
        [XmlIgnore()]
        public IDataSchema Schema { get; set; } = null;

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the EntityConfiguration class.
        /// </summary>
        public EntityConfiguration()
            : this(null)
        {
        }

        /// <summary>
        /// Instantiates a new instance of the EntityConfiguration class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="definition">The definition to consider.</param>
        /// <param name="namePreffix">The name preffix to consider.</param>
        /// <param name="detail">The detail to consider.</param>
        protected EntityConfiguration(
            string name,
            IEntityDefinition definition = default,
            string namePreffix = "entity_",
            DataElementSet detail = null)
            : this(name, definition?.Key(), namePreffix, detail)
        {
            _definition = definition;
        }

        /// <summary>
        /// Instantiates a new instance of the EntityConfiguration class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="definitionUniqueId">The definition unique ID to consider.</param>
        /// <param name="namePreffix">The name preffix to consider.</param>
        /// <param name="detail">The detail to consider.</param>
        protected EntityConfiguration(
            string name,
            string definitionUniqueId,
            string namePreffix = "entity_",
            IDataElementSet detail = null)
            : base(name, definitionUniqueId, namePreffix)
        {
            DefinitionUniqueId = definitionUniqueId;
            Detail = detail;
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
            EntityConfiguration dataEntityItem = base.Clone() as EntityConfiguration;
            if (this.Schema != null)
                dataEntityItem.Schema = this.Schema.Clone() as DataSchema;

            return dataEntityItem;
        }

        #endregion
    }
}