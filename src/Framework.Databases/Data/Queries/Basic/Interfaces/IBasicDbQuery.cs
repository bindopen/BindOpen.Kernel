using BindOpen.Framework.Databases.Extensions.Carriers;
using System.Collections.Generic;

namespace BindOpen.Framework.Data.Queries
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBasicDbQuery : IDbQuery
    {
        /// <summary>
        /// 
        /// </summary>
        List<DbQueryFromStatement> FromStatements { get; set; }

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
        List<DbQueryOrderByStatement> OrderByStatements { get; set; }

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

        // Mutators ---------------------------------------

        /// <summary>
        /// Sets the specified fields.
        /// </summary>
        /// <param name="fields">The fields to consider.</param>
        /// <returns>Returns this instance.</returns>
        IBasicDbQuery WithFields(params DbField[] fields);

        /// <summary>
        /// 
        /// </summary>
        IBasicDbQuery From(params IDbQueryFromStatement[] statements);

        /// <summary>
        /// 
        /// </summary>
        IBasicDbQuery WithIdFields(params DbField[] fields);

        /// <summary>
        /// 
        /// </summary>
        IBasicDbQuery AsDistinct();

        /// <summary>
        /// 
        /// </summary>
        IBasicDbQuery OrderBy(params IDbQueryOrderByStatement[] statements);

        /// <summary>
        /// 
        /// </summary>
        IBasicDbQuery WithTop(int top);

        /// <summary>
        /// 
        /// </summary>
        IBasicDbQuery WithTableAlias(string tableAlias);
    }
}