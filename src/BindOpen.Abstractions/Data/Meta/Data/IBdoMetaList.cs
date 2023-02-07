﻿using BindOpen.Data.Items;
using BindOpen.Logging;
using BindOpen.Runtime.Scopes;

namespace BindOpen.Data.Meta
{
    /// <summary>
    /// 
    /// </summary>
    public partial interface IBdoMetaList :
        ITBdoList<IBdoMetaData>,
        IBdoMetaData
    {
        new void Clear();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        new IBdoMetaList Add(
            params IBdoMetaData[] items);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        new IBdoMetaList With(
            params IBdoMetaData[] items);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="scope"></param>
        /// <param name="varSet"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        object GetData(
            int index,
            IBdoScope scope = null,
            IBdoMetaList varSet = null,
            IBdoLog log = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="scope"></param>
        /// <param name="varSet"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        Q GetData<Q>(
            int index,
            IBdoScope scope = null,
            IBdoMetaList varSet = null,
            IBdoLog log = null);
    }
}