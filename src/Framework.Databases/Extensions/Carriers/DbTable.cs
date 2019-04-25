using System;
using System.Xml.Serialization;
using BindOpen.Framework.Core.Data.Items.Source;
using BindOpen.Framework.Core.Extensions.Attributes;
using BindOpen.Framework.Core.Extensions.Carriers;
using BindOpen.Framework.Core.Extensions.Items.Carriers;

namespace BindOpen.Framework.Databases.Extensions.Carriers
{
    /// <summary>
    /// This class represents a database data table.
    /// </summary>
    [Serializable()]
    [XmlType("DbTable", Namespace = "https://bindopen.org/xsd")]
    [XmlRoot(ElementName = "dbTable", Namespace = "https://bindopen.org/xsd", IsNullable = false)]
    [Carrier(
        Name = "databases$dbTable",
        DataSourceKind = DataSourceKind.Database,
        Description = "Database table.",
        CreationDate = "2016-09-14"
    )]
    public class DbTable : Carrier
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// Data module of this instance.
        /// </summary>
        [XmlIgnore()]
        [DetailProperty(Name = "dataModule")]
        public string DataModule { get; set; }

        /// <summary>
        /// Data module of this instance.
        /// </summary>
        [XmlIgnore()]
        [DetailProperty(Name = "schema")]
        public string Schema { get; set; }

        /// <summary>
        /// Alias of this instance.
        /// </summary>
        [XmlIgnore()]
        [DetailProperty(Name = "alias")]
        public string Alias { get; set; }

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the DbDataTable class.
        /// </summary>
        public DbTable() : base()
        {
        }

        /// <summary>
        /// Instantiates a new instance of the DbTable class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="schema">The schema to consider.</param>
        public DbTable(
            string name,
            string schema)
            : base(name)
        {
            this.Schema = schema;
        }

        /// <summary>
        /// Instantiates a new instance of the DbTable class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="schema">The schema to consider.</param>
        /// <param name="dataModuleName">The name of the data module to consider.</param>
        public DbTable(
            string name,
            string schema,
            string dataModuleName)
            : this(name, dataModuleName)
        {
            this.DataModule = dataModuleName;
        }

        #endregion

    }
}
