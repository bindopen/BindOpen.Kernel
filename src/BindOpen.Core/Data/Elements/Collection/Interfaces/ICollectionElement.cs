using BindOpen.Application.Scopes;
using BindOpen.System.Diagnostics;
using BindOpen.System.Scripting;
using System.Collections.Generic;

namespace BindOpen.Data.Elements
{
    /// <summary>
    /// 
    /// </summary>
    public interface ICollectionElement : IDataElement
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="groupId"></param>
        /// <returns></returns>
        IDataElement GetElement(string name, string groupId = null);

        /// <summary>
        /// 
        /// </summary>
        List<DataElement> Elements { get; set; }

        /// <summary>
        /// 
        /// </summary>
        new CollectionElementSpec Specification { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="elementName"></param>
        /// <param name="scope"></param>
        /// <param name="scriptVariableSet">The script variable set to consider.</param>
        /// <param name="log"></param>
        /// <returns></returns>
        object GetElementObject(
            string elementName = null,
            IBdoScope scope = null,
            IBdoScriptVariableSet scriptVariableSet = null,
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
        T GetElementObject<T>(
            string elementName = null,
            IBdoScope scope = null,
            IBdoScriptVariableSet scriptVariableSet = null,
            IBdoLog log = null);
    }
}