using BindOpen.Data.Expression;
using BindOpen.Data.Items;
using BindOpen.Data.Queries;
using BindOpen.Extensions.Runtime;
using System.Xml.Serialization;

namespace BindOpen.Extensions.Carriers
{
    /// <summary>
    /// This class represents a database data table.
    /// </summary>
    [XmlType("DbTable", Namespace = "https://bindopen.org/xsd")]
    [XmlRoot(ElementName = "dbTable", Namespace = "https://bindopen.org/xsd", IsNullable = false)]
    [BdoCarrier(
        Name = "databases$dbTable",
        DatasourceKind = DatasourceKind.Database,
        Description = "Database table.",
        CreationDate = "2016-09-14"
    )]
    public class DbTable : BdoCarrier, IDbQueryItem
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// Data module of this instance.
        /// </summary>
        [DetailProperty(Name = "dataModule")]
        public string DataModule { get; set; }

        /// <summary>
        /// Data module of this instance.
        /// </summary>
        [DetailProperty(Name = "schema")]
        public string Schema { get; set; }

        /// <summary>
        /// Alias of this instance.
        /// </summary>
        [DetailProperty(Name = "alias")]
        public string Alias { get; set; }

        /// <summary>
        /// Value of this instance.
        /// </summary>
        [DetailProperty(Name = "value")]
        public DataExpression Expression { get; set; }

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
        // ACCCESSORS
        // ------------------------------------------

        #region Accessors

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return this;
        }

        #endregion

        // ------------------------------------------
        // MUTATORS
        // ------------------------------------------

        #region Mutators

        /// <summary>
        /// Sets the specified data module.
        /// </summary>
        /// <param name="dataModule">The data module to consider.</param>
        /// <returns>Returns this instance.</returns>
        public DbTable WithDataModule(string dataModule)
        {
            DataModule = dataModule;
            return this;
        }

        /// <summary>
        /// Sets the specified schema.
        /// </summary>
        /// <param name="schema">The schema to consider.</param>
        /// <returns>Returns this instance.</returns>
        public DbTable WithSchema(string schema)
        {
            Schema = schema;
            return this;
        }

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

        // ------------------------------------------
        // OPERATORS
        // ------------------------------------------

        #region Operators

        /// <summary>
        /// Returns the data expression string corresponding to this instance.
        /// </summary>
        /// <param name="table">The table to consider.</param>
        public static implicit operator string(DbTable table)
            => DbQueryBuilder.GetBdoScript(table);

        #endregion
    }
}
