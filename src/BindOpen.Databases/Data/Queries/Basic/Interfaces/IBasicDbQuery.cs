using BindOpen.Extensions.Carriers;
using System;
using System.Collections.Generic;

namespace BindOpen.Data.Queries
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
        /// The returned IDs of this instance.
        /// </summary>
        /// <remarks>This string is split with a comma.</remarks>
        List<DbField> ReturnedIdFields { get; set; }

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
        /// Sets the specified returned ID fields.
        /// </summary>
        /// <param name="fields">The fields to consider.</param>
        /// <returns>Returns this instance.</returns>
        IBasicDbQuery WithReturnedIdFields(params DbField[] fields);

        /// <summary>
        /// Sets the fields using an initialization function.
        /// </summary>
        /// <param name="initiliazer">The initiliazation function to consider.</param>
        /// <returns>Returns this instance.</returns>
        IBasicDbQuery WithFields(Func<IBasicDbQuery, DbField[]> initiliazer);

        /// <summary>
        /// Adds the specified fields.
        /// </summary>
        /// <param name="fields">The fields to consider.</param>
        /// <returns>Returns this instance.</returns>
        IBasicDbQuery AddField(DbField field);

        /// <summary>
        /// Adds the specified fields.
        /// </summary>
        /// <param name="canBeAdded">Indicates whether the field can be added.</param>
        /// <param name="fields">The fields to consider.</param>
        /// <returns>Returns this instance.</returns>
        IBasicDbQuery AddField(bool canBeAdded, DbField field);

        /// <summary>
        /// Adds the fields using an initialization function.
        /// </summary>
        /// <param name="initiliazer">The initiliazation function to consider.</param>
        /// <returns>Returns this instance.</returns>
        IBasicDbQuery AddField(Func<IBasicDbQuery, DbField> initiliazer);

        /// <summary>
        /// Adds the fields using an initialization function.
        /// </summary>
        /// <param name="canBeAdded">Indicates whether the field can be added.</param>
        /// <param name="initiliazer">The initiliazation function to consider.</param>
        /// <returns>Returns this instance.</returns>
        IBasicDbQuery AddField(bool canBeAdded, Func<IBasicDbQuery, DbField> initiliazer);

        /// <summary>
        /// 
        /// </summary>
        IBasicDbQuery WithFroms(params IDbQueryFromStatement[] statements);

        /// <summary>
        /// Sets the specified ID fields.
        /// </summary>
        /// <param name="fields">The ID fields to consider.</param>
        /// <returns>Returns this instance.</returns>
        IBasicDbQuery WithIdFields(params DbField[] fields);

        /// <summary>
        /// Sets the ID fields using an initialization function.
        /// </summary>
        /// <param name="initiliazer">The initiliazation function to consider.</param>
        /// <returns>Returns this instance.</returns>
        IBasicDbQuery WithIdFields(Func<IBasicDbQuery, DbField[]> initiliazer);

        /// <summary>
        /// Adds the specified ID field.
        /// </summary>
        /// <param name="field">The ID field to consider.</param>
        /// <returns>Returns this instance.</returns>
        IBasicDbQuery AddIdField(DbField field);

        /// <summary>
        /// Adds the specified ID field.
        /// </summary>
        /// <param name="canBeAdded">Indicates whether the field can be added.</param>
        /// <param name="field">The ID field to consider.</param>
        /// <returns>Returns this instance.</returns>
        IBasicDbQuery AddIdField(bool canBeAdded, DbField field);

        /// <summary>
        /// Adds the ID field using an initialization function.
        /// </summary>
        /// <param name="initiliazer">The initiliazation function to consider.</param>
        /// <returns>Returns this instance.</returns>
        IBasicDbQuery AddIdField(Func<IBasicDbQuery, DbField> initiliazer);

        /// <summary>
        /// Adds the ID field using an initialization function.
        /// </summary>
        /// <param name="canBeAdded">Indicates whether the field can be added.</param>
        /// <param name="initiliazer">The initiliazation function to consider.</param>
        /// <returns>Returns this instance.</returns>
        IBasicDbQuery AddIdField(bool canBeAdded, Func<IBasicDbQuery, DbField> initiliazer);

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
        IBasicDbQuery WithTableWithAlias(string tableAlias);
    }
}