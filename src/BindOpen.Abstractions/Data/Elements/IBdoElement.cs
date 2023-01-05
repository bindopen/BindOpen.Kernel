using BindOpen.Data.Items;
using BindOpen.Logging;
using BindOpen.Runtime.Scopes;
using System.Collections.Generic;

namespace BindOpen.Data.Elements
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBdoElement :
        IBdoItem,
        ITIdentifiedPoco<IBdoElement>, ITNamedPoco<IBdoElement>,
        ITGloballyTitledPoco<IBdoElement>, ITGloballyDescribedPoco<IBdoElement>, IReferenced,
        ITIndexedPoco<IBdoElement>, ITDetailedPoco<IBdoElement>
    {
        /// <summary>
        /// 
        /// </summary>
        DataItemizationMode ItemizationMode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        IBdoElement WithItemizationMode(DataItemizationMode mode);

        /// <summary>
        /// 
        /// </summary>
        IBdoReference ItemReference { get; set; }

        /// <summary>
        /// 
        /// </summary>
        IBdoElement WithItemReference(IBdoReference reference);

        /// <summary>
        /// 
        /// </summary>
        string ItemScript { get; set; }

        /// <summary>
        /// 
        /// </summary>
        IBdoElement WithItemScript(string script);

        /// <summary>
        /// 
        /// </summary>
        DataValueTypes ValueType { get; }

        /// <summary>
        /// 
        /// </summary>
        IBdoElement WithValueType(DataValueTypes valueType);

        /// <summary>
        /// 
        /// </summary>
        List<IBdoElementSpec> Specs { get; set; }

        /// <summary>
        /// 
        /// </summary>
        IBdoElementSpec GetSpecification(string name = null);

        /// <summary>
        /// 
        /// </summary>
        IBdoElement WithSpecifications(params IBdoElementSpec[] specs);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IBdoElementSpec NewSpecification();

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
        IBdoElement ClearItem();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objs"></param>
        IBdoElement WithItem(params object[] objs);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="scope"></param>
        /// <param name="varElementSet"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        object GetItem(
            IBdoScope scope = null,
            IBdoElementSet varElementSet = null,
            IBdoLog log = null);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="Q"></typeparam>
        /// <param name="scope"></param>
        /// <param name="varElementSet"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        Q GetItem<Q>(
            IBdoScope scope = null,
            IBdoElementSet varElementSet = null,
            IBdoLog log = null);
    }
}