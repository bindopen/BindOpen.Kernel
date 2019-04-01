using System.Collections.Generic;
using BindOpen.Framework.Core.Application.Scopes;
using BindOpen.Framework.Core.Data.Items.Sets;
using BindOpen.Framework.Core.System.Diagnostics;
using BindOpen.Framework.Core.System.Scripting;

namespace BindOpen.Framework.Core.Data.Elements.Sets
{
    public interface IDataElementSet : IGenericDataItemSet<IDataElement>
    {
        IDataElement this[string key] { get; }

        List<IDataElement> Elements { get; set; }

        IDataElement AddElement(IDataElement element, IDataElementSet referenceElementSet = null);
        bool AddElementItem(string elementName, object item = null, IAppScope appScope = null, IScriptVariableSet scriptVariableSet = null, ILog log = null);
        List<object> AddElementItems(string elementName, List<object> items = null, IAppScope appScope = null, IScriptVariableSet scriptVariableSet = null, ILog log = null);
        List<int> GetAvailableIndexes(int maxIndex);
        List<string> GetCommonItemKeys(IDataElementSet elementSet);
        string GetDescriptionLabel(string key, string variantName = "*", string defaultVariantName = "*", string[] parameters = null);
        object GetElementItem(string elementName = null, object indexItem = null, IAppScope appScope = null, IScriptVariableSet scriptVariableSet = null, ILog log = null);
        object GetElementItemObject(string elementName, IAppScope appScope = null, IScriptVariableSet scriptVariableSet = null, ILog log = null);
        List<object> GetElementItemObjects(IAppScope appScope = null, IScriptVariableSet scriptVariableSet = null, ILog log = null);
        List<object> GetElementItems(string elementName, IAppScope appScope = null, IScriptVariableSet scriptVariableSet = null, ILog log = null);
        List<string> GetGroupIds();
        IDataElement GetItem(string key);
        IDataElement GetElement(string name, string groupId);
        List<IDataElement> GetElementsWithGroupId(string groupId);
        string GetTextNode(string nodeName, string indent);
        string GetTitleLabel(string key, string variantName = "*", string defaultVariantName = "*", string[] parameters = null);
        bool HasItem(string key);
        void RemoveElement(string key);
        List<IDataElement> Sort(string groupId = null);

        bool ElementsSpecified { get; }
    }
}