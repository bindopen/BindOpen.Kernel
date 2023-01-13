using BindOpen.MetaData.Items;
using BindOpen.Logging;
using BindOpen.Runtime.Scopes;
using System.Collections.Generic;

namespace BindOpen.MetaData.Elements
{
    /// <summary>
    /// 
    /// </summary>
    public interface ITBdoMetaElement<TElement, TSpec, TItem> : IBdoMetaElement
        where TElement : IBdoMetaElement
        where TSpec : IBdoMetaElementSpec
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
        new TElement ClearItem();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objs"></param>
        TElement WithItem(params TItem[] objs);

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
        new TElement WithSpecifications(params IBdoMetaElementSpec[] specs);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="scope"></param>
        /// <param name="varElementSet"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        List<TItem> GetItemList(
            IBdoScope scope = null,
            IBdoElementSet varElementSet = null,
            IBdoLog log = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="scope"></param>
        /// <param name="varElementSet"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        TItem GetFirstItem(
            IBdoScope scope = null,
            IBdoElementSet varElementSet = null,
            IBdoLog log = null);
    }
}