using BindOpen.Framework.Application.Scopes;
using BindOpen.Framework.Data.Common;
using BindOpen.Framework.Data.Items;
using BindOpen.Framework.Data.References;
using BindOpen.Framework.Data.Specification;
using BindOpen.Framework.System.Diagnostics;
using BindOpen.Framework.System.Diagnostics.Events;
using BindOpen.Framework.System.Scripting;
using System.Collections.Generic;

namespace BindOpen.Framework.Data.Elements
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDataElement : IDescribedDataItem, IIndexed
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        object this[int index] { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        object this[string name] { get; }

        /// <summary>
        /// 
        /// </summary>
        object First { get; }

        /// <summary>
        /// 
        /// </summary>
        EventKinds? EventKind { get; set; }

        /// <summary>
        /// 
        /// </summary>
        bool EventKindSpecified { get; }

        /// <summary>
        /// 
        /// </summary>
        DataItemizationMode ItemizationMode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        DataReferenceDto ItemReference { get; set; }

        /// <summary>
        /// 
        /// </summary>
        List<object> Items { get; set; }

        /// <summary>
        /// 
        /// </summary>
        string ItemScript { get; set; }

        /// <summary>
        /// 
        /// </summary>
        DataElementSet PropertyDetail { get; set; }

        /// <summary>
        /// 
        /// </summary>
        DataElementSpec Specification { get; set; }

        /// <summary>
        /// 
        /// </summary>
        DataValueType ValueType { get; set; }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        bool AddItem(object item, IBdoLog log = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="items"></param>
        /// <param name="log"></param>
        void AddItems(object[] items, IBdoLog log = null);

        /// <summary>
        /// 
        /// </summary>
        void ClearItems();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="elementSpecificationAreas"></param>
        /// <returns></returns>
        object Clone(string[] elementSpecificationAreas = null);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        DesignControlType GetDefaultControlType();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nodeName"></param>
        /// <param name="indent"></param>
        /// <returns></returns>
        string GetTextNode(string nodeName, string indent);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="indexItem"></param>
        /// <param name="isCaseSensitive"></param>
        /// <returns></returns>
        bool HasItem(object indexItem, bool isCaseSensitive = false);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        bool RemoveItem(object item);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        void SetItem(object item);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="items"></param>
        void SetItems(object[] items);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        void SwitchItems(object value1, object value2);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <param name="aNewItem"></param>
        void UpdateItem(object item, object aNewItem);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        DataElementSpec NewSpecification();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="scope"></param>
        /// <param name="scriptVariableSet"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        object GetObject(
            IBdoScope scope = null,
            IBdoScriptVariableSet scriptVariableSet = null,
            IBdoLog log = null);
    }
}