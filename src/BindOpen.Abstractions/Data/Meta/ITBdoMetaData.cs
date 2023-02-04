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
        where TSpec : IBdoMetaSpec
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
        /// <param name="scope"></param>
        /// <param name="varSet"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        new TItem GetData(
            IBdoScope scope = null,
            IBdoMetaSet varSet = null,
            IBdoLog log = null);
    }
}