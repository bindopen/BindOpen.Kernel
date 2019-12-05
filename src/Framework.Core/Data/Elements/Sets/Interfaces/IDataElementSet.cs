using System.Collections.Generic;
using BindOpen.Framework.Core.Application.Scopes;
using BindOpen.Framework.Core.Data.Items.Sets;
using BindOpen.Framework.Core.System.Diagnostics;
using BindOpen.Framework.Core.System.Scripting;

namespace BindOpen.Framework.Core.Data.Elements.Sets
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDataElementSet : IDataItemSet<DataElement>
    {
        /// <summary>
        /// 
        /// </summary>
        List<DataElement> Elements { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="element"></param>
        /// <param name="referenceElementSet"></param>
        /// <returns></returns>
        IDataElement AddElement(IDataElement element, IDataElementSet referenceElementSet = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="elementName"></param>
        /// <param name="item"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        bool AddElementItem(string elementName, object item = null, IBdoLog log = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="elementName"></param>
        /// <param name="items"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        List<object> AddElementItems(string elementName, object[] items = null, IBdoLog log = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="maxIndex"></param>
        /// <returns></returns>
        List<int> GetAvailableIndexes(int maxIndex);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="elementSet"></param>
        /// <returns></returns>
        List<string> GetCommonItemKeys(IDataElementSet elementSet);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="variantName"></param>
        /// <param name="defaultVariantName"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        string GetDescriptionLabel(string key, string variantName = "*", string defaultVariantName = "*", string[] parameters = null);

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
        IDataElement GetElement(string name, string groupId = null);

        /// <summary>
        /// /
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        List<IDataElement> GetElementsWithGroupId(string groupId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nodeName"></param>
        /// <param name="indent"></param>
        /// <returns></returns>
        string GetTextNode(string nodeName, string indent);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="variantName"></param>
        /// <param name="defaultVariantName"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        string GetTitleLabel(string key, string variantName = "*", string defaultVariantName = "*", string[] parameters = null);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        void RemoveElement(string key);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        List<IDataElement> Sort(string groupId = null);

        /// <summary>
        /// 
        /// </summary>
        bool ElementsSpecified { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="elementName"></param>
        /// <param name="scope"></param>
        /// <param name="scriptVariableSet"></param>
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
        /// <param name="scriptVariableSet"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        T GetElementObject<T>(
            string elementName = null,
            IBdoScope scope = null,
            IBdoScriptVariableSet scriptVariableSet = null,
            IBdoLog log = null);
    }
}