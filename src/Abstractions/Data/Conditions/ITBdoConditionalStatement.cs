﻿using BindOpen.System.Data.Meta;
using BindOpen.System.Logging;
using BindOpen.System.Scoping;
using System.Collections.Generic;

namespace BindOpen.System.Data.Conditions
{
    /// <summary>
    /// 
    /// </summary>
    public interface ITBdoConditionalStatement<TItem> : IList<(TItem Item, IBdoCondition Condition)>, IBdoObject
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param key="scope"></param>
        /// <param key="varSet"></param>
        /// <param key="log"></param>
        /// <returns></returns>
        TItem GetItem(
            IBdoScope scope = null,
            IBdoMetaSet varSet = null,
            IBdoLog log = null);

    }
}