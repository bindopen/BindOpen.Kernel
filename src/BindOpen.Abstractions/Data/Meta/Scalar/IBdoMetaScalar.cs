using BindOpen.Logging;
using BindOpen.Runtime.Scopes;
using System.Collections.Generic;

namespace BindOpen.Data.Meta
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBdoMetaScalar :
        ITBdoMetaData<IBdoMetaScalar, IBdoMetaScalarSpec, object>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="objs"></param>
        IBdoMetaScalar WithData(object obj);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="scope"></param>
        /// <param name="varSet"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        Q GetData<Q>(
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
        List<object> GetDataList(
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
        List<Q> GetDataList<Q>(
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