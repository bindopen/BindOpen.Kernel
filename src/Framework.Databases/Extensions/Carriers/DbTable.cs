using BindOpen.Framework.Core.Data.Items.Source;
using BindOpen.Framework.Core.Extensions.Attributes;
using BindOpen.Framework.Core.Extensions.Carriers;
using BindOpen.Framework.Core.Extensions.Runtime.Items;
using System;
using System.Xml.Serialization;

namespace BindOpen.Framework.Databases.Extensions.Carriers
{
    /// <summary>
    /// This class represents a database data table.
    /// </summary>
    [Serializable()]
    [XmlType("DbTable", Namespace = "https://bindopen.org/xsd")]
    [XmlRoot(ElementName = "dbTable", Namespace = "https://bindopen.org/xsd", IsNullable = false)]
    [BdoCarrier(
        Name = "databases$dbTable",
        DatasourceKind = DatasourceKind.Database,
        Description = "Database table.",
        CreationDate = "2016-09-14"
    )]
    public class DbTable : BdoCarrier
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

        #endregion


        // ------------------------------------------
        // MUTATORS
        // ------------------------------------------

        #region Mutators

        /// <summary>
        /// Sets the specified alias.
        /// </summary>
        /// <param name="alias">The alias to consider.</param>
        /// <returns>Returns this instance.</returns>
        public DbTable WithAlias(string alias)
        {
            Alias = alias;
            return this;
        }

        #endregion
    }
}
