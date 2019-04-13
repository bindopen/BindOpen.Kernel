using System.Collections;
using BindOpen.Framework.Core.Data.Items;
using BindOpen.Framework.Core.Data.Items;

namespace BindOpen.Framework.Core.System.Scripting
{
    public interface IScriptVariableSet : IDataItem
    {
        object GetValue(string variableName);
        bool Has(string variableName);
        bool SetValue(StoredDataItem item);
        DictionaryEntry SetValue(string name, object value);
    }
}