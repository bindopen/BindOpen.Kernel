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
        /// Defines the parameter specifications of this instance.
        /// </summary>
        /// <param name="parameterSpecSet">The set of parameter specifications to consider.</param>
        /// <returns>Return this instance.</returns>
        IDbQuery WithParameters(DataElementSpecSet parameterSpecSet);

        /// <summary>
        /// Defines the parameter specifications of this instance.
        /// </summary>
        /// <param name="parameterSpecs">The set of parameter specifications to consider.</param>
        /// <returns>Return this instance.</returns>
        IDbQuery WithParameters(params IDataElementSpec[] parameterSpecs);
    }
}