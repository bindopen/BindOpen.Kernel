using System.Collections.Generic;
using BindOpen.Framework.Core.Application.Scopes;
using BindOpen.Framework.Core.System.Diagnostics;
using BindOpen.Framework.Core.System.Scripting;

namespace BindOpen.Framework.Core.Data.Elements.Collection
{
    public interface ICollectionElement : IDataElement
    {
        IDataElement GetElement(string name, string groupId = null);

        new List<DataElement> Elements { get; set; }

        new CollectionElementSpec Specification { get; set; }

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