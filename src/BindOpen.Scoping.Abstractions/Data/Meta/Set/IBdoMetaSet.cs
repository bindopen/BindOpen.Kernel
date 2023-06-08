﻿using BindOpen.Logging;
using BindOpen.Scoping.Scopes;

namespace BindOpen.Scoping.Data.Meta
{
    /// <summary>
    /// 
    /// </summary>
    public partial interface IBdoMetaSet :
        ITBdoSet<IBdoMetaData>, IBdoMetaData
    {
        new void Clear();

        /// <summary>
        /// 
        /// </summary>
        /// <param key="items"></param>
        /// <returns></returns>
        new IBdoMetaSet Add(
            params IBdoMetaData[] items);

        /// <summary>
        /// 
        /// </summary>
        /// <param key="items"></param>
        /// <returns></returns>
        new IBdoMetaSet With(
            params IBdoMetaData[] items);

        /// <summary>
        /// 
        /// </summary>
        /// <param key="scope"></param>
        /// <param key="varSet"></param>
        /// <param key="log"></param>
        /// <returns></returns>
        object GetData(
            int index,
            IBdoScope scope = null,
            IBdoMetaSet varSet = null,
            IBdoLog log = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param key="scope"></param>
        /// <param key="varSet"></param>
        /// <param key="log"></param>
        /// <returns></returns>
        Q GetData<Q>(
            int index,
            IBdoScope scope = null,
            IBdoMetaSet varSet = null,
            IBdoLog log = null);
    }
}