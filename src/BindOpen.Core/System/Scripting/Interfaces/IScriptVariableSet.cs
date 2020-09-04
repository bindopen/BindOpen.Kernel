using BindOpen.Data.Items;
using System.Collections.Generic;

namespace BindOpen.System.Scripting
{
    /// <summary>
    /// 
    /// </summary>
    public interface IScriptVariableSet : IDataItem
    {
        /// <summary>
        /// 
        /// </summary>
        Dictionary<string, object> Variables { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="variableName"></param>
        /// <returns></returns>
        object GetValue(string variableName);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="variableName"></param>
        /// <returns></returns>
        bool Has(string variableName);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        IScriptVariableSet SetValue(IStoredDataItem item);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        IScriptVariableSet SetValue(string name, object value);
    }
}