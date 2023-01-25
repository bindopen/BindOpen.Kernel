using BindOpen.Data.Items;
using BindOpen.Logging;
using BindOpen.Runtime.Scopes;
using System.Collections.Generic;

namespace BindOpen.Data.Meta
{
    /// <summary>
    /// 
    /// </summary>
    public interface ITBdoMetaData<TElement, TSpec, TItem> : IBdoMetaData
        where TElement : IBdoMetaData
        where TSpec : IBdoMetaDataSpec
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
        new TSpec NewSpecification();

        /// <summary>
        /// 
        /// </summary>
        new TSpec GetSpecification(string name = null);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        new TElement ClearItems();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objs"></param>
        TElement WithItems(params TItem[] objs);

        /// <summary>
        /// 
        /// </summary>
        new TElement WithItemizationMode(DataItemizationMode mode);

        /// <summary>
        /// 
        /// </summary>
        new TElement WithItemReference(IBdoReference reference);

        /// <summary>
        /// 
        /// </summary>
        new TElement WithItemScript(string script);

        /// <summary>
        /// 
        /// </summary>
        new TElement WithValueType(DataValueTypes valueType);

        /// <summary>
        /// 
        /// </summary>
        new TElement WithIndex(int? index);

        /// <summary>
        /// 
        /// </summary>
        new TElement WithSpecifications(params IBdoMetaDataSpec[] specs);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="scope"></param>
        /// <param name="varSet"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        new TItem Item(
            IBdoScope scope = null,
            IBdoMetaSet varSet = null,
            IBdoLog log = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="scope"></param>
        /// <param name="varSet"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        new Q Item<Q>(
            IBdoScope scope = null,
            IBdoMetaSet varSet = null,
            IBdoLog log = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="scope"></param>
        /// <param name="varSet"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        new List<TItem> Items(
            IBdoScope scope = null,
            IBdoMetaSet varSet = null,
            IBdoLog log = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="scope"></param>
        /// <param name="varSet"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        new List<Q> Items<Q>(
            IBdoScope scope = null,
            IBdoMetaSet varSet = null,
            IBdoLog log = null);
    }
}