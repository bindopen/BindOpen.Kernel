using System.Collections.Generic;
using BindOpen.Framework.Core.Application.Scopes;
using BindOpen.Framework.Core.Data.Items.Sets;
using BindOpen.Framework.Core.System.Diagnostics;
using BindOpen.Framework.Core.System.Scripting;

namespace BindOpen.Framework.Core.Data.Elements.Sets
{
    public interface IDataElementSet : IDataItemSet<DataElement>
    {
        DataElement this[string key] { get; }

        List<DataElement> Elements { get; set; }

        IDataElement AddElement(IDataElement element, IDataElementSet referenceElementSet = null);
        bool AddElementItem(string elementName, object item = null, ILog log = null);
        List<object> AddElementItems(string elementName, object[] items = null, ILog log = null);
        List<int> GetAvailableIndexes(int maxIndex);
        List<string> GetCommonItemKeys(IDataElementSet elementSet);
        string GetDescriptionLabel(string key, string variantName = "*", string defaultVariantName = "*", string[] parameters = null);

        List<string> GetGroupIds();
        IDataElement GetElement(string name, string groupId = null);
        List<IDataElement> GetElementsWithGroupId(string groupId);
        string GetTextNode(string nodeName, string indent);
        string GetTitleLabel(string key, string variantName = "*", string defaultVariantName = "*", string[] parameters = null);
        bool HasItem(string key);
        void RemoveElement(string key);
        List<IDataElement> Sort(string groupId = null);

        bool ElementsSpecified { get; }

        object GetElementObject(
            string elementName = null,
            IAppScope appScope = null,
            IScriptVariableSet scriptVariableSet = null,
            ILog log = null);

        T GetElementObject<T>(
            string elementName = null,
            IAppScope appScope = null,
            IScriptVariableSet scriptVariableSet = null,
            ILog log = null);
    }
}