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
    public interface IDataElement : IDescribedDataItem, IIndexed
    {
        object this[int index] { get; }
        object this[string name] { get; }
        object First { get; }

        EventKind? EventKind { get; set; }
        bool EventKindSpecified { get; }
        //object FirstItem { get; set; }
        DataItemizationMode ItemizationMode { get; set; }
        DataReferenceDto ItemReference { get; set; }
        List<object> Items { get; set; }
        string ItemScript { get; set; }
        //XElement ItemXElement { get; set; }
        DataElementSet PropertyDetail { get; set; }
        DataElementSpec Specification { get; set; }
        DataValueType ValueType { get; set; }

        bool AddItem(object item, ILog log = null);
        void AddItems(object[] items, ILog log = null);
        void ClearItems();
        object Clone(string[] elementSpecificationAreas = null);
        DesignControlType GetDefaultControlType();
        string GetTextNode(string nodeName, string indent);
        bool HasItem(object indexItem, bool isCaseSensitive = false);
        bool RemoveItem(object item);
        void SetItem(object item);
        void SetItems(object[] items);
        void SwitchItems(object value1, object value2);
        void UpdateItem(object item, object aNewItem);
        IDataElementSpec NewSpecification();

        object GetObject(
            IAppScope appScope = null,
            IScriptVariableSet scriptVariableSet = null,
            ILog log = null);
    }
}