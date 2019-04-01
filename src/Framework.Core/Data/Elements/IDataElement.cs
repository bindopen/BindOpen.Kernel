using System.Collections.Generic;
using System.Xml.Linq;
using BindOpen.Framework.Core.Application.Scopes;
using BindOpen.Framework.Core.Data.Common;
using BindOpen.Framework.Core.Data.Dto;
using BindOpen.Framework.Core.Data.Elements.Sets;
using BindOpen.Framework.Core.Data.Items;
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

        EventKind? EventKind { get; set; }
        bool EventKindSpecified { get; }
        object FirstItem { get; set; }
        DataItemizationMode ItemizationMode { get; set; }
        IDataReference ItemReference { get; set; }
        List<object> Items { get; set; }
        string ItemScript { get; set; }
        XElement ItemXElement { get; set; }
        IDataElementSet PropertyDetail { get; set; }
        IDataElementSpec Specification { get; set; }
        DataValueType ValueType { get; set; }

        bool AddItem(object item, IAppScope appScope = null, IScriptVariableSet scriptVariableSet = null, ILog log = null);
        void AddItems(List<object> items, IAppScope appScope = null, IScriptVariableSet scriptVariableSet = null, ILog log = null);
        void ClearItems();
        object Clone(string[] elementSpecificationAreas = null);
        DesignControlType GetDefaultControlType();
        object GetItem(object indexItem = null, IAppScope appScope = null, IScriptVariableSet scriptVariableSet = null, ILog log = null);
        object GetItemObject(IAppScope appScope = null, IScriptVariableSet scriptVariableSet = null, ILog log = null);
        List<object> GetItems(IAppScope appScope = null, IScriptVariableSet scriptVariableSet = null, ILog log = null);
        object GetObjectFromString(string stringValue, IAppScope appScope = null, ILog log = null);
        string GetStringFromObject(object object1, ILog log = null);
        string GetTextNode(string nodeName, string indent);
        bool HasItem(object indexItem, bool isCaseSensitive = false);
        bool RemoveItem(object item);
        void SetItem(object item, IAppScope appScope = null);
        void SetItems(List<object> items);
        void SwitchItems(object value1, object value2);
        void UpdateItem(object item, object aNewItem);
        bool NewSpecification();
    }
}