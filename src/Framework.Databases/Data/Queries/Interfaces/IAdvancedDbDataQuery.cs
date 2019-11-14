﻿using System.Collections.Generic;
using BindOpen.Framework.Core.Data.Expression;

namespace BindOpen.Framework.Databases.Data.Queries
{
    /// <summary>
    /// 
    /// </summary>
    public interface IAdvancedDbQuery : IDbQuery
    {
        /// <summary>
        /// 
        /// </summary>
        List<IDbQueryFromStatement> FromClauses { get; set; }

        /// <summary>
        /// 
        /// </summary>
        IDbQueryGroupByStatement GroupByClause { get; set; }

        /// <summary>
        /// 
        /// </summary>
        IDbQueryHavingStatement HavingClause { get; set; }

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
        IDataExpression WhereClause { get; set; }
    }
}