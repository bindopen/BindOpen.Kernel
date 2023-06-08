﻿using BindOpen.Logging;
using BindOpen.Scoping.Scopes;
using System.Collections.Generic;

namespace BindOpen.Scoping.Data.Meta
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBdoMetaData :
        IBdoObjectNotMetable,
        INamed, IReferenced, IIndexed,
        ITChild<IBdoMetaData>,
        ITUpdatable<IBdoMetaData>
    {
        /// <summary>
        /// The kind of meta data of this instance.
        /// </summary>
        BdoMetaDataKind MetaDataKind { get; }

        /// <summary>
        /// 
        /// </summary>
        ITBdoSet<IBdoSpec> Specs { get; set; }

        // Data

        /// <summary>
        /// 
        /// </summary>
        DataMode DataMode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        IBdoReference Reference { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        void Clear();

        /// <summary>
        /// 
        /// </summary>
        /// <param key="scope"></param>
        /// <param key="varSet"></param>
        /// <param key="log"></param>
        /// <returns></returns>
        object GetData(
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
        T GetData<T>(
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
        IList<object> GetDataList(
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
        IList<T> GetDataList<T>(
            IBdoScope scope = null,
            IBdoMetaSet varSet = null,
            IBdoLog log = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param key="objs"></param>
        void SetData(object obj);
    }
}