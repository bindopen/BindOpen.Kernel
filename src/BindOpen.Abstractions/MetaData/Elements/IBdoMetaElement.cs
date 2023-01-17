using BindOpen.Logging;
using BindOpen.MetaData.Items;
using BindOpen.Runtime.Scopes;
using System.Collections.Generic;

namespace BindOpen.MetaData.Elements
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBdoMetaElement :
        IBdoItem,
        ITIdentifiedPoco<IBdoMetaElement>, ITNamedPoco<IBdoMetaElement>,
        ITGloballyTitledPoco<IBdoMetaElement>, ITGloballyDescribedPoco<IBdoMetaElement>, IReferenced,
        ITIndexedPoco<IBdoMetaElement>, ITDetailedPoco<IBdoMetaElement>
    {
        /// <summary>
        /// 
        /// </summary>
        DataItemizationMode ItemizationMode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        IBdoMetaElement WithItemizationMode(DataItemizationMode mode);

        /// <summary>
        /// 
        /// </summary>
        IBdoReference ItemReference { get; set; }

        /// <summary>
        /// 
        /// </summary>
        IBdoMetaElement WithItemReference(IBdoReference reference);

        /// <summary>
        /// 
        /// </summary>
        string ItemScript { get; set; }

        /// <summary>
        /// 
        /// </summary>
        IBdoMetaElement WithItemScript(string script);

        /// <summary>
        /// 
        /// </summary>
        DataValueTypes ValueType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        IBdoMetaElement WithValueType(DataValueTypes valueType);

        /// <summary>
        /// 
        /// </summary>
        List<IBdoMetaElementSpec> Specs { get; set; }

        /// <summary>
        /// 
        /// </summary>
        IBdoMetaElementSpec GetSpecification(string name = null);

        /// <summary>
        /// 
        /// </summary>
        IBdoMetaElement WithSpecifications(params IBdoMetaElementSpec[] specs);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IBdoMetaElementSpec NewSpecification();

        /// <summary>
        /// Indicates whether this instance is compatible with the specified item.
        /// </summary>
        /// <param name="item">The item to consider.</param>
        /// <returns></returns>
        bool IsCompatibleWithItem(object item);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IBdoMetaElement ClearItems();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objs"></param>
        IBdoMetaElement WithItems(params object[] objs);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="scope"></param>
        /// <param name="varElementSet"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        object ItemObject(
            IBdoScope scope = null,
            IBdoMetaElementSet varElementSet = null,
            IBdoLog log = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="scope"></param>
        /// <param name="varElementSet"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        object Item(
            IBdoScope scope = null,
            IBdoMetaElementSet varElementSet = null,
            IBdoLog log = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="scope"></param>
        /// <param name="varElementSet"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        Q Item<Q>(
            IBdoScope scope = null,
            IBdoMetaElementSet varElementSet = null,
            IBdoLog log = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="scope"></param>
        /// <param name="varElementSet"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        List<object> Items(
            IBdoScope scope = null,
            IBdoMetaElementSet varElementSet = null,
            IBdoLog log = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="scope"></param>
        /// <param name="varElementSet"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        List<Q> Items<Q>(
            IBdoScope scope = null,
            IBdoMetaElementSet varElementSet = null,
            IBdoLog log = null);
    }
}