using BindOpen.Framework.Core.Application.Scopes;
using BindOpen.Framework.Core.Data.Common;
using BindOpen.Framework.Core.Data.Elements.Sets;
using BindOpen.Framework.Core.Data.Items;
using BindOpen.Framework.Core.Data.Items.Dto;
using BindOpen.Framework.Core.System.Diagnostics;
using BindOpen.Framework.Core.System.Scripting;
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