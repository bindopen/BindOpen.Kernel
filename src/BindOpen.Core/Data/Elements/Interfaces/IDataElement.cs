using BindOpen.Application.Scopes;
using BindOpen.Data.Common;
using BindOpen.Data.Items;
using BindOpen.Data.References;
using BindOpen.System.Diagnostics;
using BindOpen.System.Diagnostics.Events;
using BindOpen.System.Scripting;
using System.Collections.Generic;

namespace BindOpen.Data.Elements
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDataElement : IDescribedDataItem, IIndexed
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        object this[int index] { get; }

        /// <summary>
        /// 
        /// </summary>
        EventKinds EventKind { get; set; }

        /// <summary>
        /// 
        /// </summary>
        DataItemizationMode ItemizationMode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        DataReferenceDto ItemReference { get; set; }

        /// <summary>
        /// 
        /// </summary>
        List<object> Items { get; set; }

        /// <summary>
        /// 
        /// </summary>
        string ItemScript { get; set; }

        /// <summary>
        /// 
        /// </summary>
        DataValueTypes ValueType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="items"></param>
        IDataElement Add(params object[] items);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="items"></param>
        IDataElement WithItems(params object[] items);

        /// <summary>
        /// 
        /// </summary>
        IDataElement ClearItems();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        IDataElement Remove(params object[] items);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IDataElementSpec NewSpecification();

        /// <summary>
        /// 
        /// </summary>
        IDataElementSpec Specification { get; set; }

        /// <summary>
        /// 
        /// </summary>
        DataElementSet PropertyDetail { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="scope"></param>
        /// <param name="scriptVariableSet">The script variable set to consider.</param>
        /// <param name="log"></param>
        /// <returns></returns>
        object GetValue(
            IBdoScope scope = null,
            IScriptVariableSet scriptVariableSet = null,
            IBdoLog log = null);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="scope"></param>
        /// <param name="scriptVariableSet"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        object GetValue<T>(
            IBdoScope scope = null,
            IScriptVariableSet scriptVariableSet = null,
            IBdoLog log = null);
    }
}