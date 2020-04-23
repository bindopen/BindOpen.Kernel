using BindOpen.Application.Scopes;
using BindOpen.Data.Items;
using BindOpen.System.Diagnostics;
using BindOpen.System.Scripting;
using System.Collections.Generic;

namespace BindOpen.Data.Elements
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDataElementSet : ITDataItemSet<IDataElement>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="elementKey"></param>
        /// <param name="item"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        IDataElementSet AddValue(string elementKey, object item = null, IBdoLog log = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="elementKey"></param>
        /// <param name="items"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        IDataElementSet AddValue(string elementKey, object[] items = null, IBdoLog log = null);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        List<string> GetGroupIds();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="groupId"></param>
        /// <returns></returns>
        IDataElement GetWithGroup(string name, string groupId = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="elementName"></param>
        /// <param name="scope"></param>
        /// <param name="scriptVariableSet">The script variable set to consider.</param>
        /// <param name="log"></param>
        /// <returns></returns>
        object GetValue(
            string elementName,
            IBdoScope scope = null,
            IScriptVariableSet scriptVariableSet = null,
            IBdoLog log = null);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="elementName"></param>
        /// <param name="scope"></param>
        /// <param name="scriptVariableSet">The script variable set to consider.</param>
        /// <param name="log"></param>
        /// <returns></returns>
        T GetValue<T>(
            string elementName,
            IBdoScope scope = null,
            IScriptVariableSet scriptVariableSet = null,
            IBdoLog log = null);
    }
}