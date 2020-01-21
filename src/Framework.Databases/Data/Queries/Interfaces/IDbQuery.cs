using BindOpen.Framework.Data.Common;
using BindOpen.Framework.Data.Elements;
using BindOpen.Framework.Data.Items;
using BindOpen.Framework.Extensions.Carriers;
using System.Collections.Generic;

namespace BindOpen.Framework.Data.Queries
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDbQuery : IDescribedDataItem
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
        /// The fields of this instance.
        /// </summary>
        List<DbField> Fields { get; set; }

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
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        DbField GetDataFieldWithName(string name);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="boundFieldName"></param>
        /// <returns></returns>
        DbField GetFieldWithBoundFieldName(string boundFieldName);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        List<DbField> GetForeignKeyDataFields();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        List<DbField> GetKeyDataFields();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        List<DbField> GetPrimaryKeyDataFields();

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
        IDbQuery AddParameter(IDataElement parameter);

        /// <summary>
        /// Defines the parameter specifications of this instance.
        /// </summary>
        /// <param name="parameters">The set of parameter specifications to consider.</param>
        /// <returns>Return this instance.</returns>
        IDbQuery UsingParameters(params IDataElementSpec[] parameterSpecs);

        /// <summary>
        /// Adds the specified parameter to this instance.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="value">The data table to consider.</param>
        /// <returns>Return this added parameter.</returns>
        IDataElement UseParameter(
            string name,
            object value = null);

        /// <summary>
        /// Adds the specified parameter to this instance.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="valueType">The data value type to consider.</param>
        /// <param name="value">The data table to consider.</param>
        /// <returns>Return this added parameter.</returns>
        IDataElement UseParameter(
            string name,
            DataValueType valueType,
            object value);

    }
}