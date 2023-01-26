using BindOpen.Data.Elements;
using BindOpen.Data.Items;
using BindOpen.Logging;
using BindOpen.Runtime.Scopes;
using System.Collections.Generic;

namespace BindOpen.Data.Meta
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBdoMetaData :
        IBdoItem,
        IIdentified, INamed,
        IGloballyTitled, IGloballyDescribed, IReferenced,
        IIndexed, IDetailed
    {
        /// <summary>
        /// The kind of meta data of this instance.
        /// </summary>
        BdoMetaDataKind Kind { get; }

        /// <summary>
        /// The parent instance.
        /// </summary>
        IBdoMetaData Parent { get; set; }

        /// <summary>
        /// 
        /// </summary>
        DataItemizationMode ItemizationMode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        IBdoMetaData WithItemizationMode(DataItemizationMode mode);

        /// <summary>
        /// 
        /// </summary>
        IBdoReference ItemReference { get; set; }

        /// <summary>
        /// 
        /// </summary>
        IBdoMetaData WithItemReference(IBdoReference reference);

        /// <summary>
        /// 
        /// </summary>
        string ItemScript { get; set; }

        /// <summary>
        /// 
        /// </summary>
        IBdoMetaData WithItemScript(string script);

        /// <summary>
        /// 
        /// </summary>
        DataValueTypes ValueType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        IBdoMetaData WithValueType(DataValueTypes valueType);

        /// <summary>
        /// 
        /// </summary>
        List<IBdoMetaDataSpec> Specs { get; set; }

        /// <summary>
        /// 
        /// </summary>
        IBdoMetaDataSpec GetSpecification(string name = null);

        /// <summary>
        /// 
        /// </summary>
        IBdoMetaData WithSpecifications(params IBdoMetaDataSpec[] specs);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IBdoMetaDataSpec NewSpecification();

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
        IBdoMetaData ClearItems();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objs"></param>
        IBdoMetaData WithItems(params object[] objs);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="scope"></param>
        /// <param name="varSet"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        object Item(
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
        Q Item<Q>(
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
        List<object> Items(
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
        List<Q> Items<Q>(
            IBdoScope scope = null,
            IBdoMetaSet varSet = null,
            IBdoLog log = null);
    }
}