using BindOpen.Data.Common;
using BindOpen.Data.Elements;
using BindOpen.Data.Items;
using BindOpen.Extensions.Carriers;
using System.Collections.Generic;

namespace BindOpen.Data.Queries
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDbQuery : IDescribedDataItem, IDbQueryItem
    {
        /// <summary>
        /// The name of data module of this instance.
        /// </summary>
        string DataModule { get; set; }

        /// <summary>
        /// The table name of this instance.
        /// </summary>
        string DataTable { get; set; }

        /// <summary>
        /// The data table alias of this instance.
        /// </summary>
        string DataTableAlias { get; set; }

        /// <summary>
        /// The kind of this instance.
        /// </summary>
        DbQueryKind Kind { get; set; }

        /// <summary>
        /// The schema of this instance.
        /// </summary>
        string Schema { get; set; }

        /// <summary>
        /// The parameter specification set of this instance.
        /// </summary>
        DataElementSpecSet ParameterSpecSet { get; set; }

        /// <summary>
        /// The parameter set of this instance.
        /// </summary>
        DataElementSet ParameterSet { get; set; }

        /// <summary>
        /// The select join statement of this instance.
        /// </summary>
        List<DbTable> CTETables { get; set; }

        /// <summary>
        /// Indicates that this instance checks the existence of table or data according to the kind of queries.
        /// </summary>
        /// <param name="isExistenceChecked">Indicates whether this instance checks the existence of table or data.</param>
        /// <returns>Return this instance.</returns>
        IDbQuery CheckExistence(bool isExistenceChecked = true);

        /// <summary>
        /// Defines the parameters of this instance.
        /// </summary>
        /// <param name="parameters">The set of parameters to consider.</param>
        /// <returns>Return this instance.</returns>
        IDbQuery WithParameters(params IDataElement[] parameters);

        /// <summary>
        /// Add the specified parameter to this instance.
        /// </summary>
        /// <param name="parameter">The parameter to consider.</param>
        /// <returns>Return this instance.</returns>
        IDbQuery AddParameter(ScalarElement parameter);

        /// <summary>
        /// Defines the parameter specifications of this instance.
        /// </summary>
        /// <param name="parameters">The set of parameter specifications to consider.</param>
        /// <returns>Return this instance.</returns>
        IDbQuery UsingParameters(params IDataElementSpec[] parameterSpecs);

        /// <summary>
        /// Gets the name of this instance.
        /// </summary>
        /// <returns>Returns the name of this instance.</returns>
        /// <remarks>If the name of this instance is empty or null then the returned name is determined from this instance's properties.</remarks>
        string GetName();

        /// <summary>
        /// Adds the specified parameter to this instance.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="value">The data table to consider.</param>
        /// <returns>Return this added parameter.</returns>
        ScalarElement UseParameter(
            string name,
            object value = null);

        /// <summary>
        /// Adds the specified parameter to this instance.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="valueType">The data value type to consider.</param>
        /// <param name="value">The data table to consider.</param>
        /// <returns>Return this added parameter.</returns>
        ScalarElement UseParameter(
            string name,
            DataValueType valueType,
            object value = null);

        /// <summary>
        /// Sets the specified CTE tables.
        /// </summary>
        /// <param name="tables">The CTE tables to consider.</param>
        /// <returns>Returns this instance.</returns>
        IDbQuery WithCTE(params DbTable[] tables);

    }
}