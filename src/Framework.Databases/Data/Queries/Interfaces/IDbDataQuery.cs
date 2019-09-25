using System.Collections.Generic;
using BindOpen.Framework.Core.Data.Items;
using BindOpen.Framework.Databases.Extensions.Carriers;

namespace BindOpen.Framework.Databases.Data.Queries
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDbDataQuery: IDataItem
    {
        /// <summary>
        /// 
        /// </summary>
        string DataModule { get; set; }

        /// <summary>
        /// 
        /// </summary>
        string DataTable { get; set; }

        /// <summary>
        /// 
        /// </summary>
        string DataTableAlias { get; set; }

        /// <summary>
        /// 
        /// </summary>
        List<DbField> Fields { get; set; }

        /// <summary>
        /// 
        /// </summary>
        bool IsTrackingEnabled { get; set; }

        /// <summary>
        /// 
        /// </summary>
        DbDataQueryKind Kind { get; set; }

        /// <summary>
        /// 
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        string Schema { get; set; }

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
    }
}