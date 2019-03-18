using BindOpen.Framework.Core.Data.Elements.Sets;
using BindOpen.Framework.Core.Data.Items.Schema;
using BindOpen.Framework.Core.Extensions.Definition.Entities;
using System;
using System.Xml.Serialization;

namespace BindOpen.Framework.Core.Extensions.Configuration.Entities
{

    /// <summary>
    /// This class represents an entity configuration.
    /// </summary>
    [Serializable()]
    [XmlType("EntityConfiguration", Namespace = "http://meltingsoft.com/bindopen/xsd")]
    [XmlRoot(ElementName = "entity", Namespace = "http://meltingsoft.com/bindopen/xsd", IsNullable = false)]
    public class EntityConfiguration : TAppExtensionTitledItemConfiguration<EntityDefinition>
    {

        // ------------------------------------------
        // VARIABLES
        // ------------------------------------------

        #region Variables

        private DataElementSet _Detail = null;
        private DataSchema _Schema = null;

        #endregion


        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// The detail of this instance.
        /// </summary>
        [XmlElement("detail")]
        public DataElementSet Detail
        {
            get
            {
                return this._Detail;
            }
            set
            {
                this._Detail = value;
            }
        }

        /// <summary>
        /// The schema of this instance. 
        /// </summary>
        [XmlIgnore()]
        public DataSchema Schema
        {
            get
            {
                return this._Schema;
            }
            set
            {
                this._Schema = value;
            }
        }

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
        /// This instantiates a new instance of the EntityConfiguration class.
        /// </summary>
        /// <param name="name">The name of this instance.</param>
        /// <param name="definitionName">The definition name to consider.</param>
        /// <param name="namePreffix">The name preffix to consider.</param>
        /// <param name="detail">The path detail to consider.</param>
        public EntityConfiguration(
            String name,
            String definitionName = null,
            String namePreffix = "entity_",
            DataElementSet detail = null)
            : base(name, definitionName, null, namePreffix)
        {
            this._Detail = detail;
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
        public override Object Clone()
        {
            EntityConfiguration dataEntityItem = base.Clone() as EntityConfiguration;
            if (this._Schema != null)
                dataEntityItem.Schema = this._Schema.Clone() as DataSchema;

            return dataEntityItem;
        }

        #endregion


    }
}