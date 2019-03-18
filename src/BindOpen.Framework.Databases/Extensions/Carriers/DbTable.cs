using System;
using System.Xml.Serialization;
using BindOpen.Framework.Core.Application.Scopes;
using BindOpen.Framework.Core.Data.Items.Source;
using BindOpen.Framework.Core.Extensions.Attributes;
using BindOpen.Framework.Core.Extensions.Configuration.Carriers;
using BindOpen.Framework.Core.Extensions.Definition.Carriers;
using BindOpen.Framework.Core.Extensions.Runtime.Carriers;

namespace BindOpen.Framework.Databases.Extensions.Carriers
{
    /// <summary>
    /// This class represents a database data table.
    /// </summary>
    [Serializable()]
    [XmlType("DbTable", Namespace = "http://meltingsoft.com/bindopen/xsd")]
    [XmlRoot(ElementName = "dbTable", Namespace = "http://meltingsoft.com/bindopen/xsd", IsNullable = false)]
    [Carrier(
        Name = "dbTable",
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
        public string DataModule
        {
            get { return this.Get<string>(); }
            set { this.Set(value); }
        }

        /// <summary>
        /// Data module of this instance.
        /// </summary>
        [XmlIgnore()]
        [DetailProperty(Name = "schema")]
        public string Schema
        {
            get { return this.Get<string>(); }
            set { this.Set(value); }
        }

        /// <summary>
        /// Alias of this instance.
        /// </summary>
        [XmlIgnore()]
        [DetailProperty(Name = "alias")]
        public string Alias
        {
            get { return this.Get<string>(); }
            set { this.Set(value); }
        }

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
        /// <param name="configuration">The configuration to consider.</param>
        /// <param name="appScope">The application scope to consider.</param>
        public DbTable(
            string name,
            CarrierConfiguration configuration = null,
            AppScope appScope = null)
            : base(name, "databases$dbTable", configuration, "table_", null, appScope)
        {
        }

        /// <summary>
        /// Instantiates a new instance of the DbTable class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="schema">The schema to consider.</param>
        /// <param name="appScope">The application scope to consider.</param>
        public DbTable(
            string name,
            string schema,
            AppScope appScope = null)
            : base(name, "databases$dbTable", null, "table_", null, appScope)
        {
            this.Schema = schema;
        }

        /// <summary>
        /// Instantiates a new instance of the DbTable class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="schema">The schema to consider.</param>
        /// <param name="dataModuleName">The name of the data module to consider.</param>
        /// <param name="appScope">The application scope to consider.</param>
        public DbTable(
            string name,
            string schema,
            string dataModuleName,
            AppScope appScope = null)
            : this(name, dataModuleName, appScope)
        {
            this.DataModule = dataModuleName;
        }

        #endregion

    }
}
