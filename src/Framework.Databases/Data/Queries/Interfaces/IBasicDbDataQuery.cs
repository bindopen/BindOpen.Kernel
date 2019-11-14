using System.Collections.Generic;
using BindOpen.Framework.Databases.Extensions.Carriers;

namespace BindOpen.Framework.Databases.Data.Queries
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBasicDbQuery : IDbQuery
    {
        /// <summary>
        /// 
        /// </summary>
        List<IDbQueryFromStatement> FromClauses { get; set; }

        /// <summary>
        /// 
        /// </summary>
        List<DbField> IdFields { get; set; }

        /// <summary>
        /// 
        /// </summary>
        bool IsDistinct { get; set; }

        /// <summary>
        /// 
        /// </summary>
        List<IDbQueryOrderByStatement> OrderByStatements { get; set; }

        /// <summary>
        /// 
        /// </summary>
        int Top { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="boundFieldName"></param>
        /// <returns></returns>
        DbField GetIdFieldWithBoundFieldName(string boundFieldName);
    }
}