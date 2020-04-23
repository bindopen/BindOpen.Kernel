using BindOpen.Data.Items;

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
        IScriptVariableSet SetValue(StoredDataItem item);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        IScriptVariableSet SetValue(string name, object value);
    }
}