﻿using BindOpen.Logging;
using BindOpen.Scoping.Scopes;

namespace BindOpen.Scoping.Data.Meta
{
    /// <summary>
    /// 
    /// </summary>
    public interface ITBdoMetaObject<TItem> : IBdoMetaObject
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param key="objs"></param>
        void SetData(TItem obj);

        /// <summary>
        /// 
        /// </summary>
        /// <param key="scope"></param>
        /// <param key="varSet"></param>
        /// <param key="log"></param>
        /// <returns></returns>
        new TItem GetData(
            IBdoScope scope = null,
            IBdoMetaSet varSet = null,
            IBdoLog log = null);
    }
}