﻿using BindOpen.Framework.Core.Data.Elements;
using BindOpen.Framework.Core.Data.Items.Schema;
using BindOpen.Framework.Core.Extensions.Definition.Items;
using System;
using System.Xml.Serialization;

namespace BindOpen.Framework.Core.Extensions.Runtime.Items
{
    /// <summary>
    /// This class represents an entity configuration.
    /// </summary>
    [Serializable()]
    [XmlType("BdoEntityConfiguration", Namespace = "https://bindopen.org/xsd")]
    [XmlRoot(ElementName = "entity", Namespace = "https://bindopen.org/xsd", IsNullable = false)]
    public class BdoEntityConfiguration
        : TBdoExtensionTitledItemConfiguration<BdoEntityDefinition>, IBdoEntityConfiguration
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
        /// Instantiates a new instance of the EntityConfiguration class.
        /// </summary>
        public BdoEntityConfiguration() : base()
        {
        }

        /// <summary>
        /// Instantiates a new instance of the EntityConfiguration class.
        /// </summary>
        /// <param name="definitionUniqueId">The definition unique ID to consider.</param>
        /// <param name="schema">The schema to consider.</param>
        /// <param name="items">The items to consider.</param>
        public BdoEntityConfiguration(
            string definitionUniqueId,
            IDataSchema schema = null,
            params IDataElement[] items)
            : base(BdoExtensionItemKind.Entity, definitionUniqueId, items)
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
            BdoEntityConfiguration configuration = base.Clone() as BdoEntityConfiguration;
            if (this.Schema != null)
                configuration.Schema = this.Schema.Clone() as DataSchema;

            return configuration;
        }

        #endregion
    }
}