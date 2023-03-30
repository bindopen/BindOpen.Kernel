using BindOpen.Logging;
using BindOpen.Scopes;
using System.Collections.Generic;

namespace BindOpen.Data.Meta
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBdoMetaData :
        IBdoItemNotMetable,
        INamed, IReferenced, IIndexed,
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

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IBdoSpec NewSpec();

        // Data

        /// <summary>
        /// 
        /// </summary>
        DataMode DataMode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        DataValueTypes DataValueType { get; set; }

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
        List<object> GetDataList(
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
        List<T> GetDataList<T>(
            IBdoScope scope = null,
            IBdoMetaSet varSet = null,
            IBdoLog log = null);

        /// <summary>
        /// 
        /// </summary>
        IBdoMetaData Parent { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IBdoMetaData Root(int levelMax = 50);
    }
}