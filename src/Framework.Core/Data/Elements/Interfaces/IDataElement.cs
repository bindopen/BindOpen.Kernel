using System.Collections.Generic;
using BindOpen.Framework.Core.Application.Scopes;
using BindOpen.Framework.Core.Data.Common;
using BindOpen.Framework.Core.Data.Elements.Sets;
using BindOpen.Framework.Core.Data.Items;
using BindOpen.Framework.Core.Data.Items.Dto;
using BindOpen.Framework.Core.Data.References;
using BindOpen.Framework.Core.Data.Specification.Design;
using BindOpen.Framework.Core.System.Diagnostics;
using BindOpen.Framework.Core.System.Diagnostics.Events;
using BindOpen.Framework.Core.System.Scripting;

namespace BindOpen.Framework.Core.Data.Elements
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
        bool AddItem(object item, ILog log = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="items"></param>
        /// <param name="log"></param>
        void AddItems(object[] items, ILog log = null);

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
        /// <param name="appScope"></param>
        /// <param name="scriptVariableSet"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        object GetObject(
            IAppScope appScope = null,
            IScriptVariableSet scriptVariableSet = null,
            ILog log = null);
    }
}