﻿using BindOpen.Framework.Core.Data.Elements.Schema;
using BindOpen.Framework.Core.Data.References;
using System;
using System.Xml.Serialization;

namespace BindOpen.Framework.Core.Data.Items.Schema
{
    /// <summary>
    /// This class represents a data schema.
    /// </summary>
    [Serializable()]
    [XmlType("DataSchema", Namespace = "http://meltingsoft.com/bindopen/xsd")]
    [XmlRoot("schema", Namespace = "http://meltingsoft.com/bindopen/xsd", IsNullable = false)]
    public class DataSchema : DescribedDataItem, IDataSchema
    {
        //------------------------------------------
        // PROPERTIES
        //-----------------------------------------

        #region Properties

        /// <summary>
        /// Root zone of this instance. 
        /// </summary>
        [XmlElement("rootZone")]
        public SchemaZoneElement RootZone { get; set; } = new SchemaZoneElement();

        /// <summary>
        /// The meta schema reference of this instance. 
        /// </summary>
        [XmlElement("metaSchema.reference")]
        public DataReference MetaSchemreference { get; set; } = null;

        #endregion

        //------------------------------------------
        // CONSTRUCTORS
        //-----------------------------------------

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the DataSchema class.
        /// </summary>
        public DataSchema() : this(null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the DataSchema class.
        /// </summary>
        /// <param name="name">The name of this instance.</param>
        public DataSchema(String name) : base(name, "schema_")
        {
            SetTitleText("My schema");
        }

        #endregion

        // ------------------------------------------
        // ACCESSORS
        // ------------------------------------------

        #region Accessors

        /// <summary>
        /// Gets the schema element with the specified ID.
        /// </summary>
        /// <param name="id">The ID of the meta object to consider.</param>
        /// <param name="parentMetobject1">The parent meta object to consider.</param>
        /// <returns>The bmeta object with the specified ID.</returns>
        public SchemaElement GetElementWithId(String id, SchemaElement parentMetobject1 = null)
        {
            return RootZone?.GetElementWithId(id);
        }

        #endregion
    }
}
