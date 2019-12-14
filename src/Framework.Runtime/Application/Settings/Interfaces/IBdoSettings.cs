using BindOpen.Framework.Core.Application.Scopes;
using BindOpen.Framework.Core.Data.Common;
using BindOpen.Framework.Core.Data.Elements;
using BindOpen.Framework.Core.Data.Items;
using BindOpen.Framework.Core.System.Diagnostics;
using BindOpen.Framework.Core.System.Scripting;
using System;
using System.Runtime.CompilerServices;
using System.Xml.Schema;

namespace BindOpen.Framework.Runtime.Application.Settings
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBdoSettings : IDataItem, IReferenced
    {
        /// <summary>
        /// 
        /// </summary>
        IBdoScope BdoScope { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <returns></returns>
        T Get<T>(string name);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        object Get(string name);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        T GetProperty<T>([CallerMemberName] string propertyName = null);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="defaultValue"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        T GetProperty<T>(T defaultValue, [CallerMemberName] string propertyName = null)
            where T : struct, IConvertible;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        void Set(string name, object value);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="propertyName"></param>
        void SetProperty(object value, [CallerMemberName] string propertyName = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="specificationLevels"></param>
        /// <param name="specificationSet"></param>
        /// <param name="scope"></param>
        /// <param name="scriptVariableSet"></param>
        /// <param name="xmlSchemaSet"></param>
        /// <returns></returns>
        IBdoLog UpdateFromFile(
            string filePath,
            SpecificationLevels[] specificationLevels = null,
            IDataElementSpecSet specificationSet = null,
            IBdoScope scope = null,
            IBdoScriptVariableSet scriptVariableSet = null,
            XmlSchemaSet xmlSchemaSet = null);
    }
}