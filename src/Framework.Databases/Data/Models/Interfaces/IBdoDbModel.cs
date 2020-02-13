﻿using BindOpen.Framework.Data.Items;
using BindOpen.Framework.Data.Queries;
using BindOpen.Framework.Extensions.Carriers;

namespace BindOpen.Framework.Data.Models
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBdoDbModel : IIdentifiedDataItem
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        void OnCreating(IBdoDbModelBuilder builder);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        DbTable Table(string name, string alias = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="aliases"></param>
        /// <returns></returns>
        DbQueryJoinCondition JoinCondition(string name, params (string fieldName, string fieldAlias)[] aliases);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="alias"></param>
        /// <returns></returns>
        DbField[] Tuple(string name, string alias);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        DbField[] Tuple(string name, params (string fieldName, string fieldAlias)[] aliases);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        IStoredDbQuery Query(string name);
    }
}