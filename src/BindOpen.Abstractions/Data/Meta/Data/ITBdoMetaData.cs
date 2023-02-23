using BindOpen.Logging;
using BindOpen.Runtime.Scopes;
using System.Collections.Generic;

namespace BindOpen.Data.Meta
{
    /// <summary>
    /// 
    /// </summary>
    public interface ITBdoMetaData<TMetaData, TSpec, TItem> :
        IBdoMetaData
        where TMetaData : IBdoMetaData
        where TSpec : IBdoSpec
        where TItem : class
    {
        /// <summary>
        /// 
        /// </summary>
        new List<TSpec> Specs { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        new TSpec NewSpec();

        /// <summary>
        /// 
        /// </summary>
        new TSpec GetSpec(string name = null);

        /// <summary>
        /// 
        /// </summary>
        TMetaData WithSpecs(params TSpec[] specs);

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